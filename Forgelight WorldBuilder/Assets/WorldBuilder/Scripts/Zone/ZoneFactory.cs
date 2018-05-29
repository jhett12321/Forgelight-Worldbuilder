namespace WorldBuilder.Zone
{
    using Objects;
    using Terrain;
    using Zenject;

    public class ZoneFactory
    {
        [Inject] private ActorFactory actorFactory;
        [Inject] private TerrainFactory terrainFactory;
    }
}