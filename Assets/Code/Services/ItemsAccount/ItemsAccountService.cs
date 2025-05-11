using Code.MonoBehaviours;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Services.ItemsAccount
{
  public class ItemsAccountService : IItemsAccountService
  {
    public List<Slot> Slots { get; set; }

    private const int Match = 3;

    private readonly List<Figurine> _figurines = new();
    private readonly List<Figurine> _activeFigurines = new();

    public ItemsAccountService()
    {

    }

    public void AddFigurine(Figurine figurine)
    {
      figurine.Clicked += ProcessClick;
      figurine.Arrived += ProcessArrival;
      _figurines.Add(figurine);
    }

    private void ProcessClick(Figurine figurine)
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