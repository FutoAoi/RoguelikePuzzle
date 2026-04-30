using System.Collections;
using UnityEngine;

public class Player : CharacterBase
{
    private PlayerStatus _status;

    protected override void Start()
    {
        base.Start();

        _gameManager.Player = this;
        _status = _gameManager.PlayerStatus;
    }

    public override void Damaged(int damage)
    {
        _status.Damaged(damage);
    }
}
