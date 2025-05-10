using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "LevelBonusData", menuName = "StaticData/LevelBonus")]
  public class LevelBonusStaticData : ScriptableObject
  {
    public LevelBonusType Type;
    public GameObject Prefab;
    public float ReachThreshold;
    public float Speed;
  }
}