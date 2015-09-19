namespace System.Collections.ObjectModel
{
  using Linq;
  using Specialized;
  
  public static class ObservableCollectionExtensions
  {
    /// <summary>
    /// Listens to the CollectionChanged event when new items are added it will invoke the specified action.
    /// </summary>
    public static ObservableCollection<T> OnAdded<T>(this ObservableCollection<T> collection, Action<T> action)
    {
      Validate(collection, action);

      return ListenToCollectionChanged(collection, args =>
      {
        if (args.Action == NotifyCollectionChangedAction.Add)
        {
          foreach (var element in args.NewItems.Cast<T>())
          {
            action(element);
          }
        }
      });
    }

    /// <summary>
    /// Listens to the CollectionChanged event when all items are cleared it will invoke the specified action.
    /// </summary>
    public static ObservableCollection<T> OnCleared<T>(this ObservableCollection<T> collection, Action action)
    {
      Validate(collection, action);

      return ListenToCollectionChanged(collection, args =>
      {
        if (args.Action == NotifyCollectionChangedAction.Reset)
        {
          action.Invoke();
        }
      });
    }

    /// <summary>
    /// Listens to the CollectionChanged event when an item is moved it will invoke the specified action.
    /// </summary>
    public static ObservableCollection<T> OnMoved<T>(this ObservableCollection<T> collection, Action<T> action)
    {
      Validate(collection, action);

      return ListenToCollectionChanged(collection, args =>
      {
        if (args.Action == NotifyCollectionChangedAction.Remove)
        {
          action.Invoke(args.NewItems.Cast<T>().Single());
        }
      });
    }

    /// <summary>
    /// Listens to the CollectionChanged event when an item is moved it will invoke the specified action.
    /// </summary>
    public static ObservableCollection<T> OnMoved<T>(this ObservableCollection<T> collection, Action<T, Int32> action)
    {
      Validate(collection, action);

      return ListenToCollectionChanged(collection, args =>
      {
        if (args.Action == NotifyCollectionChangedAction.Remove)
        {
          action.Invoke(args.NewItems.Cast<T>().Single(), args.NewStartingIndex);
        }
      });
    }

    /// <summary>
    /// Listens to the CollectionChanged event when an item is moved it will invoke the specified action.
    /// </summary>
    public static ObservableCollection<T> OnMoved<T>(this ObservableCollection<T> collection, Action<T, Int32, Int32> action)
    {
      Validate(collection, action);

      return ListenToCollectionChanged(collection, args =>
      {
        if (args.Action == NotifyCollectionChangedAction.Remove)
        {
          action.Invoke(args.NewItems.Cast<T>().Single(), args.NewStartingIndex, args.OldStartingIndex);
        }
      });
    }

    /// <summary>
    /// Listens to the CollectionChanged event when all items are cleared it will invoke the specified action.
    /// </summary>
    public static ObservableCollection<T> OnRemoved<T>(this ObservableCollection<T> collection, Action<T> action)
    {
      Validate(collection, action);

      return ListenToCollectionChanged(collection, args =>
      {
        if (args.Action == NotifyCollectionChangedAction.Remove)
        {
          foreach (var element in args.OldItems.Cast<T>())
          {
            action(element);
          }
        }
      });
    }

    private static ObservableCollection<T> ListenToCollectionChanged<T>(ObservableCollection<T> collection, Action<NotifyCollectionChangedEventArgs> action)
    {
      collection.CollectionChanged += (sender, args) =>
      {
        action.Invoke(args);
      };

      return collection;
    }

    private static void Validate<T>(ObservableCollection<T> collection, Delegate action)
    {
      if (collection == null)
      {
        throw new NullReferenceException(nameof(collection));
      }

      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }
    }
  }
}
