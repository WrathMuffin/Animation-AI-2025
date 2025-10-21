using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{
    public GameObject target;

    private NavMeshAgent agent;
    private Animator animator;

    public float navSpeed = 1.5f;
    public float clipSpeed;
    bool isWalking = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isWalking)
        {
            agent.destination = target.transform.position;
        }

        else 
        {
            agent.destination = transform.position;
        }

        if (Input.GetKey(KeyCode.E))
        {
            clipSpeed = navSpeed;
        }

        animator.speed = clipSpeed;
        agent.speed = navSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Target")
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
}
