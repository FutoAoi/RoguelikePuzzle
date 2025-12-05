using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public int EnemyPos;
    [Header("コンポーネント設定")]
    [SerializeField] private StageManager _stageManager;
    [SerializeField, Tooltip("攻撃魔法")] private GameObject _attackMagicPrefab;
    [SerializeField, Tooltip("攻撃出現位置")] private RectTransform _attackStartPos;
    [SerializeField, Tooltip("敵の場所")] private RectTransform _enemyPos;

    [Header("数値設定")]
    [SerializeField, Tooltip("タイル間の移動時間")] private float _interval = 2.0f;

    [NonSerialized]public bool UP,Down;

    private GameManager _gameManager;
    private TileSlot _tileSlot;
    private RectTransform _attackRectTr,_nextRectTr;
    private bool _finish,_firstAttack;
    private int _width, _height, _currentSlot,_forwardCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.AttackManager = this;
        _width = _gameManager.StageData.GetStageData(_gameManager.StageID).Width;
        _height = _gameManager.StageData.GetStageData(_gameManager.StageID).Height;
        _forwardCount = 0;
        UP = false;
        Down = false;
        _finish = false;
        _firstAttack = true;
        _attackMagicPrefab.SetActive(false);
        //真ん中にいる場合
        EnemyPos = (_height / 2 + 1) * _width;
    }
    /// <summary>
    /// 魔法に効果かける
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackStart()
    {
        _currentSlot = (_height / 2)*_width;//初期ポジ
        while (!_finish)
        {
            //魔法の移動
            if (_firstAttack)
            {
                Debug.Log("攻撃開始！！！");
                _firstAttack=false;
                _attackMagicPrefab.SetActive(true);
                _attackRectTr = _attackMagicPrefab.GetComponent<RectTransform>();
                _attackRectTr.position = _attackStartPos.position;
                _nextRectTr = _stageManager.SlotList[_currentSlot].GetComponent<RectTransform>();
                _attackRectTr.DOMoveX(_nextRectTr.position.x, _interval)
                    .SetEase(Ease.Linear);
            }
            else
            {
                _nextRectTr = _stageManager.SlotList[_currentSlot].GetComponent<RectTransform>();
                _attackRectTr.DOMove(_nextRectTr.position,_interval)
                    .SetEase(Ease.Linear);
            }
            yield return new WaitForSeconds(_interval*0.7f);

            //効果の呼び出し
            _tileSlot = _stageManager.SlotList[_currentSlot].GetComponent<TileSlot>();

            if(!_tileSlot.IsOccupied)
            {
                Debug.Log("何もなかった,,,");
            }
            else
            {
                _gameManager.CardData.GetCardData(_tileSlot.ID).Effect.Excute();
            }

            //スロット内部の現在地移動
            if (UP)
            {
                _currentSlot -= _width;
                if (_currentSlot < 0) _finish = true;
                UP = false;
            }
            else if (Down)
            {
                _currentSlot += _width;
                if(_currentSlot > _width * _height - 1) _finish = true;
                Down = false;
            }
            else
            {
                _currentSlot++;
                _forwardCount++;
                if(_forwardCount == _width)_finish = true;
            }
            yield return new WaitForSeconds(_interval * 0.3f);
        }

        _attackRectTr.DOMove(_enemyPos.position,_interval)
            .SetEase(Ease.Linear);
        yield return new WaitForSeconds(_interval);

        if(_currentSlot == EnemyPos)
        {
            Debug.Log("攻撃！！");           
        }
        else
        {
            Debug.Log("ミス！");
        }
        //スロットの初期化
        foreach(GameObject slot in _stageManager.SlotList)
        {
            _tileSlot = slot.GetComponent<TileSlot>();
            _tileSlot.ClearSlot();
        }
        _gameManager.Reset = true;
        _forwardCount = 0;
        _finish = false;
        _firstAttack = true;
        _attackMagicPrefab.SetActive(false);
    }
}