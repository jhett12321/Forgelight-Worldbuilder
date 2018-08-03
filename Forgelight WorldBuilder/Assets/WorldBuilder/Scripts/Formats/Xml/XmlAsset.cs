namespace WorldBuilder.Formats.Xml
{
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Linq;
    using Syroot.BinaryData;

    /// <summary>
    /// Represents an XML asset.
    /// </summary>
    public abstract class XmlAsset : IReadableAsset
    {
        public ByteConverter ByteConverter => ByteConverter.Little;
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public bool Deserialize(BinaryStream stream, AssetManager assetManager)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment
            };

            List<XElement> elements = new List<XElement>();

            using (XmlReader xr = XmlReader.Create(stream, settings))
            {
                while (xr.Read())
                {
                    if (xr.NodeType == XmlNodeType.Element)
                    {
                        elements.Add(XElement.Load(xr.ReadSubtree()));
                    }
                }
            }

            return HandleXMLData(elements);
        }

        public abstract bool HandleXMLData(List<XElement> elements);
    }
}