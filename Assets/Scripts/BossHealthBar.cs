using TMPro;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI health;
    void OnTriggerEnter2D(Collider2D x)
    {
        if(x.gameObject.CompareTag("Arrows"))
        {
            int hp = int.Parse(health.text);
            hp--;
            health.text = hp.ToString();
        }
    }
}
