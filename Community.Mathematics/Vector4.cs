using System;

namespace Community.Mathematics
{
  public class Vector4 : Vector3
  {
    public Double D
    {
      get
      {
        return Data[3];
      }
      set
      {
        Data[3] = value;
      }
    }
  }
}
