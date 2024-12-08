using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool ToggleValue;
    public Sprite mute_button;
    public Sprite unmute_button;

    [Header("UNMUTE")]

    public SpriteState pressed_unmute_button;

    [Header("MUTE")]

    public SpriteState pressed_mute_button;

    public void ToggleChange() 
    {
        ToggleValue = !ToggleValue;

        if(ToggleValue){
            transform.GetComponent<Image>().sprite = mute_button;
            transform.GetComponent<Button>().spriteState = pressed_mute_button;
        }else{
            transform.GetComponent<Image>().sprite = unmute_button;
            transform.GetComponent<Button>().spriteState = pressed_unmute_button;
        }
    }
}
