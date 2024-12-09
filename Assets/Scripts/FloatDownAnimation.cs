using UnityEngine;

public class FloatDownAnimation : MonoBehaviour
{
    public float delay;
    public float buttonPos;

    public float panelPosX;

    public MasterVolume sfx;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
            sfx.play_SFX(sfx.WOOD);

            transform.localPosition = new Vector2(panelPosX, Screen.height);
            transform.LeanMoveLocalY(buttonPos, 0.5f).setEaseOutBack().delay = delay;
    
            if(panelPosX != 0){
              transform.LeanMoveLocalY(buttonPos, 0.5f).setEaseOutBack().delay = delay;
            }

            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
