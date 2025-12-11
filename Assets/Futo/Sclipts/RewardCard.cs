using UnityEngine;

public class RewardCard : MonoBehaviour
{
    [Header("カード詳細")]
    [SerializeField, Tooltip("カードのレアリティ")] private CardRarity _rarity;
    [SerializeField, Tooltip("カードの見た目")] private Sprite _sprite;
    [SerializeField, Tooltip("カードの名前")] private string _name;
    [SerializeField, Tooltip("カードの説明")] private string _description;
    [SerializeField, Tooltip("カードのコスト")] private int _cost;
    [SerializeField, Tooltip("最大回数")] private int _maxTimes;

    private CardData _data;

    private void SetCard(int ID)
    {
        _data = GameManager.Instance.CardDataBase.GetCardData(ID);
        _rarity = _data.Rarity;
        _sprite = _data.Sprite;
    }
}
