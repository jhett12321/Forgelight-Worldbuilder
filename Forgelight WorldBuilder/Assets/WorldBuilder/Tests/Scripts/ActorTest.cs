namespace WorldBuilder.Tests.Scripts
{
    using Objects;
    using UnityEngine;
    using Zenject;

    [RequireComponent(typeof(GameObjectContext))]
    public class ActorTest : MonoBehaviour
    {
        [Inject] private ForgelightGame forgelightGame;
        [Inject] private ActorFactory actorFactory;

        public string gamePath;
        public string actorDefName;

        private void Awake()
        {
            forgelightGame.SwitchActiveGame(gamePath);
        }

        private void Start()
        {
            actorFactory.CreateActor(actorDefName);
        }
    }
}