using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class BatteryHolder : MonoBehaviour, ITurnElements
{
    public bool isTurnOn;
    
    [SerializeField]
    public GameObject topListener, botListener;


    private SpriteRenderer _spriteRenderer;

    private Animator _animator;
    
    private TopColliderController _topColliderController;
    private BottomColliderController _bottomColliderController;

    [SerializeField]
    public Sprite onSprite, offSprite;
    
    private List<Action> turnOnActionList = new List<Action>();
    private List<Action> turnOffActionList = new List<Action>();
    
    private bool isTurnedOnNow = false;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _topColliderController = topListener.GetComponent<TopColliderController>();
        _bottomColliderController = botListener.GetComponent<BottomColliderController>();
    }

    private void FixedUpdate()
    {
        isTurnOn = _topColliderController.isTurnedOn && _bottomColliderController.isTurnedOn;

        if (isTurnOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void AddTurnOnAction(Action turnOnAction)
    {
        turnOnActionList.Add(turnOnAction);
    }

    public void AddTurnOffAction(Action turnOffAction)
    {
        turnOffActionList.Add(turnOffAction);
    }

    public bool isTurnedOn()
    {
        return isTurnOn;
    }
    
    public void TurnOn()
    {
        if (isTurnedOnNow) return;

        isTurnedOnNow = true;
        
        isTurnOn = true;
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = onSprite;
        }

        foreach (var action in turnOnActionList)
        {
            action.Invoke();
        }
    }

    public void TurnOff()
    {
        if (!isTurnedOnNow) return;

        isTurnedOnNow = false;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = offSprite;
        }

        foreach (var action in turnOffActionList)
        {
            action.Invoke();
        }
        isTurnOn = false;
    }
}
