using Code.Services.UIAnimation;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class UIInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private RectTransform _uiRoot;
    [SerializeField] private RectTransform _topArea;
    [SerializeField] private RectTransform _bottomArea;
    [SerializeField] private RectTransform _SuccessScreen;
    [SerializeField] private RectTransform _FailureScreen;
    private IUIAnimationService _uiAnimation;

    [Inject]
    private void Construct(IUIAnimationService uiAnimation) =>
      _uiAnimation = uiAnimation;

    public void Initialize()
    { 
      _uiAnimation.UiRoot = _uiRoot;
      _uiAnimation.TopArea = _topArea;
      _uiAnimation.BottomArea = _bottomArea;
      _uiAnimation.SuccessScreen = _SuccessScreen;
      _uiAnimation.FailureScreen = _FailureScreen;
    }
  }
}