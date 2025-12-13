using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private CardDataBase _cardData;
    [SerializeField] private RewardCard[] _rewardCard;
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private int _serectReward = 0;

    private void Start()
    {
        _cardData = GameManager.Instance.CardDataBase;
        Reward();
    }

    void Reward()
    {
        foreach (var card in _rewardCard)
        {
            card.SetCard(_cardData.GetRandomCardIDByRarity(_rarity));
        }
    }

    public void GetReward()
    {
        DeckManager.Instance.AddDeck(_rewardCard[_serectReward].CardID);
    }

    public void SetRewardNumber(int number)
    {
        _serectReward = number;
    }
}
