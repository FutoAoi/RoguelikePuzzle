using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/StageDataBase")]
public class StageDataBase : ScriptableObject
{
    [Header("ステージデータベース")]
    [SerializeField,Tooltip("ステージリスト")] private List<StageData> _stages = new();

    private Dictionary<int, StageData> _stageDictionary;

    void Initialize()
    {
        if (_stageDictionary == null)
        {
            _stageDictionary = new Dictionary<int, StageData>();
            foreach (var stage in _stages)
            {
                if (!_stageDictionary.ContainsKey(stage.StageID))
                {
                    _stageDictionary.Add(stage.StageID, stage);
                }
                else
                {
                    Debug.LogWarning($"重複したキーがあります:{stage.StageID}");
                }
            }
        }
    }

    public StageData GetStageData(int ID)
    {
        Initialize();
        if (_stageDictionary.TryGetValue(ID, out var stageData))
        {
            return stageData;
        }
        Debug.LogWarning($"ID{ID}のステージが見つかりません");
        return null;
    }
}

[System.Serializable]
public class StageData
{
    [SerializeField, Tooltip("ステージID")] private int _stageID;
    [SerializeField, Tooltip("ステージ背景")] private Sprite _background;
    [SerializeField, Tooltip("横幅")] private int _width;
    [SerializeField, Tooltip("縦幅")] private int _height;
    [SerializeField, Tooltip("エネミーID")] private int _enemyID;

    public int StageID => _stageID;
    public Sprite Background => _background;
    public int Width => _width;
    public int Height => _height;
    public int EnemyID => _enemyID;
}

