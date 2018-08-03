namespace WorldBuilder.Objects
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Formats.Pack;
    using Formats.Xml.Adr;
    using WorldEditor;
    using Zenject;

    public class ActorDefinitionManager : IEditorLoadable
    {
        [Inject] private AssetManager assetManager;

        private readonly Dictionary<string, Adr> actorDefinitions = new Dictionary<string, Adr>();

        public async Task LoadSystem(StatusReporter reporter)
        {
            await new WaitForBackgroundThread();
            actorDefinitions.Clear();

            AssetRef[] actorDefs = assetManager.GetAssetsByType(AssetType.ADR);

            for (int i = 0; i < actorDefs.Length; i++)
            {
                AssetRef assetRef = actorDefs[i];
                Adr adr = assetManager.CreateAsset<Adr>(assetRef);
                if (adr != null)
                {
                    actorDefinitions.Add(adr.Name, adr);
                }

                reporter.ReportProgress("Loading Actor Definitions", i, actorDefs.Length);
            }
        }

        public Adr GetDefinition(string name)
        {
            Adr retVal;
            actorDefinitions.TryGetValue(name, out retVal);

            return retVal;
        }
    }
}