using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

[System.Serializable] // dont reset the values when changed in inspector
public class SwitchSpline : MonoBehaviour
{
    // grab object that ahs spline container (contains how many spline in the object)
    public SplineContainer splineContainer;

    // how fast the chaarcter moves, how close the cistance of my character to teh knot to transition to the secon spline
    public float speed = 1.0f, distToKnot = 0.5f;

    // when character is near the end knot of spline 0, theyll go to start knot of spline 1
    // vice versa when near the end knot oof spline 1
    public int endKnot0, startKnot1,
               endKnot1, startKnot0;

    public KeyCode swapSpline = KeyCode.E;

    // two spline in spline container
    private Spline spline0, spline1;

    // update characters pos and face direction
    private Vector3 pos, dir;

    // positions of the knots from the public variables of the knots
    private Vector3 endKnot0Pos, starKnot1Pos,
                    endKnot1Pos, starKnot0Pos;

    private float timeTakeToMoveOnSpline; // normalize so the character is runnign at cinsistent speed on the splein

    // toggle switch, if is on the initial spline (spline0/first spline)
    private bool isSwitch = false, isInitSpline = true, isDoLerp = false;

    private void Start()
    {
        splineContainer = GetComponent<SwitchSpline>().splineContainer;

        spline0 = splineContainer.Splines[0];
        spline1 = splineContainer.Splines[1];

        BezierKnot[] knots0 = spline0.ToArray();
        BezierKnot[] knots1 = spline1.ToArray();

        endKnot0Pos = knots0[endKnot0].Position;

        starKnot1Pos = knots1[startKnot1].Position;

        endKnot1Pos = knots1[endKnot1].Position;

        starKnot0Pos = knots0[startKnot0].Position;
    }

    private void Update()
    {
        // swap spline when key pressed
        if (Input.GetKeyDown(swapSpline))
        {
            isSwitch = !isSwitch;
            Debug.Log("SWITCH TO SPLINE: " + isSwitch.ToString());
        }

        // toggle
        if (isSwitch)
        {
            // check if character is near the knot (interesction point)
            if (Vector3.Distance(transform.position, endKnot0Pos) <= distToKnot)
            {
                // no longer on spline 0, reset normal (this was causing the caracter to teleport to the next point instead of starting at knot  0
                isDoLerp = true;

                isInitSpline = false;

                if (Vector3.Distance(transform.position, starKnot1Pos) <= distToKnot)
                {
                    isDoLerp = false;
                }

                ResetNormal();
            }

            if (Vector3.Distance(transform.position, endKnot1Pos) <= distToKnot)
            {
                isDoLerp = true;

                isInitSpline = true;

                if (Vector3.Distance(transform.position, starKnot0Pos) <= distToKnot)
                {
                    isDoLerp = false;
                }

                ResetNormal();
            }
        }

        if (!isSwitch)
        {
            if (Vector3.Distance(transform.position, endKnot1Pos) <= distToKnot)
            {
                isInitSpline = true;
            }
        }

        if (isDoLerp)
        {
            if (!isInitSpline)
            {
                transform.position = Vector3.Lerp(endKnot0Pos, starKnot1Pos, timeTakeToMoveOnSpline);
            }

            if (isInitSpline)
            {
                transform.position = Vector3.Lerp(endKnot1Pos, starKnot0Pos, timeTakeToMoveOnSpline);
            }
        }

        if (isInitSpline)
        {
            MoveOnSpline(spline0);
        }

        if (!isInitSpline)
        {
            MoveOnSpline(spline1);
        }
    }

    void MoveOnSpline(Spline num)
    {
        // calculate the time it takes to move on the spline (from one point to the other)
        timeTakeToMoveOnSpline += speed * Time.deltaTime / num.GetLength();

        if (!isDoLerp)
        {
            // checks if the spline is closed, if so then loop it (spline 1 is not a closed loop)
            if (num.Closed)
            {
                // looping usign 0 (start) and 1 (ends)
                if (timeTakeToMoveOnSpline > 1.0f)
                {
                    timeTakeToMoveOnSpline -= 1.0f; // loop back to 0
                }
            }

            // pos and dir (rotate along) on the spline
            pos = SplineUtility.EvaluatePosition(num, timeTakeToMoveOnSpline);
            dir = SplineUtility.EvaluateTangent(num, timeTakeToMoveOnSpline);

            // update noe pos and rotation dir
            transform.position = pos;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    // reset normal,
    // it was in its own function becuase it was used for debugging purpose, now i dont want to move it out... EVER
    void ResetNormal()
    {
        timeTakeToMoveOnSpline = 0f;
    }
}