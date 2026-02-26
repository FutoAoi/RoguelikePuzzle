using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class AttackPointSelectButton : MonoBehaviour
{
    [SerializeField, Tooltip("非選択時透明度")] private float _alpha = 0.7f;
    [SerializeField, Tooltip("演出時間")] private float _duration = 0.4f;

    public int AttackNumber;
    public bool IsSelect { get; private set; } = false;

    AttackPointManager _attackPointManager;
    Image _img;
    GameManager _gameManager;
    Button _button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetButton();
    }

    private void ResetButton()
    {
        _gameManager = GameManager.Instance;
        _img = GetComponent<Image>();
        _attackPointManager = GetComponentInParent<AttackPointManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(RegisterAttackPosition);
        IsSelect = false;
        _img.color = new Color(1f, 1f, 1f, _alpha);
    }

    /// <summary>
    /// 攻撃場所変更
    /// </summary>
    public void RegisterAttackPosition()
    {
        //攻撃場所変更処理
        if (_gameManager == null)
        {
            ResetButton();
        }
        _gameManager.AttackManager.AttackStartPos = AttackNumber;
        IsSelect = true;
        _attackPointManager.ChangeButtonState(AttackNumber);

        //演出
        _img.color = new Color(1f, 1f, 1f, 1f);
        Debug.Log("b");
    }

    /// <summary>
    /// 非選択演出
    /// </summary>
    public void CancelRegister()
    {
        IsSelect = false;

        _img.DOColor(new Color(1f,1f,1f,_alpha),_duration);
    }
}
