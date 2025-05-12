using Code.Data;
using Code.MonoBehaviours;
using Code.StaticData;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Services.ItemsAccounting
{
  public interface IItemsAccountingService
  {
    void AddFigurine(Figurine figurine);
    List<Slot> Slots { get; set; }
    Transform DisabledItems { get; set; }
    Button RefreshButton { get; set; }
    Transform Container { get; set; }
    void Init();
    UniTask DropFigurinesAsync(LevelStaticData data, Transform container, List<ImprintKey> figurinesKeys);
  }
}