using UnityEngine;

public class Toggle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool ToggleValue;

    public void ToggleChange() 
    {
        ToggleValue = !ToggleValue;
    }
}
