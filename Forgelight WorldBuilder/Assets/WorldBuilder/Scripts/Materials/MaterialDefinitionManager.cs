namespace WorldBuilder.Materials
{
    using System.Linq;
    using Formats.Dma;
    using Formats.Dme;
    using Zenject;

    /// <summary>
    /// Provides lookup utilities for MaterialDefinitions and VertexLayouts.
    /// </summary>
    public class MaterialDefinitionManager : IInitializable
    {
        // Dependencies
        [Inject] private AssetManager assetManager;

        public const string MATERIALS_ASSET = "materials_3.xml";

        private MaterialDefinitions materialDefinitions { get; set; }

        public void Initialize()
        {
            materialDefinitions = assetManager.LoadPackAsset<MaterialDefinitions>(MATERIALS_ASSET);
        }

        /// <summary>
        /// Finds a material with the given name hash.
        /// </summary>
        /// <param name="materialNameHash">A material definition name hash.</param>
        /// <returns>The matching MaterialDefinition, otherwise null.</returns>
        public MaterialDefinition GetMaterial(uint materialNameHash)
        {
            MaterialDefinition materialDef;
            if (materialDefinitions.Materials.TryGetValue(materialNameHash, out materialDef))
            {
                return materialDef;
            }

            return null;
        }

        /// <summary>
        /// Finds a material with the given name.
        /// </summary>
        /// <param name="materialName">The material's name.</param>
        /// <returns>The matching MaterialDefinition, otherwise null.</returns>
        public MaterialDefinition GetMaterial(string materialName)
        {
            return materialDefinitions.Materials.Values.FirstOrDefault(entry => entry.Name == materialName);
        }

        /// <summary>
        /// Finds a vertex layout with the given name hash.
        /// </summary>
        /// <param name="vertexLayoutNameHash">The vertex layout's name hash.</param>
        /// <returns>The matching VertexLayout, otherwise null.</returns>
        public VertexLayout GetVertexLayout(uint vertexLayoutNameHash)
        {
            VertexLayout vertexLayout;
            if (materialDefinitions.VertexLayouts.TryGetValue(vertexLayoutNameHash, out vertexLayout))
            {
                return vertexLayout;
            }

            return null;
        }

        /// <summary>
        /// Finds a vertex layout with the given name.
        /// </summary>
        /// <param name="vertexLayoutName">The vertex layout's name.</param>
        /// <returns>The matching VertexLayout, otherwise null.</returns>
        public VertexLayout GetVertexLayout(string vertexLayoutName)
        {
            return materialDefinitions.VertexLayouts.Values.FirstOrDefault(entry => entry.Name == vertexLayoutName);
        }
    }
}