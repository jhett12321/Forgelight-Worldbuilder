namespace WorldBuilder.WorldEditor
{
    using UnityEngine;
    using UnityEngine.UI;

    public class StatusReporter : MonoBehaviour
    {
        private Text statusBarText;

        public void Awake()
        {
            statusBarText = GetComponent<Text>();
        }

        public async void ReportProgress(string taskName, int processed, int total)
        {
            await new WaitForUpdate();
            int progress = Mathf.RoundToInt((float)processed / total * 100);

            if (progress == 100)
            {
                statusBarText.text = "";
            }
            else
            {
                statusBarText.text = $"({progress}% complete) {taskName} - {processed}/{total}";
            }
        }
    }
}