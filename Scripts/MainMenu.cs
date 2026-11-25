using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]
    [SerializeField] private Button newGameButton;


    public void OnNewGameClicked() 
    {
        newGameButton.interactable = false;
        // create a new game - which will initialize our game data
        SceneManager.LoadSceneAsync("Metaforum Floor");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
