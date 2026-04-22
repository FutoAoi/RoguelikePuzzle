using TMPro;
using UnityEngine;

public class DescriptionPanel : MonoBehaviour
{
    [Header("-----éQè∆-----")]
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Durability;
    public TextMeshProUGUI Description;

    private void Start()
    {
        UpdateText(null, true);
    }
    public void UpdateText(CardData data,bool isClear)
    {
        if (isClear)
        {
            Name.text = null;
            Cost.text = null;
            Durability.text = null;
            Description.text = null;
        }
        else
        {
            Name.text = data.Name;
            Cost.text = data.Cost.ToString();
            Durability.text = data.MaxTimes.ToString();
            Description.text = data.Description;
        }   
    }
}
