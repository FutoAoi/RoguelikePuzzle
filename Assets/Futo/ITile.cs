public interface ITile
{
    string Name { get; }
    string Description { get; }
    int Cost { get; }
    EffectBase Effect { get; }
}