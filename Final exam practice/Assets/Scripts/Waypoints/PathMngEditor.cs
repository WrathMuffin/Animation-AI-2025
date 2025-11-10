using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PathManager))] // directive tells unity to use this custom editor in inspector
public class PathMngEditor : Editor
// inheriting form Editor let me modify unity's GUI adn draw elemesnts and
// modify it in scene without running the game
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

    //  custom GUI in inspector
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

    // draw the points to movearound and delete/clear
    void DrawGUIForPoints()
    {
        if (thePath != null && thePath.Count > 0)
        {
            for (int i = 0; i < thePath.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                Waypoint p = thePath[i];

                Color c = GUI.color;

                if (selectedPoint == p) { GUI.color = Color.magenta; }

                Vector3 oldPos = p.GetPos();
                Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                if (EditorGUI.EndChangeCheck()) { p.SetPos(newPos); }

                // deleete button
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    // deletion
                    toDelete.Add(i); // add index to teh delete list
                }

                GUI.color = c;
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
                current ++;
            }
        }

        if (isRepaint) { Repaint(); }
    }

    public void DrawPathLine(Waypoint p1, Waypoint p2)
    {
        // draw line between two points (currpoint and next point)
        Color c = Handles.color;
        Handles.color = Color.yellow;
        Handles.DrawLine(p1.GetPos(), p2.GetPos());
        Handles.color = c;

    }

    public bool DrawPoint(Waypoint p)
    {
        bool isChanged = false;

        if (selectedPoint == p)
        {
            Color c = Handles.color;
            Handles.color = Color.yellow;

            EditorGUI.BeginChangeCheck();

            Vector3 oldPos = p.GetPos();
            Vector3 newPos = Handles.PositionHandle(oldPos, Quaternion.identity);

            float handleSize = HandleUtility.GetHandleSize(newPos);

            Handles.SphereHandleCap(-1, newPos, Quaternion.identity, handleSize * 0.25f, EventType.Repaint);

            if (EditorGUI.EndChangeCheck())
            {
                p.SetPos(newPos);
            }

            Handles.color = c;
        }

        else
        {
            Vector3 currPos = p.GetPos();
            float handleSize = HandleUtility.GetHandleSize(currPos);
            if (Handles.Button(currPos, Quaternion.identity, handleSize * 0.25f, handleSize * 0.25f, Handles.SphereHandleCap))
            {
                isChanged = true;
                selectedPoint = p;
            }
        }

        return isChanged;
    }
}