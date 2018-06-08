namespace WorldBuilder.WorldEditor.Buttons
{
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    [RequireComponent(typeof(GameObjectContext))]
    public class RenderDistancePicker : MonoBehaviour
    {
        private const string PREF_KEY = "render_distance";

        // Dependencies
        [Inject] private CameraManager cameraManager;

        // Inspector
        public InputField distance;

        private void Start()
        {
            int persistentDistance = PlayerPrefs.GetInt(PREF_KEY, 3000);

            UpdateClipPlane(persistentDistance);
            distance.text = persistentDistance.ToString();
        }

        public void OnChangeButtonPressed()
        {
            int newDistance;

            if (!int.TryParse(distance.text, out newDistance))
            {
                return;
            }

            PlayerPrefs.SetInt(PREF_KEY, newDistance);
            UpdateClipPlane(newDistance);
        }

        private void UpdateClipPlane(int newDistance)
        {
            cameraManager.VCam.m_Lens.FarClipPlane = newDistance;
        }
    }
}