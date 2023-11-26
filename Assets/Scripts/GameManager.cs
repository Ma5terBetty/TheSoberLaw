using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    [SerializeField] public GameObject player;
    [SerializeField] Scene currentScene;
    int currentLevel;
    
    private bool isAlive;
    public bool dontDestroyOnLoad;
    public bool isLevel1Completed;
    public bool isLevel2Completed;
    public bool isBossDefeated;
    public static bool IsGamePaused;
    public bool gameOver;

    public bool isLevelStarted;

    Vector3 startPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        player = GameObject.FindGameObjectWithTag("Player");
        startPos = GameObject.FindGameObjectWithTag("StartPos").GetComponent<Transform>().position;

        currentScene = SceneManager.GetActiveScene();

        if (player != null)
        {
            player.gameObject.SetActive(true);
            player.transform.position = startPos;
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        isLevel1Completed = false;
        isLevel2Completed = false;
        isBossDefeated = false;
        isLevelStarted = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.IsGamePaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }
    /// <summary>
    /// Pauses the game by setting the Time.timeScale from 1f to 0f.
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
        IsGamePaused = true;
    }
    /// <summary>
    /// Unpauses the game by setting the Time.timeScale from 0f to 1f.
    /// </summary>
    void Unpause()
    {
        Time.timeScale = 1.0f;
        IsGamePaused = false;
    }
    public void LoadMainMenu()
    {
        Unpause();
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Changes level by level's ID.
    /// </summary>
    /// <param name="scene">Level to be loaded.</param>
    /// <returns>Load a new level scene</returns>
    public void ChangeLevel(int scene)
    {
        Unpause();
        SceneManager.LoadScene(scene);
    }
    /// <summary>
    /// Finds player's prefab on the current scene.
    /// </summary>
    public void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
