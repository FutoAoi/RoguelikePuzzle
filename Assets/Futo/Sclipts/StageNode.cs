using System.Collections.Generic;
using UnityEngine;
public enum StageNodeType
{
    None,
    Battle
}

[System.Serializable]
public class StageNode
{
    public StageNodeType StageNodeType;
    public int StageID;
    public List<StageNode> NextStageNodes = new List<StageNode>();
    public StageNodeUI UI;
}
