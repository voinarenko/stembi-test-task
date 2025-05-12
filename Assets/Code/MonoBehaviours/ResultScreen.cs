using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.MonoBehaviours
{
  public class ResultScreen : MonoBehaviour
  {
    public event Action PlayClicked;
    public event Action QuitClicked;
    
    [SerializeField] private Image _result;
    [SerializeField] private List<Sprite> _resultSprites;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _quitButton;

    public void Show(bool isWin)
    {
      _result.sprite = _resultSprites[!isWin ? 0 : 1];
      Subscribe();
    }

    private void OnPlayButtonClick()
    {
      Unsubscribe();
      PlayClicked?.Invoke();
    }

    private void OnQuitButtonClick()
    {
      Unsubscribe();
      QuitClicked?.Invoke();
    }

    private void Subscribe()
    {
      _playButton.onClick.AddListener(OnPlayButtonClick);
      _quitButton.onClick.AddListener(OnQuitButtonClick);
    }

    private void Unsubscribe()
    {
      _playButton.onClick.RemoveListener(OnPlayButtonClick);
      _quitButton.onClick.RemoveListener(OnQuitButtonClick);
    }
  }
}