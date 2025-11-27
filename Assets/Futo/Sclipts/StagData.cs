using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StageData
{
    [SerializeField] private int _stageID;
    [SerializeField] private Sprite _background;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private GameObject _enemy;

    public int StageID => _stageID;
    public Sprite Background => _background;
    public int Width => _width;
    public int Height => _height;
    public GameObject Enemy => _enemy;
}
