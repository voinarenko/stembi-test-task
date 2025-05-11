using System;
using UnityEngine;

namespace Code.MonoBehaviours
{
  public class Figurine : MonoBehaviour
  {
    public event Action<Figurine> Clicked;
    public event Action<Figurine> Arrived;
    
    [SerializeField] private SpriteRenderer _shape;
    [SerializeField] private SpriteRenderer _icon;

    public void Init(Sprite icon, Vector3 iconScale, Vector3 shapeScale, Color color)
    {
      _icon.sprite = icon;
      _icon.transform.localScale = iconScale;
      _shape.transform.localScale = shapeScale;
      _shape.color = color;
    }

    public void InvokeClick() =>
      Clicked?.Invoke(this);
  }
}