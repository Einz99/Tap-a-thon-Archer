using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{

    public string panel;
    public GameObject backdrop;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start(){
        

        if(panel == "confirm"){
            transform.localScale = Vector2.zero;
        }
    }

    public void Open(){
        backdrop.SetActive(true);
        backdrop.GetComponent<CanvasGroup>().alpha = 0;
        backdrop.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f).setIgnoreTimeScale(true);

        if(panel == "info"){
            transform.localPosition = new Vector2(0f, Screen.height);
            transform.LeanMoveLocalY(0, 0.5f).setEaseOutBack().setIgnoreTimeScale(true).delay = 0.1f;
        }
        
        if(panel == "settings"){
            transform.localPosition = new Vector2(0f, Screen.height);
            transform.LeanMoveLocalY(0, 0.5f).setEaseOutBack().setIgnoreTimeScale(true).delay = 0.1f;
        }

        if(panel == "confirm"){
            transform.LeanScale(Vector2.one, 0.4f).setEaseOutBack().setIgnoreTimeScale(true);
        }

    }

    public void Close(){
        backdrop.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f).setIgnoreTimeScale(true);

        if(panel == "info"){
            transform.LeanMoveLocalY(Screen.height, 0.4f).setEaseInBack().setIgnoreTimeScale(true);
        }

        if(panel == "settings"){
            transform.LeanMoveLocalY(Screen.height, 0.4f).setEaseInBack().setIgnoreTimeScale(true);
        }

        if(panel == "confirm"){
            transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack().setIgnoreTimeScale(true);
        }


        backdrop.SetActive(false);
    }

}
