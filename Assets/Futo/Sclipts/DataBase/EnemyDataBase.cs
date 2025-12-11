using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataBase/EnemyDataBase")]
public class EnemyDataBase : ScriptableObject
{
    [Header("エネミーデータベース")]
    [SerializeField, Tooltip("エネミーリスト")] private List<EnemyData> _enemys = new();

    private Dictionary<int, EnemyData> _enemyDictionary;

    void Initialize()
    {
        if (_enemyDictionary == null)
        {
            _enemyDictionary = new Dictionary<int, EnemyData>();
            foreach (var enemy in _enemys)
            {
                if (!_enemyDictionary.ContainsKey(enemy.EnemyID))
                {
                    _enemyDictionary.Add(enemy.EnemyID, enemy);
                }
                else
                {
                    Debug.LogWarning($"重複したキーがあります:{enemy.EnemyID}");
                }
            }
        }
    }

    public EnemyData GetEnemyData(int ID)
    {
        Initialize();
        if (_enemyDictionary.TryGetValue(ID, out var enemyData))
        {
            return enemyData;
        }
        Debug.LogWarning($"ID{ID}のステージが見つかりません");
        return null;
    }
}

[System.Serializable]
public class EnemyData
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
