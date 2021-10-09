using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    // the rigid body of the launcher
    public Rigidbody2D rigidbody;

    // the ball being launched
    public GameObject ball;

    // the rigid body for the ball
    public Rigidbody2D ballBody;

    // the gate stopping the ball form being launched.
    public GameObject gate;

    [Header("Functions")]

    // enable movement
    public bool controlMove = true;

    // movement speed for launcher.
    public Vector2 moveSpeed = new Vector2(5.0F, 5.0F);

    // enable rotation
    public bool controlRot = true;

    // becomes 'true' when the ball is launched.
    public bool launched = false;

    // launch power
    public float launchPower = 100;

    // Start is called before the first frame update
    void Start()
    {
        // if 'launcherBody' is empty.
        if (ball == null)
            rigidbody = GetComponent<Rigidbody2D>();

        // if 'ball' is empty.
        if (ball == null)
            ball = GameObject.Find("Ball");

        // gets the rigid body.
        if (ball != null && ballBody == null)
            ballBody = ball.GetComponent<Rigidbody2D>();

        // finds the 'gate' object that stops the ball from being launched
        if (gate == null)
            gate = GameObject.Find("Gate");
    }


    // checks to see if the cursor is in the window.
    public static bool CursorInWindow()
    {
        // checks area
        bool inX, inY;

        // gets the viewport position
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // check horizontal an vertical.
        inX = (viewPos.x >= 0 && viewPos.x <= 1.0);
        inY = (viewPos.y >= 0 && viewPos.y <= 1.0);

        return (inX && inY);
        
    }

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

        // deactivates the gate.
        if (gate != null)
            gate.SetActive(false);

        // ball force
        Vector3 force = transform.up * launchPower;
        ballBody.AddForce(force, ForceMode2D.Impulse);
        launched = true;
    }

    // Update is called once per frame
    void Update()
    {
        // enable user movement.
        if(controlMove)
        {
            // gets movement direction
            Vector2 direc = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 force = Vector2.Scale(direc, moveSpeed);

            // gets rigid body force.
            // rigidbody.AddForce(force, ForceMode2D.Impulse);

            // translates object.
            transform.Translate(force * Time.deltaTime);
        }

        // enable user rotation.
        if (controlRot)
        {
            // rotate to mouse position.
            if ((Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0) && CursorInWindow())
            {
                RotateToFaceMouse2D();
            }
        }


        // launches the ball
        if (Input.GetAxisRaw("Fire1") != 0 && Input.anyKeyDown)
        {
            LaunchBall();
        }

        

    }
}
