using Code.Infrastructure.AssetManagement;
using Code.Services.Random;
using UnityEngine;

namespace Code.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    public RectTransform UIRoot { get; set; }
    public Transform ElementsContainer { get; set; }
    private readonly IAssets _assets;
    private readonly IRandomService _randomService;

    public GameFactory(IAssets assets, IRandomService randomService)
    {
      _assets = assets;
      _randomService = randomService;
    }
  }
}