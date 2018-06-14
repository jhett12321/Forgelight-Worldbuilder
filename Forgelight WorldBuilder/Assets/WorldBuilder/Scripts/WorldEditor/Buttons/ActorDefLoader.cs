namespace WorldBuilder.WorldEditor.Buttons
{
    using Objects;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    // TODO Replace with dynamic UI system.
    [RequireComponent(typeof(GameObjectContext))]
    public class ActorDefLoader : MonoBehaviour
    {
        // Dependencies
        [Inject] private ActorFactory actorFactory;
        [Inject] private CameraManager cameraManager;

        // Inspector
        public InputField ActorField;

        public async void OnCreateActorButtonPressed()
        {
            ForgelightActor actor = await actorFactory.CreateActor(ActorField.text);
            actor.transform.position = cameraManager.UnityCamera.transform.position;
        }
    }
}