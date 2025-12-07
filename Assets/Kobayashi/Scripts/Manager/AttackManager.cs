using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField, Tooltip("タイルの発光色")] private Color _glowingColor;

    [NonSerialized] public MagicVector _currentVector;

    private GameManager _gameManager;
    private TileSlot _tileSlot;
    private RectTransform _attackRectTr,_nextRectTr;
    private Image _slotImg;
    private Color _startColor;
    private bool _finish,_firstAttack,_isAttack;
    private int _width, _height;
    private Vector2Int _currentSlot,_speedInt;
    private Vector2 _outPos,_goalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.AttackManager = this;
        _width = _gameManager.StageDataBase.GetStageData(_gameManager.StageID).Width;
        _height = _gameManager.StageDataBase.GetStageData(_gameManager.StageID).Height;
        _finish = false;
        _firstAttack = true;
        _attackMagicPrefab.SetActive(false);
        //真ん中にいる場合
        EnemyPos = _height / 2 * _width;
    }
    /// <summary>
    /// 魔法に効果かける
    /// </summary>
    /// <returns></returns>
    public IEnumerator AttackStart()
    {
        _currentSlot = new Vector2Int(_height / 2 , 0);//初期ポジ
        while (!_finish)
        {
            //魔法の移動
            if (_firstAttack)
            {
                Debug.Log("攻撃開始！！！");
                _firstAttack=false;
                _attackMagicPrefab.SetActive(true);
                _currentVector = MagicVector.Right;
                _attackRectTr = _attackMagicPrefab.GetComponent<RectTransform>();
                _attackRectTr.position = _attackStartPos.position;
                _nextRectTr = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<RectTransform>();
                _attackRectTr.DOMoveX(_nextRectTr.position.x, _interval)
                    .SetEase(Ease.Linear);
            }
            else
            {
                _nextRectTr = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<RectTransform>();
                _attackRectTr.DOMove(_nextRectTr.position,_interval)
                    .SetEase(Ease.Linear);
            }
            yield return new WaitForSeconds(_interval * 0.5f);

            _slotImg = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<Image>();
            _startColor = _slotImg.color;
            _slotImg.DOColor(_glowingColor, _interval * 0.1f)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(_interval * 0.2f);

            //効果の呼び出し
            _tileSlot = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<TileSlot>();

            if (!_tileSlot.IsOccupied)
            {
                Debug.Log("何もなかった,,,");
            }
            else
            {
                _gameManager.CardDataBase.GetCardData(_tileSlot.ID).Effect.Excute();
            }

            //スロット内部の現在地移動
            switch(_currentVector)
            {
                case MagicVector.UP:
                    _speedInt = new Vector2Int(-1, 0);
                    break;
                case MagicVector.Down:
                    _speedInt = new Vector2Int(1, 0);
                    break;
                case MagicVector.Left:
                    _speedInt = new Vector2Int(0, -1);
                    break;
                case MagicVector.Right:
                    _speedInt = new Vector2Int(0, 1);
                    break;
            }

            _currentSlot += _speedInt;
            Debug.Log(_currentSlot);
            if (_currentSlot.y >= _width)
            {
                _finish = true;
                _isAttack = true;
                Debug.Log("攻撃！！");
            }
            if(_currentSlot.x < 0 || _currentSlot.x >_height - 1 || _currentSlot.y < 0)
            {
                _finish = true;
                _isAttack = false;
                Debug.Log("ミス！");
            }
            yield return new WaitForSeconds(_interval * 0.3f);
            _slotImg.DOColor(_startColor, _interval * 0.1f)
                .SetEase(Ease.Linear);
        }

        if (_isAttack)
        {
            _attackRectTr.DOMove(_enemyPos.position, _interval)
                .SetEase(Ease.Linear);
        }
        else
        {
            if (_currentSlot.x < 0) _outPos = new Vector2(0, 1);
            if (_currentSlot.x > _height - 1) _outPos = new Vector2(0, -1);
            if (_currentSlot.y < 0) _outPos = new Vector2(-1, 0);
            _goalPos = (Vector2)_attackRectTr.position + _outPos * 3f;
            _attackRectTr.DOMove(_goalPos,_interval)
                .SetEase(Ease.Linear);
        }
            yield return new WaitForSeconds(_interval);
        //スロットの初期化
        foreach(List<GameObject> Hslot in _stageManager.SlotList)
        {
            foreach (GameObject slot in Hslot)
            {
                _tileSlot = slot.GetComponent<TileSlot>();
                _tileSlot.ClearSlot();
            }
        }
        _gameManager.Reset = true;
        _finish = false;
        _firstAttack = true;
        _attackMagicPrefab.SetActive(false);
    }
    /// <summary>
    /// 方向転換
    /// </summary>
    /// <param name="vector"></param>
    public void ChangeVector(MagicVector vector)
    {
        _currentVector = vector;
        Debug.Log("方向転換！");
    }
}