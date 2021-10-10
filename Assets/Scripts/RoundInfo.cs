using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// round info that is transferred between scenes.
// this is instantiated when a round ends, provides values to the next round, and is destroyed once that round begins.
public class RoundInfo : MonoBehaviour
{
    // the amount of rounds passed.
    public int clearedRounds = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
