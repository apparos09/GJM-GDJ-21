using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// title manager
public class TitleManager : MonoBehaviour
{
    // The exit button.
    public Button exitButton;

    // Start is called before the first frame update
    void Start()
    {
        // The exit button.
        if(exitButton != null)
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

    // called when the screen size changes.
    // I'm pretty sure I didn't get time to use this.
    public void OnScreenResolutionChange(int option)
    {
        switch (option)
        {
            default:
            case 0: 
            case 1: // 1024 X 576
                Screen.SetResolution(1024, 576, false);
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;

            case 2: // 1280 X 720
                Screen.SetResolution(1280, 720, false);
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;

            case 3: // 1920 X 1080 - Windowed (just does full screen again)
                Screen.SetResolution(1920, 1080, false);
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;

            case 4: // Full Screen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
                break;
        }
    }

    // plays the game
    public void PlayGame()
    {
        // loads the round scene.
        SceneManager.LoadScene("RoundScene");
    }

    // exits the game
    public void ExitApplication()
    {
        Application.Quit();
    }
}
