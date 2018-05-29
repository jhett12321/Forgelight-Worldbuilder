namespace WorldBuilder.Objects
{
    using System.Collections.Generic;
    using Formats.Adr;
    using Formats.Dme;
    using Materials;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Zenject;

    /// <summary>
    /// Handles creation, and destruction of actor instances.
    /// </summary>
    public class ActorFactory
    {
        // Dependencies
        [Inject] private AssetManager assetManager;
        [Inject] private ActorDefinitionManager actorDefinitionManager;
        [Inject] private ActorMaterialFactory materialFactory;
        [Inject] private ActorMeshFactory actorMeshFactory;

        private Dictionary<string, ForgelightActor> cachedActors = new Dictionary<string, ForgelightActor>();
        private GameObject actorPoolParent;

        [Inject]
        public ActorFactory(GameManager gameManager)
        {
            gameManager.OnGameLoaded += OnGameLoaded;
        }

        private void OnGameLoaded()
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
            Adr actorDefinition = actorDefinitionManager.GetDefinition(actorDefName);

            if (actorDefinition != null)
            {
                return CreateActor(actorDefinition);
            }

            Debug.LogWarning("Actor \"" + actorDefName + "\" does not exist!");
            return null;
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
            Dme modelDef = assetManager.LoadPackAsset<Dme>(actorDefinition.Base);

            Assert.IsNotNull(modelDef);

            // Deserialization
            UnityEngine.Mesh mesh = actorMeshFactory.CreateMeshFromDme(modelDef);
            Material[] materials = materialFactory.GetActorMaterials(modelDef);

            Assert.IsNotNull(mesh);
            Assert.IsNotNull(materials);

            // GameObject "Prefab"
            GameObject actorSource = new GameObject(actorDefinition.DisplayName);
            ForgelightActor actor = actorSource.AddComponent<ForgelightActor>();
            MeshFilter meshFilter = actorSource.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = actorSource.AddComponent<MeshRenderer>();

            // Assign deserialized data
            meshFilter.sharedMesh = mesh;
            meshRenderer.sharedMaterials = materials;

            // TODO LOD Groups
            actor.Init(actorDefinition);
            actor.transform.SetParent(actorPoolParent.transform);

            return actor;
        }
    }
}