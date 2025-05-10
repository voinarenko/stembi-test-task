using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.MonoBehaviours
{
  public class Figurine : MonoBehaviour, IPointerClickHandler
  {
    [SerializeField] private Image _shape;
    [SerializeField] private Image _icon;
    
    public void Init(Sprite icon, Vector3 iconScale, Color color)
    {
      _icon.sprite = icon;
      _icon.transform.localScale = iconScale;
      _shape.color = color;
    }

    public void OnPointerClick(PointerEventData eventData) =>
      Destroy(gameObject);
  }
}