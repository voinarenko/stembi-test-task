using Code.StaticData;
using UnityEngine;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string LevelDataPath = "StaticData/LevelData";
    private LevelStaticData _level;

    public void LoadLevel() => 
      _level = Resources.Load<LevelStaticData>(LevelDataPath);

    public LevelStaticData GetLevel() =>
      _level;
  }
}