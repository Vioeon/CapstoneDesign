using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;  // ���� ��� ���
using TMPro;  // �ؽ�Ʈ �޽� ���� ��� ���
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks  // �ٸ� ���� ���� �޾Ƶ��̱�
{
    public static Launcher Instance;  // Launcher ��ũ��Ʈ�� �޼���� ����ϱ� ���� ����
    public PhotonView pv;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField nickNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;

    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomListItemPrefab;

    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerListItemPrefab;

    [SerializeField] GameObject startGameButton;

    // �� ����� �����ϱ� ���� ��ųʸ� �ڷ���
    private Dictionary<string, GameObject> roomDict = new Dictionary<string, GameObject>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        OnLogin();
    }

    void OnLogin()
    {
        Debug.Log("������ ���� ������");
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();  // ������ ���� ������ ���� ������ ������ ����
    }

    public override void OnConnectedToMaster()  // ������ ������ ���� �� �۵�
    {
        Debug.Log("01. ������ ���� ����");
        PhotonNetwork.JoinLobby();  // ������ ���� ���� �� �κ�� ����
        PhotonNetwork.AutomaticallySyncScene = true;  // �ڵ����� ��� ������� scene�� ���� �����ش�.
    }

    public override void OnJoinedLobby()  // �κ� ���� �� �۵�
    {
        MenuManager.Instance.OpenMenu("title");  // �κ� ������ Ÿ��Ʋ ȭ�� Ŵ
        Debug.Log("02. �κ� ����");
        //PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        // ���� ��� �̸� �������� ���ںٿ��� �����ֱ�
    }

    public void CreateRoom()  // �游���
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;  // �� �̸��� ���̸� �� �ȸ������
        }
        // CreateRoom(���̸�, ��ɼ�)
        //new RoomOptions { MaxPlayers = 2 }
        PhotonNetwork.CreateRoom(roomNameInputField.text, new RoomOptions { MaxPlayers = 4 });  // ���� ��Ʈ��ũ������� roomNameInputField.text�� �̸����� ���� �����.
        MenuManager.Instance.OpenMenu("loading");  // �ε�â ����
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("03. �� ���� �Ϸ�");
    }
    public override void OnJoinedRoom()  // �濡 ������ �۵�
    {
        Debug.Log("04. �� ���� �Ϸ�");
        MenuManager.Instance.OpenMenu("room");  // �� �޴� ����
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;  // �� �� �̸� ǥ��

          //��ǲ�ʵ忡 �Է��� �ؽ�Ʈ�� �г������� ����

        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);  // �濡 ���� �����ִ� �̸�ǥ�� ����
        }
        for(int i = 0; i < players.Count(); i++)
        {
            //���� �濡 ���� �濡�ִ� ��� ��� ��ŭ �̸�ǥ �߰� �ϱ�
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);  // ���常 ���ӽ��� ��ư ������ ����
    }
    public void SetNickname()
    {
        PhotonNetwork.NickName = nickNameInputField.text;
    }
    public override void OnMasterClientSwitched(Player newMasterClient)  // ������ ������ ������ �ٲ���� ��
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);  // ���常 ���ӽ��� ��ư ������ ����
    }

    public override void OnCreateRoomFailed(short returnCode, string message)  // �� ����� ���н� �۵�
    {
        Debug.Log("�� ���� ����");
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");  // ���� �޴� ����
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);  // 1�� ������ ���忡�� scene ��ȣ�� 1�����̱� �����̴�. 0�� �ʱ� ��
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();  // �� ������ ���� ��Ʈ��ũ ���
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);  // ���� ��Ʈ��ũ�� JoinRoom ��� �ش��̸��� ���� ������ �����Ѵ�. 
        MenuManager.Instance.OpenMenu("loading");  // �ε�â ����
    }

    public override void OnLeftRoom()  // ���� ������ ȣ��
    {
        Debug.Log("Leave Room");
        MenuManager.Instance.OpenMenu("title");  // �� ������ ���� �� Ÿ��Ʋ �޴� ȣ��
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)  // ������ �� ����Ʈ ���
    {
        Debug.Log("�� ����Ʈ ������Ʈ ��� : " + roomList.Count);

        GameObject tempRoom;

        foreach (var room in roomList)  
        {
            if (room.RemovedFromList == true)  // ���� ������ ���
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // �� ������ ����(����)�� ���
            else
            {
                if(roomDict.ContainsKey(room.Name) == false)  // ���� ó�� ������ ���
                {
                    GameObject _room = Instantiate(roomListItemPrefab, roomListContent);
                    _room.GetComponent<RoomListItem>().SetUp(room);
                    roomDict.Add(room.Name, _room);
                }
                else  // �� ������ �����ϴ� ���
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomListItem>().SetUp(room);
                }
                // instantiate�� prefab�� roomListContent��ġ�� ������ְ� �� �������� i��° �븮��Ʈ�� �ȴ�. 
                //Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            }
            
            //Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }

        /*
        foreach (Transform trans in roomListContent)  // �����ϴ� ��� roomListContent
        {
            Destroy(trans.gameObject);  // �븮��Ʈ ������Ʈ�� �ɶ����� �������
        }
        int roomCount = roomList.Count;
        for(int i=0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!roomList.Contains(roomList[i]))
                    roomList.Add(roomList[i]);
                else
                    roomList[roomList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (roomList.IndexOf(roomList[i]) != -1){ 
                roomList.RemoveAt(roomList.IndexOf(roomList[i]));
            }
        }*/
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)  // �ٸ� �÷��̾ �濡 ������ �۵�
    {
        // instantiate�� prefab�� playerListContent��ġ�� ������ְ� �� �������� �̸� �޾Ƽ� ǥ��. 
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
