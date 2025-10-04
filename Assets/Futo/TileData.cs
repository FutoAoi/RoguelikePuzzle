using UnityEngine;
[System.Serializable]
public class TileData : ITile
{
    [SerializeField] int _tileID;
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] int _cost;
    [SerializeField] EffectBase _effect;

    public int TileID => _tileID;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public EffectBase Effect => _effect;
}