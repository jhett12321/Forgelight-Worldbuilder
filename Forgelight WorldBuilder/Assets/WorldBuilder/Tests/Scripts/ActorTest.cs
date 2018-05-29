namespace WorldBuilder.Tests.Scripts
{
    using Objects;
    using UnityEngine;
    using Zenject;

    [RequireComponent(typeof(GameObjectContext))]
    public class ActorTest : MonoBehaviour
    {
        [Inject] private GameManager gameManager;
        [Inject] private ActorFactory actorFactory;

        public string gamePath;
        public string actorDefName;

        private void Awake()
        {
            gameManager.SwitchGame();
            gameManager.OnGameLoaded += OnGameLoaded;
        }

        private void OnGameLoaded(ForgelightGame game)
        {
            actorFactory.CreateActor(actorDefName);
        }
    }
}