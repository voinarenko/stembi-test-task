using Code.Infrastructure.Factory;
using Code.Infrastructure.Loading;
using Code.Infrastructure.States.Factory;
using Code.Infrastructure.States.GameStates;
using Code.Infrastructure.States.StateMachine;
using Code.Services.Async;
using Code.Services.Input;
using Code.Services.InputProcessing;
using Code.Services.ItemsAccount;
using Code.Services.ItemsGeneration;
using Code.Services.Random;
using Code.Services.StaticData;
using Code.Services.UIAnimation;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, IInitializable
  {
    private const string CurtainResourcePath = "Curtain";

    public override void InstallBindings()
    {
      BindInputServices();
      BindInfrastructureServices();
      BindCommonServices();
      BindGameplayServices();
      BindGameFactory();
      BindStateMachine();
      BindStateFactory();
      BindGameStates();
      BindItemsServices();
      BindUIServices();
    }

    private void BindInputServices()
    {
      Container.Bind<IInputService>().To<InputService>().AsSingle();
      Container.Bind<IInputProcessingService>().To<InputProcessingService>().AsSingle();
    }

    private void BindInfrastructureServices() =>
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();

    private void BindCommonServices()
    {
      Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
      Container.Bind<IAsyncService>().To<AsyncService>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
      Container.Bind<ILoadingCurtain>()
        .To<LoadingCurtain>()
        .FromComponentInNewPrefabResource(CurtainResourcePath)
        .AsSingle();
    }

    private void BindGameplayServices() =>
      Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();

    private void BindGameFactory() =>
      Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();

    private void BindStateMachine() =>
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

    private void BindStateFactory() =>
      Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadLevelState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LevelLoopState>().AsSingle();
    }

    private void BindItemsServices()
    {
      Container.Bind<IItemsAccountService>().To<ItemsAccountingService>().AsSingle();
      Container.Bind<IItemGenerationService>().To<ItemGenerationService>().AsSingle();
    }

    public void Initialize() =>
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();

    private void BindUIServices() =>
      Container.Bind<IUIAnimationService>().To<UIAnimationService>().AsSingle();
  }
}