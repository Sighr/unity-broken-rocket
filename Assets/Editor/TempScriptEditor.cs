using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TempScript))]
public class TempScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        TempScript script = (TempScript) target;
        if (GUILayout.Button("Set cloud scripts"))
        {
            if (script.settings == null)
                return;
            
            foreach (Transform transform in script.gameObject.transform)
            {
                var cloudScript = transform.gameObject.AddComponent<CloudScript>();
                cloudScript.settings = script.settings;
            }
        }
    }
}