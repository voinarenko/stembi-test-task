using Code.Data;
using Code.Infrastructure.Factory;
using Code.MonoBehaviours;
using Code.Services.Async;
using Code.Services.InputProcessing;
using Code.Services.ItemsGeneration;
using Code.Services.StaticData;
using Code.Services.UIAnimation;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Services.ItemsAccounting
{
  public class ItemsAccountingService : IItemsAccountingService
  {
    public List<Slot> Slots { get; set; }
    public Transform DisabledItems { get; set; }
    public Button RefreshButton { get; set; }
    public Transform Container { get; set; }

    private const int Match = 3;
    private const float ResultShowDelay = 1;

    private readonly List<Figurine> _figurines = new();
    private readonly List<Figurine> _activeFigurines = new();

    private readonly IUIAnimationService _uiAnimation;
    private readonly IInputProcessingService _inputProcessing;
    private readonly IAsyncService _async;
    private readonly IGameFactory _gameFactory;
    private readonly IItemGenerationService _itemGeneration;
    private readonly IStaticDataService _staticData;

    public ItemsAccountingService(IUIAnimationService uiAnimation, IInputProcessingService inputProcessing,
      IAsyncService async, IGameFactory gameFactory, IItemGenerationService itemGeneration,
      IStaticDataService staticData)
    {
      _uiAnimation = uiAnimation;
      _inputProcessing = inputProcessing;
      _async = async;
      _gameFactory = gameFactory;
      _itemGeneration = itemGeneration;
      _staticData = staticData;
    }

    public void Init() =>
      RefreshButton.onClick.AddListener(RefreshField);

    public async UniTask DropFigurinesAsync(LevelStaticData data, Transform container, List<ImprintKey> figurinesKeys)
    {
      foreach (var figurine in figurinesKeys.Select(entry => _gameFactory.GetFigurine(data, entry, data.ShapeScale,
                 data.IconScale, container)))
      {
        AddFigurine(figurine);
        await _async.WaitForSeconds(data.NextFigurineDelay);
      }
    }

    public void AddFigurine(Figurine figurine)
    {
      UnsubscribeFrom(figurine);
      SubscribeTo(figurine);
      _figurines.Add(figurine);
    }

    private void ProcessClick(Figurine figurine)
    {
      figurine.Clicked -= ProcessClick;
      _figurines.Remove(figurine);
      var emptySlot = Slots.FirstOrDefault(s => s.IsEmpty);
      if (!emptySlot)
        return;

      figurine.transform.SetParent(emptySlot.transform);
      figurine.MoveToSlot();
      figurine.OccupiedSlot = emptySlot;
      emptySlot.IsEmpty = false;
      emptySlot = Slots.FirstOrDefault(s => s.IsEmpty);

      if (!emptySlot) 
        ShowResult(isSuccess: false).Forget();
    }

    private void ProcessArrival(Figurine figurine)
    {
      figurine.Arrived -= ProcessArrival;
      _activeFigurines.Add(figurine);
      CheckPairing();
    }

    private void CheckPairing()
    {
      if (_activeFigurines.Count < Match)
        return;

      var groupedFigurines = _activeFigurines
        .GroupBy(f => f.DataKey)
        .FirstOrDefault(g => g.Count() >= Match);

      if (groupedFigurines == null)
        return;

      var figurinesToRemove = groupedFigurines.Take(Match).ToList();
      foreach (var figurine in figurinesToRemove)
      {
        _activeFigurines.Remove(figurine);
        figurine.OccupiedSlot.IsEmpty = true;
        figurine.transform.SetParent(DisabledItems);
        figurine.gameObject.SetActive(false);
        UnsubscribeFrom(figurine);
        _gameFactory.ReturnFigurine(figurine);
      }
      if (_activeFigurines.Count == 0 && _figurines.Count == 0) 
        ShowResult(isSuccess: true).Forget();
    }

    private void RefreshField() =>
      RefreshFieldAsync().Forget();

    private async UniTaskVoid RefreshFieldAsync()
    {
      _uiAnimation.HideUIElements();
      var data = _staticData.GetLevel();
      var figurinesKeys = _itemGeneration.GenerateRefreshedFigurineKeys(data, _activeFigurines, _figurines.Count);
      foreach (var figurine in _figurines)
      {
        UnsubscribeFrom(figurine);
        figurine.gameObject.SetActive(false);
        figurine.transform.SetParent(DisabledItems);
        _gameFactory.ReturnFigurine(figurine);
      }
      _figurines.Clear();
      await DropFigurinesAsync(data, Container, figurinesKeys);
      _uiAnimation.ShowUIElements();
    }

    private void SubscribeTo(Figurine figurine)
    {
      figurine.Clicked += ProcessClick;
      figurine.Arrived += ProcessArrival;
    }

    private void UnsubscribeFrom(Figurine figurine)
    {
      figurine.Clicked -= ProcessClick;
      figurine.Arrived -= ProcessArrival;
    }

    private async UniTaskVoid ShowResult(bool isSuccess)
    {
      _inputProcessing.Deactivate();
      await _async.WaitForSeconds(ResultShowDelay);
      _uiAnimation.ShowResult(isSuccess);
    }
  }
}