using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Koldun_do");
    }
}
