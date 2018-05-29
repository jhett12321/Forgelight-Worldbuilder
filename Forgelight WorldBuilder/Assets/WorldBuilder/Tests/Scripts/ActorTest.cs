namespace WorldBuilder.Tests.Scripts
{
    using UnityEngine;
    using Zenject;
    using Zone;

    [RequireComponent(typeof(GameObjectContext))]
    public class ActorTest : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        [Inject] private ZoneFactory zoneFactory;

        public string zoneName;

        private void Awake()
        {
            gameManager.SwitchGame();
            gameManager.OnGameLoaded += OnGameLoaded;
        }

        private void OnGameLoaded()
        {
            zoneFactory.LoadZoneFromPacks(zoneName);
        }
    }
}