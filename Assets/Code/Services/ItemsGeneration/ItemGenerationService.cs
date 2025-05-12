using Code.Data;
using Code.MonoBehaviours;
using Code.Services.Random;
using Code.StaticData;
using System.Collections.Generic;
using System.Linq;

namespace Code.Services.ItemsGeneration
{
  public class ItemGenerationService : IItemGenerationService
  {
    private const int Match = 3;
    private readonly IRandomService _random;
    
    public ItemGenerationService(IRandomService random) =>
      _random = random;
    
    public List<Imprint> GenerateRandomFigurineList(LevelStaticData data)
    {
      var allCombinations = (from shape in data.Shapes
        from color in data.Colors
        from icon in data.Icons
        select new Imprint(shape, icon, color)).ToList();

      Shuffle(allCombinations);

      var result = new List<Imprint>();
      var tripletCount = data.TotalFigurines / Match;
      var remainder = data.TotalFigurines % Match;

      for (var i = 0; i < tripletCount && i < allCombinations.Count; i++)
      {
        var combo = allCombinations[i];
        result.Add(combo);
        result.Add(combo);
        result.Add(combo);
      }

      for (var i = 0; i < remainder; i++)
      {
        var combo = allCombinations[_random.Range(0, tripletCount)];
        result.Add(combo);
      }

      Shuffle(result);
      return result;
    }
    
    public List<Imprint> RefreshFigurines(LevelStaticData data, List<Figurine> activeFigurines, int figurinesCount)
    {
      var newImprints = new List<Imprint>();

      var activeCounts = activeFigurines
        .GroupBy(f => f.Data)
        .ToDictionary(g => g.Key, g => g.Count());

      foreach (var (imprint, currentCount) in activeCounts)
      {
        var remainder = currentCount % Match;
        var toAdd = remainder == 0 ? 0 : Match - remainder;

        for (var k = 0; k < toAdd; k++)
          newImprints.Add(imprint);
      }

      var neededCount = figurinesCount - newImprints.Count;

      var allCombinations = (
        from shape in data.Shapes
        from color in data.Colors
        from icon in data.Icons
        select new Imprint(shape, icon, color)
      ).ToList();

      Shuffle(allCombinations);

      var uniqueToAdd = new List<Imprint>();

      var i = 0;
      while (uniqueToAdd.Count < neededCount)
      {
        if (i >= allCombinations.Count)
          i = 0;

        var imprint = allCombinations[i];
        for (var j = 0; j < Match && uniqueToAdd.Count < neededCount; j++)
          uniqueToAdd.Add(imprint);

        i++;
      }

      newImprints.AddRange(uniqueToAdd);
      Shuffle(newImprints);

      return newImprints;
    }

    private void Shuffle<T>(List<T> list)
    {
      for (var i = list.Count - 1; i > 0; i--)
      {
        var j = _random.Range(0, i + 1);
        (list[i], list[j]) = (list[j], list[i]);
      }
    }
  }
}