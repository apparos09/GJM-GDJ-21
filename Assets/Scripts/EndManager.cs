using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// the ending manager
public class EndManager : MonoBehaviour
{
    // the amount of rounds passed.
    public int clearedRounds = 0;

    // the text to show cleared rounds
    public TMPro.TextMeshProUGUI roundText = null;

    // The exit button.
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // round information.
        RoundInfo roundInfo = FindObjectOfType<RoundInfo>();

        if (roundInfo != null)
            clearedRounds = roundInfo.clearedRounds;

        // text display
        if (roundText != null)
            roundText.text = "Rounds Cleared: " + clearedRounds.ToString();

        // destroys the game object.
        if(roundInfo != null)
            Destroy(roundInfo.gameObject);

        // The exit button.
        if (exitButton != null)
        {
            // If this is running in WebGl, disable the exit button.
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                exitButton.interactable = false;
            }
            else
            {
                // The exit button is on.
                exitButton.interactable = true;
            }
        }
    }

    // returns to the title screen.
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetAxisRaw())
        {

        }
    }
}
