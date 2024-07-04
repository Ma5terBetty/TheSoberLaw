using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(ObserverMessages message, params object[] args);
}
