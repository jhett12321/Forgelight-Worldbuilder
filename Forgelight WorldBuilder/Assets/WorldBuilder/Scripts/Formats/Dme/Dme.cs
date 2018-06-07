namespace WorldBuilder.Formats.Dme
{
    using System;
    using System.Collections.Generic;
    using Dma;
    using Syroot.BinaryData;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Utils;
    using Utils.Cryptography;
    using Utils.Pools;
    using Material = Dma.Material;

    /// <summary>
    /// Represents a Forgelight Model (DME) file.
    /// </summary>
    public class Dme : IReadableAsset, IPoolResetable
    {
        public ByteConverter ByteConverter => ByteConverter.Little;

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ModelType ModelType { get; private set; }

        private enum TextureType
        {
            Invalid,
            Diffuse,
            Bump,
            Spec
        }

        #region Structure
        // Header
        public const string MAGIC = "DMOD";
        public uint Version { get; private set; }
        public uint ModelHeaderOffset { get; private set; }

        // DMA
        public List<string> TextureStrings { get; private set; }
        public List<Material> Materials { get; private set; }

        // Bounding Box
        public Vector3 Min { get; private set; }
        public Vector3 Max { get; private set; }

        // Meshes
        public List<Mesh> Meshes { get; private set; }

        // Bone Maps
        public List<BoneMap> BoneMaps { get; private set; }

        // Bone Map Entries
        public List<BoneMapEntry> BoneMapEntries { get; private set; }
        #endregion

        public Dme()
        {
            TextureStrings = new List<string>();
            Materials = new List<Material>();
            Meshes = new List<Mesh>();
            BoneMaps = new List<BoneMap>();
            BoneMapEntries = new List<BoneMapEntry>();
        }

        public void Reset()
        {
            TextureStrings.Clear();
            Materials.Clear();
            Meshes.Clear();
            BoneMaps.Clear();
            BoneMapEntries.Clear();
        }

        public bool Deserialize(BinaryStream stream)
        {
            // Header
            string magic = stream.ReadString(4);
            Assert.AreEqual(MAGIC, magic, "Model File header does not match magic value!");

            Version = stream.ReadUInt32();

            if (!Enum.IsDefined(typeof(ModelType), (int)Version))
            {
                Debug.LogWarning("Could not decode model " + Name + ". Unknown DME version " + Version);
                return false;
            }

            ModelType = (ModelType)Version;

            ModelHeaderOffset = stream.ReadUInt32();

            // DMA
            Dma.LoadFromStream(stream, TextureStrings, Materials);

            // Bounding Box
            Min = stream.ReadVector3();
            Max = stream.ReadVector3();

            // Meshes
            uint meshCount = stream.ReadUInt32();

            for (int i = 0; i < meshCount; ++i)
            {
                Mesh mesh = Mesh.LoadFromStream(stream, Materials);

                if (mesh == null)
                {
                    continue;
                }

                Material material = Materials[(int) mesh.MaterialIndex];
                foreach (Material.Parameter parameter in material.Parameters)
                {
                    LookupTextures(mesh, parameter, TextureStrings);

                    if (mesh.BaseDiffuse != null && mesh.BumpMap != null && mesh.SpecMap != null)
                    {
                        break;
                    }
                }

                Meshes.Add(mesh);
            }

            // Bone Maps
            uint boneMapCount = stream.ReadUInt32();

            for (int i = 0; i < boneMapCount; ++i)
            {
                BoneMap boneMap = BoneMap.LoadFromStream(stream);

                if (boneMap != null)
                {
                    BoneMaps.Add(boneMap);
                }
            }

            // Bone Map Entries
            uint boneMapEntryCount = stream.ReadUInt32();

            for (int i = 0; i < boneMapEntryCount; ++i)
            {
                BoneMapEntry boneMapEntry = BoneMapEntry.LoadFromStream(stream);

                if (boneMapEntry != null)
                {
                    BoneMapEntries.Add(boneMapEntry);
                }
            }

            return true;
        }

        /// <summary>
        /// Finds the correct diffuse, specular and packed normal maps for the given mesh.
        /// </summary>
        /// <param name="mesh">The origin mesh.</param>
        /// <param name="parameter">The material parameter containing the hashed texture name, or some other parameter.</param>
        /// <param name="textureStrings">A list of available textures for this mesh.</param>
        private static void LookupTextures(Mesh mesh, Material.Parameter parameter, List<string> textureStrings)
        {
            if (parameter.Data.Length != 4 && parameter.Type != Material.Parameter.D3DXParameterType.Texture || parameter.Class != Material.Parameter.D3DXParameterClass.Object)
            {
                return;
            }

            TextureType textureType = GetTextureType(parameter.NameHash);

            if (textureType == TextureType.Invalid)
            {
                return;
            }

            uint textureHash = BitConverter.ToUInt32(parameter.Data, 0);

            foreach (string textureString in textureStrings)
            {
                if (Jenkins.OneAtATime(textureString.ToUpper()) == textureHash)
                {
                    switch (textureType)
                    {
                        case TextureType.Diffuse:
                            mesh.BaseDiffuse = textureString;
                            break;
                        case TextureType.Bump:
                            mesh.BumpMap = textureString;
                            break;
                        case TextureType.Spec:
                            mesh.SpecMap = textureString;
                            break;
                    }

                    return;
                }
            }
        }

        private static TextureType GetTextureType(uint parameterHash)
        {
            uint baseDiffuseHash = Jenkins.OneAtATime("baseDiffuse");
            uint BaseDiffuseHash = Jenkins.OneAtATime("BaseDiffuse");
            uint basediffuseHash = Jenkins.OneAtATime("basediffuse");

            uint SpecHash = Jenkins.OneAtATime("Spec");
            uint specHash = Jenkins.OneAtATime("spec");

            uint BumpHash = Jenkins.OneAtATime("Bump");
            uint BumpMapHash = Jenkins.OneAtATime("BumpMap");

            if (parameterHash == baseDiffuseHash || parameterHash == BaseDiffuseHash || parameterHash == basediffuseHash)
            {
                return TextureType.Diffuse;
            }

            if (parameterHash == SpecHash || parameterHash == specHash)
            {
                return TextureType.Spec;
            }

            if (parameterHash == BumpHash || parameterHash == BumpMapHash)
            {
                return TextureType.Bump;
            }

            return TextureType.Invalid;
        }
    }
}
