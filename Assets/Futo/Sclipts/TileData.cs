using UnityEngine;

[System.Serializable]
public class TileData
{
    [SerializeField] int _tileID;
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private EffectBase _effect;

    public int TileID => _tileID;
    public string Name => _name;
    public string Description => _description;
    public int Cost => _cost;
    public EffectBase Effect => _effect;
}