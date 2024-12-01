using UnityEngine;
using UnityEngine.EventSystems;

public class HoldingScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject Archer;
    private ArcherMovement AM;
    private ArrowSpawn AS;

    // Cooldown variables for AS.checkHolding(false)
    public float cooldownTime = 2f; // Cooldown duration for releasing the button
    private bool onCooldown = false;
    private float cooldownTimer = 0f;

    void Start()
    {
        AM = Archer.GetComponent<ArcherMovement>();
        AS = Archer.GetComponent<ArrowSpawn>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Button press started!");
        AM.isHolding = true;
        AS.checkHolding(true); // This action is unaffected by cooldown
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Button press released!");
        AM.isHolding = false;
        if (onCooldown)
        {
            Debug.Log("AS.checkHolding(false) is on cooldown! Cannot trigger.");
            return; // Prevent calling AS.checkHolding(false) during cooldown
        }

        AS.checkHolding(false); // Trigger the action
        StartCooldown(); // Start the cooldown
    }

    void Update()
    {
        // Handle cooldown timer
        if (onCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                onCooldown = false; // Reset cooldown
                Debug.Log("Cooldown for AS.checkHolding(false) has ended.");
                AS.checkHolding(false);
            }
        }
    }

    private void StartCooldown()
    {
        onCooldown = true; // Set the cooldown flag
        cooldownTimer = cooldownTime; // Initialize the cooldown timer
        Debug.Log($"Cooldown for AS.checkHolding(false) started for {cooldownTime} seconds.");
    }
}