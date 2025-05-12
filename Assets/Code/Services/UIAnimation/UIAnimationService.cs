using DG.Tweening;
using UnityEngine;

namespace Code.Services.UIAnimation
{
  public class UIAnimationService : IUIAnimationService
  {
    public RectTransform UiRoot { get; set; }
    public RectTransform TopArea { get; set; }
    public RectTransform BottomArea { get; set; }
    public RectTransform SuccessScreen { get; set; }
    public RectTransform FailureScreen { get; set; }

    private const float AnimationDuration = 0.5f;
    private const float ResultTargetPositionX = 0;

    private float _topAreaHidePositionY;
    private float _topAreaTargetPositionY;
    private float _bottomAreaHidePositionY;
    private float _bottomAreaTargetPositionY;
    private float _resultSuccessHidePositionX;
    private float _resultFailureHidePositionX;

    public void Init()
    {
      InitUIPositions();
      SetupUIElements();
    }

    public void ShowUIElements()
    {
      TopArea.DOLocalMoveY(_topAreaTargetPositionY, AnimationDuration)
        .SetEase(Ease.OutBounce);
      BottomArea.DOLocalMoveY(_bottomAreaTargetPositionY, AnimationDuration)
        .SetEase(Ease.OutBounce);
    }

    public void HideUIElements()
    {
      TopArea.DOLocalMoveY(_topAreaHidePositionY, AnimationDuration)
        .SetEase(Ease.InBounce);
      BottomArea.DOLocalMoveY(_bottomAreaHidePositionY, AnimationDuration)
        .SetEase(Ease.InBounce);
    }

    public void ShowResult(bool isSuccess)
    {
      HideUIElements();
      if (isSuccess)
        SuccessScreen.DOLocalMoveX(ResultTargetPositionX, AnimationDuration)
          .SetEase(Ease.OutBounce);
      else
        FailureScreen.DOLocalMoveX(ResultTargetPositionX, AnimationDuration)
          .SetEase(Ease.OutBounce);
    }

    public void HideResult(bool isSuccess)
    {
      if (isSuccess)
        SuccessScreen.DOLocalMoveX(_resultSuccessHidePositionX, AnimationDuration);
      else
        FailureScreen.DOLocalMoveX(_resultFailureHidePositionX, AnimationDuration);
    }

    private void InitUIPositions()
    {
      _topAreaHidePositionY = UiRoot.rect.height / 2 + TopArea.rect.height;
      _topAreaTargetPositionY = UiRoot.rect.height / 2;
      _bottomAreaHidePositionY = -UiRoot.rect.height / 2 - BottomArea.rect.height;
      _bottomAreaTargetPositionY = -UiRoot.rect.height / 2;
      SuccessScreen.sizeDelta = new Vector2(UiRoot.rect.width, UiRoot.rect.height);
      _resultSuccessHidePositionX = UiRoot.rect.width;
      FailureScreen.sizeDelta = new Vector2(UiRoot.rect.width, UiRoot.rect.height);
      _resultFailureHidePositionX = -UiRoot.rect.width;
    }

    private void SetupUIElements()
    {
      TopArea.localPosition = new Vector3(0, _topAreaHidePositionY, 0);
      BottomArea.localPosition = new Vector3(0, _bottomAreaHidePositionY, 0);
      SuccessScreen.localPosition = new Vector3(_resultSuccessHidePositionX, 0, 0);
      FailureScreen.localPosition = new Vector3(_resultFailureHidePositionX, 0, 0);
    }
  }
}