namespace WorldBuilder
{
    using Materials;
    using Objects;
    using UnityEngine;
    using Zenject;

    [CreateAssetMenu(fileName = "AppConfig", menuName = "WorldBuilder/Application Configuration")]
    public class AppInstaller : ScriptableObjectInstaller
    {
        public string MaterialsAsset = "materials_3.xml";

        public override void InstallBindings()
        {
            Container.Bind<AssetManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<MaterialDefinitionManager>().FromNew().AsSingle().WithArguments(MaterialsAsset).NonLazy();

            Container.Bind<ActorFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<MeshFactory>().FromNew().AsSingle().NonLazy();
        }
    }
}