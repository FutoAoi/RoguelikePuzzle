using UnityEngine;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RoomType _roomType;
    [SerializeField] private int _stageID;
    [SerializeField] private Room[] _nextroom;

    private MapManager _mapManager;
    private int _floorIndex;
    private int _roomIndex;
    public void SetRoomData(GenerateRoomData roomData,MapManager mapManager)
    {
        _roomType = roomData.RoomType;
        _stageID = roomData.StageID;
        _floorIndex = roomData.FloorIndex;
        _roomIndex = roomData.RoomIndex;
        _mapManager = mapManager;
    }

    public void SetNextRoom(Room[] nextRoom)
    {
        _nextroom = nextRoom;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_floorIndex == _mapManager.MapData.CurrentFloorIndex + 1)
        {
            _mapManager.MoveTo(_roomIndex);
            GameManager.Instance.StageID = _stageID;
        }
        else
        {
            Debug.Log("ç°ÇÕëIëÇ≈Ç´Ç»Ç¢ïîâÆ");
        }
    }
}
