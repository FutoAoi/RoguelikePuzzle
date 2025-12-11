using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackMagic : MonoBehaviour
{
    public int AttackPower = 1;

    [SerializeField, Tooltip("タイルの発光色")] private Color _glowingColor;

    [NonSerialized] public MagicVector _currentVector;

    private GameManager _gameManager;
    private AttackManager _attackManager;
    private Action _onDisable;
    private TileSlot _tileSlot;
    private RectTransform _attackRectTr, _nextRectTr;
    private StageManager _stageManager;
    private Image _slotImg;
    private Color _startColor;
    private Vector2Int _currentSlot, _speedInt;
    private Vector2 _outPos, _goalPos;
    private bool _finish, _firstAttack, _isAttack;
    private int _width, _height;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _attackManager = FindAnyObjectByType<AttackManager>();
        _stageManager = FindAnyObjectByType<StageManager>();
        _width = _gameManager.StageDataBase.GetStageData(_gameManager.StageID).Width;
        _height = _gameManager.StageDataBase.GetStageData(_gameManager.StageID).Height;
        _finish = false;
        _firstAttack = true;
    }
    public void Initialize(Action onDisable)
    {
        _onDisable = onDisable;
    }
    /// <summary>
    /// 魔法を壊す
    /// </summary>
    public void DestroyMagic(bool isPlayer)
    {
        _attackManager.AttackFinish(isPlayer);
        _onDisable?.Invoke();
        gameObject.SetActive(false);
    }
    public IEnumerator Attack(Vector2Int startPos, MagicVector startVector, RectTransform startRectTr,float interval,bool isPlayer)
    {
        _currentSlot = startPos;//初期ポジ
        while (!_finish)
        {
            //魔法の移動
            if (_firstAttack)
            {
                _firstAttack = false;
                _currentVector = startVector;
                _attackRectTr = GetComponent<RectTransform>();
                _attackRectTr.position = startRectTr.position;
                _nextRectTr = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<RectTransform>();
                _attackRectTr.DOMoveX(_nextRectTr.position.x, interval)
                    .SetEase(Ease.Linear);
            }
            else
            {
                _nextRectTr = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<RectTransform>();
                _attackRectTr.DOMove(_nextRectTr.position, interval)
                    .SetEase(Ease.Linear);
            }
            yield return new WaitForSeconds(interval * 0.5f);

            _slotImg = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<Image>();
            _startColor = _slotImg.color;
            _slotImg.DOColor(_glowingColor, interval * 0.1f)
                .SetEase(Ease.Linear);

            yield return new WaitForSeconds(interval * 0.2f);

            //効果の呼び出し
            _tileSlot = _stageManager.SlotList[_currentSlot.x][_currentSlot.y].GetComponent<TileSlot>();

            if (!_tileSlot.IsOccupied)
            {
                Debug.Log("何もなかった,,,");
            }
            else
            {
                _gameManager.CardDataBase.GetCardData(_tileSlot.ID).Effect.Excute();
                _tileSlot.DecreaseTimes(1);
            }

            //スロット内部の現在地移動
            switch (_currentVector)
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
            if (isPlayer)
            {
                if (_currentSlot.y >= _width)//プレイヤー攻撃成功時
                {
                    _finish = true;
                    _isAttack = true;
                }
                if (_currentSlot.x < 0 || _currentSlot.x > _height - 1 || _currentSlot.y < 0)
                {
                    _finish = true;
                    _isAttack = false;
                    Debug.Log("ミス！");
                }
            }
            else
            {
                if(_currentSlot.y < 0)//エネミー攻撃成功時
                {
                    _finish = true;
                    _isAttack = true;
                }
                if(_currentSlot.x < 0 || _currentSlot.x > _height - 1 || _currentSlot.y < 0 || _currentSlot.y >= _width)
                {
                    _finish = true;
                    _isAttack = false;
                    Debug.Log("ミス！");
                }
            }
            yield return new WaitForSeconds(interval * 0.3f);
            _slotImg.DOColor(_startColor, interval * 0.1f)
                .SetEase(Ease.Linear);
        }

        if (_isAttack)
        {
            if (isPlayer)
            {
                Enemy attackedEnemy = _stageManager.EnemyList[_currentSlot.x];
                _attackRectTr.DOMove(attackedEnemy.transform.position, interval)
                    .SetEase(Ease.Linear);
                attackedEnemy.Hit(AttackPower);
            }
            else
            {
                Player player = FindAnyObjectByType<Player>();
                _attackRectTr.DOMove(player.transform.position, interval)
                    .SetEase(Ease.Linear);
                StartCoroutine(player.Damaged(AttackPower, 0.1f));
            } 
        }
        else
        {
            if (_currentSlot.x < 0) _outPos = new Vector2(0, 1);
            if (_currentSlot.x > _height - 1) _outPos = new Vector2(0, -1);
            if (_currentSlot.y < 0) _outPos = new Vector2(-1, 0);
            if (_currentSlot.y >= _width) _outPos = new Vector2(1, 0);
            _goalPos = (Vector2)_attackRectTr.position + _outPos * 3f;
            _attackRectTr.DOMove(_goalPos, interval)
                .SetEase(Ease.Linear);
        }
        yield return new WaitForSeconds(interval);

        //スロットに接地フラグいれる
        foreach (List<GameObject> Hslot in _stageManager.SlotList)
        {
            foreach (GameObject slot in Hslot)
            {
                _tileSlot = slot.GetComponent<TileSlot>();
                if (_tileSlot.IsOccupied)
                    _tileSlot.IsLastTimeCard = true;
                else _tileSlot.IsLastTimeCard = false;
            }
        }

        _finish = false;
        _firstAttack = true;
        DestroyMagic(isPlayer);
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
