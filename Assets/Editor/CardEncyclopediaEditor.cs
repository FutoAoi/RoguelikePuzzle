using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardEncyclopedia))]
public class CardEncyclopediaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CardEncyclopedia script = (CardEncyclopedia)target;
        GUILayout.Space(10);

        if (GUILayout.Button("ê}ä”Çê∂ê¨"))
        {
            script.Generate();
        }
    }
}
