using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "GhostData", menuName = "StaticData/Ghost", order = 0)]
  public class GhostStaticData : ScriptableObject
  {
    public EnemyType Type;
    public float ShootSpeed;
    public float ReachThreshold;
    public int Damage;
    public int HitPoints;
  }
}