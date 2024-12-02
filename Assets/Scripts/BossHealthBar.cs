using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public float MaxHealth;
    private float currentHealth;
    private float damage = 50;
    public Slider BossHp;
    public GameObject extraHearts;
    private void Start()
    {
        currentHealth = MaxHealth;
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
            }
        }
    }
}
