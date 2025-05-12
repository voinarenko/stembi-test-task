using Code.MonoBehaviours;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services.ItemsAccount
{
  public interface IItemsAccountService
  {
    void AddFigurine(Figurine figurine);
    List<Slot> Slots { get; set; }
    Transform DisabledItems { get; set; }
  }
}