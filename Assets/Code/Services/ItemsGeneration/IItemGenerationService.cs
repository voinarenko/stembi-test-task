using Code.Data;
using Code.StaticData;
using System.Collections.Generic;

namespace Code.Services.ItemsGeneration
{
  public interface IItemGenerationService
  {
    List<Imprint> GenerateRandomFigurineList(LevelStaticData data);
  }
}