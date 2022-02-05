using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoldunDialogs : MonoBehaviour
{
    [SerializeField] public Bubble koldunBubble;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dialog());
    }

    public void FinishDialog()
    {
        StartCoroutine(finishDialog());
    }

    private IEnumerator dialog()
    {
        yield return new WaitForSeconds(2);
        yield return koldunBubble.ShowTextWithDelay(
            "Так, вроде оно! Сюда ведёт след великих технологий." , 1);
        yield return koldunBubble.ShowTextWithDelay(
            "Я должен их заполучить." , 2);
    }

    private IEnumerator finishDialog()
    {
        yield return new WaitForSeconds(1);
        yield return koldunBubble.ShowTextWithDelay(
            "Вот оно! Наконец-то..." , 4);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
