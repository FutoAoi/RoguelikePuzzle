using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class AttackPointSerectButton : MonoBehaviour
{
    public int AttackNumber;
    GameManager _gameManager;
    Button _button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameManager.Instance;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RegisterAttackPosition);
    }

    public void RegisterAttackPosition()
    {
        _gameManager.AttackManager.AttackStartPos = AttackNumber;
    }
}
