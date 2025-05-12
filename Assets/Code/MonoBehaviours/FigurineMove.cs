using DG.Tweening;
using System;
using UnityEngine;

namespace Code.MonoBehaviours
{
  public class FigurineMove : MonoBehaviour
  {
    private const float AnimationDuration = 0.5f;
    public event Action Arrived;

    [SerializeField] private Rigidbody2D _rb;
    private readonly Vector3 _zoomedScale = new(1000, 1000, 1000);
    private readonly Vector3 _actionBarScale = new(250, 250, 250);

    public void ToSlot()
    {
      _rb.bodyType = RigidbodyType2D.Kinematic;

      transform.DORotate(Vector3.zero, AnimationDuration)
        .SetEase(Ease.OutSine)
        .OnComplete(() => _rb.constraints = RigidbodyConstraints2D.FreezeRotation);

      transform
        .DOScale(_zoomedScale, AnimationDuration)
        .SetEase(Ease.OutSine)
        .OnComplete(() => transform
          .DOScale(_actionBarScale, AnimationDuration)
          .SetEase(Ease.InSine));

      transform
        .DOMove(Vector2.zero, AnimationDuration)
        .SetEase(Ease.OutSine)
        .OnComplete(() => transform
          .DOLocalMove(Vector3.zero, AnimationDuration)
          .SetEase(Ease.InSine)
          .OnComplete(() =>
          {
            _rb.bodyType = RigidbodyType2D.Static;
            Arrived?.Invoke();
          }));
    }

    public void ResetData()
    {
      _rb.bodyType = RigidbodyType2D.Dynamic;
      _rb.constraints = RigidbodyConstraints2D.None;
      transform.localPosition = Vector3.zero;
      transform.localScale = Vector3.one;
    }
  }
}