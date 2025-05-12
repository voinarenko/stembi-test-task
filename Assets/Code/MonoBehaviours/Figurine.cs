using Code.Data;
using System;
using UnityEngine;

namespace Code.MonoBehaviours
{
  public class Figurine : MonoBehaviour
  {
    public event Action<Figurine> Clicked;
    public event Action<Figurine> Arrived;

    public Imprint Data { get; set; }
    public Slot OccupiedSlot { get; set; }

    [SerializeField] private FigurineMove _move;
    [SerializeField] private SpriteRenderer _shape;
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private Collider2D _collider;

    public void Init(Vector3 iconScale, Vector3 shapeScale)
    {
      _icon.sprite = Data.Icon;
      _shape.color = Data.Color;
      _icon.transform.localScale = iconScale;
      _shape.transform.localScale = shapeScale;
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