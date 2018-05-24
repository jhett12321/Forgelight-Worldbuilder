namespace WorldBuilder.Formats.Adr
{
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Linq;
    using Syroot.BinaryData;
    using UnityEngine;

    /// <summary>
    /// Represents the Actor Definitions XML File.
    /// </summary>
    public class Adr : IReadableAsset
    {
        public ByteConverter ByteConverter => ByteConverter.Little;
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public string Base { get; private set; }
        public string MaterialType { get; private set; }

        public List<Lod> Lods { get; private set; }

        public bool IsPlaceable { get; private set; }

        public bool Deserialize(BinaryStream stream)
        {
            XmlReaderSettings settings = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment
            };

            XElement root;

            try
            {
                using (XmlReader xr = XmlReader.Create(stream, settings))
                {
                    if (!xr.Read())
                    {
                        return false;
                    }

                    root = XElement.Load(xr.ReadSubtree());
                }
            }
            catch
            {
                return false;
            }

            IsPlaceable = true;
            Lods = new List<Lod>();

            foreach (XElement child in root.Elements())
            {
                if (child.Name == "Base")
                {
                    XAttribute attribute = child.Attribute("fileName");

                    if (attribute == null)
                    {
                        Debug.LogWarning("Actor " + Name + " has an invalid Base definition. This actor may not display correctly.");
                        continue;
                    }

                    Base = attribute.Value;
                }

                else if (child.Name == "Lods")
                {
                    foreach (XElement lodElement in child.Elements())
                    {
                        Lod lod = new Lod();
                        //lod.Distance = Convert.ToInt32(lodElement.Attribute("distance").Value);

                        XAttribute fileName = lodElement.Attribute("fileName");
                        if (fileName != null)
                        {
                            lod.FileName = fileName.Value;
                        }

                        XAttribute paletteName = lodElement.Attribute("paletteName");
                        if (paletteName != null)
                        {
                            lod.PaletteName = paletteName.Value;
                        }

                        Lods.Add(lod);
                    }
                }

                else if (child.Name == "Usage")
                {
                    if (child.Attribute("actorUsage").Value != "0" || child.Attribute("borrowSkeleton").Value != "0" && child.Attribute("validatePcNpc").Value != "0")
                    {
                        IsPlaceable = false;
                    }
                }

                else if (child.Name == "MaterialType")
                {
                    MaterialType = child.Attribute("type").Value;
                }
            }

            return true;
        }
    }
}
