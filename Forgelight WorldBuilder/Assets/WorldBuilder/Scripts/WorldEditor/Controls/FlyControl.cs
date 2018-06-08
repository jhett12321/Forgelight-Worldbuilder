namespace WorldBuilder.WorldEditor
{
    using System;
    using UnityEngine;
    using Zenject;

    [RequireComponent(typeof(GameObjectContext))]
    public class FlyControl : MonoBehaviour, IEditorControl
    {
        // Dependencies
        [Inject] private CameraManager cameraManager;

        // Inspector
        [Range(0.1f, 1000f)]
        public float acceleration;
        [Range(0.1f, 1000f)]
        public float shiftAcceleration;

        public bool ValidateInput() => Input.GetMouseButton(1);

        private float speed;

        public void OnModeEnter()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            cameraManager.TogglePOV(true);
        }

        public void OnModeExit()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            speed = 0;
            cameraManager.VCam.transform.rotation = cameraManager.UnityCamera.transform.rotation;
            cameraManager.TogglePOV(false);
        }

        public void Tick()
        {
            // Updates the editor camera's position based on input.
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            if (Math.Abs(vertical) < float.Epsilon && Math.Abs(horizontal) < float.Epsilon)
            {
                speed = 0;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed += shiftAcceleration * Time.deltaTime;
            }
            else
            {
                speed += acceleration * Time.deltaTime;
            }

            Vector3 velocity = cameraManager.UnityCamera.transform.forward * vertical * speed;
            velocity += cameraManager.UnityCamera.transform.right * horizontal * speed;

            cameraManager.VCam.transform.position += velocity;
        }
    }
}