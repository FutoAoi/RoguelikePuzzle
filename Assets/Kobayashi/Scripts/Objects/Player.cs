using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.Player = this;
    }
    
}
