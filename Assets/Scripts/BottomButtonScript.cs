using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BottomButtonScript : MonoBehaviour, ITurnElements
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


    private IEnumerator TurnOnWithDelay()
    {
        bool isTurnOn = false;
        while (true)
        {
            yield return new WaitForSeconds(2);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        TurnOn();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TurnOff();
    }

    public void TurnOn()
    {
        if (isTurnOn) return;
        
        _spriteRenderer.sprite = onSprite;
        foreach (var action in onPushedActionList)
        {
            action.Invoke();
        }

        isTurnOn = true;
    }

    public void TurnOff()
    {
        if (!isTurnOn) return;
        
        _spriteRenderer.sprite = offSprite;
        foreach (var action in onUnPushedActionList)
        {
            action.Invoke();
        }

        isTurnOn = false;
    }
}