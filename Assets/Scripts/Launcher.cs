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
    private float power = 10;

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

    // launches the ball
    void LaunchBall()
    {
        // ball body not set.
        if(ballBody == null)
            return;

        // ball force
        Vector2 force = ballBody.gameObject.transform.up;
        force *= power;

        ballBody.AddForce(ballBody.gameObject.transform.up, ForceMode2D.Impulse);
        launched = true;
    }

    // Update is called once per frame
    void Update()
    {
        // gets mouse position in world space
        // Debug.Log(new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0.0F));
        // Vector3 mouseWpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0.0F));
        // mouseWpos.z = transform.position.z;

        // Debug.Log(mouseWpos);

        // looks at mouse position
        // transform.LookAt(mouseWpos);

        // if (Input.GetAxisRaw("Mouse X") != 0)
        //     LaunchBall();
        // 
        // launches the ball
        if (Input.GetAxisRaw("Fire1") != 0 && Input.anyKeyDown)
        {
            // Vector3 mouseWpos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0.0F));
            // mouseWpos.z = transform.position.z;
            Debug.Log("MP: " + Input.mousePosition.ToString());
            Vector3 mouseWpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("WP: " + mouseWpos.ToString());

            // mouseWpos.z = transform.position.z;
            // Debug.Log("X:" + Vector3.Angle(transform.right, mouseWpos));
            // Debug.Log("Y:" + Vector3.Angle(transform.up, mouseWpos));
            // Debug.Log("Z:" + Vector3.Angle(transform.forward, mouseWpos));

            transform.rotation = Quaternion.identity;
            transform.Rotate(0.0F, 0.0F, -Vector3.Angle(transform.up, mouseWpos));
            
        }



        // if(Input.GetAxisRaw("Horizontal") != 0)
        // {
        //     transform.Rotate(0.0F, 0.0F, Input.GetAxisRaw("Horizontal") * 10);
        // }

    }
}
