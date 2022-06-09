using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public string SceneToLoad;

    public void Continue()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
