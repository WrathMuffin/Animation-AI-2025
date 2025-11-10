using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{
    public GameObject target;

    private NavMeshAgent agent;
    //private Animator animator;

    //public float navSpeed = 1.5f;
    //public float clipSpeed, rotSpeed = 1f;
    //bool isWalking = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (isWalking)
        //{
            agent.destination = target.transform.position;
        //}

        //else
        //{
            //agent.destination = transform.position;
        //}

        //if (Input.GetKey(KeyCode.E))
        //{
            //clipSpeed = navSpeed;
        //}

        //RotateToTarget();

        //animator.speed = clipSpeed;
        //agent.speed = navSpeed;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Target")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Target")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Target")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }

    // method to rotate towards the target
    void RotateToTarget()
    {
        float stepSize = rotSpeed * Time.deltaTime;

        // determine the target direction by subtracting the current position from the target position (parent of the target spheres)
        Vector3 targetDir = target.transform.parent.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, stepSize, 0.0f);

        // rotate towards atrget
        transform.rotation = Quaternion.LookRotation(newDir);
    }
    */
}