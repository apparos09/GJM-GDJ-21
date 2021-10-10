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

    // launcher
    public Launcher launcher;

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

        // FINDING VALUES //

        // the launcher set
        if (launcher == null)
            launcher = FindObjectOfType<Launcher>();

        // the launcher needs to be made.
        if (launcher == null)
        {
            Debug.LogError("Attempting to make Launcher.");
            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Launcher"));
            launcher = temp.GetComponent<Launcher>();
        }

        // no goal set, so find it.
        if (goal == null)
            goal = FindObjectOfType<Goal>();

        // the goal needs to be made.
        if(goal == null)
        {
            Debug.LogError("Attempting to make Goal.");
            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Goal"));
            goal = temp.GetComponent<Goal>();
        }

        // set launcher position
        if(launcher != null)
        {
            // find possible positions
            LauncherSpot[] ls = FindObjectsOfType<LauncherSpot>();
            
            // if there is a position to find.
            if (ls.Length != 0)
            {
                int index = Random.Range(0, ls.Length);
                launcher.transform.position = ls[index].transform.position;
            }
        }

        // set goal position
        if(goal != null)
        {
            // find possible positions
            GoalSpot[] gs = FindObjectsOfType<GoalSpot>();

            // if there is a position to find.
            if (gs.Length != 0)
            {
                int index = Random.Range(0, gs.Length);
                goal.transform.position = gs[index].transform.position;
            }
        }

        // TODO: if time allows, check to see if there's a ball in the launcher.
        // this would only be needed for scenes with multiple balls.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
