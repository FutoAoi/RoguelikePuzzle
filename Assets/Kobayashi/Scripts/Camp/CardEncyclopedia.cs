using TMPro;
using UnityEngine;

public class CardEncyclopedia : MonoBehaviour
{
    public CardDataBase CardDatas => _cardDatas;

    [Header("-----参照-----")]
    [SerializeField, Tooltip("カードデータベース")] private CardDataBase _cardDatas;
    [SerializeField, Tooltip("デカ表示カード")] private GameObject _bigCard;
    [SerializeField, Tooltip("説明欄")] private TextMeshProUGUI _description;
    [SerializeField, Tooltip("カードプレハブ")] private GameObject _prefab;
    [SerializeField, Tooltip("生成場所")] private Transform _parent;

    private Transform _tr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tr = transform;
    }
    public void Generate(int index)
    {
        CardData card = _cardDatas.Cards[index];

        GameObject obj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(_prefab);
        obj.transform.SetParent(_parent, false);

        obj.GetComponent<CardView>().SetCardData(card);
    }
    public void GenerateAll()
    {
        for (int i = 0; i < _cardDatas.Cards.Count; i++)
        {
            Generate(i);
        }
    }

    public void ClearAll()
    {
        for (int i = _parent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_parent.GetChild(i).gameObject);
        }
    }
}
