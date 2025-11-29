using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public List<GameObject> SlotList = new List<GameObject>();

    [SerializeField] private StageDataBase _stageData;
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private int _stageIndex;
    [SerializeField] private Image _backGroundImage;
    //’Ç‰Á‚µ‚½
    [SerializeField] private float _widthSize = 0.8f;
    [SerializeField] private float _heightSize = 0.8f;

    private GridLayoutGroup _layoutGroup;
    private StageData _stage;
    private Transform _parent;
    private GameObject _slot;

    void Start()
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        _stage = _stageData.GetStageData(_stageIndex);
        _layoutGroup.constraintCount = _stage.Width;
        _backGroundImage.sprite = _stage.Background;
        _parent = this.transform;
        SlotList.Clear();
        AdjustCellSize();
        for (int i = 0; i < _stage.Width * _stage.Height; i++)
        {
            _slot = Instantiate(_tailPrefab, Vector3.zero, Quaternion.identity, _parent);
            SlotList.Add(_slot);
        }
    }

    private void AdjustCellSize()
    {
        RectTransform _tailePanelRect = GetComponent<RectTransform>();

        float bgWidth = _tailePanelRect.rect.width;   
        float bgHeight = _tailePanelRect.rect.height; 

        float cellWidth = bgWidth / _stage.Width;
        float cellHeight = bgHeight / _stage.Height;

        _layoutGroup.cellSize = new Vector2(cellWidth * _widthSize, cellHeight * _heightSize);
    }
}
