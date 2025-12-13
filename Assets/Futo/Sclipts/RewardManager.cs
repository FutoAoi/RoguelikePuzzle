using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public RewardCard[] RewardCards => _rewardCard;

    private CardDataBase _cardData;
    [SerializeField] private RewardCard[] _rewardCard;
    [SerializeField] private CardRarity _rarity;
    [SerializeField] private int _serectReward = 0;

    private void Start()
    {
        Reward();
    }

    public void Reward()
    {
        _cardData = GameManager.Instance.CardDataBase;
        foreach (var card in _rewardCard)
        {
            card.SetCard(_cardData.GetRandomCardIDByRarity(_rarity));
        }
    }

    public void GetReward()
    {
        DeckManager.Instance.AddDeck(_rewardCard[_serectReward].CardID);
        //シーン移行入れたい
    }

    public void RewardSkip()
    {
        //シーン移行入れたい
    }

    public void SetRewardNumber(int number)
    {
        _serectReward = number;
    }
}
