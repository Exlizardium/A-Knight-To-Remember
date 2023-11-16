using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SecondRoomSpawner : MonoBehaviour
{
    ArrayRooms rooms;
    private Player_Movement player;
    private AudioManager spawnerAudio;

    [SerializeField] private GameObject currentRoom;
    [SerializeField] private GameObject startingRoom;
    [SerializeField] private GameObject startingPoint;
    [SerializeField] PanelManager panelManager;
    GameObject nextRoom;
    private GameObject[] roomsOnCurrentLevel;
    private GameObject[] leftoverGameobjectsOnCurrentLevel;
    public float levelCount = 0;

    private void Start()
    {
        rooms = FindObjectOfType<ArrayRooms>();
        player = FindObjectOfType<Player_Movement>();
        panelManager = FindObjectOfType<PanelManager>();
        spawnerAudio = FindObjectOfType<AudioManager>();

        CreateLevel();
        print(levelCount);
    }

    private void CreateLevel()
    {
        GenerateStartingRoom();
        Construction();
        PlayerPlacement();
        levelCount++;
        if(panelManager.loadingScreen != null)
        {
            panelManager.loadingScreen.SetActive(false);
        }
        if (player.drunk != null)
        {
            player.drunk.gameObject.SetActive(true);
        }
    }

    private void GenerateStartingRoom()
    {
        int lastRoomNumberAmongCentralRooms = rooms.centralRooms.Count;
        GameObject randomStartingRoom = rooms.centralRooms[Random.Range(0, lastRoomNumberAmongCentralRooms)];
        startingRoom = Instantiate(randomStartingRoom, startingPoint.transform.position, Quaternion.identity);
        currentRoom = startingRoom;
        rooms.centralRooms.Remove(randomStartingRoom);
    }

    private void PlayerPlacement()
    {
        player.transform.position = startingRoom.transform.GetChild(0).position;
    }

    private void Construction()
    {
        ConstructionTop();
        currentRoom = startingRoom;

        ConstructionBottom();
        currentRoom = startingRoom;

        ConstructionLeft();
        currentRoom = startingRoom;

        ConstructionRight();
    }

    private void BossLevelConstruction()
    {
        startingRoom = Instantiate(rooms.bossRooms[0]);
        GameObject bossRoomStartingPointAnchor = startingRoom.transform.Find("Anchor boss").gameObject;
        Vector3 placement = startingPoint.transform.localPosition - bossRoomStartingPointAnchor.transform.position;
        startingRoom.transform.position = placement;
        levelCount++;
        panelManager.loadingScreen.SetActive(false);
        player.drunk.gameObject.SetActive(true);
    }

    private void ConstructionTop()
    {
       PlaceRoomFromTopToBottom(rooms.topBottomPassage[0]);
       PlaceRoomFromTopToBottom(rooms.bottomAnchorRooms[Random.Range(0, 3)]);    
    }

    private void ConstructionBottom()
    {      
       PlaceRoomFromBottomToTop(rooms.topBottomPassage[0]);
       PlaceRoomFromBottomToTop(rooms.topAnchorRooms[Random.Range(0, 3)]);
    }

    private void ConstructionRight()
    {      
       PlaceRoomFromRightToLeft(rooms.leftRightPassage[0]);
       PlaceRoomFromRightToLeft(rooms.leftAnchorRooms[Random.Range(0, 3)]);
    }

    private void ConstructionLeft()
    { 
       PlaceRoomFromLeftToRight(rooms.leftRightPassage[0]);
       PlaceRoomFromLeftToRight(rooms.rightAnchorRooms[Random.Range(0, 3)]);
    }

    private void PlaceRoomFromTopToBottom(GameObject bottomRoom)
    {
        nextRoom = Instantiate(bottomRoom);
        GameObject anchorToBottom = nextRoom.transform.GetChild(0).Find("Anchor bottom").gameObject;
        GameObject anchorFromTop = currentRoom.transform.GetChild(0).Find("Anchor top").gameObject;
        Vector3 placement = anchorFromTop.transform.position - anchorToBottom.transform.localPosition;
        nextRoom.transform.GetChild(0).position = placement;
        currentRoom = nextRoom;
    }

    private void PlaceRoomFromBottomToTop(GameObject topRoom)
    {
        nextRoom = Instantiate(topRoom);
        GameObject anchorToTop = nextRoom.transform.GetChild(0).Find("Anchor top").gameObject;
        GameObject anchorFromBottom = currentRoom.transform.GetChild(0).Find("Anchor bottom").gameObject;
        Vector3 placement = anchorFromBottom.transform.position - anchorToTop.transform.localPosition;
        nextRoom.transform.GetChild(0).position = placement;

        currentRoom = nextRoom;
    }

    private void PlaceRoomFromRightToLeft(GameObject leftRoom)
    {
        nextRoom = Instantiate(leftRoom);
        GameObject anchorToLeft = nextRoom.transform.GetChild(0).Find("Anchor left").gameObject;
        GameObject anchorFromRight = currentRoom.transform.GetChild(0).Find("Anchor right").gameObject;
        Vector3 placement = anchorFromRight.transform.position - anchorToLeft.transform.localPosition;
        nextRoom.transform.GetChild(0).position = placement;
        currentRoom = nextRoom;
    }

    private void PlaceRoomFromLeftToRight(GameObject rightRoom)
    {
        nextRoom = Instantiate(rightRoom);
        GameObject anchorToRight = nextRoom.transform.GetChild(0).Find("Anchor right").gameObject;
        GameObject anchorFromLeft = currentRoom.transform.GetChild(0).Find("Anchor left").gameObject;
        Vector3 placement = anchorFromLeft.transform.position - anchorToRight.transform.localPosition;
        nextRoom.transform.GetChild(0).position = placement;
        currentRoom = nextRoom;
    }

    private void DeleteLevel()
    {
        roomsOnCurrentLevel = GameObject.FindGameObjectsWithTag("Room");
        if (roomsOnCurrentLevel != null)
        {
            for (int i = 0; i < roomsOnCurrentLevel.Length; i++)
            {
                Destroy(roomsOnCurrentLevel[i]);
            }
        }

        leftoverGameobjectsOnCurrentLevel = GameObject.FindGameObjectsWithTag("Added after start");
        if (leftoverGameobjectsOnCurrentLevel != null)
        {
            for (int i = 0; i < leftoverGameobjectsOnCurrentLevel.Length; i++)
            {
                Destroy(leftoverGameobjectsOnCurrentLevel[i]);
            }
        }


        if (panelManager.interactButtonPrompt.activeInHierarchy == true)
        {
            panelManager.interactButtonPrompt.SetActive(false);
        }
    }

    public void GenerateNewLevel()
    {
        print("test");
        player.drunk.gameObject.SetActive(false);
        panelManager.loadingScreen.SetActive(true);
        DeleteLevel();
        spawnerAudio.Play("Level transition");
        Invoke("CreateLevel", 2f);
    }

    public void GenerateBossLevel()
    {
        print("test");
        player.drunk.gameObject.SetActive(false);
        panelManager.loadingScreen.SetActive(true);
        DeleteLevel();
        spawnerAudio.Play("Level transition");
        Invoke("BossLevelConstruction", 2f);
    }
}
