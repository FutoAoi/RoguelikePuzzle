using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<List<GameObject>> _slotList = new List<List<GameObject>>();

    [Header("コンポーネント設定")]
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private Image _backGroundImage;
    //追加した
    [SerializeField] private float _widthSize = 0.8f;
    [SerializeField] private float _heightSize = 0.8f;

    private GridLayoutGroup _layoutGroup;
    private StageData _stage;
    private Transform _parent;
    private GameObject _slot;
    private Enemy _enemy;

    public List<List<GameObject>> SlotList => _slotList;

    public void CreateStage(int stageIndex)
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        _enemy = GetComponent<Enemy>();
        _stage = GameManager.Instance.StageDataBase.GetStageData(stageIndex);
        _layoutGroup.constraintCount = _stage.Width;
        _backGroundImage.sprite = _stage.Background;
        _parent = this.transform;
        SlotList.Clear();
        AdjustCellSize();
        
        for (int i = 0; i < _stage.Height; i++)
        {
            List<GameObject> slotListH = new List<GameObject>();
            for(int j = 0; j < _stage.Width; j++)
            {
                _slot = Instantiate(_tailPrefab, Vector3.zero, Quaternion.identity, _parent);
                _slot.name = ($"Slot{i},{j}");
                slotListH.Add(_slot);
            }
            _slotList.Add(slotListH);
        }
        _enemy.SetEnemyStatus(_stage.EnemyID);
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
