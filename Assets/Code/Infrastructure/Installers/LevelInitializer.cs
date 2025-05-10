using Code.Infrastructure.Factory;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class LevelInitializer : MonoBehaviour, IInitializable
  {
    [SerializeField] private Transform _elementsContainer;
    private IGameFactory _gameFactory;

    [Inject]
    private void Construct(IGameFactory windowFactory) =>
      _gameFactory = windowFactory;

    public void Initialize()
    {
      _gameFactory.ElementsContainer = _elementsContainer;
    }
  }
}