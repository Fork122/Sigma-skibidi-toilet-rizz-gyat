/* Name: Idan Shaviner
 * Date: 10/15/2024
 * Desb: make a pause menu when esc is pressed
 */
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject WinScreen;

    // Update is called once per frame
    //if escape is pressed pop up
    void Update()
    {
        if(transform.position.y < -570)
        {
            WinScreen.SetActive(true);
        }
        else
        {
            WinScreen.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
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
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
