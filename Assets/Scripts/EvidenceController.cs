using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EvidenceController : MonoBehaviour
{
    [SerializeField] public List<Evidence> allEvidences;

    [SerializeField] public Bubble CopBubble;

    private bool isShowingNow = false, isHidingNow = false;

    [SerializeField] public List<SpriteRenderer> guiTexture;

    [SerializeField] public float allMod, topShowMod, middleShowMod, botShowMod;

    [SerializeField] public Color choosenColor;

    public int evidenceControllerNumber = 0;

    public void onEvidenceClick(string text, int answerNumber)
    {
        ChangeColorToAnswer(answerNumber);
        SetDefaultColorToAllExcpect(answerNumber);
        GameObject.Find("ScriptHolder").GetComponent<EvidencesSaver>()
            .ChooseVariant(evidenceControllerNumber, answerNumber);
        CopBubble.ShowTextOfEvidence(text);
//        HideEvidences();
    }

    private void SetDefaultColorToAllExcpect(int expectAnswerNumber)
    {
        switch (expectAnswerNumber)
        {
            case 0:
                ChangeColorTo(guiTexture[2], allEvidences[1].startColor);
                ChangeColorTo(guiTexture[4], allEvidences[2].startColor);
                break;
            case 1:
                ChangeColorTo(guiTexture[0], allEvidences[0].startColor);
                ChangeColorTo(guiTexture[4], allEvidences[2].startColor);
                break;
            case 2:
                ChangeColorTo(guiTexture[0], allEvidences[0].startColor);
                ChangeColorTo(guiTexture[2], allEvidences[1].startColor);
                break;
        }
    }
    
    private void ChangeColorToAnswer(int answerNumber)
    {
        ChangeColorTo(guiTexture[answerNumber * 2], choosenColor);
    }

    private void ChangeColorTo(SpriteRenderer spriteRenderer, Color color)
    {
        spriteRenderer.color = new Color(color.r, color.g, color.b, spriteRenderer.color.a);
    }

    public void ShowEvidences()
    {
        isHidingNow = false;
        isShowingNow = true;
    }

    public void HideEvidences()
    {
        isHidingNow = true;
        isShowingNow = false;
    }

    private IEnumerator setHidingWithDelay()
    {
        yield return new WaitForSeconds(2);
        isHidingNow = true;
    }

    void Update()
    {
        if (isShowingNow)
        {
            IncreaseAlpha(1f);
        }
        else
        {
            if (isHidingNow)
            {
                DecreaseAlpha(0);
            }
        }
    }

    private void IncreaseAlpha(float to)
    {
        if (guiTexture[0].color.a < to)
        {
            guiTexture[0].color =
                Color.Lerp(guiTexture[0].color,
                    new Color(guiTexture[0].color.r, guiTexture[0].color.g, guiTexture[0].color.b, to),
                    Time.deltaTime * allMod * topShowMod);
            guiTexture[1].color =
                Color.Lerp(guiTexture[1].color,
                    new Color(guiTexture[1].color.r, guiTexture[1].color.g, guiTexture[1].color.b, to),
                    Time.deltaTime * allMod * topShowMod);
        }

        if (guiTexture[2].color.a < to)
        {
            guiTexture[2].color = Color.Lerp(guiTexture[2].color,
                new Color(guiTexture[2].color.r, guiTexture[2].color.g, guiTexture[2].color.b, to),
                Time.deltaTime * allMod * middleShowMod);
            guiTexture[3].color = Color.Lerp(guiTexture[3].color,
                new Color(guiTexture[3].color.r, guiTexture[3].color.g, guiTexture[3].color.b, to),
                Time.deltaTime * allMod * middleShowMod);
        }

        if (guiTexture[4].color.a < to)
        {
            guiTexture[4].color =
                Color.Lerp(guiTexture[4].color,
                    new Color(guiTexture[4].color.r, guiTexture[4].color.g, guiTexture[4].color.b, to),
                    Time.deltaTime * allMod * botShowMod);
            guiTexture[5].color =
                Color.Lerp(guiTexture[5].color,
                    new Color(guiTexture[5].color.r, guiTexture[5].color.g, guiTexture[5].color.b, to),
                    Time.deltaTime * allMod * botShowMod);
        }
    }

    private void DecreaseAlpha(float to)
    {
        if (guiTexture[0].color.a > to)
        {
            guiTexture[0].color =
                Color.Lerp(guiTexture[0].color,
                    new Color(guiTexture[0].color.r, guiTexture[0].color.g, guiTexture[0].color.b, to),
                    Time.deltaTime * allMod * topShowMod);
            guiTexture[1].color =
                Color.Lerp(guiTexture[1].color,
                    new Color(guiTexture[1].color.r, guiTexture[1].color.g, guiTexture[1].color.b, to),
                    Time.deltaTime * allMod * topShowMod);
        }

        if (guiTexture[2].color.a > to)
        {
            guiTexture[2].color = Color.Lerp(guiTexture[2].color,
                new Color(guiTexture[2].color.r, guiTexture[2].color.g, guiTexture[2].color.b, to),
                Time.deltaTime * allMod * middleShowMod);
            guiTexture[3].color = Color.Lerp(guiTexture[3].color,
                new Color(guiTexture[3].color.r, guiTexture[3].color.g, guiTexture[3].color.b, to),
                Time.deltaTime * allMod * middleShowMod);
        }

        if (guiTexture[4].color.a > to)
        {
            guiTexture[4].color =
                Color.Lerp(guiTexture[4].color,
                    new Color(guiTexture[4].color.r, guiTexture[4].color.g, guiTexture[4].color.b, to),
                    Time.deltaTime * allMod * botShowMod);
            guiTexture[5].color =
                Color.Lerp(guiTexture[5].color,
                    new Color(guiTexture[5].color.r, guiTexture[5].color.g, guiTexture[5].color.b, to),
                    Time.deltaTime * allMod * botShowMod);
        }
    }
}