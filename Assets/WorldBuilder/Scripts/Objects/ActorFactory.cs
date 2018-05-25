namespace WorldBuilder.Objects
{
    using System.Collections.Generic;
    using Formats.Adr;
    using Formats.Dme;
    using Materials;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Handles creation, and destruction of actor instances.
    /// </summary>
    public class ActorFactory
    {
        // Dependencies
        [Inject] private AssetManager assetManager;
        [Inject] private ActorMaterialFactory materialFactory;
        [Inject] private MeshFactory meshFactory;

        private Dictionary<Adr, ForgelightActor> cachedActors = new Dictionary<Adr, ForgelightActor>();
        private GameObject actorPoolParent;

        public ActorFactory()
        {
            // TODO Object Pooling
            actorPoolParent = new GameObject("Available Actors");
            actorPoolParent.SetActive(false);
        }

        public ForgelightActor CreateActor(string actorDefName)
        {
            Adr actorDefinition = assetManager.LoadPackAsset<Adr>(actorDefName);

            if (actorDefinition == null)
            {
                Debug.LogWarning("Actor \"" + actorDefName + "\" does not exist!");
                return null;
            }

            return CreateActor(actorDefinition);
        }

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

        private ForgelightActor LoadNewActor(Adr actorDefinition)
        {
            GameObject actorSource = new GameObject(actorDefinition.DisplayName);
            ForgelightActor actor = actorSource.AddComponent<ForgelightActor>();

            MeshFilter meshFilter = actorSource.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = actorSource.AddComponent<MeshRenderer>();

            Dme modelDef = assetManager.LoadPackAsset<Dme>(actorDefinition.Base);
            meshFilter.sharedMesh = meshFactory.CreateMeshFromDme(modelDef);

            // Materials
            meshRenderer.sharedMaterials = materialFactory.GetActorMaterials(modelDef);

            // TODO LOD Groups
            actor.Init(actorDefinition);
            actor.transform.SetParent(actorPoolParent.transform);

            return actor;
        }
    }
}