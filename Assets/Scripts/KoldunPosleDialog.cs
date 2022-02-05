using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoldunPosleDialog : MonoBehaviour
{
    public Bubble koldunBubble, Klerk1Bubble, Klerk2Bubble;

    public GameObject Klerk1;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return koldunBubble.ShowTextWithDelay("Не понял", 1);
        yield return Klerk1Bubble.ShowTextWithDelay("В Магнитке дошик по скидке, пошли пожрём", 1);
        yield return Klerk2Bubble.ShowTextWithDelay("Дошик 200 рублей стоит, у меня нет таких денег", 1);
        yield return new WaitForSeconds(1);
        Klerk1.GetComponent<SpriteRenderer>().flipX = true;
        yield return koldunBubble.ShowTextWithDelay("Где у вас наукой занимаются?", 2);
        yield return Klerk1Bubble.ShowTextWithDelay("Все ученые в лаборатории Квантовое Асколково. Это прямо по улице.", 3);
    }
}
