using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    [SerializeField]
    // reference to the path manager
    public PathManager pathManager;

    public Animator anime;

    // the path to follow
    List<Waypoint> thePath;
    Waypoint target; // current target waypoint

    Rigidbody rb;

    // movement and rotation parameters
    public float moveSpeed = 3f, rotSpeed = 1f;

    bool isWalk, isTouchWall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        isWalk = false;
        isTouchWall = false;

        anime.SetBool("isWalk", isWalk);

        thePath = pathManager.GetPath();

        // check if we have a valid path, if so, set the target to the first waypoint
        if (thePath != null && thePath.Count > 0)
        {
            // set the target to the first waypoint
            target = thePath[0];
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // toggle walking state when any key is pressed
            isWalk = !isWalk;
        }

        if (isWalk)
        {
            // walk when togglesd on
            RotateToTarget();
            MoveForward();

        }

        // if is touhcing wall, only idle is allowed
        if (isTouchWall)
        {
            anime.SetBool("isWalk", false);
        }

        // anythign else, allows the animation to be togleable
        else
        {
            anime.SetBool("isWalk", isWalk);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //switch to next target if we collide with the current target
        target = pathManager.GetNextTarget();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // idle when touching wall
        if (collision.gameObject.tag == "Wall")
        {
            isTouchWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // idle when not toichgin wall
        if (collision.gameObject.tag == "Wall")
        {
            isTouchWall = false;
        }
    }

    // method to rotate towards the target
    void RotateToTarget()
    {
        float stepSize = rotSpeed * Time.deltaTime;

        // determine the target direction by subtracting the current position from the target position
        Vector3 targetDir = target.pos - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);

        // rotate towards atrget
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void MoveForward()
    {
        // determine the step size using speed and time between frames
        float stepSize = moveSpeed * Time.deltaTime;
        float distToTarget = Vector3.Distance(transform.position, target.pos);

        // check if we are close enough to the target, and if so, do not move
        if (distToTarget < stepSize + .05f)
        {
            // wont stop rotating around the target!!
            // added a small buffer to the step size, so we dont jitter around the target
            // basically increase the point's range (learned this in spline activity!)
            return;
        }

        // move forward
        rb.MovePosition(transform.position + transform.forward * stepSize);
    }
}