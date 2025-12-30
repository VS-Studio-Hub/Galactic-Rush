using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayMenu()
    {
        SceneManager.LoadScene("EnterName");
    }

    public void ScoreMenu()
    {
        SceneManager.LoadScene("Score");
    }

    private void Credit()
    {
        SceneManager.LoadScene("Credit");
    }

    public void QuitMenu()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
