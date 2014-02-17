using System;

namespace System.Threading.Tasks
{
  public static class TaskExtensions
  {
    /// <summary>
    /// Continues the completed task with many more tasks. The returning task completes when all tasks run to their end.
    /// </summary>
    public static Task Then(this Task instance, params Action[] actions)
    {
      Assert.ThrowIfNull<NullReferenceException>(instance, "instance");

      return instance.ContinueWith(task =>
      {
        if (task.IsCompleted)
        {
          Parallel.Invoke(actions);
        }
      });
    }

    /// <summary>
    /// Continues the completed task with another.
    /// </summary>
    public static Task Then(this Task instance, Action action)
    {
      Assert.ThrowIfNull<NullReferenceException>(instance, "instance");
      Assert.ThrowIfNull<ArgumentNullException>(action, "action");

      return instance.ContinueWith(task =>
      {
        if (task.IsCompleted)
        {
          action.Invoke();
        }
      });
    }
  }
}
