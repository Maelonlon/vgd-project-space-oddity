using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    PlayerControls playerControls;

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    Image panel;
    public GameObject menu;

    bool isPaused = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Pause.started += OnPausePressed;
    }
    private void Start()
    {
        panel = GetComponent<Image>();
        Unpause();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Unpause();
    }

    void OnPausePressed(InputAction.CallbackContext ctx)
    {
        if (isPaused)
            Unpause();
        else
            Pause();
    }

    void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        panel.enabled = true;
        menu.SetActive(true);
    }

    void Unpause()
    {
        Time.timeScale = 1f;
        isPaused = false;
        panel.enabled = false;
        menu.SetActive(false);
    }


}

