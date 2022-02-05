using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomColliderController : MonoBehaviour
{
    public bool isTurnedOn = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("BatteryBottom"))
        {
            isTurnedOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("BatteryBottom"))
        {
            isTurnedOn = false;
        }
    }
}
