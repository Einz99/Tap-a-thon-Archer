using UnityEngine;
using UnityEngine.EventSystems;

public class HoldingScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public  GameObject Archer;
    private ArcherMovement AM;
    private ArrowSpawn AS;
    void Start()
    {
        AM = Archer.GetComponent<ArcherMovement>();
        AS = Archer.GetComponent<ArrowSpawn>();
    }

    // remove comment if holding never spawns arrow
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button press started!");
        AM.isHolding = true;
        //AS.checkHolding(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Button press released!");
        AM.isHolding = false;
        //AS.checkHolding(false);
    }

}
