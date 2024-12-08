using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealthBar : MonoBehaviour
{
    public float MaxHealth;
    private float currentHealth;
    public float damage = 1;
    public Slider BossHp;
    public GameObject extraHearts;
    public GameObject pause;
    public GameObject winningPage;
    public FightCalculation FC;
    public GameObject confetti;

    private bool isInvulnerable = false;
    private float invulnerabilityDuration = 5f;
    private bool enableP2 = true;
    void Update()
    {   
        if(extraHearts.transform.childCount == 2)
        {
            Debug.Log("Hearts == 2");
        }
        if (BossHp.value == 100)
        {
            Debug.Log("100");
        }
        if (extraHearts.transform.childCount == 2 && !isInvulnerable && FC.isPhase2 && enableP2)
        {
            Debug.Log("Entering Phase2");
            enableP2 = false;
            StartCoroutine(InvulnerabilityTimer());
        }
    }

    private void Start()
    {
        MaxHealth = FC.Health;
        currentHealth = FC.Health;
    }
    void OnTriggerEnter2D(Collider2D x)
    {
        
        if (x.gameObject.CompareTag("Arrows"))
        {
            HealthCalculations();
        }
    }

    private void HealthCalculations()
    {
        if (currentHealth > damage)
        {
            currentHealth -= damage;
            float percentage = 100 - ((currentHealth / MaxHealth) * 100);
            BossHp.value = percentage;
        }
        else
        {
            currentHealth -= currentHealth;
            BossHp.value = 100;
            if (extraHearts.transform.childCount != 0)
            {
                Transform child = extraHearts.transform.GetChild(0);
                if (BossHp.value == 100 && child != null)
                {
                    Destroy(child.gameObject);
                    currentHealth = MaxHealth;
                    BossHp.value = 0;
                }
            }
            else
            {
                Time.timeScale = 0;
                pause.SetActive(false);
                winningPage.SetActive(true);
                confetti.SetActive(true);
                confetti.GetComponent<PlayUISprite>().playAnimation("open");
            }
        }
    }
    private IEnumerator InvulnerabilityTimer()
    {
        // Make the boss invulnerable for 5 seconds
        if (gameObject.name == "Beary Boss")
        {
            gameObject.GetComponent<BossBehavior>().phase2 = true;
        }
        isInvulnerable = true;
        Debug.Log("Boss is now invulnerable!");

        // Wait for the invulnerability duration
        yield return new WaitForSeconds(invulnerabilityDuration);

        // End invulnerability after 5 seconds
        isInvulnerable = false;
        Debug.Log("Boss is no longer invulnerable.");
    }
}
