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
      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      return instance.ContinueWith(task =>
      {
        if (task.IsCompleted)
        {
          action.Invoke();
        }
      });
    }

    /// <summary>
    /// Continues the completed task with another.
    /// </summary>
    public static Task Then<TResult>(this Task<TResult> instance, Action<TResult> action)
    {
      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      return instance.ContinueWith(delegate (Task<TResult> task)
      {
        if (task.IsCompleted)
        {
          action.Invoke(task.Result);
        }
      });
    }
  }
}
