using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileDataBase")]
public class TileDataBase : ScriptableObject
{
    [SerializeField] List<TileData> tiles = new List<TileData>();
}