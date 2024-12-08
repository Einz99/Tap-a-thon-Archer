using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    public MasterVolume sfx;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PointerEnter(){
        if(transform.GetComponent<Button>().interactable){
            transform.LeanScale(new Vector2(1.1f, 1.1f), 0.2f).setEaseOutBack().setIgnoreTimeScale(true);
            sfx.play_SFX(sfx.HOVER);
        }
    }

    public void PointerExit(){
        if(transform.GetComponent<Button>().interactable){
            transform.LeanScale(new Vector2(1f, 1f), 0.2f).setEaseInBack().setIgnoreTimeScale(true);
        }
    }

    public void HandlePointerEnter(){
            sfx.play_SFX(sfx.HOVER);
            transform.LeanScale(new Vector2(1.1f, 1.1f), 0.2f).setEaseOutBack().setIgnoreTimeScale(true);
        
    }

    public void HandlePointerExit(){
     
            transform.LeanScale(new Vector2(1f, 1f), 0.2f).setEaseInBack().setIgnoreTimeScale(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
