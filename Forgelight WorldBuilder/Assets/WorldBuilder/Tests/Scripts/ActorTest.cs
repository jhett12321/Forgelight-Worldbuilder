namespace WorldBuilder.Tests.Scripts
{
    using Objects;
    using Terrain;
    using UnityEngine;
    using Zenject;

    [RequireComponent(typeof(GameObjectContext))]
    public class ActorTest : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        [Inject] private ActorFactory actorFactory;
        [Inject] private TerrainFactory terrainFactory;

        public string gamePath;
        public string actorDefName;
        public string terrainZoneName;

        private void Awake()
        {
            gameManager.SwitchGame();
            gameManager.OnGameLoaded += OnGameLoaded;
        }

        private void OnGameLoaded()
        {
            // actorFactory.CreateActor(actorDefName);
            terrainFactory.LoadZoneTerrain(terrainZoneName);
        }
    }
}