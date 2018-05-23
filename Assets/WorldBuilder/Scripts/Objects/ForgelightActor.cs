namespace WorldBuilder.Objects
{
    using Formats.Adr;
    using UnityEngine;

    /// <summary>
    /// Represents a Forgelight Actor GameObject.
    /// </summary>
    public class ForgelightActor : MonoBehaviour
    {
        public Adr ActorDefinition { get; private set; }
        public MeshRenderer MeshRenderer { get; private set; }

        public void Init(Adr actorDef, MeshRenderer meshRenderer)
        {
            this.ActorDefinition = actorDef;
            this.MeshRenderer = meshRenderer;
        }
    }
}