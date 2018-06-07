namespace WorldBuilder
{
    using System;
    using System.Threading.Tasks;
    using WorldEditor;

    /// <summary>
    /// Represents a system that must finish loading before the editor can allow interactions.
    /// </summary>
    public interface IEditorLoadable
    {
        Task LoadSystem(StatusReporter reporter);
    }
}