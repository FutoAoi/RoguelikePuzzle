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
        Debug.LogWarning($"ID{ID}の敵が見つかりません");
        return null;
    }
}
