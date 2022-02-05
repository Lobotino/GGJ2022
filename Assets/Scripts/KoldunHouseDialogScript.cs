using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KoldunHouseDialogScript : MonoBehaviour
{
    [SerializeField] Bubble koldunBubble;

    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private Image _image;

    public float speed = 1f, darkSpeed = 1f;

    public bool needToBlack;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(2);
        yield return koldunBubble.ShowTextWithDelay("Этот день настал...", 3f);

        yield return koldunBubble.ShowTextWithDelay("Всё получилось!", 3f);
        _rigidbody2D.AddForce(Vector2.right * speed, ForceMode2D.Impulse);

        yield return koldunBubble.ShowTextWithDelay("ПОЛУЧИЛОСЬ!", 4f);
        needToBlack = true;
        yield return koldunBubble.ShowTextWithDelay("АХАХАХАХА!", 5f);
        SceneManager.LoadScene("Koldun_posle");
        
    }

    private void Update()
    {
        if (needToBlack)
        {
            _image.color = Color.Lerp(_image.color, Color.black, Time.deltaTime * darkSpeed);
            
        }
    }
}
