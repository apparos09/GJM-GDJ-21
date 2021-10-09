using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the round manager
public class RoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // called when the player scores.
    public void Scored()
    {
        Debug.Log("Score!");
    }

    // called when the playerm isses.
    public void Missed()
    {
        Debug.Log("Miss!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
