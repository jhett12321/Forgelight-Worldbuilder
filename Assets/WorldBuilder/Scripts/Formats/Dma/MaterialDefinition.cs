namespace WorldBuilder.Formats.Dma
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Utils.Cryptography;

    public class MaterialDefinition
    {
        #region Structure
        public string Name { get; private set; }
        public uint NameHash { get; private set; }
        public string Type { get; private set; }
        public uint TypeHash { get; private set; }
        public List<DrawStyle> DrawStyles { get; private set; }
        #endregion

        private MaterialDefinition()
        {
            Name = string.Empty;
            NameHash = 0;
            Type = string.Empty;
            TypeHash = 0;
            DrawStyles = new List<DrawStyle>();
        }

        public static MaterialDefinition LoadFromXPathNavigator(XPathNavigator navigator)
        {
            if (navigator == null)
            {
                return null;
            }

            MaterialDefinition materialDefinition = new MaterialDefinition();

            // Name
            materialDefinition.Name = navigator.GetAttribute("Name", string.Empty);
            materialDefinition.NameHash = Jenkins.OneAtATime(materialDefinition.Name);

            // Type
            materialDefinition.Type = navigator.GetAttribute("Type", string.Empty);
            materialDefinition.TypeHash = Jenkins.OneAtATime(materialDefinition.Type);

            // Draw styles
            XPathNodeIterator entries = navigator.Select("./Array[@Name='DrawStyles']/Object[@Class='DrawStyle']");

            while (entries.MoveNext())
            {
                DrawStyle drawStyle = DrawStyle.LoadFromXPathNavigator(entries.Current);

                if (drawStyle != null)
                {
                    materialDefinition.DrawStyles.Add(drawStyle);
                }
            }

            return materialDefinition;
        }

        public override string ToString() => Name;
    }
}