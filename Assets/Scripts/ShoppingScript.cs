using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI leveltxt;
    public TextMeshProUGUI costtxt;
    private int level;
    public Button button;
    public int[] prices;
    public int maxLevel;
    private int Gold;
    public const string currencyKey = "currency";
    public const string maxHealthKey = "maxHealth";
    public const string attackPowerKey = "attackPower";
    public const string attackSpeedKey = "attackSpeed";
    public const string moveSpeedKey = "moveSpeed";
    public const string critMultKey = "criticalMultiplier";
    public const string critChanceKey = "criticalChance";
    public const string goldMultKey = "goldMultiplier";

    private void Start()
    {
        Gold = PlayerPrefs.GetInt(currencyKey, 0);
        switch (gameObject.name)
        {
            case "Attack Power":
                level = PlayerPrefs.GetInt(attackPowerKey, 1);
                break;
            case "Attack Speed":
                level = PlayerPrefs.GetInt(attackSpeedKey, 1);
                break;
            case "Critical Multiplier":
                level = PlayerPrefs.GetInt(critMultKey, 1);
                break;
            case "Critical Chance":
                level = PlayerPrefs.GetInt(critChanceKey, 1);
                break;
            case "Max Health":
                level = PlayerPrefs.GetInt(maxHealthKey, 1);
                break;
            case "Movement Speed":
                level = PlayerPrefs.GetInt(moveSpeedKey, 1);
                break;
            case "Gold Multiplier":
                level = PlayerPrefs.GetInt(goldMultKey, 1);
                break;
        }
    }
    private void Update()
    {
        if (level - 1 >= 0)
        {
            costtxt.text = prices[level - 1].ToString();
            if (Gold >= prices[level - 1] && level < maxLevel)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    public void OnUpgrade()
    {
        level++;
        int i = level;
        if (i > maxLevel - 1)
        {
            button.interactable = false;
        }
        slider.value--;
        leveltxt.text = "Level " + level;
        Gold = Gold - prices[level - 1];
        //PlayerPrefs.SetInt(currencyKey, Gold);
        switch (gameObject.name)
        {
            case "Attack Power":
                Debug.Log("Attack Power Upgraded!");
                //PlayerPrefs.SetInt(attackPowerKey, level);
                break;
            case "Attack Speed":
                Debug.Log("Attack Speed Upgraded!");
                //PlayerPrefs.SetInt(attackSpeedKey, level);
                break;
            case "Critical Multiplier":
                Debug.Log("Critical Multiplier Upgraded");
                //PlayerPrefs.SetInt(critMultKey, level);
                break;
            case "Critical Chance":
                Debug.Log("Crit Chance Upgraded");
                //PlayerPrefs.SetInt(critChanceKey, level);
                break;
            case "Max Health":
                Debug.Log("Max Health Upgraded");
                //PlayerPrefs.SetInt(maxHealthKey, level);
                break;
            case "Movement Speed":
                Debug.Log("Movement Speed Upgraded");
                //PlayerPrefs.SetInt(moveSpeedKey, level);
                break;
            case "Gold Multiplier":
                Debug.Log("Gold Multiplier Upgraded");
                //PlayerPrefs.SetInt(goldMultKey, level);
                break;
        }
        //PlayerPrefs.Save();
    }
}
