using TMPro;
using UnityEngine;

public class CardEncyclopedia : MonoBehaviour
{
    [Header("-----参照-----")]
    [SerializeField, Tooltip("デカ表示カード")] private GameObject _bigCard;
    [SerializeField, Tooltip("説明欄")] private TextMeshProUGUI _description;
    [SerializeField, Tooltip("カードプレハブ")] private GameObject _prefab;
    [SerializeField, Tooltip("生成場所")] private Transform _parent;

    private GameManager _gameManager;
    private CardDataBase _cardDataBase;
    private Transform _tr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameManager.Instance;
        _cardDataBase = _gameManager.CardDataBase;
        _tr = transform;
    }
    public void Generate()
    {
        // 既存削除（重複防止）
        for (int i = _parent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_parent.GetChild(i).gameObject);
        }

        for (int i = 0; i < _cardDataBase.Cards.Count; i++)
        {
            var obj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(_prefab);
            obj.transform.SetParent(_parent, false);

            // 必要ならここでデータセット
            // obj.GetComponent<CardView>().SetData(db.Cards[i]);
        }
    }
}
