using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class StimResponseObject : MonoBehaviour
{

    public List<string> stims;
    private Dictionary<string, Action> globalStims;
    
    // Start is called before the first frame update
    protected void Start()
    {
        globalStims = new Dictionary<string, Action>();
        globalStims.Add("Fire", FireResponse);
        globalStims.Add("Water", WaterResponse);

        //Debug.Log("Global Stims contains: " + globalStims.Keys.Count + " stims");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void AddStim(string arg)
    {
        if (globalStims.ContainsKey(arg) && !stims.Contains(arg))
        {
            stims.Add(arg); 
        }
    }

    protected void RemoveStim(string arg) 
    {
        if (globalStims.ContainsKey(arg) && stims.Contains(arg)) {
            stims.Remove(arg);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("SRTriggered");
        if (collider.gameObject.GetComponent<StimResponseObject>())
        {
            List<string> otherStims = collider.gameObject.GetComponent<StimResponseObject>().stims;

            foreach (string stim in otherStims)
            {
                if (globalStims.ContainsKey(stim))
                {
                    globalStims[stim]();
                }
            }
        }
    }

#region Responses
    virtual protected void FireResponse()
    {
        
    }

    virtual protected void WaterResponse()
    {

    }

#endregion

}
