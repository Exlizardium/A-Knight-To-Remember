using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Trader : MonoBehaviour
{
    private Player_Movement player;
    private Gold gold;
    private Button cheapBoozeButton;
    private Button cheapMeatButton;
    [SerializeField] private TextMeshProUGUI remainingFunds;
    [SerializeField] private float cheapGoodsPrice = 10f;
    [SerializeField] private float boozeToAdd = 5f;
    [SerializeField] private float healthToAdd = 5f;
    private bool boozePurchased = false;
    private bool meatPurchased = false;


    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        gold = FindObjectOfType<Gold>();
        cheapBoozeButton = GameObject.Find("CheapBooze").GetComponent<Button>();
        cheapMeatButton = GameObject.Find("CheapMeat").GetComponent<Button>();
        remainingFunds = GameObject.Find("MoneyLeftToTrade").GetComponent<TextMeshProUGUI>();
    }

    private void CheckCredibility()
    {
        if (gold.goldPoints < cheapGoodsPrice)
        {
            cheapBoozeButton.interactable = false;
            cheapMeatButton.interactable = false;
        }
        else
        {
            cheapBoozeButton.interactable = true;
            cheapMeatButton.interactable = true;
        }
    }

    private void BuyCheapBooze()
    {
        if (gold.goldPoints >= cheapGoodsPrice && boozePurchased == false)
        {
            gold.goldPoints -= cheapGoodsPrice;
            remainingFunds.text = gold.goldPoints.ToString();
            player.boozeTime += boozeToAdd;
            player.boozebarImage.fillAmount = player.boozeTime / 100f;

            boozePurchased = true;
        }
    }

    private void BuyCheapMeat()
    {
        if (gold.goldPoints >= cheapGoodsPrice && meatPurchased == false)
        {
            gold.goldPoints -= cheapGoodsPrice;
            remainingFunds.text = gold.goldPoints.ToString();
            player.health += healthToAdd;
            player.healthbarImage.fillAmount = player.health / 100f;            

            meatPurchased = true;
        }
    }

    private void Update()
    {
        CheckCredibility();
        cheapBoozeButton.onClick.AddListener(BuyCheapBooze);
        boozePurchased = false;
        cheapMeatButton.onClick.AddListener(BuyCheapMeat);
        meatPurchased = false;
        remainingFunds.text = gold.goldPoints.ToString();
    }
}
