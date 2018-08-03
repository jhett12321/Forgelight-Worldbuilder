namespace WorldBuilder.Formats.Xml.Adr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UnityEngine;
    using Utils.Pools;
    using Xml;

    /// <summary>
    /// Represents the Actor Definitions XML File.
    /// </summary>
    public class Adr : XmlAsset, IPoolDisposable
    {
        public string Base { get; private set; }
        public string MaterialType { get; private set; }

        public List<Lod> Lods { get; } = new List<Lod>();

        public bool IsPlaceable { get; private set; }

        public override bool HandleXMLData(List<XElement> elements)
        {
            // ADR's only have 1 root element.
            XElement root = elements.FirstOrDefault();
            if (root == null)
            {
                return false;
            }

            IsPlaceable = true;

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

                        XAttribute distance = lodElement.Attribute("distance");
                        if(distance != null)
                        {
                            lod.Distance = Convert.ToInt32(distance.Value);
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

        public void Dispose(AssetManager assetManager)
        {
            Lods.Clear();
        }
    }
}
