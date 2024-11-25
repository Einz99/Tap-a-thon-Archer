using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseBtn;
    
    public void OnPause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
    }
    public void OnResume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseBtn.SetActive(true);
    }
}
