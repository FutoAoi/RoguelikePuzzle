using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField] private List<List<GameObject>> _slotList = new List<List<GameObject>>();
    [SerializeField] private List<Enemy> _enemyList = new();

    [Header("コンポーネント設定")]
    [SerializeField] private GameObject _tailPrefab;
    [SerializeField] private Image _backGroundImage;
    [SerializeField] private GameObject _enemySlot;
    [SerializeField] private GameObject _enemyPanel;
    [SerializeField] private float _widthSize = 0.8f;
    [SerializeField] private float _heightSize = 0.8f;

    private GridLayoutGroup _layoutGroup;
    private StageData _stage;
    private Transform _parent;
    private Transform _enemyParent;
    private GameObject _slot;
    private GameObject _enemy;

    public List<List<GameObject>> SlotList => _slotList;
    public List<Enemy> EnemyList => _enemyList;

    public void CreateStage(int stageIndex)
    {
        _layoutGroup = GetComponent<GridLayoutGroup>();
        _stage = GameManager.Instance.StageDataBase.GetStageData(stageIndex);
        _layoutGroup.constraintCount = _stage.Width;
        _backGroundImage.sprite = _stage.Background;
        _parent = this.transform;
        _enemyParent = _enemyPanel.transform;
        SlotList.Clear();
        AdjustCellSize();
        
        for (int i = 0; i < _stage.Height; i++)
        {
            List<GameObject> slotListH = new();
            for(int j = 0; j < _stage.Width; j++)
            {
                _slot = Instantiate(_tailPrefab, Vector3.zero, Quaternion.identity, _parent);
                _slot.name = ($"Slot{i},{j}");
                slotListH.Add(_slot);
            }
            _slotList.Add(slotListH);
        }

        for (int i = 0; i < _stage.Height; i++)
        {
            _enemy = Instantiate(_enemySlot, Vector3.zero, Quaternion.identity, _enemyParent);
            _enemyList.Add(_enemy.GetComponent<Enemy>());
        }

        for (int i = 0; i < _stage.Enemies.Length; i++)
        {
            _enemyList[_stage.Enemies[i].EnemyPosition].SetEnemyStatus(_stage.Enemies[i].EnemyID);
        }

        //空の敵を設置
        for(int i = 0; i < _enemyList.Count; i++)
        {
            if (IsEnemy(i)) continue;
            _enemyList[i].SetEnemyStatus(0);
        }
    }
    /// <summary>
    /// 指定の場所にエネミーが存在しているか
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private bool IsEnemy(int index)
    {
        for(int i = 0; i < _stage.Enemies.Length; i++)
        {
            if(index == _stage.Enemies[i].EnemyPosition)return true;
        }
        return false;
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
