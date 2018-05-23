namespace WorldBuilder.Formats.Dma
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.XPath;
    using Dme;

    /// <summary>
    /// Represents the materials_3.xml file.
    /// </summary>
    public class MaterialDefinitions : IReadableAsset
    {
        public Dictionary<uint, MaterialDefinition> Materials { get; } = new Dictionary<uint, MaterialDefinition>();
        public Dictionary<uint, VertexLayout> VertexLayouts { get; } = new Dictionary<uint, VertexLayout>();

        public string Name { get; set; }
        public string DisplayName { get; set; }

        public bool Deserialize(Stream stream)
        {
            using (StreamReader streamReader = new StreamReader(stream))
            {
                string xmlDoc = streamReader.ReadToEnd();

                using (StringReader stringReader = new StringReader(xmlDoc))
                {
                    LoadFromStringReader(stringReader);
                }
            }

            return true;
        }

        private void LoadFromStringReader(TextReader stringReader)
        {
            if (stringReader == null)
            {
                return;
            }

            XPathDocument document;

            try
            {
                document = new XPathDocument(stringReader);
            }
            catch (Exception)
            {
                return;
            }

            XPathNavigator navigator = document.CreateNavigator();

            // vertex layouts
            LoadVertexLayoutsByXPathNavigator(navigator.Clone());

            // TODO: parameter groups

            // material definitions
            LoadMaterialDefinitionsByXPathNavigator(navigator.Clone());
        }

        private void LoadMaterialDefinitionsByXPathNavigator(XPathNavigator navigator)
        {
            XPathNodeIterator materialDefinitions;

            try
            {
                materialDefinitions = navigator.Select("/Object/Array[@Name='MaterialDefinitions']/Object[@Class='MaterialDefinition']");
            }
            catch (Exception)
            {
                return;
            }

            while (materialDefinitions.MoveNext())
            {
                MaterialDefinition materialDefinition = MaterialDefinition.LoadFromXPathNavigator(materialDefinitions.Current);

                if (materialDefinition != null && false == Materials.ContainsKey(materialDefinition.NameHash))
                {
                    Materials.Add(materialDefinition.NameHash, materialDefinition);
                }
            }
        }

        private void LoadVertexLayoutsByXPathNavigator(XPathNavigator navigator)
        {
            // material definitions
            XPathNodeIterator vertexLayouts;

            try
            {
                vertexLayouts = navigator.Select("/Object/Array[@Name='InputLayouts']/Object[@Class='InputLayout']");
            }
            catch (Exception)
            {
                return;
            }

            while (vertexLayouts.MoveNext())
            {
                VertexLayout vertexLayout = VertexLayout.LoadFromXPathNavigator(vertexLayouts.Current);

                if (vertexLayout != null && false == VertexLayouts.ContainsKey(vertexLayout.NameHash))
                {
                    VertexLayouts.Add(vertexLayout.NameHash, vertexLayout);
                }
            }
        }
    }
}