using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void ButtonMoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}