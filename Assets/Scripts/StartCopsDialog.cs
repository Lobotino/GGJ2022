using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCopsDialog : MonoBehaviour
{

    [SerializeField] public Bubble AndruhaBubble, GCopBubble;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(1f);
        yield return AndruhaBubble.ShowTextWithDelay("Ну давай, расследуй, я тут тебя подожду...", 3f);
        yield return AndruhaBubble.ShowTextWithDelay("Не забудь собрать как можно больше улик!", 3f);
    }
}
