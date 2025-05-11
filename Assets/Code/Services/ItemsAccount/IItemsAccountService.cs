using Code.MonoBehaviours;
using System.Collections.Generic;

namespace Code.Services.ItemsAccount
{
  public interface IItemsAccountService
  {
    void AddFigurine(Figurine figurine);
    List<Slot> Slots { get; set; }
  }
}