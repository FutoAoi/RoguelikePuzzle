using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private Room _roomPrefab;
    [SerializeField] private LineRenderer _linePrefab;
    [SerializeField] private MapManager _mapManager;

    private Room[][] _roomViews;

    public void CreateMap(GenerateMapData mapData)
    {
        int floorCount = mapData.Floors.Length;
        _roomViews = new Room[floorCount][];

        for (int f  = 0; f < floorCount; f++)
        {
            GenerateFloorData floorData = mapData.Floors[f];
            int roomCount = floorData.Rooms.Length;
            _roomViews[f] = new Room[roomCount];
            for (int r  = 0; r < roomCount; r++)
            {
                Room room = Instantiate(_roomPrefab,transform);
                room.SetRoomData(floorData.Rooms[r],_mapManager);

                room.transform.localPosition = GetRoomPosition(f, r, roomCount);

                _roomViews[f][r] = room;
            }
        }

        for (int f = 0;f < floorCount - 1; f++)
        {
            for (int r = 0; r < _roomViews[f].Length; r++)
            {
                GenerateRoomData roomData = mapData.Floors[f].Rooms[r];
                List<Room> nextRooms = new();

                foreach (int nextIndex in roomData.NextRoomIndices)
                {
                    nextRooms.Add(_roomViews[f+1][nextIndex]);
                }

                _roomViews[f][r].SetNextRoom(nextRooms.ToArray());
            }
        }

        for (int f = 0; f < floorCount - 1; f++)
        {
            for (int r = 0; r < _roomViews[f].Length; r++)
            {
                GenerateRoomData data =
                    mapData.Floors[f].Rooms[r];

                foreach (int next in data.NextRoomIndices)
                {
                    DrawLine(
                        _roomViews[f][r].transform,
                        _roomViews[f + 1][next].transform
                    );
                }
            }
        }
    }

    private void DrawLine(Transform from, Transform to)
    {
        LineRenderer line = Instantiate(_linePrefab, transform);

        line.positionCount = 2;
        line.SetPosition(0, from.position);
        line.SetPosition(1, to.position);
    }

    private Vector3 GetRoomPosition(int floorIndex, int roomIndex, int roomCount)
    {
        float xSpacing = 200f;
        float ySpacing = 200f;

        float offsetX = -(roomCount - 1) * xSpacing * 2f;

        float x = offsetX + roomIndex * xSpacing;
        float y = floorIndex * ySpacing;

        return new Vector3(x, y, 0f);
    }
}
