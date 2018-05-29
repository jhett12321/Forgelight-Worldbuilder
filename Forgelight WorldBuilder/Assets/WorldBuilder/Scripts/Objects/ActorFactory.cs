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

        private Dictionary<string, ForgelightActor> cachedActors = new Dictionary<string, ForgelightActor>();
        private GameObject actorPoolParent;

        [Inject]
        public ActorFactory(GameManager gameManager)
        {
            gameManager.OnGameLoaded += OnGameLoaded;
        }

        private void OnGameLoaded(ForgelightGame game)
        {
            if (actorPoolParent != null)
            {
                Object.Destroy(actorPoolParent);
            }

            // TODO Object Pooling
            cachedActors.Clear();
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
            if (!cachedActors.TryGetValue(actorDefinition.Name, out actorSource))
            {
                actorSource = LoadNewActor(actorDefinition);
                cachedActors[actorDefinition.Name] = actorSource;
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