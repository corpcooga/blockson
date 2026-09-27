using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    bool optionsOpened = false;

    public GameObject pauseMenuUI;
    public GameObject winUI;

    public static PauseMenu instance{get; private set;}

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !winUI.activeInHierarchy)
        {
            FindObjectOfType<AudioManager>().Play("Esc Sound");
            if (GameIsPaused && !optionsOpened)
            {
                Resume();
            } else if (!optionsOpened)
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        LoadLevel(0);
    }

    public void ToggleOptionsOpened()
    {
        optionsOpened = !optionsOpened;
    }

    public static void LoadLevel(int level)
    {
        if (level != 0) Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(level);
    }

    public static void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
