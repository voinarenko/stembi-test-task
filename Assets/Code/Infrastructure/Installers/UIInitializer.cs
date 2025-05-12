using Code.MonoBehaviours;
using Code.Services.ItemsAccounting;
using Code.Services.UIAnimation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class UIInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private RectTransform _uiRoot;
    [SerializeField] private RectTransform _topArea;
    [SerializeField] private RectTransform _bottomArea;
    [SerializeField] private RectTransform _resultScreen;
    [SerializeField] private Button _refreshButton;
    private IUIAnimationService _uiAnimation;
    private IItemsAccountingService _itemsAccounting;

    [Inject]
    private void Construct(IUIAnimationService uiAnimation, IItemsAccountingService itemsAccounting)
    {
      _itemsAccounting = itemsAccounting;
      _uiAnimation = uiAnimation;
    }

    public void Initialize()
    {
      _uiAnimation.UiRoot = _uiRoot;
      _uiAnimation.TopArea = _topArea;
      _uiAnimation.BottomArea = _bottomArea;
      _uiAnimation.ResultScreen = _resultScreen;
      if (_resultScreen.TryGetComponent(out ResultScreen resultScreen))
        _itemsAccounting.ResultScreen = resultScreen;
      _itemsAccounting.RefreshButton = _refreshButton;
    }
  }
}