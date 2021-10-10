using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// the round manager
public class RoundManager : MonoBehaviour
{
    // the round number
    public int roundNumber = 1;

    // the round launcher
    public Launcher launcher;

    [Header("Timer")]
    // timer is active.
    public bool activeTimer = true;

    // timer value.
    public float timer = 30;

    // start of the timer.
    public float timeStart = 30;

    // timer text object.
    public TMPro.TextMeshProUGUI timerText;

    // round number
    public TMPro.TextMeshProUGUI roundText;

    // Start is called before the first frame update
    void Start()
    {
        // if the launcher isn't set
        if (launcher == null)
            launcher = FindObjectOfType<Launcher>();


        // looks for round info object.
        RoundInfo roundInfo = FindObjectOfType<RoundInfo>();

        // if round info has been found.
        if(roundInfo != null)
        {
            roundInfo.InitializeRound(this); // initializes the round.
            roundNumber = roundInfo.clearedRounds + 1; // round number
            timeStart = roundInfo.roundLength;
        }

        // set timer value.
        timer = Mathf.Abs(timeStart);

        // destroys the round info object.
        if(roundInfo != null)
            Destroy(roundInfo.gameObject);
    }

    // gets the round number.
    public int GetRoundNumber()
    {
        return roundNumber;
    }

    // sets the round number, which determines the round behaviour.
    public void SetRoundNumber(int rNum)
    {
        // negative round number provided.
        if (rNum < 0)
            roundNumber = 1;
    }

    // resets the length of the round
    public void ResetTimer()
    {
        timer = Mathf.Abs(timeStart);
    }

    // instantiate round info object.
    private RoundInfo InstantiateRoundInfo()
    {
        // grabs prefab for round info, and gets the component.
        Object prefab = Resources.Load("Prefabs/Round Info");
        // object for round info component
        GameObject riObject = GameObject.Find("Round Info");
        // component for round info
        RoundInfo riComp;

        // prefab not found, so make object.
        if (prefab == null && riObject == null)
        {
            riObject = new GameObject("Round Info");
        }
        else
        {
            riObject = (GameObject)Instantiate(prefab);
        }

        // grabs component
        riComp = riObject.GetComponent<RoundInfo>();

        // prefab did not have component.
        if (riComp == null)
        {
            riComp = riObject.AddComponent<RoundInfo>();
        }

        // returns round component.
        return riComp;
    }

    // goes to the next round.
    public void NextRound()
    {
        // makes round info object.
        RoundInfo roundInfo = InstantiateRoundInfo();

        // saves as new round
        roundInfo.clearedRounds = roundNumber;

        // reloads the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ends the round.
    public void GameOver()
    {
        // makes round info object.
        RoundInfo roundInfo = InstantiateRoundInfo();

        // saves as new round
        roundInfo.clearedRounds = roundNumber - 1;

        // game over
        SceneManager.LoadScene("EndScene");
    }

    // called when the player scores.
    public void Scored()
    {
        // message
        Debug.Log("Score!");

        // go to next round.
        NextRound();

    }

    // called when the playerm isses.
    public void Missed()
    {
        // message
        Debug.Log("Miss!");

        // ...

        // round ended.
        GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        // if the timer is active.
        if(activeTimer)
        {
            // reduce timer
            timer -= Time.deltaTime;

            // if the time runs out, the player missed.
            if (timer <= 0)
            {
                // technically calls the game over scene twice.
                Debug.Log("Time Over");
                GameOver();
            }
        }

        // timer text is set.
        if(timerText != null)
        {
            timerText.text = "TIME: " + timer.ToString("F2");
        }

        // update round text.
        if(roundText!= null)
        {
            // default: "ROUND  000"
            roundText.text = "ROUND  " + roundNumber.ToString("D3");
        }
    }
}
