using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Observer
{
    #region Private Properties
    private Player _player;
    private LevelManager currentLevelManager;
    [SerializeField]
    private Scene currentScene;
    [SerializeField]
    private DifficultySO currentDifficulty;
    private Vector3 startPos;
    private bool isGameOver;
    #endregion

    #region Public Properties
    public static GameManager Instance;
    public static bool IsGamePaused;

    public LevelManager LevelManager => currentLevelManager;
    public DifficultySO DifficultyLevel => currentDifficulty;
    public Player Player => _player;
    public bool IsGameOver => isGameOver;
    public bool dontDestroyOnLoad;
    public bool isLevelStarted;
    public bool IsGameplayActive;
    #endregion

    #region Unity Functions
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
        startPos = GameObject.FindGameObjectWithTag("StartPos").GetComponent<Transform>().position;

        currentScene = SceneManager.GetActiveScene();

        if (_player != null)
        {
            _player.gameObject.SetActive(true);
            _player.transform.position = startPos;
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Start()
    {
        IsGameplayActive = false;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    #region Custom Functions
    /// <summary>
    /// Pauses the game by setting the Time.timeScale from 1f to 0f.
    /// </summary>
    public void Pause()
    {
        if (IsGamePaused)
        {
            Time.timeScale = 1.0f;
            IsGamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            IsGamePaused = true;
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    /// <summary>
    /// Changes level by level's ID.
    /// </summary>
    /// <param name="scene">Level to be loaded.</param>
    /// <returns>Load a new level scene</returns>
    public void ChangeLevel(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    /// <summary>
    /// Finds player's prefab on the current scene.
    /// </summary>
    public void FindPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GameOver()
    { 
        isGameOver = true;
        IsGameplayActive = false;
    }

    public void SetDifficulty(DifficultySO input)
    {
        currentDifficulty = input;
    }
    public void SetCurrentLevelManager(LevelManager input)
    { 
        currentLevelManager = input;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            currentLevelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
            IsGamePaused = false;
        }
    }
    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        switch (message)
        {
            case ObserverMessages.GamePaused:
                Pause();
                break;
            case ObserverMessages.GameOver:
                GameOver();
                break;
            case ObserverMessages.GameplayActive:
                IsGameplayActive = true;
                break;
            case ObserverMessages.StopGameplay:
                IsGameplayActive = false;
                break;
        }
    }
    public void AddObservable(Subject subject, EventType input)
    {
        switch (input)
        {
            case EventType.GamePaused:
                subject.RegisterObserver(this);
                break;
            case EventType.GameOver:
                subject.RegisterObserver(this);
                break;
        }
    }
    public void RemoveObservable(Subject subject, EventType input)
    {
        switch (input)
        {
            case EventType.GamePaused:
                subject.UnregisterObserver(this);
                break;
            case EventType.GameOver:
                subject.UnregisterObserver(this);
                break;
        }
    }

    public void LoadNextLevel(string level)
    {
        switch (level)
        {
            case "Level_1":
                SceneManager.LoadScene("Level_2");
                break;
            case "Level_2":
                SceneManager.LoadScene("Level_3");
                break;
        }
    }
    #endregion
}
