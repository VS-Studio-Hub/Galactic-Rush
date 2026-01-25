using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Credits;

    public void PlayMenu()
    {
        SceneManager.LoadScene("EnterName");
    }

    public void ScoreMenu()
    {
        SceneManager.LoadScene("Score");
    }

    public void Credit()
    {
        Credits.SetActive(true);
    }

    public void X()
    {
        Credits.SetActive(false);
    }

    public void QuitMenu()
    {
        //PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
