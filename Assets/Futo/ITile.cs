public interface ITile
{
    int TileID { get; }
    string Name { get; }
    string Description { get; }
    int Cost { get; }
    EffectBase Effect { get; }
}