using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    [SerializeField] public bool isEnable = false;

    [SerializeField] public float piuTimer = 1f;

    [SerializeField] public bool isRightSide = true;

    [SerializeField] public GameObject piuObject;

    private Animator _animator;

    private BatteryHolder _batteryHolder;
    
    void Start()
    {
        _batteryHolder = GetComponent<BatteryHolder>();
        _batteryHolder.AddTurnOnAction(TurnOnAction);
        _batteryHolder.AddTurnOffAction(TurnOffAction);

        _animator = GetComponent<Animator>();
    }

    private void TurnOnAction()
    {
        isEnable = true;
        StartCoroutine(piuFun());
        _animator.SetBool("isEnable", true);
    }

    private void TurnOffAction()
    {
        isEnable = false;
        _animator.SetBool("isEnable", false);
    }

    private IEnumerator piuFun()
    {
        while (isEnable)
        {
            piu();
            yield return new WaitForSeconds(piuTimer);
        }
    }

    private void piu()
    {
        var position = gameObject.transform.position;
        GameObject piu = Instantiate(piuObject,
            new Vector3(position.x + (isRightSide ? 1 : -1) * 0.471f, position.y + 0.559f, 0),
            Quaternion.identity);

        piu.GetComponent<PiuScript>().isRight = isRightSide;
    }
}