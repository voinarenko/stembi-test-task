using System;
using UnityEngine;

namespace Code.Data
{
  public class Imprint
  {
    public GameObject Shape { get; }
    public Sprite Icon { get; }
    public Color Color { get; }

    public Imprint(GameObject shape, Sprite icon, Color color)
    {
      Shape = shape;
      Icon = icon;
      Color = color;
    }

    public override bool Equals(object obj)
    {
      if (obj is Imprint other)
        return Shape == other.Shape
               && Icon == other.Icon
               && Color == other.Color;
      return false;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Shape, Icon, Color);
    }
  }
}