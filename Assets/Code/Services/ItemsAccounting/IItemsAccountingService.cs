using Code.MonoBehaviours;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.ItemsAccounting
{
  public interface IItemsAccountingService
  {
    void AddFigurine(Figurine figurine);
    List<Slot> Slots { get; set; }
    Transform DisabledItems { get; set; }
  }
}