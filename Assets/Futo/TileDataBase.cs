using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDataBase")]
public class TileDataBase : ScriptableObject
{
    [SerializeField] List<TileData> _tiles = new List<TileData>();

    Dictionary<int, TileData> _tileDictionary;
    
    void Initialize()
    {
        if(_tileDictionary == null)
        {
            _tileDictionary = new Dictionary<int, TileData>();
            foreach(var tile in _tiles)
            {
                if(!_tileDictionary.ContainsKey(tile.TileID))
                {
                    _tileDictionary.Add(tile.TileID, tile);
                }
                else
                {
                    Debug.LogWarning($"重複したキーがあります:{tile.TileID}");
                }
            }
        }
    }

    public TileData GetTileData(int ID)
    {
        Initialize();
        if(_tileDictionary.TryGetValue(ID, out var tileData))
        {
            return tileData;
        }
        Debug.LogWarning($"ID{ID}のタイルが見つかりません");
        return null;
    }
}