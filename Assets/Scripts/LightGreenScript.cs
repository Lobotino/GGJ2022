using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LightGreenScript : MonoBehaviour
{
    [SerializeField] public Sprite onSprite, offSprite;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] public GameObject turnOnButton;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        turnOnButton.GetComponent<ITurnElements>().AddTurnOnAction(TurnOn);
        turnOnButton.GetComponent<ITurnElements>().AddTurnOffAction(TurnOff);
        
//        StartCoroutine(TurnOnWithDelay());
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

    public void TurnOn()
    {
        _spriteRenderer.sprite = onSprite;
    }

    public void TurnOff()
    {
        _spriteRenderer.sprite = offSprite;
    }
}