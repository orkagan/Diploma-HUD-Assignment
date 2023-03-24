using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Playing,
    Dead,
}

public class GameManager : MonoBehaviour
{
    #region Singleton Setup
    //Staticly typed property setup for EnemySpawner.Instance
    private static GameManager _instance;
    public static GameManager Instance
    {
        get => _instance;
        private set
        {
            //check if instance of this class already exists and if so keep orignal existing instance
            if (_instance == null)
            {
                _instance = value;
            }
            else if (_instance != value)
            {
                Debug.Log($"{nameof(GameManager)} instance already exists, destroy the duplicate!");
                Destroy(value);
            }
        }
    }
    private void Awake()
    {
        Instance = this; //sets the static class instance
    }
    #endregion

    public GameState gameState;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject playerObj;
    
    void Start()
    {
        gameState = GameState.Playing;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        switch (gameState)
        {
            case GameState.Menu:
                if (Cursor.visible == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case GameState.Playing:
                if (Cursor.visible == true)
                {
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                break;
            case GameState.Dead:
                if (Cursor.visible == false)
                {
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            default:
                break;
        }
    }

    public void GameOver()
    {
        gameState = GameState.Dead;
        gameOverScreen.SetActive(true);
        //make the player stop when they die
        DisablePlayerScripts();
    }

    public void Win()
    {
        gameState = GameState.Menu;
        winScreen.SetActive(true);
        DisablePlayerScripts();
    }

    public void Retry()
    {
        //reloads current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        //quits application
        Application.Quit();
        //or if running in editor then stop play mode
#if UNITY_EDITOR
        Debug.Log("ExitGame attempted.");
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void DisablePlayerScripts()
    {
        //goes through and disables all scripts on player and child objects
        foreach (MonoBehaviour script in playerObj.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }
        foreach (MonoBehaviour script in playerObj.GetComponentsInChildren<MonoBehaviour>())
        {
            script.enabled = false;
        }
    }
}