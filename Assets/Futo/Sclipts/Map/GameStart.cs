using UnityEngine;

public class GameStart : MonoBehaviour
{
    [SerializeField] private MapData _mapData;
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private MapView _mapView;

    void Start()
    {
        GenerateMapData map = MapGenerator.GenerateMap(_mapData);
        _mapManager.SetMap(map);
        _mapView.CreateMap(map);
    }
}
