using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public bool isKoldunPlayingNow = true;

    [SerializeField] private List<GameObject> needToDestroyItems, needToShowItems;

    [SerializeField] public GameObject policeFirstCutScene, policeFinalCutScene;

    [SerializeField] public Image darkObject;

    public bool needToDark = false;
    
    public void FinishKoldunGameplay(bool isCheat)
    {
        if(!isKoldunPlayingNow) return;
        
        isKoldunPlayingNow = false;
        
        GameObject.Find("Koldun").GetComponent<KoldunDialogs>().FinishDialog();
        
        StartCoroutine(ShowCutScene());
    }

    public IEnumerator ShowCutScene()
    {
        yield return new WaitForSeconds(3);
        needToDark = true;
        yield return new WaitForSeconds(5);
        policeFirstCutScene.SetActive(true);
        foreach (var item in needToDestroyItems)
        {
            Destroy(item);
        }
        needToDark = false;
    }

    public void ShowFinalScene()
    {
        StartCoroutine(ShowFinalCutScene());
    }
    
    public IEnumerator ShowFinalCutScene()
    {
        yield return new WaitForSeconds(3);
        needToDark = true;
        yield return new WaitForSeconds(4);
        policeFinalCutScene.SetActive(true);
        needToDark = false;
    }

    public void FixedUpdate()
    {
        if (needToDark)
        {
            darkObject.color = Color.Lerp(darkObject.color, Color.black, Time.deltaTime * 2);
        }
        else
        {
            darkObject.color = Color.Lerp(darkObject.color, Color.clear, Time.deltaTime * 2);
        }
    }

    public void SetupCops()
    {
        StartCoroutine(cops());
    }

    private IEnumerator cops()
    {
        yield return new WaitForSeconds(3);
        needToDark = true;
        yield return new WaitForSeconds(5);
        policeFirstCutScene.SetActive(false);
        foreach (var item in needToShowItems)
        {
            item.SetActive(true);
        }

        needToDark = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            FinishKoldunGameplay(true);
        }

        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
