using Code.Infrastructure.Factory;
using Code.MonoBehaviours;
using Code.Services.ItemsAccount;
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
    [SerializeField] private List<Slot> _slots;
    private IGameFactory _gameFactory;
    private IInputProcessService _inputProcess;
    private IItemsAccountService _itemsAccount;

    [Inject]
    private void Construct(IGameFactory gameFactory, IInputProcessService inputProcess, IItemsAccountService itemsAccount)
    {
      _itemsAccount = itemsAccount;
      _inputProcess = inputProcess;
      _gameFactory = gameFactory;
    }

    public void Initialize()
    {
      _gameFactory.MainCamera = _camera;
      _gameFactory.LevelRoot = _levelRoot;
      _gameFactory.DropPoints = _dropPoints;
      _inputProcess.MainCamera = _camera;
      _itemsAccount.Slots = _slots;
    }
  }
}