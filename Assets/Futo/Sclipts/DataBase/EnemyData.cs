using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField, Tooltip("エネミーID")] private int _enemyID;
    [SerializeField, Tooltip("エネミーの見た目")] private Sprite _sprite;
    [SerializeField, Tooltip("エネミーの体力")] private int _enemyHP;
    [SerializeField, Tooltip("エネミーの攻撃力")] private int _enemyAP;
    [SerializeField, Tooltip("エネミーの攻撃回数")] private int _enemyAttackTime;
    [SerializeField, Tooltip("エネミーのアタックターン")] private int _enemyAT;
    [SerializeField, Tooltip("エネミーの確定ダメージのターン")] private int _enemyConfirmedAttack;
    [SerializeField, Tooltip("エネミーのイベント(盤面鑑賞)")] private int _enemyEventTurn;

    public int EnemyID => _enemyID;
    public Sprite Sprite => _sprite;
    public int EnemyHP => _enemyHP;
    public int EnemyAP => _enemyAP;
    public int EnemyAttackTime => _enemyAttackTime;
    public int EnemyAT => _enemyAT;
    public int EnemyConfirmedAttack => _enemyConfirmedAttack;
    public int EnemyEventTurn => _enemyEventTurn;
}
