using Code.Services;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    RectTransform UIRoot { get; set; }
    Transform ElementsContainer { get; set; }
  }
}