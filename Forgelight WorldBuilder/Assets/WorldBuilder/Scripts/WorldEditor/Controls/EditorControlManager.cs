using UnityEngine;

namespace WorldBuilder.WorldEditor
{
    using System.Collections.Generic;
    using Zenject;

    /// <summary>
    /// Handles input controls for the editor view.
    /// </summary>
    [RequireComponent(typeof(GameObjectContext))]
    public class EditorControlManager : MonoBehaviour
    {
        [Inject] private List<IEditorControl> editorControls;
        private IEditorControl currentControl;

        private void Update()
        {
            UpdateCurrentMode();
            currentControl?.Tick();
        }

        /// <summary>
        /// Updates the current editor mode based on input conditions set by the Editor control's implementation.
        /// </summary>
        private void UpdateCurrentMode()
        {
            if (currentControl == null)
            {
                foreach (IEditorControl control in editorControls)
                {
                    if (!control.ValidateInput())
                    {
                        continue;
                    }

                    currentControl = control;
                    control.OnModeEnter();
                    break;
                }
            }
            else if (!currentControl.ValidateInput())
            {
                currentControl.OnModeExit();
                currentControl = null;
            }
        }
    }
}