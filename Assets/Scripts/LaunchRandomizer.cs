using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomizers the launcher's values.
public class LaunchRandomizer : MonoBehaviour
{
    // if 'true', the randomizer is enabled.
    public bool enableRandomizer = true;

    // the launcher
    Launcher launcher;

    // if 'true', launcher controls that the randomizer has control over are destroyed.
    public bool disableLauncherControls = true;

    [Header("Rotation")]
    // random rotation (spins object)
    public bool randomRot;
    public float factor = 10.0F; // rotation factor
    public int direc = 0; // direction (-1 or 1)

    [Header("Position")]
    // random position
    public bool randomPosX;
    public bool randomPosY;

    [Header("Power")]
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
        // randomizer is not enabled, so return.
        if(!enableRandomizer)
        {
            return;
        }

        // if no launcher is set.
        if(launcher == null)
        {
            Debug.Log("No Launcher Set");
            launcher = FindObjectOfType<Launcher>();

            // launch set
            if(launcher == null)
            {
                Debug.Log("No Launcher Found");
                return;
            } 
        }

        // randomizes the rotation
        if(randomRot)
        {

        }

        // randomizes the position X
        if(randomPosX)
        {

        }

        // randomizes the position Y
        if (randomPosY)
        {

        }

        // randomizes power
        if(randomPower)
        {

        }
    }
}
