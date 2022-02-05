using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopColliderController : MonoBehaviour
{
    public bool isTurnedOn = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("BatteryTop"))
        {
            isTurnedOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("BatteryTop"))
        {
            isTurnedOn = false;
        }
    }
}
