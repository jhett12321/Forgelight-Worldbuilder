namespace WorldBuilder.Zone
{
    using System.IO;
    using System.Threading.Tasks;
    using Formats.Pack;
    using Formats.Zone;
    using Objects;
    using Sky;
    using Terrain;
    using UnityEngine;
    using Utils;
    using WorldEditor;
    using Zenject;
    using Object = Formats.Zone.Object;

    /// <summary>
    /// Directs individual factories and systems to replicate zone data (Actors, Terrain).
    /// </summary>
    public class ZoneFactory
    {
        [Inject] private AssetManager assetManager;
        [Inject] private ActorFactory actorFactory;
        [Inject] private TerrainFactory terrainFactory;
        [Inject] private StatusReporter statusReporter;
        [Inject] private SkyFactory skyFactory;

        private GameObject zoneObjects;
        private GameObject zoneTerrain;

        public string LoadedZoneName { get; private set; }
        public bool ZoneLoaded { get; private set; }

        public void LoadZoneFromPacks(AssetRef zone) => LoadZone(assetManager.CreateAsset<Zone>(zone));
        public void LoadZoneFromFile(string filePath) => LoadZone(assetManager.LoadExternalAsset<Zone>(filePath));

        public async void LoadZone(Zone zone)
        {
            ZoneLoaded = false;
            if (zone == null)
            {
                return;
            }

            if (zoneObjects != null)
            {
                GameObject.Destroy(zoneObjects);
            }
            if (zoneTerrain != null)
            {
                GameObject.Destroy(zoneTerrain);
            }

            zoneObjects = new GameObject(zone.Name + " Objects");
            zoneTerrain = new GameObject(zone.Name + " Terrain");

            // Hide until everything has loaded.
            zoneTerrain.SetActive(false);
            zoneObjects.SetActive(false);

            Task zoneObjTask = LoadZoneObjects(zone);
            Task zoneTerrainTask = LoadZoneTerrain(zone);

            await zoneObjTask;
            await zoneTerrainTask;

            LoadZoneSky(zone);

            // Show the objects now after loading
            zoneObjects.SetActive(true);
            zoneTerrain.SetActive(true);

            LoadedZoneName = zone.Name;
            assetManager.Dispose(zone);

            ZoneLoaded = true;
        }

        private async Task LoadZoneObjects(Zone zone)
        {
            Task[] objTasks = new Task[zone.Objects.Count];

            for (int i = 0; i < zone.Objects.Count; i++)
            {
                Object zoneObject = zone.Objects[i];
                objTasks[i] = CreateActorInstances(zoneObject);

                statusReporter.ReportProgress("Loading Zone Objects", i, zone.Objects.Count);
                await new WaitForUpdate();
            }

            foreach (Task objTask in objTasks)
            {
                await objTask;
            }

            statusReporter.ReportProgress("Loading Zone Objects", zone.Objects.Count, zone.Objects.Count);
            zoneObjects.transform.localScale = new Vector3(-1, 1, 1);
        }

        public async Task<ForgelightActor[]> CreateActorInstances(Object objectDef)
        {
            ForgelightActor[] actors = new ForgelightActor[objectDef.Instances.Count];

            for (int i = 0; i < objectDef.Instances.Count; i++)
            {
                actors[i] = await actorFactory.CreateActor(objectDef.ActorDefinition, zoneObjects.transform);
                PrepareInstance(actors[i], objectDef.Instances[i]);
            }

            return actors;
        }

        private void PrepareInstance(ForgelightActor actor, Object.Instance data)
        {
            actor.transform.SetParent(zoneObjects.transform);

            TransformData convertedTransform = MathUtils.ConvertTransform(data.Position, data.Rotation, data.Scale, true, TransformMode.Standard);

            actor.transform.SetPositionAndRotation(convertedTransform.Position, Quaternion.Euler(convertedTransform.Rotation));
            actor.transform.localScale = convertedTransform.Scale;

            actor.ID = data.ID;
            actor.DontCastShadows = data.DontCastShadows;
            actor.LODMultiplier = data.LODMultiplier;
        }

        private async Task LoadZoneTerrain(Zone zone)
        {
            AssetRef[] assets = assetManager.GetAssetsByType(AssetType.CNK1, Path.GetFileNameWithoutExtension(zone.Name));

            if (assets == null || assets.Length == 0)
            {
                Debug.LogWarningFormat("Could not locate any chunk data associated with zone \"{0}\". Skipping load of terrain.", zone);
                return;
            }

            Task<ForgelightChunk>[] chunkTasks = new Task<ForgelightChunk>[assets.Length];
            for (int i = 0; i < assets.Length; i++)
            {
                AssetRef assetRef = assets[i];
                chunkTasks[i] = terrainFactory.CreateChunk(assetRef, zoneTerrain.transform);
                statusReporter.ReportProgress("Loading Terrain Chunks", i, assets.Length);

                await new WaitForUpdate();
            }

            foreach (Task<ForgelightChunk> task in chunkTasks)
            {
                await task;
            }

            statusReporter.ReportProgress("Loading Terrain Chunks", assets.Length, assets.Length);
            zoneTerrain.transform.localScale = new Vector3(-2, 2, 2);
        }

        private void LoadZoneSky(Zone zone)
        {
            skyFactory.UpdateSky(Path.GetFileNameWithoutExtension(zone.Name));
        }
    }
}