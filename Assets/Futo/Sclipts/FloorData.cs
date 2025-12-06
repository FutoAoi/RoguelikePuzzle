using UnityEngine;

[CreateAssetMenu(menuName = "Map/FloorData")]
public class FloorData : ScriptableObject
{
    [SerializeField] private StageNodeData[] _nodeDatas;
    [SerializeField] private int _minNodes = 1;
    [SerializeField] private int _maxNodes = 4;
    public StageNodeData[] NodeDatas => _nodeDatas;
    public int MinNodes => _minNodes;
    public int MaxNodes => _maxNodes;
}
