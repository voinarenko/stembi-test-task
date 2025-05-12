using Code.Data;
using Code.MonoBehaviours;
using Code.Services;
using Code.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    Camera MainCamera { get; set; }
    Transform LevelRoot { get; set; }
    List<Transform> DropPoints { get; set; }
    Jar CreateJar(GameObject prefab);

    Figurine GetFigurine(GameObject shape, Sprite icon, Color color, Vector3 shapeScale, Vector3 iconScale,
      Transform container);

    void ReturnFigurine(Figurine figurine);
  }
}