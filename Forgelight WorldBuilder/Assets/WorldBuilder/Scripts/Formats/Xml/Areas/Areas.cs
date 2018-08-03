namespace WorldBuilder.Formats.Xml.Areas
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UnityEngine;
    using Xml;

    public class Areas : XmlAsset
    {
        public List<AreaDefinition> AreaDefinitions = new List<AreaDefinition>();

        public override bool HandleXMLData(List<XElement> elements)
        {
            foreach (XElement areaDefTag in elements)
            {
                AreaDefinition areaDefinition = new AreaDefinition();

                // Common
                areaDefinition.ID = areaDefTag.Attribute("id").Value;
                areaDefinition.Name = areaDefTag.Attribute("name").Value;
                areaDefinition.Shape = areaDefTag.Attribute("shape").Value;
                areaDefinition.Pos1 = new Vector3(float.Parse(areaDefTag.Attribute("x1").Value), float.Parse(areaDefTag.Attribute("y1").Value), float.Parse(areaDefTag.Attribute("z1").Value));

                // Shapes
                switch (areaDefinition.Shape)
                {
                    case "sphere":
                        areaDefinition.Radius = float.Parse(areaDefTag.Attribute("radius").Value);
                        break;
                    case "box":
                        areaDefinition.Pos2 = new Vector3(float.Parse(areaDefTag.Attribute("x2").Value), float.Parse(areaDefTag.Attribute("y2").Value), float.Parse(areaDefTag.Attribute("z2").Value));
                        areaDefinition.Rot = new Vector3(float.Parse(areaDefTag.Attribute("rotX").Value), float.Parse(areaDefTag.Attribute("rotY").Value), float.Parse(areaDefTag.Attribute("rotZ").Value));
                        break;
                    default:
                        Debug.LogWarning("Unknown Shape (PROBABLY A DOME): " + areaDefinition.Shape);
                        continue;
                }

                if (areaDefTag.HasElements)
                {
                    areaDefinition.Properties = new List<Property>();

                    // Properties
                    foreach (XElement childProperty in areaDefTag.Elements())
                    {
                        Property property = new Property();

                        //Common
                        property.ID = childProperty.Attribute("id").Value;
                        property.Type = childProperty.Attribute("type").Value;

                        property.Parameters = childProperty;

                        areaDefinition.Properties.Add(property);
                    }
                }

                AreaDefinitions.Add(areaDefinition);
            }

            return true;
        }
    }
}
