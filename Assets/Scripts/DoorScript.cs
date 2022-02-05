using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] public List<GameObject> turnOnElements;

    [SerializeField] public bool isHorizontal;
    [SerializeField] public float horizontalSpeed = 1f;

    private Rigidbody2D _rigidbody2D;

    public bool closed = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        foreach (var element in turnOnElements)
        {
            element.GetComponent<ITurnElements>().AddTurnOnAction(OpenDoor);
            element.GetComponent<ITurnElements>().AddTurnOffAction(CloseDoor);
        }
    }

    private void CloseDoor()
    {

        if (!isHorizontal) _rigidbody2D.gravityScale = 1;

        closed = true;
    }

    private void OpenDoor()
    {
        if (!isHorizontal) _rigidbody2D.gravityScale = -1;
        closed = false;
    }

    private void FixedUpdate()
    {
        if (isHorizontal)
        {
            if (closed)
            {
                _rigidbody2D.velocity = Vector2.right * horizontalSpeed;
            }
            else
            {
                _rigidbody2D.velocity = Vector2.left * horizontalSpeed;
            }
        }
    }
}