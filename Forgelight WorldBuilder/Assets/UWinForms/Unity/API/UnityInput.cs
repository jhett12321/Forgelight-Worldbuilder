namespace UWinForms.Unity.API
{
    using Core.API;

    public class UnityInput : IApiInput
    {
        public bool CursorVisible
        {
            get { return UnityEngine.Cursor.visible; }
            set { UnityEngine.Cursor.visible = value; }
        }
    }
}