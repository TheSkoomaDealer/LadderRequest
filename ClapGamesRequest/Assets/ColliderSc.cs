using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSc : MonoBehaviour
{
    public Rigidbody rig;
    public Animator animator;
    public GameObject Player;
    public GameObject Doll;
    public float distToGround = 0.5f;
    public bool grounded;
    public bool hit;

    public GameObject Stack;
    public GameObject Stairs1Prefab;

    public bool sendS;

    private void Start()
    {
        rig = Doll.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = Doll.transform.position;
        
    }

    private void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, Vector3.down, distToGround))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        GroundedOrNot();
    }

    private void GroundedOrNot()
    {
        if (grounded == true)
        {
            animator.SetBool("Grounded", true);
        }
        else if(grounded == false  && Player.GetComponent<PathFollower>().ClimbBool == true)
        {
            animator.SetBool("Grounded", true);
            sendS = true;
        }
        else if(grounded == false && Player.GetComponent<PathFollower>().ClimbBool == false)
        {
            animator.SetBool("Grounded", false);
            rig.useGravity = true;
            sendS = false;
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stairs")
        {
            Destroy(other);
            Player.GetComponent<PathFollower>().Stairs += 10;
            GameObject Stack = Instantiate(Stairs1Prefab, transform.position, Quaternion.identity);
        }
    }*/
}
