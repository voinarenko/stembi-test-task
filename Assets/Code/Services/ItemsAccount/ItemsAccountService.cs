using Code.MonoBehaviours;
using Code.Services.Async;
using Code.Services.InputProcess;
using Code.Services.UIAnimation;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Services.ItemsAccount
{
  public class ItemsAccountService : IItemsAccountService
  {
    public List<Slot> Slots { get; set; }

    private const int Match = 3;
    private const float ResultShowDelay = 1;

    private readonly List<Figurine> _figurines = new();
    private readonly List<Figurine> _activeFigurines = new();

    private readonly IUIAnimationService _uiAnimation;
    private readonly IInputProcessService _inputProcess;
    private readonly IAsyncService _async;

    public ItemsAccountService(IUIAnimationService uiAnimation, IInputProcessService inputProcess, IAsyncService async)
    {
      _uiAnimation = uiAnimation;
      _inputProcess = inputProcess;
      _async = async;
    }

    public void AddFigurine(Figurine figurine)
    {
      figurine.Clicked += ProcessClick;
      figurine.Arrived += ProcessArrival;
      _figurines.Add(figurine);
    }

    private void ProcessClick(Figurine figurine) => 
      ProcessClickAsync(figurine).Forget();

    private async UniTaskVoid ProcessClickAsync(Figurine figurine)
    {
      figurine.Clicked -= ProcessClick;
      _figurines.Remove(figurine);
      var emptySlot = Slots.FirstOrDefault(s => s.IsEmpty);
      if (!emptySlot)
      {
        Debug.LogError($"Cannot place {figurine} because there is no empty slots");
        return;
      }

      figurine.transform.SetParent(emptySlot.transform);
      figurine.MoveToSlot();
      figurine.OccupiedSlot = emptySlot;
      emptySlot.IsEmpty = false;
      emptySlot = Slots.FirstOrDefault(s => s.IsEmpty);

      if (!emptySlot)
      {
        _inputProcess.Deactivate();
        await _async.WaitForSeconds(ResultShowDelay);
        _uiAnimation.ShowResult(isSuccess: false);
      }
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
        .GroupBy(f => f.Data)
        .FirstOrDefault(g => g.Count() >= Match);

      if (groupedFigurines == null)
        return;

      var figurinesToRemove = groupedFigurines.Take(Match).ToList();
      foreach (var figurine in figurinesToRemove)
      {
        _activeFigurines.Remove(figurine);
        figurine.OccupiedSlot.IsEmpty = true;
        Object.Destroy(figurine.gameObject);
      }
    }
  }
}