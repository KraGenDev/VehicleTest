using Gameplay;
using Gameplay.Mechanics.Road;
using Interfaces;
using Zenject;

namespace DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IInput>().FromComponentInHierarchy().AsSingle();
            Container.Bind<RoadEnemyController>().FromComponentInHierarchy().AsSingle();

            var sceneController = new SceneController();
            Container.Bind<SceneController>().FromInstance(sceneController).AsSingle();
        }
    }
}