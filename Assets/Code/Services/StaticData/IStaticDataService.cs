using Code.StaticData;

namespace Code.Services.StaticData
{
  public interface IStaticDataService : IService
  {
    void LoadField();
    void LoadEnemies();
    void LoadHero();
    void LoadLevels();
    FieldStaticData GetField();
    EnemyStaticData GetEnemy(EnemyType type);
    HeroStaticData GetHero();
    LevelStaticData GetLevel(LevelType type);
    void LoadBonuses();
    void LoadLevelBonuses();
    BonusStaticData GetBonus(BonusType type);
    LevelBonusStaticData GetLevelBonus(LevelBonusType type);
    GhostStaticData GetGhost(EnemyType enemyType);
    void LoadGhosts();
  }
}