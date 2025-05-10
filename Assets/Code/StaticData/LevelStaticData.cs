using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
  public class LevelStaticData : ScriptableObject
  {
    public LevelType Type;
    public float Time;
    public float TimerStep;
    public GameObject ObstaclePrefab;
    public int ObstacleHitPoints;
  }
}