using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public string sceneName;

    public void OnButtonClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}