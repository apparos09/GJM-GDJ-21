using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// round info that is transferred between scenes.
// this is instantiated when a round ends, provides values to the next round, and is destroyed once that round begins.
public class RoundInfo : MonoBehaviour
{
    // the round manager for the round info
    public RoundManager roundManager;

    // the amount of rounds passed.
    public int clearedRounds = 0;

    // goal
    public Goal goal;

    // Start is called before the first frame update
    void Start()
    {
        // don't destory this object
        DontDestroyOnLoad(this);

        // finds round manager
        if (roundManager == null)
            roundManager = FindObjectOfType<RoundManager>();

        // finds the goal.
        if (goal == null)
            goal = FindObjectOfType<Goal>();
    }

    // sets up the round (looks for round manager if not set)
    public void InitializeRound()
    {
        // round manager not set.
        if(roundManager == null)
            roundManager = FindObjectOfType<RoundManager>();

        // no round manager.
        if (roundManager == null)
        {
            Debug.LogError("No Round Manager Found. Cannot Initialize Round.");
            return;
        }
        else
        {
            // initialize the round
            InitializeRound(roundManager);
        }
    }

    // initializes the scene and uses the round manager provided.
    public void InitializeRound(RoundManager rm)
    {
        // set round manager.
        roundManager = rm;

        // no goal set, so find it.
        if(goal == null)
            goal = FindObjectOfType<Goal>();

        // the goal needs to be made.
        if(goal == null)
        {
            Debug.LogError("Attempting to Make Goal.");
            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Goal"));
            goal = temp.GetComponent<Goal>();
        }

        // no goal was found, and no goal could be made.
        if(goal == null)
        {
            Debug.LogError("Goal does not exit. Cannot initialize.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
