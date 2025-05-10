using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy", order = 0)]
  public class EnemyStaticData : ScriptableObject
  {
    public EnemyType Type;
    public float ShootSpeed;
    public int Damage;
    public int HitPoints;
    public GameObject Prefab;
    public GameObject GhostPrefab;
  }
}