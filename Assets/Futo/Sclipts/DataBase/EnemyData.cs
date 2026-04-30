using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy")]
public class EnemyData : ScriptableObject
{
    public int EnemyID => _enemyID;
    public Sprite Sprite => _sprite;
    public int EnemyHP => _enemyHP;
    public int EnemyAP => _enemyAP;
    public int EnemyAttackTime => _enemyAttackTime;
    public int EnemyAT => _enemyAT;
    public bool CanBoardInterference => _canBoardInterference;
    public int BoardInterferenceTurn => _boardInterferenceTurn;
    public int EffectTime => _effectTime;
    public IEffect[] CardEffects => _cardEffects;
    public bool CanBuff => _canBuff;
    public int BuffTurn => _buffTurn;
    public int[] RewardAmount => _rewardAmount;


    [SerializeField, Tooltip("エネミーID")] private int _enemyID;
    [SerializeField, Tooltip("エネミーの見た目")] private Sprite _sprite;
    [SerializeField, Tooltip("エネミーの体力")] private int _enemyHP;
    [SerializeField, Tooltip("エネミーの攻撃力")] private int _enemyAP;
    [SerializeField, Tooltip("エネミーの攻撃回数")] private int _enemyAttackTime;
    [SerializeField, Tooltip("エネミーのアタックターン")] private int _enemyAT;
    
    [Header("-----盤面干渉-----")]
    [SerializeField, Tooltip("エネミーの盤面鑑賞攻撃フラグ")] private bool _canBoardInterference;
    [SerializeField, ShowIf("_canBoardInterference"), Tooltip("盤面干渉のターン")] private int _boardInterferenceTurn;
    [SerializeField, ShowIf("_canBoardInterference"), Tooltip("盤面干渉の数")] private int _effectTime;
    [SerializeField, ShowIf("_canBoardInterference"), Tooltip("設置魔法陣")] private IEffect[] _cardEffects;

    [Header("-----バフ-----")]
    [SerializeField, Tooltip("エネミーのバフ攻撃フラグ")] private bool _canBuff;
    [SerializeField, ShowIf("_canBuff"), Tooltip("エネミーのバフ攻撃のターン")] private int _buffTurn;

    [Header("-----報酬-----")]
    [SerializeField, Tooltip("報酬量")] private int[] _rewardAmount = new int[2];


    /// <summary>
    /// ランダムな報酬量を決める
    /// </summary>
    /// <returns>報酬量</returns>
    public int RandomReword()
    {
        return _rewardAmount[Random.Range(0, _rewardAmount.Length)];
    }
}
