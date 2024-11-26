using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AndroidBack : MonoBehaviour
{

    // Update is called once per frame
    private bool pressAgain = true;
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

    private void BackButtonPress()
    {
        if (OtherPage)
        {
            ScrollMenu.GetComponent<ScrollMenu>().NavigateToPage(0);
            OtherPage = false;
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
                    GameObject.Find("Back to Menu").GetComponent<Button>().onClick.Invoke();
                }
                if (transform.name == "Back To Shop")
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
                    GameObject.Find("Shop Cancel").GetComponent<Button>().onClick.Invoke();
                }
                if (transform.name == "Back To Shop")
                {
                    GameObject.Find("Resume").GetComponent<Button>().onClick.Invoke();
                }
                pressAgain = true;
                OtherPage = false;
            }
        }
    }
}
