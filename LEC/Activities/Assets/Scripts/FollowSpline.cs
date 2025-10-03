using UnityEngine;
using UnityEngine.Splines;

[System.Serializable] // dont reset the values when changed in inspector
public class FollowSpline : MonoBehaviour
{
    // grab object that ahs spline container (contains how many spline in the object)
    public float speed = 1.0f;

    private SplineContainer splineContainer;

    private Vector3 pos, dir;
    private float timeTakeToMoveOnSpline; // normalize time

    private void Start()
    {
        splineContainer = GetComponent<SwitchSpline>().splineContainer;
    }

    private void Update()
    {
        // calculate the time it takes to move on the spline (from one point to the other)
        timeTakeToMoveOnSpline += speed * Time.deltaTime / splineContainer.Spline.GetLength();

        // looping usign 0 (start) and 1 (ends)
        if (timeTakeToMoveOnSpline > 1.0f)
        {
            timeTakeToMoveOnSpline -= 1.0f; // loop back to 0
        }


        // pos and dir (rotate along) on the spline
        pos = SplineUtility.EvaluatePosition(splineContainer.Spline, timeTakeToMoveOnSpline);
        dir = SplineUtility.EvaluateTangent(splineContainer.Spline, timeTakeToMoveOnSpline);

        // update noe pos and rotation dir
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(dir);
    }
}
