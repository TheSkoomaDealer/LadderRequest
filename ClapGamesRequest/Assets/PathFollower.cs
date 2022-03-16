using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;
using UnityEngine.SceneManagement;

public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    float distanceTravelled;

    private float climbHeight = 0.3f;
    private Vector3 newPos;
    public GameObject Doll;
    public GameObject StairsPrefab;
    public bool ClimbBool;
    public Rigidbody rig;
    public Animator animator;
    public GameObject startPanel;//Start
    public GameObject FailPanel;//GameOver
    public GameObject WinPanel;//Win
    public Text WinTxt;//Win
    public GameObject GroundCheck;
    public GameObject StairBuilder;
    private int Stairs;


    public GameObject x3;
    public GameObject x10;

    void Update()
    {
        Stairs = Doll.GetComponent<Stack>().StairCount;

        if(Stairs == 0)
        {
            Fall();
        }
        if (ClimbBool && (Stairs > 1) && Doll.GetComponent<RagdollController>().Final == false)//When button pressed
        {
            climbHeight += Mathf.Lerp(0, 3, 2 * Time.deltaTime);//Climb Height Increase Rate
        }
        else if(climbHeight >= 0.05f && GroundCheck.GetComponent<ColliderSc>().sendS == false && Doll.GetComponent<RagdollController>().Final == false)//When relased and falled to ground, it prevents to fall more
        {
            climbHeight -= 0.05f;
            Fall();
        }
        
        

        if(Doll.GetComponent<RagdollController>().Final == false)
        {
            distanceTravelled += speed * Time.deltaTime;
            newPos = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            transform.position = new Vector3(newPos.x, climbHeight, newPos.z);
        }

        FinalMoment();

    }

    public void climbing()//Button pressed
    {
        ClimbBool = true;
        rig.useGravity = false;
    }

    public void climbing2()
    {
        rig.velocity = new Vector3(0, 0, 0);//For rebuild stairs while falling
    }

    public void Fall()//Button Relased
    {
        ClimbBool = false;
        rig.useGravity = true;
    }

    public void startGame()
    {
        speed = 2;
        animator.SetBool("Start", true);
        startPanel.SetActive(false);
    }

    public void FinalMoment()
    {
        if(Doll.GetComponent<RagdollController>().Final == true)
        {
            if (Stairs >= 1 && Doll.GetComponent<RagdollController>().x10 == false && Doll.GetComponent<RagdollController>().x3 == false)
            {
                speed = 0.05f;
                ClimbBool = true;
                rig.useGravity = false;
                StairBuilder.GetComponent<BuildStarisSC>().BuildingStairs();
                transform.position += new Vector3(speed, 0, 0);
            }
            else if (Stairs >= 1 && Doll.GetComponent<RagdollController>().x3 == true)
            {
                speed = 0.05f;
                ClimbBool = true;
                rig.useGravity = false;
                StairBuilder.GetComponent<BuildStarisSC>().BuildingStairs();
                transform.position += new Vector3(speed, 0, 0);
            }
            else if (Stairs == 0 && Doll.GetComponent<RagdollController>().x3 == true && Doll.GetComponent<RagdollController>().x10 == false)
            {
                speed = 0;
                animator.SetBool("Finish", true);
                WinPanel.SetActive(true);
                WinTxt.text = "x3 Score! Press to next scene";

            }
            else if (Doll.GetComponent<RagdollController>().x10 == true)
            {
                speed = 0;
                animator.SetBool("Finish", true);
                WinPanel.SetActive(true);
                WinTxt.text = "x10 Score!  Press to next scene";
            }
            else if(Stairs == 0 && Doll.GetComponent<RagdollController>().x3 == false && Doll.GetComponent<RagdollController>().x10 == false)
            {
                speed = 0;
                animator.SetBool("Fail", true);
                FailPanel.SetActive(true);
            }
            
        }
    }
    
}
