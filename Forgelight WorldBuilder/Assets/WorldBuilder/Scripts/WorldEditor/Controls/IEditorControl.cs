namespace WorldBuilder.WorldEditor
{
    public interface IEditorControl
    {
        /// <summary>
        /// Should return true when the editor can operate under this mode.
        /// </summary>
        bool ValidateInput();
        /// <summary>
        /// Called when the editor enters this control mode.
        /// </summary>
        void OnModeEnter();
        /// <summary>
        /// Called when the editor leaves this control mode.
        /// </summary>
        void OnModeExit();

        /// <summary>
        /// Called every update while this control is valid.
        /// </summary>
        void Tick();

    }
}