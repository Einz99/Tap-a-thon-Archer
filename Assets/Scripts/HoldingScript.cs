using UnityEngine;
using UnityEngine.EventSystems;

public class HoldingScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Archer; // Reference to the Archer GameObject
    private ArcherMovement AM;
    private ArrowSpawn AS;

    public float cooldownTime = 2f; // Cooldown duration for releasing the button
    private bool isOnCooldown = false;

    void Start()
    {
        // Cache references to required components
        AM = Archer.GetComponent<ArcherMovement>();
        AS = Archer.GetComponent<ArrowSpawn>();
    }

    // Called when the button is pressed
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isOnCooldown)
        {
            Debug.Log("Cannot activate AS.checkHolding(true) - still on cooldown!");
            return;
        }

        Debug.Log("Button press started!");
        AM.isHolding = true;       // Enable holding
        AS.checkHolding(true);     // Trigger holding action
    }

    // Called when the button is released
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isOnCooldown)
        {
            Debug.Log("Release ignored - action is still on cooldown.");
            return;
        }

        Debug.Log("Button press released!");
        AM.isHolding = false;       // Disable holding
        StartCooldown();            // Start cooldown
    }

    private void StartCooldown()
    {
        Debug.Log($"Cooldown started for {cooldownTime} seconds.");
        isOnCooldown = true;           // Activate cooldown
        Invoke(nameof(EndCooldown), cooldownTime); // Schedule the end of cooldown
    }

    private void EndCooldown()
    {
        Debug.Log("Cooldown ended. Calling AS.checkHolding(false).");
        isOnCooldown = false;          // Reset cooldown flag
        AS.checkHolding(false);        // Automatically trigger the action after cooldown
    }
}
