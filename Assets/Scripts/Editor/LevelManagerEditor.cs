using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelManager))]

public class LevelManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var script = (LevelManager)target;

        script.IsTimedLevel = EditorGUILayout.Toggle("Victory is timed", script.IsTimedLevel);

        if (script.IsTimedLevel)
        {
            script.TimeToComplete = EditorGUILayout.FloatField("Level time limit", script.TimeToComplete);
            return;
        }
        else
        {
            script.ScoreToReach = EditorGUILayout.IntField("Enemies to kill", script.ScoreToReach);
        }
    }
}
