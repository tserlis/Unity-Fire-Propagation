using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSR : StimResponseObject
{

    public List<GameObject> flamePoints;
    public GameObject flamePrefab;

    private GameObject fire;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void FireResponse()
    {
        if (!stims.Contains("Fire")) {
            AddStim("Fire");

            foreach (GameObject point in flamePoints)
            {
                fire = Instantiate(flamePrefab, point.transform);
            }
        }
    }

    override protected void WaterResponse() 
    {
        if (stims.Contains("Fire")) {
            fire.SetActive(false);
            RemoveStim("Fire");
        }
    }
}
