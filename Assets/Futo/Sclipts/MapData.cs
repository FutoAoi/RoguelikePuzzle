using UnityEngine;

public enum RoomType
{
    None,
    Buttle,
}

[CreateAssetMenu(menuName = "Map/MapData")]
public class MapData : ScriptableObject
{
    [SerializeField] private FloorData[] _floorDatas;
    public FloorData[] FloorDatas => _floorDatas;
}

[CreateAssetMenu(menuName = "Map/FloorData")]
public class FloorData : ScriptableObject
{
    [SerializeField] private RoomData[] roomData;
    [SerializeField] private int _minNodes = 1;
    [SerializeField] private int _maxNodes = 4;

    public RoomData[] RoomDatas => roomData;
    public int MinNodes => _minNodes;
    public int MaxNodes => _maxNodes;
}

[System.Serializable]
public class RoomData
{
    [SerializeField] private RoomType _roomType;
    [SerializeField] private int _stageID;

    public RoomType RoomType => _roomType;
    public int StageID => _stageID;
}
