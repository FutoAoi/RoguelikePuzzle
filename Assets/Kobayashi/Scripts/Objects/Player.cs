using System.Collections;
using UnityEngine;

public class Player : CharacterBase
{
    private PlayerStatus _status;
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.Player = this;
        _status = _gameManager.PlayerStatus;
    }

    public override void Damaged(int damage)
    {
        _status.Damaged(damage);
    }
}
