using System;
using System.Collections.Generic;

using Vector = MathNet.Numerics.LinearAlgebra.Vector<double>;

namespace System.Linq
{
  public static class EnumerableExtensions
  {
    public static Vector Average(this IEnumerable<Vector> source)
    {
      if (source == null)
      {
        throw new NullReferenceException("source");
      }

      var collection = source as ICollection<Vector>;

      if (collection != null)
      {
        var reciprocal = 1D / collection.Count;
        var values = Enumerable.Range(0, collection.First().Count).Select(index => collection.Select(vector => vector[index]).Sum() * reciprocal).ToArray();

        return Vector.Build.Dense(values);
      }

      using (var enumerator = source.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          var counter = 1;
          var values = enumerator.Current.ToArray();

          for (; enumerator.MoveNext(); counter++)
          {
            for (var index = 0; index < enumerator.Current.Count; index++)
            {
              values[index] += enumerator.Current[index];
            }
          }

          return Vector.Build.Dense(values).DivideByThis(counter);
        }
        
        throw new ArgumentException("collection contains no elements");
      }
    }

    public static Vector ToVector(this IEnumerable<Double> source)
    {
      return Vector.Build.DenseOfEnumerable(source);
    }
  }
}
