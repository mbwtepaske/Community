namespace System.Spatial
{
  using Collections;
  using Collections.Generic;
  using Diagnostics;
  using Linq;
  using Text;

  [DebuggerDisplay("{ToString(\"F5\", null)}")]
  public class Vector : ICloneable, IEnumerable<Double>, IEquatable<Vector>, IFormattable
  {
    /// <summary>
    /// The storage of the vector.
    /// </summary>
    public readonly VectorStorage Storage;

    /// <summary>
    /// Gets the length of the vector.
    /// </summary>
    public Int32 Count
    {
      get
      {
        return Storage.Count;
      }
    }

    public Double this[Int32 index]
    {
      get
      {
        return Storage[index];
      }
      set
      {
        Storage[index] = value;
      }
    }

    #region Initialization
    
    public Vector(Int32 count, Double defaultValue = 0D) 
      : this(new VectorStorage(Enumerable.Repeat(defaultValue, count).ToArray()))
    {
    }

    public Vector(Int32 count, Func<Int32, Double> valueFactory)
      : this(new VectorStorage(Enumerable.Range(0, count).Select(valueFactory).ToArray()))
    {
    }

    public Vector(params Double[] values) 
      : this(new VectorStorage(values))
    {
    }

    public Vector(Vector vector)
      : this(vector.Storage.IsReadOnly ? vector.Storage : vector.Storage.Clone())
    {
    }

    public Vector(VectorStorage storage)
    {
      Storage = storage;
    }

    #endregion

    #region Cloning
    
    Object ICloneable.Clone()
    {
      return Clone();
    }

    /// <summary>
    /// Returns a deep clone of the vector.
    /// </summary>
    public virtual Vector Clone()
    {
      return new Vector(Storage.Clone());
    }

    #endregion

    #region Enumeration
    
    IEnumerator<Double> IEnumerable<Double>.GetEnumerator()
    {
      return Storage.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return Storage.GetEnumerator();
    }

    #endregion

    #region Equating
    
    public override Boolean Equals(Object other)
    {
      return Equals(other as Vector);
    }

    public Boolean Equals(Vector other)
    {
      return Equals(other, EqualityComparer<Double>.Default);
    }

    public Boolean Equals(Vector other, IEqualityComparer<Double> comparer)
    {
      if (comparer == null)
      {
        throw new ArgumentNullException("comparer");
      }

      if (other == null || Count != other.Count)
      {
        return false;
      }

      if (!ReferenceEquals(this, other))
      {
        for (var index = 0; index < Count; index++)
        {
          if (!comparer.Equals(Storage[index], other.Storage[index]))
          {
            return false;
          }
        }
      }

      return true;
    }

    public override Int32 GetHashCode()
    {
      return Storage.Enumerate(value => value.GetHashCode()).Aggregate((left, right) => left.GetHashCode() ^ right.GetHashCode());
    }

    #endregion

    #region Formatting
    
    public override String ToString()
    {
      return ToString(null, null);
    }

    public String ToString(String format)
    {
      return ToString(format, null);
    }

    public String ToString(String format, IFormatProvider formatProvider)
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.Append("[");
      stringBuilder.Append(String.Join(", ", Storage.Enumerate(value => value.ToString(format, null))));
      stringBuilder.Append("]");

      return stringBuilder.ToString();
    }

    #endregion

    #region Operations

    public virtual Vector Add(Vector right)
    {
      Verify(right);

      return new Vector(Count, index => this[index] + right[index]);
    }

    public virtual Vector Add(Double right)
    {
      return new Vector(Count, index => this[index] + right);
    }

    public virtual Vector Divide(Vector right)
    {
      Verify(right);

      return new Vector(Count, index => this[index] / right[index]);
    }

    public virtual Vector Divide(Double right)
    {
      return new Vector(Count, index => this[index] / right);
    }

    public virtual Double DotProduct(Vector right)
    {
      Verify(right);

      return Enumerable.Range(0, Count).Select(index => Storage[index] * right.Storage[index]).Sum();
    }

    public Double Magnitude()
    {
      return Math.Sqrt(MagnitudeSquare());
    }

    public Double MagnitudeSquare()
    {
      return Storage.Enumerate(value => value * value).Sum();
    }

    public virtual Vector Modulo(Vector right)
    {
      Verify(right);

      return new Vector(Count, index => this[index] % right[index]);
    }

    public virtual Vector Modulo(Double right)
    {
      return new Vector(Count, index => this[index] % right);
    }

    public virtual Vector Multiply(Matrix right)
    {
      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (Count != right.RowCount)
      {
        throw new ArgumentDimensionMismatchException("right.RowCount", Count);
      }

      return new Vector(Count, index => DotProduct(right.GetColumn(index)));
    }

    public virtual Vector Multiply(Vector right)
    {
      Verify(right);

      return new Vector(Count, index => this[index] + right[index]);
    }

    public virtual Vector Multiply(Double right)
    {
      return new Vector(Count, index => this[index] * right);
    }

    public virtual Vector Normalize()
    {
      var magnitude = Magnitude();

      return magnitude > 0 ? Divide(magnitude) : new Vector(Count, Double.NaN);
    }

    public virtual Vector Subtract(Vector right)
    {
      Verify(right);

      return new Vector(Count, index => this[index] - right[index]);
    }

    public virtual Vector Subtract(Double right)
    {
      return new Vector(Count, index => this[index] - right);
    }

    public virtual Vector Subvector(Int32 index, Int32 length)
    {
      return new Vector(length, currentIndex => Storage[currentIndex + index]);
    }

    protected virtual void Verify(Vector right)
    {
      if (right == null)
      {
        throw new ArgumentNullException("right");
      }

      if (Count != right.Count)
      {
        throw new ArgumentDimensionMismatchException("right");
      }
    }

    #endregion

    #region Operators

    public static Vector operator +(Vector vector)
    {
      return vector.Clone();
    }

    public static Vector operator +(Vector left, Vector right)
    {
      return left.Add(right);
    }

    public static Vector operator +(Vector left, Double right)
    {
      return left.Add(right);
    }

    public static Vector operator +(Double left, Vector right)
    {
      return right.Add(left);
    }

    public static Vector operator -(Vector vector)
    {
      return vector.Multiply(-1D);
    }

    public static Vector operator -(Vector left, Vector right)
    {
      return left.Subtract(right);
    }

    public static Vector operator -(Vector left, Double right)
    {
      return left.Subtract(right);
    }

    public static Vector operator -(Double left, Vector right)
    {
      return new Vector(right.Count, left).Subtract(right);
    }

    public static Vector operator *(Vector left, Double right)
    {
      return left.Multiply(right);
    }

    public static Vector operator *(Double left, Vector right)
    {
      return right.Multiply(left);
    }

    public static Vector operator *(Vector left, Vector right)
    {
      return left.Multiply(right);
    }

    public static Vector operator /(Double left, Vector right)
    {
      return new Vector(right.Count, left).Subtract(right);
    }

    public static Vector operator /(Vector left, Double right)
    {
      return left.Divide(right);
    }

    public static Vector operator /(Vector left, Vector right)
    {
      return left.Divide(right);
    }

    public static Vector operator %(Vector left, Double right)
    {
      return left.Modulo(right);
    }

    public static Vector operator %(Double left, Vector right)
    {
      return new Vector(right.Count, left).Modulo(right);
    }

    public static Vector operator %(Vector left, Vector right)
    {
      return left.Modulo(right);
    }

    #endregion
  }
}