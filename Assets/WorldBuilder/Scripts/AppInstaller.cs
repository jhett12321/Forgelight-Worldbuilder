namespace WorldBuilder
{
    using Materials;
    using Objects;
    using UnityEngine;
    using Zenject;

    [CreateAssetMenu(fileName = "AppConfig", menuName = "WorldBuilder/Application Configuration")]
    public class AppInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ForgelightGame>().FromNew().AsSingle().NonLazy();
            Container.Bind<AssetManager>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MaterialDefinitionManager>().FromNew().AsSingle().NonLazy();

            Container.Bind<ActorFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<MeshFactory>().FromNew().AsSingle().NonLazy();
        }
    }
}