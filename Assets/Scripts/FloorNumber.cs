using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorNumber : MonoBehaviour
{
    private SecondRoomSpawner roomSpawner;
    private TextMeshProUGUI floorNumberCountDisplay;

    private void Start()
    {
        roomSpawner = FindObjectOfType<SecondRoomSpawner>();
        floorNumberCountDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateRoomNumberInMenu()
    {
        floorNumberCountDisplay.text = roomSpawner.levelCount.ToString();
    }

    void Update()
    {
        UpdateRoomNumberInMenu();
    }
}
