using Code.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Services.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private Dictionary<EnemyType, EnemyStaticData> _enemies;
    private Dictionary<EnemyType, GhostStaticData> _ghosts;
    private Dictionary<BonusType, BonusStaticData> _bonuses;
    private Dictionary<LevelBonusType, LevelBonusStaticData> _levelBonuses;
    private FieldStaticData _field;
    private HeroStaticData _hero;
    private Dictionary<LevelType, LevelStaticData> _levels;

    public void LoadHero() => _hero = Resources.Load<HeroStaticData>("StaticData/HeroData");

    public void LoadField() => _field = Resources.Load<FieldStaticData>("StaticData/FieldData");

    public void LoadLevels() =>
      _levels = Resources
        .LoadAll<LevelStaticData>("StaticData/Levels")
        .ToDictionary(x => x.Type, x => x);

    public void LoadEnemies() =>
      _enemies = Resources
        .LoadAll<EnemyStaticData>("StaticData/Enemies")
        .ToDictionary(x => x.Type, x => x);

    public void LoadGhosts() =>
      _ghosts = Resources
        .LoadAll<GhostStaticData>("StaticData/Enemies")
        .ToDictionary(x => x.Type, x => x);

    public void LoadBonuses() =>
      _bonuses = Resources
        .LoadAll<BonusStaticData>("StaticData/Bonuses")
        .ToDictionary(x => x.Type, x => x);

    public void LoadLevelBonuses()
    {
      _levelBonuses = Resources
        .LoadAll<LevelBonusStaticData>("StaticData/Bonuses")
        .ToDictionary(x => x.Type, x => x);
    }

    public FieldStaticData GetField() =>
      _field;

    public LevelStaticData GetLevel(LevelType type) =>
      _levels[type];

    public EnemyStaticData GetEnemy(EnemyType type) =>
      _enemies[type];

    public GhostStaticData GetGhost(EnemyType type) =>
      _ghosts[type];

    public BonusStaticData GetBonus(BonusType type) =>
      _bonuses[type];

    public LevelBonusStaticData GetLevelBonus(LevelBonusType type) =>
      _levelBonuses[type];

    public HeroStaticData GetHero() =>
      _hero;
  }
}