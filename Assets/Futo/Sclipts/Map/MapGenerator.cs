using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    public static GenerateMapData GenerateMap(MapData mapData)
    {
        GenerateMapData map = new();
        map.CurrentFloorIndex = 0;
        map.CurrentRoomIndex = 0;

        int floorCount = mapData.FloorDatas.Length;
        map.Floors = new GenerateFloorData[floorCount];

        for(int f = 0; f < floorCount; f++)
        {
            FloorData floorData = mapData.FloorDatas[f];

            int roomCount = Random.Range(floorData.MinNodes, floorData.MaxNodes + 1);

            GenerateFloorData floor = new();
            floor.Rooms = new GenerateRoomData[roomCount];

            for (int r = 0; r < roomCount; r++)
            {
                RoomData baseRoom = floorData.RoomDatas[Random.Range(0, floorData.RoomDatas.Length)];

                floor.Rooms[r] = new GenerateRoomData { FloorIndex = f, RoomIndex = r, StageID = baseRoom.StageID,RoomType = baseRoom.RoomType,IsCleared = false };
            }

            map.Floors[f] = floor;
        }

        for (int f = 0;f < floorCount - 1; f++)
        {
            GenerateFloorData currentFloor = map.Floors[f];
            GenerateFloorData nextFloor = map.Floors[f + 1];

            for (int r = 0;r < currentFloor.Rooms.Length; r++)
            {
                List<int> nextIndices = new();

                //‚±‚±‚Å‚«‚ê‚¢‚É‚Å‚«‚éH
                int first = Random.Range(0, nextFloor.Rooms.Length);
                nextIndices.Add(first);
                if(nextFloor.Rooms.Length > 1 && Random.value < 0.5f)
                {
                    int second;
                    do
                    {
                        second = Random.Range(0,nextFloor.Rooms.Length);
                    }
                    while(second == first);

                    nextIndices.Add(second);
                }
                currentFloor.Rooms[r].NextRoomIndices = nextIndices.ToArray();
            }
        }
        return map;
    }
}
