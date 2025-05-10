using Code.Services.StaticData;
using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "BonusData", menuName = "StaticData/Bonus")]
  public class BonusStaticData : ScriptableObject
  {
    public BonusType Type;
    public float Amount;
    public GameObject Prefab;
    public float ReachThreshold;
    public float Speed;
  }
}