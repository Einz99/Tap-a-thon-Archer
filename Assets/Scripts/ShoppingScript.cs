using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI leveltxt;
    public TextMeshProUGUI costtxt;
    private int level;
    public Button button;
    private int[] prices;
    public int maxLevel;
    private static int Gold;
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
        GameObject.Find("Amount_txt").GetComponent<TextMeshProUGUI>().text = Gold.ToString();
        prices = new int[maxLevel + 1];
        switch (gameObject.name)
        {
            case "Attack Power":
                level = PlayerPrefs.GetInt(attackPowerKey, 0);
                for (int i = 0; i < prices.Length; i++)
                {
                    int baseprice = 50;
                    if(i != maxLevel)
                    {
                        prices[i] = (int)math.round(baseprice * math.pow(1.5, i + 1));
                    }
                    else
                    {
                        prices[i] = 0;
                    }
                }
                break;
            case "Attack Speed":
                level = PlayerPrefs.GetInt(attackSpeedKey, 0);
                for (int i = 0; i < prices.Length; i++)
                {
                    int baseprice = 50;
                    if(i != maxLevel)
                    {
                        prices[i] = (int)math.round(baseprice * math.pow(1.5, i + 1));
                    }
                    else
                    {
                        prices[i] = 0;
                    }
                }
                break;
            case "Critical Multiplier":
                level = PlayerPrefs.GetInt(critMultKey, 0);
                for (int i = 0; i < prices.Length; i++)
                {
                    int baseprice = 50;
                    if(i != maxLevel)
                    {
                        prices[i] = (int)math.round(baseprice * math.pow(1.5, i + 1));
                    }
                    else
                    {
                        prices[i] = 0;
                    }
                }
                break;
            case "Critical Chance":
                level = PlayerPrefs.GetInt(critChanceKey, 0);
                for (int i = 0; i < prices.Length; i++)
                {
                    int baseprice = 50;
                    if(i != maxLevel)
                    {
                        prices[i] = (int)math.round(baseprice * math.pow(1.5, i + 1));
                    }
                    else
                    {
                        prices[i] = 0;
                    }
                }
                break;
            case "Max Health":
                level = PlayerPrefs.GetInt(maxHealthKey, 0);
                for (int i = 0; i < prices.Length; i++)
                {
                    int baseprice = 50;
                    if(i != maxLevel)
                    {
                        prices[i] = (int)math.round(baseprice * math.pow(1.8, i + 1));
                    }
                    else
                    {
                        prices[i] = 0;
                    }
                }
                break;
            case "Movement Speed":
                level = PlayerPrefs.GetInt(moveSpeedKey, 0);
                for (int i = 0; i < prices.Length; i++)
                {
                    int baseprice = 50;
                    if(i != maxLevel)
                    {
                        prices[i] = (int)math.round(baseprice * math.pow(1.6, i + 1));
                    }
                    else
                    {
                        prices[i] = 0;
                    }
                }
                break;
            case "Gold Multiplier":
                level = PlayerPrefs.GetInt(goldMultKey, 0);
                for (int i = 0; i < prices.Length; i++)
                {
                    int baseprice = 50;
                    if(i != maxLevel)
                    {
                        prices[i] = (int)math.round(baseprice * math.pow(2, i + 1));
                    }
                    else
                    {
                        prices[i] = 0;
                    }
                }
                break;
        }
        switch (gameObject.name)
        {
            case "Attack Power":
                slider.value = maxLevel - PlayerPrefs.GetInt(attackPowerKey, 0);
                leveltxt.text = "LVL. " + level;
                break;
            case "Attack Speed":
                slider.value = maxLevel - PlayerPrefs.GetInt(attackSpeedKey, 0);
                leveltxt.text = "LVL. " + level;
                break;
            case "Critical Multiplier":
                slider.value = maxLevel - PlayerPrefs.GetInt(critMultKey, 0);
                leveltxt.text = "LVL. " + level;
                break;
            case "Critical Chance":
                slider.value = maxLevel - PlayerPrefs.GetInt(critChanceKey, 0);
                leveltxt.text = "LVL. " + level;
                break;
            case "Max Health":
                slider.value = maxLevel - PlayerPrefs.GetInt(maxHealthKey, 0);
                leveltxt.text = "LVL. " + level;
                break;
            case "Movement Speed":
                slider.value = maxLevel - PlayerPrefs.GetInt(moveSpeedKey, 0);
                leveltxt.text = "LVL. " + level;
                break;
            case "Gold Multiplier":
                slider.value = maxLevel - PlayerPrefs.GetInt(goldMultKey, 0);
                leveltxt.text = "LVL. " + level;
                break;
            
        }
    }

    public Sprite insufficient_image;
    public Sprite max_image;



    private void Update()
    {
        if(prices[level] == 0){
            costtxt.text = "MAX";
        }else{
            costtxt.text = prices[level].ToString();
        }

        if (level > maxLevel-1)
        {

            button.GetComponent<Image>().sprite = max_image;
            button.interactable = false;
            return;
        }

        if (Gold < prices[level])
        {
            button.interactable = false;
            button.GetComponent<Image>().sprite = insufficient_image;
            return;
        }
    }

    public void OnUpgrade()
    {   
        Gold -= prices[level];
        slider.value--;
        level++;
        leveltxt.text = "LVL. " + level;
        GameObject.Find("Amount_txt").GetComponent<TextMeshProUGUI>().text = Gold.ToString();
        PlayerPrefs.SetInt(currencyKey, Gold);
        switch (gameObject.name)
        {
            case "Attack Power":
                Debug.Log("Attack Power Upgraded!");
                PlayerPrefs.SetInt(attackPowerKey, level);
                break;
            case "Attack Speed":
                Debug.Log("Attack Speed Upgraded!");
                PlayerPrefs.SetInt(attackSpeedKey, level);
                break;
            case "Critical Multiplier":
                Debug.Log("Critical Multiplier Upgraded");
                PlayerPrefs.SetInt(critMultKey, level);
                break;
            case "Critical Chance":
                Debug.Log("Crit Chance Upgraded");
                PlayerPrefs.SetInt(critChanceKey, level);
                break;
            case "Max Health":
                Debug.Log("Max Health Upgraded");
                PlayerPrefs.SetInt(maxHealthKey, level);
                break;
            case "Movement Speed":
                Debug.Log("Movement Speed Upgraded");
                PlayerPrefs.SetInt(moveSpeedKey, level);
                break;
            case "Gold Multiplier":
                Debug.Log("Gold Multiplier Upgraded");
                PlayerPrefs.SetInt(goldMultKey, level);
                break;
        }
        if (level > maxLevel-1 || Gold < prices[level])
        {
            button.interactable = false;
        }
        PlayerPrefs.Save();
    }
}
