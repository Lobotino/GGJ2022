using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCheat : MonoBehaviour
{
    [SerializeField]
    public GameObject cheatObjects;

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.G))
        {
            cheatObjects.SetActive(true);
        }
    }
}
