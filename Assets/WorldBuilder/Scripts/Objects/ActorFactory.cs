namespace WorldBuilder.Objects
{
    using System.Collections.Generic;
    using Formats.Adr;
    using Formats.Dme;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Handles creation, and destruction of actor instances.
    /// </summary>
    public class ActorFactory
    {
        private Dictionary<Adr, ForgelightActor> cachedActors = new Dictionary<Adr, ForgelightActor>();
        private GameObject actorPoolParent;

        public ActorFactory()
        {
            // TODO Object Pooling
            actorPoolParent = new GameObject("Available Actors");
            actorPoolParent.SetActive(false);
        }

        // Dependencies
        [Inject] private AssetManager assetManager;
        [Inject] private MeshFactory meshFactory;

        public ForgelightActor CreateActor(Adr actorDefinition)
        {
            ForgelightActor actorSource;
            if (!cachedActors.TryGetValue(actorDefinition, out actorSource))
            {
                actorSource = LoadNewActor(actorDefinition);
            }

            ForgelightActor actorInstance = GameObject.Instantiate(actorSource.gameObject).GetComponent<ForgelightActor>();

            return actorInstance;
        }

        public ForgelightActor LoadNewActor(Adr actorDefinition)
        {
            GameObject actorSource = new GameObject(actorDefinition.DisplayName);
            ForgelightActor actor = actorSource.AddComponent<ForgelightActor>();

            MeshFilter meshFilter = actorSource.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = actorSource.AddComponent<MeshRenderer>();

            // TODO Materials and Textures

            Dme modelDef = assetManager.LoadPackAsset<Dme>(actorDefinition.Base);
            meshFilter.sharedMesh = meshFactory.CreateMeshFromDme(modelDef);

            // TODO LOD Groups

            actor.Init(actorDefinition, meshRenderer);

            return actor;
        }

    }
}