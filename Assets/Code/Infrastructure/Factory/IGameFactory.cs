using Code.MonoBehaviours;
using Code.Services;
using Code.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    RectTransform UIRoot { get; set; }
    Transform ElementsContainer { get; set; }
    List<Transform> DropPoints { get; set; }
    Figurine CreateFigurine(GameObject shape, Sprite icon, Color color, Vector3 shapeScale, Vector3 iconScale);
    List<(GameObject shape, Sprite icon, Color color)> GenerateRandomFigurineList(LevelStaticData data);
  }
}