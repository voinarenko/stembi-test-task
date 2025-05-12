using Code.Data;
using Code.MonoBehaviours;
using Code.Services.Random;
using Code.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public Camera MainCamera { get; set; }
    public Transform LevelRoot { get; set; }
    public List<Transform> DropPoints { get; set; }

    private readonly Queue<Figurine> _figurinesPool = new();
    private readonly List<Sprite> _shapeSprites = new();
    private readonly IRandomService _random;
    private int _previousDropPointIndex = -1;

    public GameFactory(IRandomService random) =>
      _random = random;

    public void Init(LevelStaticData data)
    {
      foreach (var item in data.Shapes)
        if (item.TryGetComponent<Figurine>(out var figurine)) 
          _shapeSprites.Add(figurine.Shape.sprite);
    }

    public Jar CreateJar(GameObject prefab)
    {
      var go = Object.Instantiate(prefab, LevelRoot);
      return !go.TryGetComponent<Jar>(out var jar) ? null : jar;
    }

    public Figurine GetFigurine(LevelStaticData data, ImprintKey key, Vector3 shapeScale, Vector3 iconScale,
      Transform container)
    {
      if (_figurinesPool.Count == 0)
        return CreateFigurine(data, key, shapeScale, iconScale, container);

      var figurine = _figurinesPool.Dequeue();
      figurine.ResetData();
      figurine.transform.SetParent(container);
      figurine.transform.position = SelectRandomDropPoint();
      figurine.gameObject.SetActive(true);
      figurine.DataKey = key;
      figurine.Init(_shapeSprites[key.ShapeIndex], data.Icons[key.IconIndex], data.Colors[key.ColorIndex]);
      return figurine;
    }

    public void ReturnFigurine(Figurine figurine) =>
      _figurinesPool.Enqueue(figurine);

    private Figurine CreateFigurine(LevelStaticData data, ImprintKey key, Vector3 shapeScale, Vector3 iconScale,
      Transform container)
    {
      var go = Object.Instantiate(data.Shapes[key.ShapeIndex], container);
      go.transform.position = SelectRandomDropPoint();
      if (!go.TryGetComponent<Figurine>(out var figurine))
        return null;

      figurine.DataKey = key;
      figurine.Init(data.Icons[key.IconIndex], data.Colors[key.ColorIndex], iconScale, shapeScale);
      return figurine;
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