using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PhaseChangeButton : MonoBehaviour
{
    [SerializeField] private BattlePhase _battlePhase;
    private Button _button;
    void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PushButton);
    }

    private void PushButton()
    {
        GameManager.Instance.CurrentPhase = _battlePhase;
    }
}
