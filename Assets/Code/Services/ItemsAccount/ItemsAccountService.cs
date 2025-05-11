using Code.MonoBehaviours;
using System.Collections.Generic;

namespace Code.Services.ItemsAccount
{
  public class ItemsAccountService : IItemsAccountService
  {
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
      _figurines.Remove(figurine);
    }

    private void ProcessArrival(Figurine figurine)
    {
      _activeFigurines.Add(figurine);
      CheckPairing();
    }

    private void CheckPairing()
    {
      foreach (var figurine in _activeFigurines)
      {
        var firstEntry = _activeFigurines[0]
      }
    }
  }
}