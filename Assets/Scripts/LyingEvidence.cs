using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LyingEvidence : MonoBehaviour
{
    [SerializeField] public EvidenceController EvidenceController;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GameObject().name.Equals("GCop"))
        {
            EvidenceController.ShowEvidences();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GameObject().name.Equals("GCop"))
        {
            EvidenceController.HideEvidences();
        }
    }
}