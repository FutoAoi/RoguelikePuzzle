using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StageData
{
    [SerializeField,Tooltip("ステージID")] private int _stageID;
    [SerializeField,Tooltip("ステージ背景")] private Sprite _background;
    [SerializeField,Tooltip("横幅")] private int _width;
    [SerializeField,Tooltip("縦幅")] private int _height;
    [SerializeField,Tooltip("エネミーID")] private int _enemyID;

    public int StageID => _stageID;
    public Sprite Background => _background;
    public int Width => _width;
    public int Height => _height;
    public int EnemyID => _enemyID;
}
