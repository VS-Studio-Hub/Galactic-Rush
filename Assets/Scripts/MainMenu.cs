using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //public GameObject credits;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    //public void Credits()
    //{
    //    credits.SetActive(true);
    //}

    //public void BackCredits()
    //{
    //    credits.SetActive(false);
    //}

    public void QuitGame()
    {
        Application.Quit();
    }

}
