Param 
(
  [Parameter(Mandatory=$True)]
  [String] $Specification,

  [Switch] $Publish  
)

$NuGetExecutable = "NuGet.exe"
$global:ExitCode = 1

function Output
{
	#region Parameters
	
	[CmdLetBinding()]
	Param
  (
		[Parameter(ValueFromPipeline=$True)]
		[array] $Messages,

		[Parameter] [ValidateSet("Error", "Warning", "Information")]
		[string] $Level = "Information",
			
		[Parameter]
		[Switch] $NoConsoleOut = $False,
			
		[Parameter]
		[String] $ForegroundColor = 'White',
			
		[Parameter]
    [ValidateRange(1,30)]
		[Int16] $Indent = 0,

		[Parameter]
		[IO.FileInfo] $Path = ".\NuGet.log",
			
		[Parameter]
		[Switch] $Clobber,
			
		[Parameter]
		[String] $EventLogName,
			
		[Parameter]
		[String] $EventSource,
			
		[Parameter]
		[Int32] $EventID = 1
			
	)
		
	#endregion

	Begin
  {
  }

	Process 
  {		
		if ($Messages.Length -gt 0)
    {
			Try
      {			
				ForEach($m in $Messages)
        {			
					if ($NoConsoleOut -eq $false)
          {
						switch ($Level)
            {
							'Error'
              { 
								Write-Error $m -ErrorAction SilentlyContinue
								Write-Host ('{0}{1}' -f (" " * $Indent), $m) -ForegroundColor Red
							}
							'Warning'
              { 
								Write-Warning $m 
							}
							'Information'
              { 
								Write-Host ('{0}{1}' -f (" " * $Indent), $m) -ForegroundColor $ForegroundColor
							}
						}
					}

					if ($m.Trim().Length -gt 0)
          {
						$msg = '{0}{1} [{2}] : {3}' -f (" " * $Indent), (Get-Date -Format "yyyy-MM-dd HH:mm:ss"), $Level.ToUpper(), $m
	
						if ($Clobber)
            {
							$msg | Out-File -FilePath $Path -Force
						} 
            else
            {
							$msg | Out-File -FilePath $Path -Append
						}
					}
			
					if ($EventLogName)
          {			
						if (-not $EventSource)
            {
							$EventSource = ([IO.FileInfo] $MyInvocation.ScriptName).Name
						}
			
						if(-not [Diagnostics.EventLog]::SourceExists($EventSource))
            { 
							[Diagnostics.EventLog]::CreateEventSource($EventSource, $EventLogName) 
						} 

						$log = New-Object System.Diagnostics.EventLog  
						$log.set_log($EventLogName)  
						$log.set_source($EventSource) 
				
						switch ($Level) 
            {
							"Error"       { $log.WriteEntry($Message, 'Error', $EventID) }
							"Warning"     { $log.WriteEntry($Message, 'Warning', $EventID) }
							"Information" { $log.WriteEntry($Message, 'Information', $EventID) }
						}
					}
				}
			} 
			catch
      {
				throw "Failed to create log entry in: '$Path'. The error was: '$_'."
			}
		}
	}

	End {}

	<#
		.SYNOPSIS
			Writes logging information to screen and log file simultaneously.

		.DESCRIPTION
			Writes logging information to screen and log file simultaneously. Supports multiple log levels.

		.PARAMETER Messages
			The messages to be logged.

		.PARAMETER Level
			The type of message to be logged.
			
		.PARAMETER NoConsoleOut
			Specifies to not display the message to the console.
			
		.PARAMETER ConsoleForeground
			Specifies what color the text should be be displayed on the console. Ignored when switch 'NoConsoleOut' is specified.
		
		.PARAMETER Indent
			The number of spaces to indent the line in the log file.

		.PARAMETER Path
			The log file path.
		
		.PARAMETER Clobber
			Existing log file is deleted when this is specified.
		
		.PARAMETER EventLogName
			The name of the system event log, e.g. 'Application'.
		
		.PARAMETER EventSource
			The name to appear as the source attribute for the system event log entry. This is ignored unless 'EventLogName' is specified.
		
		.PARAMETER EventID
			The ID to appear as the event ID attribute for the system event log entry. This is ignored unless 'EventLogName' is specified.

		.EXAMPLE
			PS C:\> Output -Message "It's all good!" -Path C:\MyLog.log -Clobber -EventLogName 'Application'

		.EXAMPLE
			PS C:\> Output -Message "Oops, not so good!" -Level Error -EventID 3 -Indent 2 -EventLogName 'Application' -EventSource "My Script"

		.INPUTS
			System.String

		.OUTPUTS
			No output.
			
		.NOTES
			Revision History:
				2011-03-10 : Andy Arismendi - Created.
	#>
}

function Process
{
	param
  (
    [String] $fileName, 
    [String] $arguments,
    [Boolean] $start = $True,
    [Boolean] $wait = $True
  )

	$pinfo = New-Object System.Diagnostics.ProcessStartInfo
	$pinfo.RedirectStandardError = $true
	$pinfo.RedirectStandardOutput = $true
	$pinfo.UseShellExecute = $false
	$pinfo.FileName = $fileName
	$pinfo.Arguments = $arguments

	$p = New-Object System.Diagnostics.Process
	$p.StartInfo = $pinfo

  if ($start)
  {
    $p.Start() | Out-Null
  }
  
  if ($wait)
  {
    $p.WaitForExit() | Out-Null
  }
	return $p
}

function Package
{
  Param
  (
    [String] $fileName
  )

  Output "Creating package..." -ForegroundColor Green
  
  $task = Process $NuGetExecutable ("pack {0} -Verbosity Detailed" -f $fileName)
  $output = ($task.StandardOutput.ReadToEnd() -Split '[\r\n]') |? { Output $_ }
  $error = ($task.StandardError.ReadToEnd() -Split '[\r\n]') |? { Output $_ Error }
    
	Return $task.ExitCode
}

function Publish
{
  Param
  (
    [String] $fileName
  )
  
  Output "Publishing package..." -ForegroundColor Green

	# Get nuget config
	[xml]
  $nugetConfig = Get-Content .\NuGet.Config
	
	$nugetConfig.configuration.packageSources.add | ForEach-Object
  {
		$url = $_.value

		Output "Repository: $url"

		# Try to push package
		$task = Process $NuGetExecutable ("push {0} -Source {1}" -f $fileName, $url)
    $output = ($task.StandardOutput.ReadToEnd() -Split '[\r\n]') |? { Output $_ }
    $error = ($task.StandardError.ReadToEnd() -Split '[\r\n]') |? { Output $_ Error }
    
		Return $task.ExitCode
	}
}

$exit = Package 

# Check if package should be published
if ($Publish -and $global:ExitCode -eq 0) 
{
	$global:ExitCode = Publish
}

Output " "
Output "Exit Code: '$global:ExitCode'..." -ForegroundColor Gray

$host.SetShouldExit($global:ExitCode)
Exit $global:ExitCode