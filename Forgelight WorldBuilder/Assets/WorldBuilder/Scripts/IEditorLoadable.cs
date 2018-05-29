namespace WorldBuilder
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a system that must finish loading before the editor can allow interactions.
    /// </summary>
    public interface IEditorLoadable
    {
        string TaskName { get; }
        Task LoadSystem(IProgress<int> progress);
    }
}