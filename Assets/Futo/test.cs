using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] TileDataBase tileData;
    [SerializeField] TMP_Text TextMeshPro;
    void Start()
    {
        TextMeshPro.text = tileData.GetTileData(2).Description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
