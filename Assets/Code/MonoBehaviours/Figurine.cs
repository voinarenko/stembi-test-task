using Code.Data;
using System;
using UnityEngine;

namespace Code.MonoBehaviours
{
  public class Figurine : MonoBehaviour
  {
    public event Action<Figurine> Clicked;
    public event Action<Figurine> Arrived;

    public ImprintKey DataKey { get; set; }
    public Slot OccupiedSlot { get; set; }
    public SpriteRenderer Shape;

    [SerializeField] private FigurineMove _move;
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private Collider2D _collider;

    public void Init(Sprite icon, Color color, Vector3 iconScale, Vector3 shapeScale)
    {
      _icon.sprite = icon;
      Shape.color = color;
      _icon.transform.localScale = iconScale;
      Shape.transform.localScale = shapeScale;
      _move.Arrived += OnArrival;
    }
    
    public void Init(Sprite shape, Sprite icon, Color color)
    {
      Shape.sprite = shape;
      _icon.sprite = icon;
      Shape.color = color;
      _move.Arrived += OnArrival;
    }

    public void InvokeClick() =>
      Clicked?.Invoke(this);

    public void MoveToSlot()
    {
      _collider.enabled = false;
      _move.ToSlot();
    }

    public void ResetData()
    {
      _move.Arrived -= OnArrival;
      OccupiedSlot = null;
      _collider.enabled = true;
      _move.ResetData();
    }
    
    private void OnArrival()
    {
      _move.Arrived -= OnArrival;
      Arrived?.Invoke(this);
    }
  }
}