using Code.Data;
using Code.MonoBehaviours;
using Code.StaticData;
using System.Collections.Generic;

namespace Code.Services.ItemsGeneration
{
  public interface IItemGenerationService
  {
    List<ImprintKey> GenerateRandomFigurineKeys(LevelStaticData data);
    List<ImprintKey> GenerateRefreshedFigurineKeys(LevelStaticData data, List<Figurine> activeFigurines, int figurinesCount);
  }
}