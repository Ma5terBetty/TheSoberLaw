using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_FadeScreen : ElementUI
{
    [SerializeField] private TextOnScreen textData;
    [SerializeField] private Image background;
    [SerializeField] private Text title;
    [SerializeField] private Text subtitle;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    IEnumerator Start()
    {
        title.text = textData.title;
        subtitle.text = textData.subtitle;

        yield return new WaitForSeconds(textData.timeOnScreen);

        while (canvasGroup.alpha > 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            canvasGroup.alpha -= Time.deltaTime;
        }

        GameManager.Instance.OnNotify(ObserverMessages.GameplayActive);
        SetEnableElements(false);
    }

    IEnumerator FadeIn()
    {
        string currentLevel = SceneManager.GetActiveScene().name;

        if (currentLevel != "Level_3")
        {
            SetEnableElements(true);
            title.text = string.Empty;
            subtitle.text = string.Empty;

            while (canvasGroup.alpha < 1)
            {
                yield return new WaitForSeconds(Time.deltaTime);
                canvasGroup.alpha += Time.deltaTime;
            }

            GameManager.Instance.LoadNextLevel(currentLevel);
        }
    }

    private void SetEnableElements(bool input)
    {
        canvasGroup.enabled = input;
        background.enabled = input;
        title.enabled = input;
        subtitle.enabled = input;
    }

    public override void OnNotify(ObserverMessages message, params object[] args)
    {
        if (message == ObserverMessages.LevelCompleted)
        { 
            StartCoroutine(FadeIn());
        }
    }
}
