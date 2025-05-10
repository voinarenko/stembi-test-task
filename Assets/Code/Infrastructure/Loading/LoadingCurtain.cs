using DG.Tweening;
using UnityEngine;

namespace Code.Infrastructure.Loading
{
  public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
  {
    [SerializeField] private CanvasGroup _curtain;

    public void Show()
    {
      gameObject.SetActive(true);
      _curtain.alpha = 1;
    }

    public void Hide() =>
      _curtain.DOFade(0, 1).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
  }
}