using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    [SerializeField] public List<GameObject> turnOnElements;

    private Rigidbody2D _rigidbody2D;

    public bool turnOn = false;

    private LiftController _liftController;

    [SerializeField] public bool isOnTop = false;

    [SerializeField] public bool needToMoveOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _liftController = GetComponentInChildren<LiftController>();
        foreach (var element in turnOnElements)
        {
            element.GetComponent<ITurnElements>().AddTurnOnAction(TurnOn);
            element.GetComponent<ITurnElements>().AddTurnOffAction(TurnOff);
        }
    }

    private void TurnOn()
    {
        turnOn = true;
    }

    private void TurnOff()
    {
        turnOn = false;
    }

    public void StartUp()
    {
        _rigidbody2D.gravityScale = -1;
    }

    public void StartDown()
    {
        _rigidbody2D.gravityScale = 1;
    }

    public void Stop()
    {
        _rigidbody2D.gravityScale = 0;
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("LiftTopStop") && _liftController.clickCounter == 0)
        {
            Stop();
            _liftController.clickCounter = 1;
            isOnTop = true;
        }
        else
        {
            if (other.gameObject.tag.Equals("LiftBottomStop") && _liftController.clickCounter == 2)
            {
                Stop();
                _liftController.clickCounter = 3;
                isOnTop = false;
            }
        }
    }
}