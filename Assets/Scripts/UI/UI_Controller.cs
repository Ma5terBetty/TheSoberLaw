using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public static UI_Controller Instance;

    [SerializeField]
    private ElementUI PlayerHealth,
                      FadeScreen,
                      Pause,
                      TimeCounter,
                      ScoreCount,
                      DefeatScreen,
                      BossHealth,
                      WinScreen;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// Registra el sujeto al observador.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="input"></param>
    public void AddObservable(Subject subject, UI_ElementType input)
    {
        switch (input)
        {
            case UI_ElementType.PlayerHealth:
                TryToRegister(subject, PlayerHealth);
                break;
            case UI_ElementType.FadeScreen:
                TryToRegister(subject, FadeScreen);
                break;
            case UI_ElementType.PauseScreen:
                TryToRegister(subject, Pause);
                break;
            case UI_ElementType.GameOverScreen:
                TryToRegister(subject, DefeatScreen);
                break;
            case UI_ElementType.TimeCounter:
                TryToRegister(subject, TimeCounter);
                break;
            case UI_ElementType.ScoreCounter:
                TryToRegister(subject, ScoreCount);
                break;
            case UI_ElementType.BossHealth:
                TryToRegister(subject, BossHealth);
                break;
        }
    }
    /// <summary>
    /// Remueve el sujeto del observador.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="input"></param>
    public void RemoveObservable(Subject subject, UI_ElementType input)
    {
        switch (input)
        {
            case UI_ElementType.PlayerHealth:
                TryToUnregister(subject, PlayerHealth);
                break;
            case UI_ElementType.FadeScreen:
                TryToUnregister(subject, FadeScreen);
                break;
            case UI_ElementType.PauseScreen:
                TryToUnregister(subject, Pause);
                break;
            case UI_ElementType.GameOverScreen:
                TryToUnregister(subject, DefeatScreen);
                break;
            case UI_ElementType.TimeCounter:
                TryToUnregister(subject, TimeCounter);
                break;
            case UI_ElementType.ScoreCounter:
                TryToUnregister(subject, ScoreCount);
                break;
            case UI_ElementType.BossHealth:
                TryToUnregister(subject, BossHealth);
                break;
        }
    }

    /// <summary>
    /// Registra un elemento al observador siempre que no sea nulo.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="element"></param>
    private void TryToRegister(Subject subject, ElementUI element)
    {
        if (element != null)
        {
            subject.RegisterObserver(element);
        }
    }
    /// <summary>
    /// Remueve un elemento del observador.
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="element"></param>
    private void TryToUnregister(Subject subject, ElementUI element)
    {
        if (element != null)
        {
            subject.UnregisterObserver(element);
        }
        else
        {
            //Debug.Log($"The subject {subject.name} tried to remove a null element");
        }
    }
}
