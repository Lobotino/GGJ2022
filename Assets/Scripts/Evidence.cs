using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Evidence : MonoBehaviour
{
    [SerializeField]
    public string reasonText = "";

    [SerializeField] public int answerNumber = 0;
    
    private EvidenceController _evidenceController;

    public Color startColor;
    
    void Start()
    {
        startColor = GetComponent<SpriteRenderer>().color;
        _evidenceController = GetComponentInParent<EvidenceController>();
    }
    
    private void OnMouseDown()
    {
        _evidenceController.onEvidenceClick(reasonText, answerNumber);
    }
}
