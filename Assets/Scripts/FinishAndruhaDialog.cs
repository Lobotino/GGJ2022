using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAndruhaDialog : MonoBehaviour
{
    [SerializeField] public Bubble AndruhaBubble;

    [SerializeField] public GameController _GameController;

    private bool isTriggerAlready = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggerAlready) return;

        isTriggerAlready = true;
        StartCoroutine(StartDialog());
    }

    private IEnumerator StartDialog()
    {
        yield return new WaitForSeconds(1f);
        yield return AndruhaBubble.ShowTextWithDelay("Ну что, всё нашел?", 3f);
        yield return AndruhaBubble.ShowTextWithDelay("Поехали в участок оформляться", 3f);
        yield return _GameController.ShowFinalCutScene();
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}