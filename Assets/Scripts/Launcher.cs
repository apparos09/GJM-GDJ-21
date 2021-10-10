using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviour
{
    // the ball being launched
    public GameObject ball;

    // the rigid body for the ball
    public Rigidbody2D ballBody;

    // the gate stopping the ball form being launched.
    public GameObject gate;

    // launch power bar
    public Slider powerBar;

    // becomes 'true' when the ball is launched.
    public bool launched = false;

    [Header("Functions")]

    // enable movement
    public bool controlMove = true;

    // movement speed for launcher.
    public Vector2 moveSpeed = new Vector2(5.0F, 5.0F);

    // enable rotation
    public bool controlRot = true;

    // control launch power
    public bool controlPower = true;

    // launch power
    public float launchPower = 100.0F;

    // increments for changing power (multiplied by delta time).
    public float powerInc = 40.0F;

    // the limits on how high and low launch power can be.
    public Vector2 powerLimits = new Vector2(20.0F, 100.0F);

    // Start is called before the first frame update
    void Start()
    {
        // if 'ball' is empty.
        if (ball == null)
            ball = GameObject.Find("Ball");

        // gets the rigid body.
        if (ball != null && ballBody == null)
            ballBody = ball.GetComponent<Rigidbody2D>();

        // finds the 'gate' object that stops the ball from being launched
        if (gate == null)
            gate = GameObject.Find("Gate");

        // finds the power bar if it's not set.
        if (powerBar == null)
            powerBar = FindObjectOfType<Slider>();
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

    // checks to see if the launcher is in the view.
    private bool WorldPointInView(Vector3 point)
    {
        // checks area
        bool inX, inY;

        // gets the viewport position
        Vector3 viewPos = Camera.main.WorldToViewportPoint(point);

        // check horizontal and vertical.
        inX = (viewPos.x >= 0.0F && viewPos.x <= 1.0);
        inY = (viewPos.y >= 0.0F && viewPos.y <= 1.0);

        return (inX && inY);
    }

    // launches the ball
    private void LaunchBall()
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

            // setting force
            Vector2 force = Vector2.Scale(direc, moveSpeed);

            // gets rigid body force.
            // rigidbody.AddForce(force, ForceMode2D.Impulse);

            // translation amount
            Vector2 tlate = force * Time.deltaTime;

            // translates object.
            if (WorldPointInView(transform.position + new Vector3(tlate.x, tlate.y, 0.0F)))
            {
                // returns to default orientation for proper translation.
                Quaternion rot = transform.rotation;
                transform.rotation = Quaternion.identity;

                // translates to transform
                transform.Translate(force * Time.deltaTime);
                transform.rotation = rot;
            }
                
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

        // controls the launch power
        if(controlPower)
        {
            // direction of power
            float direc = 0.0F;

            // decrease
            if (Input.GetKey(KeyCode.Less) || Input.GetKey(KeyCode.LeftBracket) || Input.GetKey(KeyCode.LeftCurlyBracket) ||
                Input.GetKey(KeyCode.Keypad1))
                direc = -1.0F;

            // increase
            if (Input.GetKey(KeyCode.Greater) || Input.GetKey(KeyCode.RightBracket) || Input.GetKey(KeyCode.RightCurlyBracket) ||
                Input.GetKey(KeyCode.Keypad2))
                direc = 1.0F;

            // the direction has been set.
            if(direc != 0.0F)
            {
                launchPower += powerInc * direc * Time.deltaTime;
                launchPower = Mathf.Clamp(launchPower, powerLimits.x, powerLimits.y);
            }
        }

        // adjusting power bar
        if(powerBar != null)
        {
            float value = Mathf.Clamp01(Mathf.InverseLerp(powerLimits.x, powerLimits.y, launchPower));
            powerBar.value = value;
        }

        // launches the ball
        if ((Input.GetAxisRaw("Fire1") != 0 || Input.GetAxisRaw("Jump") != 0) && launched == false)
        {
            LaunchBall();
        }

        

    }
}
