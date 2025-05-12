using UnityEngine;

namespace Code.Services.UIAnimation
{
  public interface IUIAnimationService
  {
    RectTransform UiRoot { get; set; }
    RectTransform TopArea { get; set; }
    RectTransform BottomArea { get; set; }
    RectTransform SuccessScreen { get; set; }
    RectTransform FailureScreen { get; set; }
    void Init();
    void ShowUIElements();
    void HideUIElements();
    void ShowResult(bool isSuccess);
    void HideResult(bool isSuccess);
  }
}