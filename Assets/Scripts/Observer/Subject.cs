using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public List<Observer> _observers = new List<Observer>();

    /// <summary>
    /// Me registro los observadores
    /// </summary>
    /// <param name="observer"></param>
    public virtual void RegisterObserver(Observer observer)
    {
        if (_observers != null)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }
    }

    /// <summary>
    /// Me saco los observadores
    /// </summary>
    /// <param name="observer"></param>
    public virtual void UnregisterObserver(Observer observer)
    {
        if (_observers != null)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }
    }

    /// <summary>
    /// Notifico a los observadores
    /// </summary>
    /// <param name="message"></param>
    /// <param name="args"></param>
    public void Notify(ObserverMessages message, params object[] args)
    {
        if (_observers == null) return;
        if (_observers.Count == 0) return;

        foreach (var observers in _observers)
        {
            if (observers == null)
            {
                Debug.Log($"Observer is null");
                return;
            }

            observers.OnNotify(message, args);
        }
    }

    
}
