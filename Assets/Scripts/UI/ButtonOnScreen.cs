using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOnScreen : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void LoadMainMenu()
    { 
        GameManager.Instance.LoadMainMenu();
    }
}
