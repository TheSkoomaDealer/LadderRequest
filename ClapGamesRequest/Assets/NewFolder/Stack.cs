using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public Stack instance;

    [SerializeField] private float distanceBetweenObjects;
    [SerializeField] private Transform previousObj;
    [SerializeField] private Transform parent;

    public int StairCount;
    public GameObject Referance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        distanceBetweenObjects = previousObj.localScale.y;
    }

    public void PickUp(GameObject Stair, bool needTag = false, string tag = null, bool downOrUp = true)
    {
        if (previousObj == null)///***LookHere
        {
            previousObj = Referance.transform;
        }
        Stair.transform.parent = parent;
        Vector3 desPos = previousObj.localPosition;
        desPos.z += downOrUp ? distanceBetweenObjects : -distanceBetweenObjects;
        StairCount += 1;
        Stair.transform.localPosition = desPos;
        previousObj = Stair.transform;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Stairs")
        {
            PickUp(other.gameObject, true, "Stair", true);
        }
    }

    public void PlaceIt()
    {
        if(parent.transform.childCount > StairCount)
        {
            Destroy(parent.GetComponent<Transform>().GetChild(StairCount).gameObject);
        }
    }
}
