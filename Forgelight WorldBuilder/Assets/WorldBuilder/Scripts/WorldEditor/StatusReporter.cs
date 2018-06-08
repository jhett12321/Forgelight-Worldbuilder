namespace WorldBuilder.WorldEditor
{
    using UnityEngine;
    using UnityEngine.UI;

    public class StatusReporter : MonoBehaviour
    {
        public Text statusBarText;

        public async void ReportProgress(string taskName, int processed, int total)
        {
            await new WaitForUpdate();
            int progress = Mathf.RoundToInt((float)processed / total * 100);

            if (progress == 100)
            {
                gameObject.SetActive(false);
            }
            else
            {
                if (!gameObject.activeSelf)
                {
                    gameObject.SetActive(true);
                }

                statusBarText.text = $"({progress}% complete) {taskName} - {processed}/{total}";
            }
        }
    }
}