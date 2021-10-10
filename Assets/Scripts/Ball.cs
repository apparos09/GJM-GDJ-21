using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the ball being launched.
public class Ball : MonoBehaviour
{
    // the round manager
    public RoundManager roundManager;

    // if 'true', this is the ball held by the launcher.
    public bool launcherBall = true;

    // ball count
    static int ballCount = 0;

    // bounds for the screen - deletes ball ounce outside of these bounds
    Vector2 xBounds = new Vector2(-0.75F, 1.75F);
    Vector2 yBounds = new Vector2(-0.75F, 1.75F);
    
    // Start is called before the first frame update
    void Start()
    {
        // finds the round manager.
        if (roundManager == null)
            roundManager = FindObjectOfType<RoundManager>();

        // if this is the ball held by the launcher, change its position.
        if(launcherBall)
        {
            // finds the ball holder.
            GameObject holder = GameObject.Find("Ball Holder");

            // gets position in world space.
            if (holder != null)
                transform.position = holder.transform.position;
        }

        // adds to ball count
        ballCount++;
    }

    // checks if the ball is in the game bounds.
    public bool InBounds()
    {
        // checks area
        bool inX, inY;

        // checks if the ball is in the view bounds
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // TODO: adjust y so that the ball falls back down.

        // check horizontal and vertical.
        inX = (viewPos.x >= xBounds.x && viewPos.x <= xBounds.y);
        inY = (viewPos.y >= yBounds.x && viewPos.y <= yBounds.y);

        // in x and y bounds
        return (inX && inY);
    }

    // remaining balls on the field.
    public static int GetBallCount()
    {
        return ballCount;
    }

    // Update is called once per frame
    void Update()
    {
        // ball out of bounds, so destroy it.
        if (!InBounds())
        {
            roundManager.Missed(); // say object has missed.
            Destroy(gameObject); // destroy object.
            // Destroy()
        }
            
    }

    // called when the object is destroyed.
    private void OnDestroy()
    {
        // reduce ball count.
        ballCount--;
    }
}
