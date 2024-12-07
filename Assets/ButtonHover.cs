using UnityEngine;

public class ButtonHover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PointerEnter(){
        transform.LeanScale(new Vector2(1.1f, 1.1f), 0.2f).setEaseOutBack().setIgnoreTimeScale(true);
    }

    public void PointerExit(){
        transform.LeanScale(new Vector2(1f, 1f), 0.2f).setEaseInBack().setIgnoreTimeScale(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
