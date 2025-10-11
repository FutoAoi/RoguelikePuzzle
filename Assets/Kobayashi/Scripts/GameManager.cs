using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private TileHand _tileHand;
    public Transform HandArea;
    public GameObject CardPrefab;
    [SerializeField] private TileDataBase _tileData;

    public TileDataBase TileDataBase => _tileData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _tileHand.HandOrganize();
        }
    }
}
