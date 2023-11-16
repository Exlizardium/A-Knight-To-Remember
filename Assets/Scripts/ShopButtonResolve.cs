using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonResolve : MonoBehaviour
{
    private Gold gold;
    private Player_Combat player;

    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI moneyLeft;
    [SerializeField] private TextMeshProUGUI ResolveStat;
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
        ResolveStat.text = player.shieldDuration.ToString();
    }

    private void CheckItemPurchase()
    {
        if (gold.goldPoints >= itemPrice && transaction == false)
        {
            player.shieldDuration = player.shieldDuration + 0.5f;
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
        ResolveStat.text = player.shieldDuration.ToString();
        purchaseButton.onClick.AddListener(CheckItemPurchase);
        transaction = false;
    }
}