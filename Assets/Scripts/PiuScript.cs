using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PiuScript : MonoBehaviour
{
    [SerializeField] public float piuSpeed;

    [SerializeField] public bool isRight = true;

    [SerializeField] public Color firstColor, secondColor;

    [SerializeField] public float colorChangeSpeed = 1f;

    private bool isFirstColorNow = true;

    private Rigidbody2D _rigidbody;

    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

//    private void Update()
//    {
//        ChangeColorTo(isFirstColorNow ? secondColor : firstColor);
//    }
//
//    private void ChangeColorTo(Color color)
//    {
//        if (_spriteRenderer.color.Equals(color))
//        {
//            isFirstColorNow = !isFirstColorNow;
//            return;
//        }
//        
//        _spriteRenderer.color =
//            Color.Lerp(color,
//                new Color(color.r, color.g, color.b, 1),
//                Time.deltaTime * colorChangeSpeed);
//    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2((isRight ? 1 : -1) * piuSpeed, _rigidbody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals(tag) || other.gameObject.tag.Equals("Player") || other.gameObject.tag.Equals("Lazer")) return;

        if (other.gameObject.tag.Equals("LazerCollector"))
        {
            other.gameObject.GetComponent<LazerCollectorScript>().Ping();
        }
        
        Destroy(gameObject);
    }
}
