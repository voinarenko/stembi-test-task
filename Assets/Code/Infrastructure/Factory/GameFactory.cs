using Code.Data;
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
    public Transform LevelRoot { get; set; }
    public List<Transform> DropPoints { get; set; }

    private readonly Queue<Figurine> _figurinesPool = new();
    private readonly IRandomService _random;
    private int _previousDropPointIndex = -1;

    public GameFactory(IRandomService random) =>
      _random = random;

    public Jar CreateJar(GameObject prefab)
    {
      var go = Object.Instantiate(prefab, LevelRoot);
      return !go.TryGetComponent<Jar>(out var jar) ? null : jar;
    }

    public Figurine GetFigurine(GameObject shape, Sprite icon, Color color, Vector3 shapeScale, Vector3 iconScale,
      Transform container)
    {
      if (_figurinesPool.Count == 0) 
        return CreateFigurine(shape, icon, color, shapeScale, iconScale, container);
      
      var figurine = _figurinesPool.Dequeue();
      figurine.transform.SetParent(container);
      figurine.transform.position = SelectRandomDropPoint();
      figurine.gameObject.SetActive(true);
      figurine.Data = new Imprint(shape, icon, color);
      return figurine;
    }

    public void ReturnFigurine(Figurine figurine) => 
      _figurinesPool.Enqueue(figurine);
    
    private Figurine CreateFigurine(GameObject shape, Sprite icon, Color color, Vector3 shapeScale, Vector3 iconScale,
      Transform container)
    {
      var go = Object.Instantiate(shape, container);
      go.transform.position = SelectRandomDropPoint();
      if (!go.TryGetComponent<Figurine>(out var figurine))
        return null;

      figurine.Data = new Imprint(shape, icon, color);
      figurine.Init(iconScale, shapeScale);
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