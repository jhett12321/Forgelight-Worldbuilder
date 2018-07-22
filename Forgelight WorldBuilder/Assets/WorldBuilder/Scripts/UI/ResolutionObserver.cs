namespace WorldBuilder.UI
{
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Generates events for changes in screen resolution.
    /// </summary>
    public class ResolutionObserver : ITickable
    {
        private Resolution lastRes;

        public event ResolutionChangeEvent OnResolutionChanged;
        public delegate void ResolutionChangeEvent(Resolution lastRes, Resolution newRes);

        public void Tick()
        {
            Resolution currentRes = UnityEngine.Screen.currentResolution;
            if (currentRes.width == lastRes.width && currentRes.height == lastRes.height)
            {
                return;
            }

            OnResolutionChanged?.Invoke(lastRes, currentRes);
            lastRes = currentRes;
        }
    }
}