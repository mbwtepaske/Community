namespace System
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Gets the service object of the specified type.
    /// </summary>
    /// <returns>
    /// A service object of type <typeparamref name="TService"/>.-or- null if there is no service object of type <typeparamref name="TService"/>.
    /// </returns>
    /// <typeparam name="TService">
    /// An object that specifies the type of service object to get. 
    /// </typeparam>
    public static TService GetService<TService>(this IServiceProvider serviceProvider)
    {
      return (TService)serviceProvider.GetService(typeof(TService));
    }
  }
}
