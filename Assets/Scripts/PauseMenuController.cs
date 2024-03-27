using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject controlsMenu;

    public TextBoxManager textBoxManager;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (textBoxManager.isActive && Input.GetButtonDown("Pause"))
        {
            return;
        }
        else if (Input.GetButtonDown("Pause") && !isActive)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isActive = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.GetButtonDown("Pause") && isActive)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            isActive = false;
            pauseMenu.SetActive(false);
            controlsMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Controls()
    {
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Back()
    {
        pauseMenu.SetActive(true);
        controlsMenu.SetActive(false);
    }

    public void Resume()
    {
        isActive = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
