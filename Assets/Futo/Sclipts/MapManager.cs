using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private int _mapHeight;
    [SerializeField] private MapData _mapData;
    [SerializeField] private Room _room;
    [SerializeField] List<List<Room>> _rooms = new();

    [SerializeField] private int _seed;

    private void Start()
    {
        _mapHeight = _mapData.FloorDatas.Length;
        MapCreate();
    }

    public void MapCreate()
    {
        for (int i = 0; i < _mapHeight; i++)
        {
            List<Room> roomsH = new();
            FloorData floorData = _mapData.FloorDatas[i];
            for(int j =  0; j < Random.Range(floorData.MinNodes,floorData.MaxNodes + 1); j++)
            {
                Room room = Instantiate(_room, transform.position + Vector3.up * i * 100 + Vector3.right * j * 100, Quaternion.identity, this.transform);
                room.SetRoomData(floorData.RoomDatas[Random.Range(0, floorData.RoomDatas.Length)]);
                roomsH.Add(room);
            }
            _rooms.Add(roomsH);
        }
    }
}
