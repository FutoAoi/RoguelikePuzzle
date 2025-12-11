using UnityEngine;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RoomType _roomType;
    [SerializeField] private int _stageID;
    [SerializeField] private Room[] _nextroom;

    public void SetRoomData(RoomData roomData)
    {
        _roomType = roomData.RoomType;
        _stageID = roomData.StageID;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(_stageID);
    }
}
