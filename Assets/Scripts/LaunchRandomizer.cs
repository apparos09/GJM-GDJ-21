using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomizers the launcher's values.
public class LaunchRandomizer : MonoBehaviour
{
    // the launcher
    Launcher launcher;

    // random rotation (spins object)
    public bool randomRot;

    // random position
    public bool randomPosX;
    public bool randomPosY;

    // random power (increaseas and decreases)
    public bool randomPower;
    

    // Start is called before the first frame update
    void Start()
    {
        // launcher not set.
        if (launcher == null)
            launcher = FindObjectOfType<Launcher>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
