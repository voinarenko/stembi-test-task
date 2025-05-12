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

    public List<ImprintKey> GenerateRandomFigurineKeys(LevelStaticData data)
    {
      var allCombinations = new List<ImprintKey>();

      for (var s = 0; s < data.Shapes.Count; s++)
      for (var c = 0; c < data.Colors.Count; c++)
      for (var i = 0; i < data.Icons.Count; i++)
        allCombinations.Add(new ImprintKey(s, i, c));

      Shuffle(allCombinations);

      var result = new List<ImprintKey>();
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
    
    public List<ImprintKey> GenerateRefreshedFigurineKeys(LevelStaticData data, List<Figurine> activeFigurines, int figurinesCount)
    {
      var newKeys = new List<ImprintKey>();

      var activeCounts = activeFigurines
        .GroupBy(f => f.DataKey)
        .ToDictionary(g => g.Key, g => g.Count());

      foreach (var (key, count) in activeCounts)
      {
        var remainder = count % Match;
        var toAdd = remainder == 0 ? 0 : Match - remainder;

        for (var i = 0; i < toAdd; i++)
          newKeys.Add(key);
      }

      var needed = figurinesCount - newKeys.Count;

      var allCombinations = new List<ImprintKey>();

      for (var s = 0; s < data.Shapes.Count; s++)
      for (var c = 0; c < data.Colors.Count; c++)
      for (var i = 0; i < data.Icons.Count; i++)
        allCombinations.Add(new ImprintKey(s, i, c));

      Shuffle(allCombinations);

      var uniqueToAdd = new List<ImprintKey>();
      var index = 0;

      while (uniqueToAdd.Count < needed)
      {
        if (index >= allCombinations.Count)
          index = 0;

        var combo = allCombinations[index];
        for (var j = 0; j < Match && uniqueToAdd.Count < needed; j++)
          uniqueToAdd.Add(combo);

        index++;
      }

      newKeys.AddRange(uniqueToAdd);
      Shuffle(newKeys);

      return newKeys;
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