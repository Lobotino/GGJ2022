using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LazerCollectorScript : MonoBehaviour, ITurnElements
{
    [SerializeField] public Sprite onSprite, offSprite;

    private SpriteRenderer _spriteRenderer;

    private List<Action> onPushedActionList = new List<Action>();
    private List<Action> onUnPushedActionList = new List<Action>();

    private bool isTurnedOnNow = false;

    public float startLazerPingTimer = 2f;
    private float lazerPingTimer;

    private void Start()
    {
        lazerPingTimer = startLazerPingTimer;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void AddTurnOnAction(Action action)
    {
        onPushedActionList.Add(action);
    }

    public void AddTurnOffAction(Action action)
    {
        onUnPushedActionList.Add(action);
    }

    public bool isTurnedOn()
    {
        return isTurnedOnNow;
    }

    public void FixedUpdate()
    {
        if (lazerPingTimer > 0)
        {
            lazerPingTimer -= Time.deltaTime;
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
    {
        if (isTurnedOnNow) return;
        
        isTurnedOnNow = true;
        _spriteRenderer.sprite = onSprite;
        foreach (var action in onPushedActionList)
        {
            action.Invoke();
        }
    }

    public void TurnOff()
    {
        if (!isTurnedOnNow) return;
        
        _spriteRenderer.sprite = offSprite;
        foreach (var action in onUnPushedActionList)
        {
            action.Invoke();
        }
        isTurnedOnNow = false;
    }

    public void Ping()
    {
        TurnOn();
        lazerPingTimer = startLazerPingTimer;
    }
}