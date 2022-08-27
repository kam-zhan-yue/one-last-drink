using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListener<T> : MonoBehaviour
{
    [TextArea(1,8)]
    public string description = string.Empty;
    
    public abstract GameEvent<T> gameEvent { get; }
    
    // These have to be provided by the inheritor
    public abstract UnityEvent<T> unityEvent { get; }

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.UnRegister(this);
    }

    public void OnEventRaised(T _value)
    {
        unityEvent.Invoke(_value);
    }
}

public class GameEventListener : MonoBehaviour
{
    [TextArea(1,8)]
    public string description = string.Empty;

    public GameEvent gameEvent;
    public UnityEvent unityEvent;

    private void OnEnable()
    {
        gameEvent.Register(this);
    }

    private void OnDisable()
    {
        gameEvent.UnRegister(this);
    }

    public void OnEventRaised()
    {
        unityEvent.Invoke();
    }
}