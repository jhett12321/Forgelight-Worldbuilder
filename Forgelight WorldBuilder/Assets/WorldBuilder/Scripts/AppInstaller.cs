﻿namespace WorldBuilder
{
    using Materials;
    using Objects;
    using UnityEngine;
    using WorldEditor;
    using Zenject;

    [CreateAssetMenu(fileName = "EditorConfig", menuName = "WorldBuilder/Editor Config")]
    public class AppInstaller : ScriptableObjectInstaller
    {
        public Material ActorSharedMaterial;

        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<AssetManager>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MaterialDefinitionManager>().FromNew().AsSingle().NonLazy();

            Container.Bind<ActorFactory>().FromNew().AsSingle().NonLazy();
            Container.Bind<ActorMaterialFactory>().FromNew().AsSingle().WithArguments(ActorSharedMaterial).NonLazy();
            Container.Bind<MeshFactory>().FromNew().AsSingle().NonLazy();

            Container.Bind<CameraManager>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<IEditorControl>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}