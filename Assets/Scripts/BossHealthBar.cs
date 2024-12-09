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
    public bool onDefense = false;
    private float defense;
    private bool isInvulnerable = false;
    private float invulnerabilityDuration = 5f;
    private bool enableP2 = true;
    private const string difficultyKey = "difficulty";

    public MasterVolume sfx;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;


        MaxHealth = FC.Health;
        currentHealth = FC.Health;
        int difficulty = PlayerPrefs.GetInt(difficultyKey, 1);
        switch (difficulty)
        {
            case 1: defense = 50f; break;
            case 2: defense = 70f; break;
            case 3: defense = 90f; break;
            default: defense = 50f; break;
        }
    }
    void Update()
    {
        if (extraHearts.transform.childCount == 2)
        {
            Debug.Log("Hearts == 2");
        }
        if (extraHearts.transform.childCount == 2 && !isInvulnerable && FC.isPhase2 && enableP2)
        {
            Debug.Log("Entering Phase2");
            enableP2 = false;
            StartCoroutine(InvulnerabilityTimer());
        }
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
        flash();
    
        if (currentHealth > damage)
        {
            if (onDefense)
            {
                damage = ApplyDefense(damage, defense); // Modify damage with defense
            }

            
            currentHealth -= damage;
            
            sfx.play_SFX(sfx.ARROW_HIT);
            if(gameObject.name == "Beary Boss"){sfx.play_SFX(sfx.BEAR_DAMAGED);}
            if(gameObject.name == "Slime"){sfx.play_SFX(sfx.SLIME_DAMAGED);}
            if(gameObject.name == "Golem Boss"){sfx.play_SFX(sfx.GOLEM_DAMAGED);}

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
                sfx.play_SFX(sfx.VICTORY);
                confetti.SetActive(true);
                confetti.GetComponent<PlayUISprite>().playAnimation("open");
            }
        }
    }
    private float ApplyDefense(float incomingDamage, float defenseValue)
    {
        float damageReduction = defenseValue / 100f;
        return incomingDamage * (1 - damageReduction);
    }
    public IEnumerator DefenseTimer(float duration)
    {
        onDefense = true;  // Enable defense
        Debug.Log("Defense activated!");

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // After the defense duration ends, turn off defense
        onDefense = false;
        Debug.Log("Defense deactivated!");
    }


    private IEnumerator InvulnerabilityTimer()
    {
        // Make the boss invulnerable for 5 seconds
        if (gameObject.name == "Beary Boss")
        {
            gameObject.GetComponent<BossBehavior>().phase2 = true;
        }
        if (gameObject.name == "Golem Boss")
        {
            gameObject.GetComponent<GolemBossBehavior>().phase2 = true;
        }
        if (gameObject.name == "Slime")
        {
            gameObject.GetComponent<SlimeBossBehavior>().phase2 = true;
        }
        isInvulnerable = true;
        Debug.Log("Boss is now invulnerable!");

        // Wait for the invulnerability duration
        yield return new WaitForSeconds(invulnerabilityDuration);

        // End invulnerability after 5 seconds
        isInvulnerable = false;
        Debug.Log("Boss is no longer invulnerable.");
    }

    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    public SpriteRenderer spriteRenderer;

    public Material originalMaterial;

    private Coroutine flashRoutine;

    public void flash(){
        if(flashRoutine != null){
            StopCoroutine(flashRoutine);
        }


        flashRoutine = StartCoroutine(flashCoroutine());
    }

    private IEnumerator flashCoroutine(){

        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }


}
