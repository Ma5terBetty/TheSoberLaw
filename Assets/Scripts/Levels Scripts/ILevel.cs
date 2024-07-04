using UnityEngine;

public interface ILevel
{
    bool IsLevelCompleted();
    void OnLevelCompleted();
    /// <summary>
    /// Set the initial parameters for the level
    /// </summary>
    void OnLevelStarted();
}
