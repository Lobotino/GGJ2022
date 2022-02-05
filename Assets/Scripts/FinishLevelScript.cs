using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLevelScript : MonoBehaviour
{
    public Image image;

    public float darkSpeed = 2;

    public bool endingNow = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    private IEnumerator nextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (endingNow)
        {
            image.color = Color.Lerp(image.color, Color.black, Time.deltaTime * darkSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        endingNow = true;
        StartCoroutine(nextScene());
    }
}