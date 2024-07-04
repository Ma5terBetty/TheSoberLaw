using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject levelsButtons;
    [SerializeField] private GameObject difficultyButtons;
    private int levelToLoad;
    private int difficultyLevel;

    private void Awake()
    {
        credits.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ToggleCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }
    public void ToggleDifficulty()
    { 
        difficultyButtons.SetActive(!difficultyButtons.activeSelf);
    }
    public void SelectLevel(int input)
    {
        levelToLoad = input;
        ToggleDifficulty();
        ToggleLevels();
    }
    public void SelectDifficulty(DifficultySO input)
    { 
        GameManager.Instance.SetDifficulty(input);
        LoadLevel();
    }
    void LoadLevel()
    { 
        GameManager.Instance.ChangeLevel(levelToLoad);
    }

    public void ToggleLevels()
    { 
        levelsButtons.SetActive(!levelsButtons.activeSelf);
    }
}
