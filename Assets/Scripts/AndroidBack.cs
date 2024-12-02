using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AndroidBack : MonoBehaviour
{

    // Update is called once per frame
    private bool pressAgain = false;
    public bool OtherPage = false;
    public GameObject ScrollMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Perform your desired action when the player presses the back button
            BackButtonPress();
        }
    }

    public void toOtherPage()
    {
        OtherPage = true;
    }

    private void BackButtonPress()
    {
        if (OtherPage)
        {
            OtherPage = false;
            if (transform.name == "Shop Back")
            {
                Button Checking = GameObject.Find("Back to Shop").GetComponent<Button>();
                if (Checking != null)
                {
                    Checking.onClick.Invoke();
                }
                else
                {
                    Debug.Log("No button exist");
                }
                return;
            }
            ScrollMenu.GetComponent<ScrollMenu>().NavigateToPage(0);
        }
        else
        {
            if (pressAgain)
            {
                if (transform.name == "Menu Back")
                {
                    GameObject.Find("Quit").GetComponent<Button>().onClick.Invoke();
                }
                if (transform.name == "Shop Back")
                {
                    Button Checking = GameObject.Find("Back to Shop").GetComponent<Button>();
                    if (Checking != null)
                    {
                        Checking.onClick.Invoke();
                    }
                    else
                    {
                        Debug.Log("No button exist");
                    }
                }
                if (transform.name == "Boss Fight Back")
                {
                    GameObject.Find("Pause Button").GetComponent<Button>().onClick.Invoke();
                }
                pressAgain = false;
                OtherPage = false;
            }
            else
            {
                if (transform.name == "Menu Back")
                {
                    GameObject.Find("Cancel").GetComponent<Button>().onClick.Invoke();
                }
                if (transform.name == "Shop Back")
                {
                    Button Checking = GameObject.Find("Back to Menu").GetComponent<Button>();
                    if (Checking != null)
                    {
                        Checking.onClick.Invoke();
                    }
                    else
                    {
                        Debug.Log("No button exist");
                    }
                }
                if (transform.name == "Boss Fight Back")
                {
                    GameObject.Find("Resume").GetComponent<Button>().onClick.Invoke();
                }
                pressAgain = true;
                OtherPage = false;
            }
        }
    }
}
