using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private void Start()
    {
        GameManager.Instance.Player = this;
    }
}
