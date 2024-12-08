using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool ToggleValue = true;
    public Sprite mute_button;
    public Sprite unmute_button;

    public MasterVolume master;

    [Header("UNMUTE")]

    public SpriteState pressed_unmute_button;

    [Header("MUTE")]

    public SpriteState pressed_mute_button;

    public void CheckStart(){
        if(master.Settings.musicLevel != 0 && master.Settings.sfxLevel != 0){
            transform.GetComponent<Image>().sprite = mute_button;
            transform.GetComponent<Button>().spriteState = pressed_mute_button;
        }else{
            Debug.Log("UNMUTE");
            transform.GetComponent<Image>().sprite = unmute_button;
            transform.GetComponent<Button>().spriteState = pressed_unmute_button;
        }
    }

    public void ToggleChange() 
    {
        ToggleValue = !ToggleValue;

        if(ToggleValue){
            Debug.Log("UNMUTE");
            transform.GetComponent<Image>().sprite = unmute_button;
            transform.GetComponent<Button>().spriteState = pressed_unmute_button;
            master.UnmuteAll();
        }else{
            transform.GetComponent<Image>().sprite = mute_button;
            transform.GetComponent<Button>().spriteState = pressed_mute_button;
            master.MuteAll();
        }
    }
}
