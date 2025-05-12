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
    void Init(LevelStaticData data);
    Jar CreateJar(GameObject prefab);

    Figurine GetFigurine(LevelStaticData data, ImprintKey key, Vector3 shapeScale, Vector3 iconScale,
      Transform container);

    void ReturnFigurine(Figurine figurine);
  }
}