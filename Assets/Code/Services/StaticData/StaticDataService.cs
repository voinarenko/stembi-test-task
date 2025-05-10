using Code.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private LevelStaticData _level;

    public void LoadLevel() => 
      _level = Resources.Load<LevelStaticData>("StaticData/LevelData");

    public LevelStaticData GetLevel() =>
      _level;
  }
}