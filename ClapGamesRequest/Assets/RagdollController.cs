using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RagdollController : MonoBehaviour
{
    //************************************************
    //2 Farklı Yöntem denedim ancak ragdoll konusunda başarılı olamadım, bu konuda zorlandım. RagDoll yerine Sad animasyonunu kullandım. RagDoll'da ise geldiğim yere kadar da bıraktım, inceleyebilirsiniz...
    //************************************************

    public BoxCollider mainCollider;
    public GameObject ThisGuyRig;
    public Animator ThisGuyAnim;
    Collider[] ragdollColliders;
    Rigidbody[] limbsRigidBodies;

    public CapsuleCollider DontTouchIt;

    public bool hit;
    public bool Final = false;

    public GameObject Player;
    public GameObject GroundCheck;
    public GameObject StairBuilder;

    public bool x3 = false;
    public bool x10 = false;

    public int Coins;
    public Text CoinTxt;

    private void Start()
    {
        if (PlayerPrefs.HasKey("coins"))
        {
            Coins = PlayerPrefs.GetInt("coins");
            CoinTxt.text = "COINS: " + Coins;
        }
        GetRagdollBits();
        RagDollModeOff();
    }
    void GetRagdollBits()
    {
        ragdollColliders = ThisGuyRig.GetComponentsInChildren<Collider>();
        limbsRigidBodies = ThisGuyRig.GetComponentsInChildren<Rigidbody>();
    }
    /*void RagDollModeOn()
    {
        ThisGuyAnim.enabled = false;
        Player.GetComponent<PathFollower>().enabled = false;
        GroundCheck.GetComponent<ColliderSc>().enabled = false;
        StairBuilder.GetComponent<BuildStarisSC>().enabled = false;
        foreach (Collider col in ragdollColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in limbsRigidBodies)
        {
            rigid.isKinematic = true;
        }

        mainCollider.enabled = false;
        
        GetComponent<Rigidbody>().isKinematic = true;
    }*/

    void RagDollModeOff()// Disable RagDoll
    {
        foreach(Collider col in ragdollColliders)
        {
            col.enabled = false;
        }
        DontTouchIt.enabled = true;
        foreach (Rigidbody rigid in limbsRigidBodies)
        {
            rigid.isKinematic = true;
        }

        ThisGuyAnim.enabled = true;
        mainCollider.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            hit = true;
            ThisGuyAnim.SetBool("Fail", hit);
            Player.GetComponent<PathFollower>().enabled = false;
            GetComponent<Stack>().StairCount = 0;
            GroundCheck.GetComponent<ColliderSc>().enabled = false;
            //RagDollModeOn();
        }

        if(other.gameObject.tag == "Final")
        {
            Final = true;
        }
        

        if(other.gameObject.tag == "x3")
        {
            x3 = true;
        }
        else x3 = false;
        if (other.gameObject.tag == "x10")
        {
            x10 = true;
        }
        else x10 = false;

        if(other.gameObject.tag == "Coins")
        {
            Destroy(other.gameObject);
            Coins += 1;
            CoinTxt.text = "COINS: " + Coins;
            PlayerPrefs.SetInt("coins", Coins);
            
        }
    }

    

    /*public GameObject Player;
    public GameObject GroundCheck;

    void Start()
    {
        //setRigidbodyState(true);
    }

    // Update is called once per frame
    void Update()
    {
        //FailFunc();
    }

    /*void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
            rigidbody.useGravity = !state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }
    public void FailFunc()
    {
        if(GroundCheck.GetComponent<ColliderSc>().hit == true)
        {
            setRigidbodyState(false);
            Player.GetComponent<PathFollower>().enabled = false;
            GroundCheck.GetComponent<ColliderSc>().enabled = false;
            this.GetComponent<Animator>().enabled = false;
            setRigidbodyState(false);
        }
        
    }*/
    public void NextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
