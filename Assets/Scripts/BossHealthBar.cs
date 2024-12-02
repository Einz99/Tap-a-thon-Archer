using UnityEngine;
using UnityEngine.UI;

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
    private int firsthit;
    
    private void Start()
    {
        MaxHealth = FC.Health;
        currentHealth = FC.Health;
        firsthit = 0;
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
            Debug.Log(percentage);
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
            }
        }
    }
}
