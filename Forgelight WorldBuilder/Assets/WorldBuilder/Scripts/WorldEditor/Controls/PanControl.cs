namespace WorldBuilder.WorldEditor
{
    using UnityEngine;
    using Zenject;

    [RequireComponent(typeof(GameObjectContext))]
    public class PanControl : MonoBehaviour, IEditorControl
    {
        // Dependencies
        [Inject] private CameraManager cameraManager;

        // Inspector
        [Range(0.1f, 1000f)]
        public float panSpeed;

        private Vector3 lastPos;

        public bool ValidateInput() => Input.GetMouseButton(2);

        public void OnModeEnter()
        {
            lastPos = cameraManager.UnityCamera.ScreenToWorldPoint(Input.mousePosition);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
        }

        public void OnModeExit()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void Tick()
        {
            Vector3 delta = cameraManager.UnityCamera.ScreenToWorldPoint(Input.mousePosition) - lastPos;

            cameraManager.VCam.transform.position += delta;

            lastPos = cameraManager.UnityCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}