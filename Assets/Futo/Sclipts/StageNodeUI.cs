using UnityEngine;
using UnityEngine.EventSystems;

public class StageNodeUI : MonoBehaviour, IPointerClickHandler
{
    public StageNode StageNodeData;
    public RectTransform rect => GetComponent<RectTransform>();
    public void OnPointerClick(PointerEventData eventData)
    {
        MapManager.Instance.OnNodeClicked(StageNodeData);
    }
}
