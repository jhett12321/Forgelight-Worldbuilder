namespace WorldBuilder.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Formats.Adr;
    using Formats.Pack;
    using Zenject;

    public class ActorDefinitionManager : IEditorLoadable
    {
        [Inject] private AssetManager assetManager;

        private readonly Dictionary<string, Adr> actorDefinitions = new Dictionary<string, Adr>();

        public string TaskName => "Loading Actor Definitions";
        public Task LoadSystem(IProgress<int> progress)
        {
            actorDefinitions.Clear();

            return Task.Run(() =>
            {
                AssetRef[] actorDefs = assetManager.GetAssetsByType(AssetType.ADR);

                for (int i = 0; i < actorDefs.Length; i++)
                {
                    AssetRef assetRef = actorDefs[i];
                    Adr adr = assetManager.CreateAsset<Adr>(assetRef);
                    if (adr != null)
                    {
                        actorDefinitions.Add(adr.Name, adr);
                    }

                    progress.Report(i * 100 / actorDefs.Length);
                }
            });
        }

        public Adr GetDefinition(string name)
        {
            Adr retVal;
            actorDefinitions.TryGetValue(name, out retVal);

            return retVal;
        }
    }
}