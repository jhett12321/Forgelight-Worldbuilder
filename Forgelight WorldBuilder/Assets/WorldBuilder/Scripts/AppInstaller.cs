namespace WorldBuilder
{
    using Materials;
    using Objects;
    using Terrain;
    using UnityEngine;
    using WorldEditor;
    using Zenject;
    using Zone;

    [CreateAssetMenu(fileName = "EditorConfig", menuName = "WorldBuilder/Editor Config")]
    public class AppInstaller : ScriptableObjectInstaller
    {
        public ForgelightActor MissingActorPrefab;
        public Material ActorSharedMaterial;
        public Material TerrainSharedMaterial;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EditorManager>().FromNew().AsSingle().NonLazy();

            Container.Bind<GameManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<AssetManager>().FromNew().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<MaterialDefinitionManager>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ActorDefinitionManager>().FromNew().AsSingle().NonLazy();

            Container.Bind<ZoneFactory>().FromNew().AsSingle().NonLazy();

            Container.Bind<ActorFactory>().FromNew().AsSingle().WithArguments(MissingActorPrefab).NonLazy();
            Container.Bind<ActorMaterialFactory>().FromNew().AsSingle().WithArguments(ActorSharedMaterial).NonLazy();
            Container.Bind<ActorMeshFactory>().FromNew().AsSingle().NonLazy();

            Container.Bind<TerrainFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<ChunkMaterialFactory>().FromNew().AsSingle().WithArguments(TerrainSharedMaterial).NonLazy();
            Container.Bind<ChunkMeshFactory>().FromNew().AsSingle().NonLazy();
        }
    }
}