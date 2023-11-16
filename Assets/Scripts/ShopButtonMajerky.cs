using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButtonMajerky : MonoBehaviour
{
    private Gold gold;
    private Player_Movement player;

    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI moneyLeft;
    [SerializeField] private TextMeshProUGUI MajerkyStat;
    [SerializeField] private Button purchaseButton;
    public float itemPrice = 50f;
    private bool transaction = false;

    private void Start()
    {
        gold = FindObjectOfType<Gold>();
        player = FindObjectOfType<Player_Movement>();
        purchaseButton = GetComponent<Button>();
        itemPriceText.text = itemPrice.ToString();
        MajerkyStat.text = ((int)player.health).ToString();
    }

    private void CheckItemPurchase()
    {
        if (gold.goldPoints >= itemPrice && transaction == false)
        {
            player.health += 50;
            player.healthbarImage.fillAmount = player.health / 100f;
            gold.goldPoints -= itemPrice;
            PriceAndMoneyCalculation();
            transaction = true;
        }
    }

    private void PriceAndMoneyCalculation()
    {
        itemPrice += 10;
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
        MajerkyStat.text = ((int)player.health).ToString();
        purchaseButton.onClick.AddListener(CheckItemPurchase);
        transaction = false;
    }
}
