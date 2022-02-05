using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public GameObject mainCamera;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Bubble _bubble;

    [SerializeField]
    private float cameraParallax = 1.0f;

    private bool isMoveHorizontal, isFlip, isMoveForward, isMoveBackward, isIdle = true;
    private static readonly int IsMoveForward = Animator.StringToHash("isMoveForward");
    private static readonly int IsMoveHorizontal = Animator.StringToHash("isMoveHorizontal");
    private static readonly int IsMoveBackward = Animator.StringToHash("isMoveBackward");
    private static readonly int IsIdle = Animator.StringToHash("isIdle");

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        mainCamera = GameObject.Find("Main Camera");
    }

    void FixedUpdate()
    {
        CheckSound();
        CheckAnimation();

//        _horizontalMove = Input.GetAxisRaw("Horizontal");
        var position = transform.position;
        
        MovePlayer(position);
        mainCamera.transform.position = new Vector3(position.x, position.y + cameraParallax, -50);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoAction();
        }
    }

    public void DoAction()
    {
//        var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, 1f);
//        foreach (var col in colliders)
//        {
//            if (col.tag.Equals("Item"))
//            {
//                col.gameObject.GetComponent<IitemsPickupable>().OnItemPickup(this);
//            }
//            else
//            {
//                if (col.tag.Equals("Traps"))
//                {
//                    if (keysCount > 0 && !col.gameObject.GetComponent<TrapPrefs>().isBroken)
//                    {
//                        keysCount--;
//                        col.gameObject.GetComponent<TrapPrefs>().isBroken = true;
//                    }
//                }
//            }
//        }
    }

    private void MovePlayer(Vector2 position)
    {
        float resultXMove = position.x, resultYMove = position.y;

        if (!(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)))
        {
            if (Input.GetKey(KeyCode.A))
            {
                resultXMove = position.x - speed;
                isMoveHorizontal = true;
                isFlip = true;
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    resultXMove = position.x + speed;
                    isMoveHorizontal = true;
                    isFlip = false;
                }
                else
                {
                    isMoveHorizontal = false;
                }
            }
        }

        if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)))
        {
            if (Input.GetKey(KeyCode.S))
            {
                isMoveBackward = true;
                isMoveForward = false;
                resultYMove = position.y - speed;
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    resultYMove = position.y + speed;
                    isMoveForward = true;
                    isMoveBackward = false;
                }
                else
                {
                    isMoveBackward = false;
                    isMoveForward = false;
                }
            }
        }

        _rigidbody.MovePosition(Vector2.Lerp(position, new Vector2(resultXMove, resultYMove), Time.deltaTime));
    }

    private void CheckSound()
    {
        //        if (Math.Abs(_rigidbody.velocity.x) > 0.01) //сука звук
//        {
//            walkAudio.Play();
//        }
//        else
//        {
//            walkAudio.Pause();
//        }
    }

    private void CheckAnimation()
    {
        _spriteRenderer.flipX = isFlip;

        if (isMoveForward)
        {
            AnimMoveForward();
        }
        else
        {
            if (isMoveHorizontal)
            {
                AnimMoveHorizontal();
            }
            else
            {
                if (isMoveBackward)
                {
                    AnimMoveBackward();
                }
                else
                {
                    AnimIdle();
                }
            }
        }
    }

    private void AnimMoveForward()
    {
        isMoveForward = true;
        isMoveBackward = false;
        isMoveHorizontal = false;
        isIdle = false;
        SyncWithAnimator();
    }

    private void AnimMoveBackward()
    {
        isMoveForward = false;
        isMoveBackward = true;
        isMoveHorizontal = false;
        isIdle = false;
        SyncWithAnimator();
    }

    private void AnimMoveHorizontal()
    {
        isMoveForward = false;
        isMoveBackward = false;
        isMoveHorizontal = true;
        isIdle = false;
        SyncWithAnimator();
    }

    private void AnimIdle()
    {
        isMoveForward = false;
        isMoveBackward = false;
        isMoveHorizontal = false;
        isIdle = true;
        SyncWithAnimator();
    }

    private void SyncWithAnimator()
    {
        if (_animator != null)
        {
            _animator.SetBool(IsMoveForward, isMoveForward);
            _animator.SetBool(IsMoveBackward, isMoveBackward);
            _animator.SetBool(IsMoveHorizontal, isMoveHorizontal);
            _animator.SetBool(IsIdle, isIdle);
        }
    }
}