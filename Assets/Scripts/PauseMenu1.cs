/* Name: Idan Shaviner
 * Date: 10/15/2024
 * Desb: make a pause menu when esc is pressed
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu1 : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public
    // Update is called once per frame
    //if escape is pressed pop up
   /*
    private void Start()
    {
        SceneManager.GetActiveScene();
    }
   */
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    //resume method
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    //pause method
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
    /*   public void LoadMenu()
       {
           Debug.Log("Loading Menu...");
       }
    */
    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("Level Select");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void ResetLevel()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        // string sceneName = currentScene.name;
        Resume();
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.UnloadScene(scene);
        SceneManager.LoadScene(scene);
    }

}
