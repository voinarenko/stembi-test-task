using Code.Infrastructure.Factory;
using Code.Services.ItemsProcess;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _levelRoot;
    [SerializeField] private List<Transform> _dropPoints;
    [SerializeField] private Transform _actionBar;
    private IGameFactory _gameFactory;
    private IInputProcessService _inputProcess;

    [Inject]
    private void Construct(IGameFactory gameFactory, IInputProcessService inputProcess)
    {
      _inputProcess = inputProcess;
      _gameFactory = gameFactory;
    }

    public void Initialize()
    {
      _gameFactory.MainCamera = _camera;
      _gameFactory.LevelRoot = _levelRoot;
      _gameFactory.DropPoints = _dropPoints;
      _inputProcess.MainCamera = _camera;
    }
  }
}