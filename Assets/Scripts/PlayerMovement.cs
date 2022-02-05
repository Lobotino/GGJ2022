using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public AudioSource walkAudio;

    [SerializeField] public Animator animator;

    [SerializeField] public GameObject deadCollider;

    [SerializeField] public bool isKoldun = true;

    [SerializeField] public float actionRadius = 1f;

    [SerializeField] public float rotateItemsSpeed = 2f;

    private CapsuleCollider2D aliveCollider2D;

    private SpringJoint2D _springJoint2D;

    private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidbody;


    public float speed = 1f;
    public float jumpForce = 1f;

    public float runX = 1f;
    public float cameraYParalax = 0.5f;
    private float _horizontalMove;

    [SerializeField] public Bubble _bubble;

    private bool isTalkNow = false;


    public float deadVelocity = -20.0f;
    private bool shouldDead = false;
    private bool isDead = false;

    private bool canDoActionNow = true;
    private bool hasConnectedBody = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        _springJoint2D = GetComponent<SpringJoint2D>();
        aliveCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void CheckFallDead()
    {
        if (!isKoldun || isDead) return;

        if (shouldDead)
        {
            if (_rigidbody.velocity.y > -0.001f) SetDead();
        }
        else
        {
            if (_rigidbody.velocity.y < deadVelocity)
            {
                shouldDead = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger && other.gameObject.tag.Equals("Door") && isKoldun)
        {
            SetDead();
        }
    }

    public void SetDead()
    {
        isDead = true;
        Destroy(aliveCollider2D);
        deadCollider.SetActive(true);
    }

    private void Update()
    {
        CheckFallDead();

        animator.SetFloat("xVelocity", Math.Abs(_rigidbody.velocity.x));
        animator.SetFloat("yVelocity", _rigidbody.velocity.y);
        animator.SetBool("isDead", isDead);
    }

    void FixedUpdate()
    {
        if (isDead) return;

        if (walkAudio != null)
        {
            if (Math.Abs(_rigidbody.velocity.x) > 0.01) //сука звук
            {
                walkAudio.Play();
            }
            else
            {
                walkAudio.Pause();
            }
        }


        _horizontalMove = Input.GetAxisRaw("Horizontal");
        var position = transform.position;
//        mainCamera.transform.position = new Vector3(position.x, position.y + cameraYParalax, -10);

//
//        isRun = Mathf.Abs(_horizontalMove) > 0.002f;
//

        if (_horizontalMove > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            if (_horizontalMove < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }

        if (Input.GetKey(KeyCode.A) && _rigidbody.velocity.y < 0.01f)
        {
            _rigidbody.velocity = new Vector2(runX * -speed, _rigidbody.velocity.y);
        }
        else
        {
            if (Input.GetKey(KeyCode.D) && _rigidbody.velocity.y < 0.01f)
            {
                _rigidbody.velocity = new Vector2(runX * speed, _rigidbody.velocity.y);
            }
        }

        if (Input.GetKey(KeyCode.Space) && _rigidbody.velocity.y < 0.01f)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

//        if (Input.GetKey(KeyCode.E) && !_bubble.isTalkNow)
//        {
//            _bubble.ShowAllTexts();
//        }

        if (_rigidbody.velocity.y < deadVelocity - 3)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, deadVelocity - 3);
        }

        if (Input.GetKey(KeyCode.E) && canDoActionNow)
        {
            StartCoroutine(DoAction());
        }

        if (Input.GetKey(KeyCode.F) && canDoActionNow)
        {
            StartCoroutine(PickItem());
        }

        if (hasConnectedBody)
        {
            if (Input.GetKey(KeyCode.R))
            {
                _springJoint2D.enableCollision = false;
                if (Input.GetKey(KeyCode.A))
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    _springJoint2D.connectedBody.transform.rotation = Quaternion.Lerp(transform.rotation,
                        Quaternion.AngleAxis(270, new Vector3(0, 0, 1)), Time.deltaTime * rotateItemsSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
                    _springJoint2D.connectedBody.transform.rotation = Quaternion.Lerp(transform.rotation,
                        Quaternion.AngleAxis(90, new Vector3(0, 0, 1)), Time.deltaTime * rotateItemsSpeed);
                }
            }
            else
            {
                _springJoint2D.enableCollision = true;
            }
        }
    }

    private IEnumerator DoAction()
    {
        canDoActionNow = false;

        var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, actionRadius);
        foreach (var col in colliders)
        {
            if (col.tag.Equals("Usable"))
            {
                col.gameObject.GetComponent<IUsableItem>().UseItem(this);
                break;
            }
        }

        yield return new WaitForSeconds(0.2f);
        canDoActionNow = true;
    }

    private IEnumerator PickItem()
    {
        canDoActionNow = false;
        if (hasConnectedBody)
        {
            UnconnectBody();
            hasConnectedBody = false;
        }
        else
        {
            var colliders = Physics2D.OverlapCircleAll(gameObject.transform.position, actionRadius);

            foreach (var col in colliders)
            {
                if (col.tag.Equals("Pickable"))
                {
                    ConnectBody(col.GameObject().GetComponent<Rigidbody2D>());
                    hasConnectedBody = true;
                    break;
                }
            }
        }

        yield return new WaitForSeconds(0.2f);
        canDoActionNow = true;
    }

    private void ConnectBody(Rigidbody2D a_Rigidbody2D)
    {
        _springJoint2D.enabled = true;
        _springJoint2D.connectedBody = a_Rigidbody2D;
    }

    private void UnconnectBody()
    {
        _springJoint2D.connectedBody = null;
        _springJoint2D.enabled = false;
    }
}