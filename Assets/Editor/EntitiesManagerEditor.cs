using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntitiesManager)), CanEditMultipleObjects]
public class EntitiesManagerEditor : Editor
{
    private Vector2 position;
    private float innerRadius;
    private float outerRadius;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EntitiesManager manager = (EntitiesManager) target;
        EditorGUILayout.LabelField("Spawn");
        position = EditorGUILayout.Vector2Field("Position", position);
        innerRadius = EditorGUILayout.FloatField("Inner Radius", innerRadius);
        outerRadius = EditorGUILayout.FloatField("Outer Radius", outerRadius);
        if (GUILayout.Button("Spawn At Position"))
        {
            manager.CreateAtLocation(position);
        }
        if (GUILayout.Button("Spawn At Ring"))
        {
            manager.CreateInRing(position, innerRadius, outerRadius);
        }
        if (GUILayout.Button("Spawn Random"))
        {
            manager.CreateAtLocation(Random.insideUnitCircle * 400 + Vector2.up * 500);
        }
    }
}
