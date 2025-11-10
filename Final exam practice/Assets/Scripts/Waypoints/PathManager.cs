using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]

    public List<Waypoint> path;
    public List<GameObject> prefabPoints;

    public GameObject spherePrefab;

    int currPointIndex = 0;

    private void Start()
    {
        prefabPoints = new List<GameObject>();

        foreach (Waypoint wp in path)
        {
            GameObject go = Instantiate(spherePrefab);
            go.transform.position = wp.pos;
            prefabPoints.Add(go);
        }
    }

    private void Update()
    {
        // updat all rpefabs to waypoint locations
        for (int i = 0; i < path.Count; i++)
        {
            Waypoint wp = path[i];
            GameObject go = prefabPoints[i];
            go.transform.position = wp.pos;
        }
    }

    public List<Waypoint> GetPath()
    {
        if (path == null) { path = new List<Waypoint>(); }

        return path;
    }

    public void CreateAddPoint()
    {
        Waypoint go = new Waypoint();

        path.Add(go);
    }

    public Waypoint GetNextTarget()
    {
        int nextPointIndex = (currPointIndex + 1) % path.Count;
        currPointIndex = nextPointIndex;

        return path[nextPointIndex];
    }
}
