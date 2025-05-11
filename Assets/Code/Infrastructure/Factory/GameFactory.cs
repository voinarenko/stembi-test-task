using Code.MonoBehaviours;
using Code.Services.Random;
using Code.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public Camera MainCamera { get; set; }
    public RectTransform UIRoot { get; set; }
    public Transform LevelRoot { get; set; }
    public List<Transform> DropPoints { get; set; }

    private readonly IRandomService _random;
    private int _previousDropPointIndex = -1;

    public GameFactory(IRandomService random) =>
      _random = random;

    public Jar CreateJar(GameObject prefab)
    {
      var go = Object.Instantiate(prefab, LevelRoot);
      return !go.TryGetComponent<Jar>(out var jar) ? null : jar;
    }

    public List<(GameObject shape, Sprite icon, Color color)> GenerateRandomFigurineList(LevelStaticData data)
    {
      var allCombinations = (from shape in data.Shapes
        from color in data.Colors
        from icon in data.Icons
        select (shape, icon, color)).ToList();

      Shuffle(allCombinations);

      var result = new List<(GameObject, Sprite, Color)>();
      var tripletCount = data.TotalFigurines / 3;
      var remainder = data.TotalFigurines % 3;

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

    public Figurine CreateFigurine(GameObject shape, Sprite icon, Color color, Vector3 shapeScale, Vector3 iconScale,
      Transform container)
    {
      var go = Object.Instantiate(shape, container);
      go.transform.position = SelectRandomDropPoint();
      if (!go.TryGetComponent<Figurine>(out var figurine))
        return null;

      figurine.Init(icon, iconScale, shapeScale, color);
      return figurine;
    }

    private void Shuffle<T>(List<T> list)
    {
      for (var i = list.Count - 1; i > 0; i--)
      {
        var j = _random.Range(0, i + 1);
        (list[i], list[j]) = (list[j], list[i]);
      }
    }

    private Vector3 SelectRandomDropPoint()
    {
      if (DropPoints.Count == 0)
        return Vector3.zero;

      int randomIndex;

      switch (DropPoints.Count)
      {
        case 1:
          randomIndex = 0;
          break;
        case 2:
          randomIndex = _previousDropPointIndex == 0 ? 1 : 0;
          break;
        default:
        {
          randomIndex = _random.Range(0, DropPoints.Count - 1);
          if (randomIndex >= _previousDropPointIndex) randomIndex++;
          break;
        }
      }

      _previousDropPointIndex = randomIndex;
      return DropPoints[randomIndex].position;
    }
  }
}