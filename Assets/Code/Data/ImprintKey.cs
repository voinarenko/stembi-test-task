using System;

namespace Code.Data
{
  public readonly struct ImprintKey : IEquatable<ImprintKey>
  {
    public readonly int ShapeIndex;
    public readonly int IconIndex;
    public readonly int ColorIndex;

    public ImprintKey(int shapeIndex, int iconIndex, int colorIndex)
    {
      ShapeIndex = shapeIndex;
      IconIndex = iconIndex;
      ColorIndex = colorIndex;
    }

    public bool Equals(ImprintKey other) =>
      ShapeIndex == other.ShapeIndex &&
      IconIndex == other.IconIndex &&
      ColorIndex == other.ColorIndex;

    public override bool Equals(object obj) =>
      obj is ImprintKey other && Equals(other);

    public override int GetHashCode() =>
      HashCode.Combine(ShapeIndex, IconIndex, ColorIndex);

    public override string ToString() =>
      $"{ShapeIndex}-{IconIndex}-{ColorIndex}";
  }
}