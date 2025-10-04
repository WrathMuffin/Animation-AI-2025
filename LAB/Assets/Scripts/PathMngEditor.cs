using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathManager))]
public class PathMngEditor : MonoBehaviour
{
        [SerializeField]
        PathManager pathManager;

    [SerializeField]
    List<Waypoint> thePath;
    List<int> toDelete;

    Waypoint selectedPoint = null;
    bool isRepaint = true;

    private void OnSceneGUI()
    {
        thePath = pathManager.GetPath();
        DrawPath(thePath);
    }

    private void OnEnable()
    {
        pathManager = target as PathManager;
        toDelete = new List<int>();
    }

    override public void OnInspectorGUI()
    {
        this.serializedObject.Update();
        thePath = pathManager.GetPath();

        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Path");

        DrawGUIForPoints();

        // button to add points
        if (GUILayout.Button("Add point to path"))
        {
            pathManager.CreateAddPoint();
        }

        EditorGUILayout.EndVertical();
        SceneView.RepaintAll();
    }

    void DrawGUIForPoints()
    {
        if (thePath != null && thePath.Count > 0)
        {
            for (int i = 0; i < thePath.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                Waypoint p = thePath[i];

                Vector3 oldPos = p.GetPos();
                Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                if (EditorGUI.EndChangeCheck()) { p.SetPos(newPos); }

                // deleete button
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    // deletion
                    toDelete.Add(i); // add index to delete list
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        if (toDelete.Count > 0)
        {
            foreach (int i in toDelete)
                thePath.RemoveAt(i); //remove from path
            
            toDelete.Clear(); // clear delete list for next time
        }
    }

    public void DrawPath(List<Waypoint> path)
    {
        // draw ui connecitng each point dots
        if (path != null)
        {
            int current = 0;
            
            foreach (Waypoint wp in path)
            {
                // draw curr point
                isRepaint = DrawPoint(wp);

                int next = (current + 1) % path.Count;
                Waypoint wpNext = path[next];

                DrawPathLine(wp, wpNext);

                // advance counter
                current += 1;
            }
        }

        if (isRepaint) { Repaint(); }
    }

    public bool DrawPoint(Waypoint p)
    {
        bool isChanged = false;

        if (selectedPoint = p)
        {
            Color col = Handles.color;
            Handles.color = Color.cyan;

            EditorGUI.BeginChangeCheck();

            Vector3 oldPos = p.GetPos();
            Vector3 newPos = Handles.PositionHandle(oldPos, Quaternion.identity);

            //float handleSize = Han
        }
    }
}

