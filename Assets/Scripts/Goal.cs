using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the goal for the game.
public class Goal : MonoBehaviour
{
    // the round manager
    public RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        // finds the round manager.
        if (roundManager == null)
            roundManager = FindObjectOfType<RoundManager>();
    }

    // collision triggered.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // collision tag
        string colTag = collision.tag;

        // if a ball collided.
        if(colTag == "Ball")
        {
            roundManager.Scored();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
