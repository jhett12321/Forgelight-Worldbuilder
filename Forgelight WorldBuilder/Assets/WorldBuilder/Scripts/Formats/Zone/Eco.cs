namespace WorldBuilder.Formats.Zone
{
    using System.Collections.Generic;
    using System.IO;
    using Syroot.BinaryData;

    public class Eco
    {
        #region Structure
        public uint Index { get; private set; }
        public string Name { get; private set; }
        public string ColorNXMap { get; private set; }
        public string SpecBlendNyMap { get; private set; }
        public uint DetailRepeat { get; private set; }
        public float BlendStrength { get; private set; }
        public float SpecMin { get; private set; }
        public float SpecMax { get; private set; }
        public float SpecSmoothnessMin { get; private set; }
        public float SpecSmoothnessMax { get; private set; }
        public string PhysicsMaterial { get; private set; }
        public List<Layer> Layers { get; private set; }
        public class Layer
        {
            public class Tint
            {
                public ByteColor Color { get; set; }
                public uint Percentage { get; set; }
            }

            public float Density { get; set; }
            public float MinScale { get; set; }
            public float MaxScale { get; set; }
            public float SlopePeak { get; set; }
            public float SlopeExtent { get; set; }
            public float MinElevation { get; set; }
            public float MaxElevation { get; set; }
            public byte MinAlpha { get; set; }
            public string Flora { get; set; }
            public List<Tint> Tints { get; set; }
        }
        #endregion

        public static Eco ReadFromStream(Stream stream)
        {
            Eco eco = new Eco();

            eco.Index = stream.ReadUInt32();

            eco.Name = stream.ReadString(StringCoding.ZeroTerminated);
            eco.ColorNXMap = stream.ReadString(StringCoding.ZeroTerminated);
            eco.SpecBlendNyMap = stream.ReadString(StringCoding.ZeroTerminated);
            eco.DetailRepeat = stream.ReadUInt32();
            eco.BlendStrength = stream.ReadSingle();
            eco.SpecMin = stream.ReadSingle();
            eco.SpecMax = stream.ReadSingle();
            eco.SpecSmoothnessMin = stream.ReadSingle();
            eco.SpecSmoothnessMax = stream.ReadSingle();
            eco.PhysicsMaterial = stream.ReadString(StringCoding.ZeroTerminated);

            eco.Layers = new List<Layer>();
            uint layerCount = stream.ReadUInt32();

            for (uint i = 0; i < layerCount; i++)
            {
                Layer layer = new Layer();
                layer.Density = stream.ReadSingle();
                layer.MinScale = stream.ReadSingle();
                layer.MaxScale = stream.ReadSingle();
                layer.SlopePeak = stream.ReadSingle();
                layer.SlopeExtent = stream.ReadSingle();
                layer.MinElevation = stream.ReadSingle();
                layer.MaxElevation = stream.ReadSingle();
                layer.MinAlpha = stream.Read1Byte();
                layer.Flora = stream.ReadString(StringCoding.ZeroTerminated);

                layer.Tints = new List<Layer.Tint>();
                uint tintCount = stream.ReadUInt32();

                for (uint j = 0; j < tintCount; j++)
                {
                    Layer.Tint tint = new Layer.Tint();

                    tint.Color = stream.ReadByteColor(ColorMemberOrder.AlphaLast);
                    tint.Percentage = stream.ReadUInt32();

                    layer.Tints.Add(tint);
                }

                eco.Layers.Add(layer);
            }

            return eco;
        }

        public void WriteToStream(Stream stream)
        {
            //Eco
            stream.Write(Index);

            stream.Write(Name, StringCoding.ZeroTerminated);
            stream.Write(ColorNXMap, StringCoding.ZeroTerminated);
            stream.Write(SpecBlendNyMap, StringCoding.ZeroTerminated);
            stream.Write(DetailRepeat);
            stream.Write(BlendStrength);
            stream.Write(SpecMin);
            stream.Write(SpecMax);
            stream.Write(SpecSmoothnessMin);
            stream.Write(SpecSmoothnessMax);
            stream.Write(PhysicsMaterial, StringCoding.ZeroTerminated);

            //Layers
            stream.Write((uint) Layers.Count);

            foreach (Layer layer in Layers)
            {
                stream.Write(layer.Density);
                stream.Write(layer.MinScale);
                stream.Write(layer.MaxScale);
                stream.Write(layer.SlopePeak);
                stream.Write(layer.SlopeExtent);
                stream.Write(layer.MinElevation);
                stream.Write(layer.MaxElevation);
                stream.Write(layer.MinAlpha);
                stream.Write(layer.Flora, StringCoding.ZeroTerminated);

                //Tints
                stream.Write((uint) layer.Tints.Count);

                foreach (Layer.Tint tint in layer.Tints)
                {
                    stream.Write(tint.Color, ColorMemberOrder.AlphaLast);
                    stream.Write(tint.Percentage);
                }
            }
        }
    }
}
