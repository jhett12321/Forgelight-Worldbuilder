namespace UWinForms.Unity
{
    using Examples;
    using UnityEngine;

    public class UnitWinFormsExamples : MonoBehaviour
    {
        private void Start()
        {
            var form = new FormExamples();

            form.Show();
        }
    }
}