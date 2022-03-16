using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStarisSC : MonoBehaviour
{
    public GameObject Doll;
    public GameObject Player;
    public GameObject StairsPrefab;
    public float Stair;


    void Update()
    {
        //BuildingStairs();
        StartCoroutine(BuildingStairs());
    }

    public IEnumerator BuildingStairs()
    {
        if(Player.GetComponent<PathFollower>().ClimbBool == true && (Doll.GetComponent<Stack>().StairCount > 0))
        {
            GameObject newStairs = Instantiate(StairsPrefab, transform.position, Doll.transform.rotation);
            Doll.GetComponent<Stack>().StairCount -= 1;
            Doll.GetComponent<Stack>().PlaceIt();
            Destroy(newStairs, 5f);
            yield return new WaitForSeconds(2f);
        }
    }
}
