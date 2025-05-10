using Code.Infrastructure.Factory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private List<Transform> _dropPoints;
    [SerializeField] private Transform _figurinesContainer;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory windowFactory) =>
      _gameFactory = windowFactory;

    public void Initialize()
    {
      _gameFactory.DropPoints = _dropPoints;
      _gameFactory.ElementsContainer = _figurinesContainer;
    }
  }
}