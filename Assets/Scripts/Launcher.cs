using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // the ball being launched
    public GameObject ball;

    // the rigid body for the ball
    public Rigidbody2D ballBody;

    // launch power
    public float power = 100;

    // becomes 'true' when the ball is launched.
    public bool launched = false;

    // Start is called before the first frame update
    void Start()
    {
        // if 'ball' is empty.
        if(ball == null)
            ball = GameObject.Find("Ball");

        // gets the rigid body.
        if (ball != null && ballBody == null)
            ballBody = ball.GetComponent<Rigidbody2D>();
    }


    // checks to see if the cursor is in the window.
    // public static bool CursorInWindow()
    // {
    //     bool inX, inY;
    //     inX = (Input.GetAxisRaw("Mouse X") >= 0.0F && Input.GetAxisRaw("Mouse X") <= Screen.width);
    //     inY = (Input.GetAxisRaw("Mouse Y") >= 0.0F && Input.GetAxisRaw("Mouse Y") <= Screen.height);
    // 
    //     Debug.Log("Screen: " + new Vector2(Screen.width, Screen.height).ToString());
    // 
    //     return (inX && inY);
    //     
    // }

    // rotates to face the mouse.
    public void RotateToFaceMouse2D()
    {
        // mouse world position
        Vector3 mouseWpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // move to be relative to world origin
        Vector3 fromOrigin = mouseWpos - transform.position;
        fromOrigin.z = 0.0F;

        // rotation value
        float theta = Vector3.Angle(Vector3.up, fromOrigin);

        // rotation direction
        float direc = (mouseWpos.x < transform.position.x) ? 1 : -1;

        // reset to base rotation
        Vector2 rotXY = new Vector2(transform.rotation.x, transform.rotation.y); // save x and y
        transform.rotation = Quaternion.identity;

        // rotates to face camera
        transform.Rotate(0.0F, 0.0F, theta * direc);

        // give back x and y rotations
        Quaternion objectRot = transform.rotation;
        objectRot.x = rotXY.x;
        objectRot.y = rotXY.x;
        transform.rotation = objectRot;
    }

    // launches the ball
    void LaunchBall()
    {
        // ball body not set.
        if (ballBody == null)
            return;

        // ball force
        Vector3 force = transform.up * power;
        ballBody.AddForce(force, ForceMode2D.Impulse);
        launched = true;
    }

    // Update is called once per frame
    void Update()
    {
        // rotate to mouse position.
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0 && Input.anyKeyDown)
        {
            RotateToFaceMouse2D();
        }


        // launches the ball
        if (Input.GetAxisRaw("Fire1") != 0 && Input.anyKeyDown)
        {
            LaunchBall();
        }

        

    }
}
