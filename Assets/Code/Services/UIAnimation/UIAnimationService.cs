using Code.MonoBehaviours;
using DG.Tweening;
using UnityEngine;

namespace Code.Services.UIAnimation
{
  public class UIAnimationService : IUIAnimationService
  {
    public RectTransform UiRoot { get; set; }
    public RectTransform TopArea { get; set; }
    public RectTransform BottomArea { get; set; }
    public RectTransform ResultScreen { get; set; }

    private const float AnimationDuration = 0.5f;
    private const float ResultTargetPositionX = 0;

    private ResultScreen _resultScreen;

    private float _topAreaHidePositionY;
    private float _topAreaTargetPositionY;
    private float _bottomAreaHidePositionY;
    private float _bottomAreaTargetPositionY;
    private float _resultHidePositionX;

    public void Init()
    {
      ResultScreen.TryGetComponent(out _resultScreen);
      InitUIPositions();
      SetupUIElements();
    }

    public void ShowUIElements()
    {
      TopArea.gameObject.SetActive(true);
      TopArea.DOLocalMoveY(_topAreaTargetPositionY, AnimationDuration)
        .SetEase(Ease.OutBounce);
      BottomArea.gameObject.SetActive(true);
      BottomArea.DOLocalMoveY(_bottomAreaTargetPositionY, AnimationDuration)
        .SetEase(Ease.OutBounce);
    }

    public void HideUIElements()
    {
      TopArea.DOLocalMoveY(_topAreaHidePositionY, AnimationDuration)
        .SetEase(Ease.InBounce)
        .OnComplete(() => TopArea.gameObject.SetActive(false));
      BottomArea.DOLocalMoveY(_bottomAreaHidePositionY, AnimationDuration)
        .SetEase(Ease.InBounce)
        .OnComplete(() => BottomArea.gameObject.SetActive(false));
    }

    public void ShowResult(bool isSuccess)
    {
      HideUIElements();
      _resultScreen.Show(isSuccess);
      ResultScreen.gameObject.SetActive(true);
      ResultScreen.DOLocalMoveX(ResultTargetPositionX, AnimationDuration)
        .SetEase(Ease.OutBounce);
    }

    public void HideResult(bool isSuccess) =>
      ResultScreen.DOLocalMoveX(_resultHidePositionX, AnimationDuration)
        .SetEase(Ease.InBounce)
        .OnComplete(() => ResultScreen.gameObject.SetActive(false));

    private void InitUIPositions()
    {
      _topAreaHidePositionY = UiRoot.rect.height / 2 + TopArea.rect.height;
      _topAreaTargetPositionY = UiRoot.rect.height / 2;
      _bottomAreaHidePositionY = -UiRoot.rect.height / 2 - BottomArea.rect.height;
      _bottomAreaTargetPositionY = -UiRoot.rect.height / 2;
      ResultScreen.sizeDelta = new Vector2(UiRoot.rect.width, UiRoot.rect.height);
      _resultHidePositionX = UiRoot.rect.width;
    }

    private void SetupUIElements()
    {
      TopArea.gameObject.SetActive(false);
      BottomArea.gameObject.SetActive(false);
      ResultScreen.gameObject.SetActive(false);
      TopArea.localPosition = new Vector3(0, _topAreaHidePositionY, 0);
      BottomArea.localPosition = new Vector3(0, _bottomAreaHidePositionY, 0);
      ResultScreen.localPosition = new Vector3(_resultHidePositionX, 0, 0);
    }
  }
}