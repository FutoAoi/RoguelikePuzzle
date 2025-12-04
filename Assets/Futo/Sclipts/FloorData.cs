using UnityEngine;

[CreateAssetMenu(menuName = "Map/FloorData")]
public class FloorData : ScriptableObject
{
    [SerializeField] private StageNodeData[] nodeDatas; 
    public StageNodeData[] NodeDatas => nodeDatas;
}
