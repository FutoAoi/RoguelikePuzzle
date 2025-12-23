using UnityEngine;

[CreateAssetMenu(menuName = "Datas/Enemy")]
public class EnemyData : ScriptableObject
{
    [SerializeField, Tooltip("エネミーID")] private int _enemyID;
    [SerializeField, Tooltip("エネミーの見た目")] private Sprite _sprite;
    [SerializeField, Tooltip("エネミーの体力")] private int _enemyHP;
    [SerializeField, Tooltip("エネミーの攻撃力")] private int _enemyAP;
    [SerializeField, Tooltip("エネミーのアタックターン")] private int _enemyAT;

    public int EnemyID => _enemyID;
    public Sprite Sprite => _sprite;
    public int EnemyHP => _enemyHP;
    public int EnemyAP => _enemyAP;
    public int EnemyAT => _enemyAT;
}
