using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class WallButtonScript : MonoBehaviour, IUsableItem, ITurnElements
{
    [SerializeField] public Sprite onSprite, offSprite;

    private SpriteRenderer _spriteRenderer;

    private List<Action> onPushedActionList = new List<Action>();
    private List<Action> onUnPushedActionList = new List<Action>();

    private bool isTurnOn = false;

    private void Start()
    {
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
        return isTurnOn;
    }

    public void TurnOn()
    {
        _spriteRenderer.sprite = onSprite;
        foreach (var action in onPushedActionList)
        {
            action.Invoke();
        }
    }

    public void TurnOff()
    {
        _spriteRenderer.sprite = offSprite;
        foreach (var action in onUnPushedActionList)
        {
            action.Invoke();
        }
    }

    public void UseItem(PlayerMovement playerMovement)
    {
        if (isTurnOn)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }

        isTurnOn = !isTurnOn;
    }
}