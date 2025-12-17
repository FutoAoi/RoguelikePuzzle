using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    [SerializeField] private Room _roomPrefab;
    [SerializeField] private RectTransform _linePrefab;
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

                RectTransform rt = room.GetComponent<RectTransform>();

                rt.anchoredPosition = GetRoomPosition(f, r, roomCount);

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
                GenerateRoomData data = mapData.Floors[f].Rooms[r];

                foreach (int next in data.NextRoomIndices)
                {
                    DrawUILine(
                        _roomViews[f][r].GetComponent<RectTransform>(),
                        _roomViews[f + 1][next].GetComponent<RectTransform>()
                    );
                }
            }
        }
    }

    // ---------- UI用ライン描画 ----------
    private void DrawUILine(RectTransform from, RectTransform to)
    {
        RectTransform line = Instantiate(_linePrefab, transform);

        Vector2 start = from.anchoredPosition;
        Vector2 end = to.anchoredPosition;

        Vector2 dir = end - start;
        float distance = dir.magnitude;

        // 位置：中点
        line.anchoredPosition = start + dir * 0.5f;

        // 長さ
        line.sizeDelta = new Vector2(
            line.sizeDelta.x,
            distance
        );

        // 回転
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        line.localRotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private Vector3 GetRoomPosition(int floorIndex, int roomIndex, int roomCount)
    {
        float xSpacing = 200f;
        float ySpacing = 200f;

        float offsetX = -(roomCount - 1) * xSpacing * 0.5f;
        float offsetY = -700f;

        float x = offsetX + roomIndex * xSpacing;
        float y = offsetY + floorIndex * ySpacing;

        return new Vector3(x, y, 0f);
    }
}
