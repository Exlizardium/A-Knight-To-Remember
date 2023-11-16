using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public float goldPoints = 0;
    private Text goldDisplay;

    private void Start()
    {
        goldDisplay = GetComponent<Text>();
    }

    private void GoldToText()
    {
        goldDisplay.text = goldPoints.ToString();
        PlayerPrefs.SetString("Gold", goldPoints.ToString());
    }

    private void Update()
    {
        GoldToText();
    }
}
