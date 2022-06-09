using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void LevelOne()
    {
        SceneManager.LoadScene("01_Level");
    }

    public void Two()
    {
        SceneManager.LoadScene("02_Level");
    }

    public void Three()
    {
        SceneManager.LoadScene("03_Level");
    }

    public void Four()
    {
        SceneManager.LoadScene("04_Level");
    }

    public void Five()
    {
        SceneManager.LoadScene("05_Level");
    }
}
