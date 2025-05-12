using Code.Infrastructure.Factory;
using Code.MonoBehaviours;
using Code.Services.InputProcessing;
using Code.Services.ItemsAccount;
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
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private Transform _disabledItems;
    private IGameFactory _gameFactory;
    private IInputProcessingService _inputProcessing;
    private IItemsAccountService _itemsAccount;

    [Inject]
    private void Construct(IGameFactory gameFactory, IInputProcessingService inputProcessing, IItemsAccountService itemsAccount)
    {
      _itemsAccount = itemsAccount;
      _inputProcessing = inputProcessing;
      _gameFactory = gameFactory;
    }

    public void Initialize()
    {
      _gameFactory.MainCamera = _camera;
      _gameFactory.LevelRoot = _levelRoot;
      _gameFactory.DropPoints = _dropPoints;
      _inputProcessing.MainCamera = _camera;
      _itemsAccount.Slots = _slots;
      _itemsAccount.DisabledItems = _disabledItems;
    }
  }
}