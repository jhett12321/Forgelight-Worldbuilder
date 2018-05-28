namespace WorldBuilder.WorldEditor
{
    using Cinemachine;
    using UnityEngine;

    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraManager : MonoBehaviour
    {
        [Range(0, 1)]
        public float Deceleration;

        public Camera UnityCamera { get; private set; }
        public CinemachineVirtualCamera VCam { get; private set; }
        public CinemachinePOV POVComponent { get; private set; }

        private string horizontalAxisName;
        private string verticalAxisName;

        private void Awake()
        {
            VCam = GetComponent<CinemachineVirtualCamera>();
            UnityCamera = Camera.main;
            POVComponent = VCam.GetCinemachineComponent<CinemachinePOV>();

            horizontalAxisName = POVComponent.m_HorizontalAxis.m_InputAxisName;
            verticalAxisName = POVComponent.m_VerticalAxis.m_InputAxisName;

            // Disable POV Module or we might start partially in the wrong state.
            TogglePOV(false);
        }

        public void TogglePOV(bool enabled)
        {
            if (enabled)
            {
                POVComponent.m_HorizontalAxis.m_DecelTime = Deceleration;
                POVComponent.m_HorizontalAxis.m_InputAxisName = horizontalAxisName;
                POVComponent.m_VerticalAxis.m_InputAxisName = verticalAxisName;
                POVComponent.m_VerticalAxis.m_DecelTime = Deceleration;
            }
            else
            {
                DisableAxis(ref POVComponent.m_HorizontalAxis);
                DisableAxis(ref POVComponent.m_VerticalAxis);
            }
        }

        private void DisableAxis(ref AxisState axis)
        {
            axis.m_DecelTime = 0;
            axis.m_InputAxisValue = 0;
            axis.m_InputAxisName = null;
        }
    }
}