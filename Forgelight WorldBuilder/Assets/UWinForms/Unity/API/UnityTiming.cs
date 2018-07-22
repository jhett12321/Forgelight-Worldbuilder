namespace UWinForms.Unity.API
{
    using Core.API;

    public class UnityTiming : IApiTiming
    {
        public float DeltaTime { get { return UnityEngine.Time.deltaTime; } }
    }
}
