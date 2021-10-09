using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // changes the scene using the scene number.
    public static void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);

    }

    // changes the scene using the scene name.
    public static void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    // returns the skybox of the scene.
    public static Material GetSkybox()
    {
        return RenderSettings.skybox;
    }

    // sets the skybox of the scene.
    public static void SetSkybox(Material newSkybox)
    {
        RenderSettings.skybox = newSkybox;
    }

    // exits the game
    public void ExitApplication()
    {
        Application.Quit();
    }
}
