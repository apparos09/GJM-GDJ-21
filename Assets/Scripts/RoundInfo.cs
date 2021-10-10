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

    // length of the round.
    public float roundLength = 30.0F;

    // launcher
    public Launcher launcher;

    // goal
    public Goal goal;

    // ball
    public Ball ball;

    // layout options
    public const int LAYOUT_OPTIONS = 3;

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
        // the spot the launcher will be on.
        LauncherSpot launchSpot = null;

        // set round manager.
        roundManager = rm;

        // FINDING VARIABLES //

        // the launcher set
        if (launcher == null)
            launcher = FindObjectOfType<Launcher>();

        // the launcher needs to be made.
        if (launcher == null)
        {
            Debug.LogError("Attempting to make launcher.");
            GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Launcher"));
            launcher = temp.GetComponent<Launcher>();

            // shift launcher position
            launcher.transform.Translate(12.0F, 0.0F, 0.0F);
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

            // shift launcher position
            goal.transform.Translate(-12.0F, 0.0F, 0.0F);
        }

        // checks for the ball.
        if (ball == null)
        {
            // finds balls
            Ball[] balls = FindObjectsOfType<Ball>();

            // checks balls to find launcher ball
            for(int i = 0; i < balls.Length; i++)
            {
                if(balls[i].launcherBall)
                {
                    ball = balls[i];
                    break;
                }
            }

            // ball not found, so instantiate new one.
            if(ball == null)
            {
                GameObject temp = (GameObject)Instantiate(Resources.Load("Prefabs/Ball"));
                ball = temp.GetComponent<Ball>();
                ball.launcherBall = true;
            }
        }

        // SETTING UP STAGE
        InitializeRoundLayout();

        // SETTING POSITIONS //
        
        // set launcher position
        if(launcher != null)
        {
            // find possible positions
            LauncherSpot[] ls = FindObjectsOfType<LauncherSpot>(false);
            
            // if there is a position to find.
            if (ls.Length != 0)
            {
                int index = Random.Range(0, ls.Length);
                launchSpot = ls[index];
                launcher.transform.position = launchSpot.transform.position;
            }
        }

        // set goal position
        if(goal != null)
        {
            // find possible positions
            GoalSpot[] gs = FindObjectsOfType<GoalSpot>(false);

            // if there is a position to find.
            if (gs.Length != 0)
            {
                int index = Random.Range(0, gs.Length);
                goal.transform.position = gs[index].transform.position;
            }
        }

        // DIFFICULTY FACTORS
        InitializeDifficulty(launchSpot);
    }

    // initializes the round layout
    public void InitializeRoundLayout()
    {
        // round layout
        int layoutOption = Random.Range(0, LAYOUT_OPTIONS) + 1;

        // layout object
        GameObject layout;

        // checks to see which map to load.
        switch(layoutOption)
        {
            default:
            case 0:
            case 1: // empty 1
                layout = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/Empty Stage 1"));
                break;
            case 2: // empty 2
                layout = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/Empty Stage 2"));
                break;
            case 3: // walled stage 1
                layout = (GameObject)Instantiate(Resources.Load("Prefabs/Stages/Walled Stage 1"));
                break;
        }
    }

    // initialize round difficulty
    public void InitializeDifficulty()
    {
        InitializeDifficulty(null);
    }

    // initializes the difficulty of the round
    public void InitializeDifficulty(LauncherSpot launchSpot)
    {
        int mode = Random.Range(0, 6);

        // gets the randomizer
        LaunchRandomizer lr = FindObjectOfType<LaunchRandomizer>();

        // gets the randomizer if it wasn't found.
        if (lr == null && launcher != null)
            lr = launcher.GetComponent<LaunchRandomizer>();

        // launch randomizer found.
        if (lr != null)
        {
            // settings
            lr.enableRandomizer = true;
            lr.disableControls = true;

            // launcher;
            switch (mode)
            {
                default:
                case 0: // no handicap
                    break;

                case 1: // disable move
                    launcher.controlMove = false;
                    break;

                case 2: // moving launcher (x)
                    // ran out of time
                    break;

                case 3: // moving launcher (y)
                    // ran out of time
                    break;

                case 4: // random rotation left
                    lr.randomRot = true;
                    // direction is randomly set when rotation is applied.
                    break;

                case 5: // random power
                    lr.randomPower = true;
                    break;
            }
        }


        // changing round length.
        if (clearedRounds >= 30)
        {
            roundLength = 8.0F;
        }
        else if (clearedRounds >= 25)
        {
            roundLength = 10.0F;
        }
        else if(clearedRounds >= 20)
        {
            roundLength = 15.0F;
        }
        else if (clearedRounds >= 15)
        {
            roundLength = 20.0F;
        }
        else if (clearedRounds >= 10)
        {
            roundLength = 25.0F;
        }
        else if (clearedRounds >= 5)
        {
            roundLength = 30.0F;
        }
        else // default
        {
            roundLength = 30.0F;
        }
    }
}
