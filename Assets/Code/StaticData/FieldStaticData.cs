using UnityEngine;

namespace Code.StaticData
{
  [CreateAssetMenu(fileName = "FieldData", menuName = "StaticData/Field", order = 0)]
  public class FieldStaticData : ScriptableObject
  {
    public int Size;
    public int Depth;
    public float Spacing;
    public float Speed;
    public float Pause;
    public float GameOverTarget;
  }
}