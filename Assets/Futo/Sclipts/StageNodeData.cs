using UnityEngine;

[System.Serializable]
public class StageNodeData
{
    [SerializeField] private StageNodeType _type;
    [SerializeField] private int _stageID;

    public StageNodeType Type => _type;
    public int StageID => _stageID;
}
