using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// round info that is transferred between scenes.
public class RoundInfo : MonoBehaviour
{
    // the amount of rounds passed.
    public int passedRounds = 0;

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
