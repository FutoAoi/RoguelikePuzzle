using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(CardEncyclopedia))]
public class CardEncyclopediaEditor : Editor
{
    private int _selectedIndex = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CardEncyclopedia script = (CardEncyclopedia)target;
        List<CardData> cards = script.CardDatas.Cards;

        //カード選択UI作成
        string[] names = new string[cards.Count];
        for (int i = 0; i < cards.Count; i++)
        {
            names[i] = cards[i].Name;
        }
        GUILayout.Space(10);
        _selectedIndex = EditorGUILayout.Popup("カード選択", _selectedIndex, names);

        if (GUILayout.Button("選択カード生成"))
        {
            script.Generate(_selectedIndex);
        }

        GUILayout.Space(10);

        if (GUILayout.Button("全カード生成"))
        {
            Undo.RegisterFullObjectHierarchyUndo(script.gameObject, "Generate Cards");
            script.GenerateAll();
        }

        if (GUILayout.Button("全削除"))
        {
            script.ClearAll();
        }
    }
}
