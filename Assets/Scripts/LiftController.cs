using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LiftController : MonoBehaviour, IUsableItem
{
    [SerializeField] public List<GameObject> turnOnElements;

    [SerializeField] public LiftScript lift;

    [SerializeField] public Sprite spriteOn, spriteOff;

    private SpriteRenderer _spriteRenderer;
    public int clickCounter;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        foreach (var element in turnOnElements)
        {
            element.GetComponent<ITurnElements>().AddTurnOnAction(TurnOn);
            element.GetComponent<ITurnElements>().AddTurnOffAction(TurnOff);
        }
    }

    private void TurnOff()
    {
        _spriteRenderer.sprite = spriteOff;
    }

    private void TurnOn()
    {
        _spriteRenderer.sprite = spriteOn;

        if (lift.needToMoveOnStart)
        {
            if (lift.isOnTop)
            {
                clickCounter = 2;
                lift.StartDown();
            }
            else
            {
                clickCounter = 0;
                lift.StartUp();
            }
        }
    }

    public void NextClick()
    {
        if (!lift.turnOn) return;

        clickCounter++;

        if (clickCounter > 3)
        {
            clickCounter = 0;
        }

        switch (clickCounter)
        {
            case 0:
                lift.StartUp();
                break;
            case 1:
                lift.Stop();
                break;
            case 2:
                lift.StartDown();
                break;
            case 3:
                lift.Stop();
                break;
        }
    }

    public void UseItem(PlayerMovement playerMovement)
    {
        NextClick();
    }
}