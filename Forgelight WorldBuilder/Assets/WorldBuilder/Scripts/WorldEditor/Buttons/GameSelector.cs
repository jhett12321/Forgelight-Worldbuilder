namespace WorldBuilder.WorldEditor.Buttons
{
    using UnityEngine;
    using Zenject;

    // TODO Replace with dynamic UI system.
    [RequireComponent(typeof(GameObjectContext))]
    public class GameSelector : MonoBehaviour
    {
        [Inject] private GameManager gameManager;

        public void OnSelectorClicked()
        {
            gameManager.SwitchGame();
        }
    }
}