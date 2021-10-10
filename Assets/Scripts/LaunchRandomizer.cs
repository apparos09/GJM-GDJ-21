using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// randomizers the launcher's values.
public class LaunchRandomizer : MonoBehaviour
{
    // if 'true', the randomizer is enabled.
    public bool enableRandomizer = true;

    // the launcher
    public Launcher launcher;

    // if 'true', launcher controls that the randomizer has control over are destroyed.
    public bool disableControls = true;

    [Header("Rotation")]
    // random rotation (spins object)
    public bool randomRot;
    public float rotSpeed = 200.0F; // rotation factor
    public int rotDirec = 0; // direction (-1 or 1)

    [Header("Position")]
    // random position
    public bool randomPosX;
    public bool randomPosY;

    [Header("Power")]
    // random power (increaseas and decreases)
    public bool randomPower;
    public float powerSpeed = 60.0F;
    public int powerDirec = -1;// power change (+/-)

    // Start is called before the first frame update
    void Start()
    {
        // launcher not set.
        if (launcher == null)
        {
            // searches for launcher component on current object.
            launcher = gameObject.GetComponent<Launcher>();

            // if the launcher is still false, search the whole scene.
            if(launcher == null)
                launcher = FindObjectOfType<Launcher>();
        }

        // could not find launcher, and was not given one.
        if (launcher == null)
            Debug.LogError("No Launcher Set or Found.");
    }

    // randomizes the rotation direction
    protected void RandomizeRotationDirection()
    {
        // number
        int num = Random.Range(0, 2);

        // choice
        switch (num)
        {
            default:
            case 0: // left (clockwise)
                rotDirec = -1;
                break;

            case 1: // right (counter-clockwise)
                rotDirec = 1;
                break;
        }

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
            // if 'true', disable the launcher rotation control.
            if (disableControls)
                launcher.controlRot = false;


            // randomizes the rotation direction.
            if (rotDirec == 0)
                RandomizeRotationDirection();

            // rotates
            launcher.transform.Rotate(0.0F, 0.0F, rotSpeed * rotDirec * Time.deltaTime);
        }

        // randomizes the position X
        if(randomPosX)
        {
            // if 'true', disable the launcher movement control.
            if (disableControls)
                launcher.controlMove = false;
        }

        // randomizes the position Y
        if (randomPosY)
        {
            // if 'true', disable the launcher movement control.
            if (disableControls)
                launcher.controlMove = false;
        }

        // randomizes power
        if(randomPower)
        {
            // random power
            if (disableControls)
                launcher.controlPower = false;

            // gets the value
            float value = launcher.launchPower + powerSpeed * powerDirec * Time.deltaTime;

            // clamp
            value = Mathf.Clamp(value, launcher.powerLimits.x, launcher.powerLimits.y);

            // end of bar reached.
            if (value <= launcher.powerLimits.x || value >= launcher.powerLimits.y)
                powerDirec *= -1;

            // set launch power.
            launcher.launchPower = value;
            
        }
    }
}
