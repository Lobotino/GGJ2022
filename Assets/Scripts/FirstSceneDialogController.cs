using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FirstSceneDialogController : MonoBehaviour
{
    [SerializeField] public SpriteRenderer GCopSpriteRenderer;

    [SerializeField] public float AndruhaSpeed = 5f, GCopSpeed = 4f;
    
    [SerializeField] public Rigidbody2D AndruhaBody, GCopBody;

    [SerializeField]
    public Bubble AndruhaDialog, GCopDialog;

    [SerializeField] public GameController _gameController;

    public Animator AndruhaAnimator;

    private bool AndruhaOut, GGCopOut;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dialogCoroutine());
    }

    private IEnumerator dialogCoroutine()
    {
        yield return new WaitForSeconds(2);
        yield return GCopDialog.ShowTextWithDelay(0, 2);
        yield return AndruhaDialog.ShowTextWithDelay(0, 2);
        yield return GCopDialog.ShowTextWithDelay(1, 2);
        yield return AndruhaDialog.ShowTextWithDelay(1, 2);
        yield return GCopDialog.ShowTextWithDelay(2, 2);
        yield return GCopDialog.ShowTextWithDelay(3, 1);
        
        yield return new WaitForSeconds(1);
        GGCopOut = true;
        GCopSpriteRenderer.flipX = true;
        yield return new WaitForSeconds(0.5f);
        AndruhaOut = true;
        AndruhaAnimator.SetBool("isRun", true);
        
        _gameController.SetupCops();
    }

    private void FixedUpdate()
    {
        if (AndruhaOut)
        {
            AndruhaBody.velocity = new Vector2(-AndruhaSpeed, AndruhaBody.velocity.y);
        }
        
        if (GGCopOut)
        {
            GCopBody.velocity = new Vector2(-GCopSpeed, GCopBody.velocity.y);
        }
    }
}
