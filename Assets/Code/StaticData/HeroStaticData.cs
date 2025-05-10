using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero", order = 0)]
  public class HeroStaticData : ScriptableObject
  {
    public int Health;
    public float Speed;
    public float ShieldDuration;
  }
}