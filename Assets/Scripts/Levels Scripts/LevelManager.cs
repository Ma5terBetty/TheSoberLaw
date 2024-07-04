using System.Collections;
using System;
using UnityEngine;

public class LevelManager : Subject, ILevel
{
    #region PRIVATE PROPERTIES
    private bool isLevelCompleted = false;
    private float levelTimer;
    private float auxCounter;
    private int scoreCount;
    [SerializeField] private bool isTimedLevel;
    #endregion

    public bool IsTimedLevel;
    public float TimeToComplete;
    public int ScoreToReach;

    public float TimeCount => levelTimer;
    public int CurrentScore => scoreCount;

    #region UNITY_FUNCTIONS
    private void Start()
    {
        GameManager.Instance.AddObservable(this, EventType.GamePaused);

        UI_Controller.Instance.AddObservable(this, UI_ElementType.FadeScreen);
        UI_Controller.Instance.AddObservable(this, UI_ElementType.PauseScreen);

        UI_Controller.Instance.AddObservable(this, UI_ElementType.TimeCounter);
        UI_Controller.Instance.AddObservable(this, UI_ElementType.ScoreCounter);

        UI_Controller.Instance.AddObservable(this, UI_ElementType.WinScreen);
        UI_Controller.Instance.AddObservable(this, UI_ElementType.GameOverScreen);

        OnLevelStarted();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Notify(ObserverMessages.GamePaused);
        }

        if (!GameManager.IsGamePaused && IsTimedLevel && !GameManager.Instance.IsGameOver)
        {
            auxCounter += Time.deltaTime;

            if (auxCounter > 1 && levelTimer > 0)
            {
                auxCounter = 0;
                levelTimer--;
                Notify(ObserverMessages.UpdateTimeCount, levelTimer);

                if (levelTimer <= 0)
                {
                    isLevelCompleted = true;
                    OnLevelCompleted();
                }
            }
        }
    }

    private void OnDisable()
    {
        UI_Controller.Instance.RemoveObservable(this, UI_ElementType.FadeScreen);
        UI_Controller.Instance.RemoveObservable(this, UI_ElementType.PauseScreen);
        UI_Controller.Instance.RemoveObservable(this, UI_ElementType.TimeCounter);
        UI_Controller.Instance.RemoveObservable(this, UI_ElementType.ScoreCounter);
        UI_Controller.Instance.RemoveObservable(this, UI_ElementType.WinScreen);
        UI_Controller.Instance.RemoveObservable(this, UI_ElementType.GameOverScreen);
    }
    #endregion

    #region CUSTOM_FUNCTIONS
    public bool IsLevelCompleted()
    {
        return isLevelCompleted;
    }
    public void OnLevelCompleted()
    {
        Notify(ObserverMessages.LevelCompleted);
    }
    public void OnLevelStarted()
    {
        isLevelCompleted = false;

        if (IsTimedLevel)
        {
            levelTimer = TimeToComplete;
            Notify(ObserverMessages.LevelStarted, TimeToComplete);
        }
        else
        {
            scoreCount = ScoreToReach;
            Notify(ObserverMessages.LevelStarted, scoreCount);
        }
    }
    public void EnemyKilled()
    {
        if (!IsTimedLevel)
        {
            scoreCount--;
            Notify(ObserverMessages.EnemyKilled, scoreCount);
            if (scoreCount <= 0)
            {
                OnLevelCompleted();
            }
        }
    }
    public void MainMenuButton()
    {
        Notify(ObserverMessages.GamePaused);
        GameManager.Instance.LoadMainMenu();
    }
    public void ContinueButton()
    {
        Notify(ObserverMessages.GamePaused);
    }
    public void ExitButton()
    { 
        Application.Quit();
    }
    #endregion
}
