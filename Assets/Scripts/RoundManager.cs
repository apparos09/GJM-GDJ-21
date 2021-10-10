using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// the round manager
public class RoundManager : MonoBehaviour
{
    // the round number
    public int roundNumber = 1;

    [Header("Timer")]
    // timer is active.
    public bool activeTimer = true;

    // timer value.
    public float timer = 100;

    // start of the timer.
    public float timeStart = 100;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // sets the round number, which determines the round behaviour.
    public void SetRoundNumber(int rNum)
    {
        // negative round number provided.
        if (rNum < 0)
            roundNumber = 1;
    }

    // goes to the next round.
    public void NextRound()
    {
        // reloads the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ends the round.
    public void GameOver()
    {
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
    }
}
