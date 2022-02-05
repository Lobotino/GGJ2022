using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkScript : MonoBehaviour
{
    public Text text;
    public float blinkTick = 0.4f;

    private Color startColor;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        startColor = text.color;
        StartCoroutine(tick());
    }

    IEnumerator tick()
    {
        while (true)
        {
            yield return new WaitForSeconds(blinkTick);
            text.color = Color.clear;
            yield return new WaitForSeconds(blinkTick/2);
            text.color = startColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
