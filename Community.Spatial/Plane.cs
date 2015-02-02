using System;
using System.Linq;

using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

using Vector = MathNet.Numerics.LinearAlgebra.Vector<System.Double>;

namespace System.Spatial
{
  public static class Plane
  {
    ///// <summary>
    ///// Gets the distance of the plane from the origin.
    ///// </summary>
    //public Double Distance
    //{
    //  get
    //  {
    //    return Storage.At(Storage.Length - 1);
    //  }
    //}

    ///// <summary>
    ///// Gets the normal of the plane.
    ///// </summary>
    //public Vector Normal
    //{
    //  get
    //  {
    //    return new Vector(Storage.Enumerate().Take(Storage.Length - 2)).Normalize();
    //  }
    //}

    ///// <summary>
    ///// Initializes a plane.
    ///// </summary>
    //public Plane(params Double[] values) : base(values)
    //{
    //}

    ///// <summary>
    ///// Initializes a plane.
    ///// </summary>
    ///// <param name="direction">The direction of the plane normal.</param>
    ///// <param name="distance">The offset from the origin along the normal axis.</param>
    //public Plane(Vector direction, Double distance = 0D) : base(direction, distance)
    //{
    //}
    
    //public static Double Dot(Vector plane, Vector vector)
    //{
    //  if (vector == null)
    //  {
    //    throw new ArgumentNullException("vector");
    //  }

    //  if (plane.Count != vector.Count)
    //  {
    //    throw new ArgumentException("vector must be the same dimension as the plane");
    //  }

    //  return Utility.Equals(1D, vector.GetLengthSquare())
    //    ? Normal.DotProduct(vector) + Distance
    //    : Normal.DotProduct(vector);
    //}

    //public Double DotCoordinate(Vector<Double> vector)
    //{
    //  if (vector == null)
    //  {
    //    throw new ArgumentNullException("vector");
    //  }

    //  if (vector.Count != Count - 1)
    //  {
    //    throw new ArgumentException("vector must be the same dimension as the plane");
    //  }

      
    //}
    
    //public override String ToString()
    //{
    //  return ToString("E", null);
    //}

    //public String ToString(String format, IFormatProvider formatProvider)
    //{
    //  return String.Join(", ", Normal.Append(Distance).Select((value, index) => Convert.ToChar('A' + index) + ": " + value.ToString(format, formatProvider)));
    //}
  }
}