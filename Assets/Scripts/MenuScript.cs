using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private SC_FPSController controller;

    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M)) && !menu.activeSelf)
        {
            MenuOn();
        }else
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && menu.activeSelf)
        {
            MenuOff();
        }
    }

    /// <summary>
    /// Turn the menu on and stop the game
    /// </summary>
    private void MenuOn()
    {
        menu.SetActive(true);
        controller.enabled = false;
        characterController.enabled = false;
        Time.timeScale = 0f;
        AudioListener.pause = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Turn of the menu and continue the game
    /// </summary>
    private void MenuOff()
    {
        menu.SetActive(false);
        controller.enabled = true;
        characterController.enabled = true;
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Exit the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

}
