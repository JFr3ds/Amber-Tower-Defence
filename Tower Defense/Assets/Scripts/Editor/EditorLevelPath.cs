using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelPath))]
public class EditorLevelPath : Editor
{
    private LevelPath path;
    private void OnEnable()
    {
        path = target as LevelPath;
    }

    private void OnSceneGUI()
    {
        for (int i = 0; i <  path.points.Count; i++)
        {
            path.points[i] = Handles.DoPositionHandle(path.points[i], Quaternion.identity);
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(path.points[i], Vector3.up, path.minDistance);
            if (i > 0)
            {
                Handles.DrawLine(path.points[i], path.points[i-1]);
            }
        }
    }
}
