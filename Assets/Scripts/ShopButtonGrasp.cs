using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonGrasp : MonoBehaviour
{
    private Gold gold;
    private Player_Combat player;

    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI moneyLeft;
    [SerializeField] private TextMeshProUGUI GraspStat;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private float itemPrice = 100f;
    private float priceMultiplier = 1;
    private bool transaction = false;

    private void Start()
    {
        gold = FindObjectOfType<Gold>();
        player = FindObjectOfType<Player_Combat>();
        purchaseButton = GetComponent<Button>();
        itemPriceText.text = itemPrice.ToString();
        GraspStat.text = player.attackRange.ToString();
    }

    private void CheckItemPurchase()
    {
        if (gold.goldPoints >= itemPrice && transaction == false)
        {
            player.attackRange = player.attackRange + 0.2f;
            gold.goldPoints -= itemPrice;
            PriceAndMoneyCalculation();
            transaction = true;
        }
    }

    private void PriceAndMoneyCalculation()
    {
        priceMultiplier++;
        itemPrice *= priceMultiplier;
        itemPriceText.text = itemPrice.ToString();
    }

    private void CheckCredibility()
    {
        if (gold.goldPoints < 100)
        {
            purchaseButton.interactable = false;
        }
        else
        {
            purchaseButton.interactable = true;
        }
    }

    private void Update()
    {
        CheckCredibility();

        moneyLeft.text = gold.goldPoints.ToString();
        GraspStat.text = player.attackRange.ToString();
        purchaseButton.onClick.AddListener(CheckItemPurchase);
        transaction = false;
    }
}