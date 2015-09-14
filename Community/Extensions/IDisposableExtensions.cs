namespace System
{
  public static class IDisposableExtensions
  {
    public static Action AsAction(this IDisposable disposable) => disposable.Dispose;
  }
}