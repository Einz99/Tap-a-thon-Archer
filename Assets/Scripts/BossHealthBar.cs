using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float MaxHealth;
    private float currentHealth;
    private float damage = 500;
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
            float percentage = currentHealth / MaxHealth;
            Debug.Log(percentage);
            BossHp.value += percentage * 100;
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
