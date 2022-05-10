using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;  // 포톤 기능 사용
using TMPro;  // 텍스트 메쉬 프로 기능 사용
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks  // 다른 포톤 반응 받아들이기
{
    public static Launcher Instance;  // Launcher 스크립트를 메서드로 사용하기 위해 선언
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

    // 룸 목록을 저장하기 위한 딕셔너리 자료형
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
        Debug.Log("마스터 서버 연결중");
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();  // 설정한 포톤 서버에 따라 마스터 서버에 연결
    }

    public override void OnConnectedToMaster()  // 마스터 서버에 연결 시 작동
    {
        Debug.Log("01. 마스터 서버 접속");
        PhotonNetwork.JoinLobby();  // 마스터 서버 연결 시 로비로 연결
        PhotonNetwork.AutomaticallySyncScene = true;  // 자동으로 모든 사람들의 scene을 통일 시켜준다.
    }

    public override void OnJoinedLobby()  // 로비에 연결 시 작동
    {
        MenuManager.Instance.OpenMenu("title");  // 로비에 들어오면 타이틀 화면 킴
        Debug.Log("02. 로비에 접속");
        //PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
        // 들어온 사람 이름 랜덤으로 숫자붙여서 정해주기
    }

    public void CreateRoom()  // 방만들기
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;  // 방 이름이 빈값이면 방 안만들어짐
        }
        // CreateRoom(방이름, 방옵션)
        //new RoomOptions { MaxPlayers = 2 }
        PhotonNetwork.CreateRoom(roomNameInputField.text, new RoomOptions { MaxPlayers = 4 });  // 포톤 네트워크기능으로 roomNameInputField.text의 이름으로 방을 만든다.
        MenuManager.Instance.OpenMenu("loading");  // 로딩창 열기
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("03. 방 생성 완료");
    }
    public override void OnJoinedRoom()  // 방에 들어갔을때 작동
    {
        Debug.Log("04. 방 입장 완료");
        MenuManager.Instance.OpenMenu("room");  // 룸 메뉴 열기
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;  // 들어간 방 이름 표시

          //인풋필드에 입력한 텍스트를 닉네임으로 설정

        Player[] players = PhotonNetwork.PlayerList;
        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);  // 방에 들어가면 전에있던 이름표들 삭제
        }
        for(int i = 0; i < players.Count(); i++)
        {
            //내가 방에 들어가면 방에있는 사람 목록 만큼 이름표 뜨게 하기
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);  // 방장만 게임시작 버튼 누르기 가능
    }
    public void SetNickname()
    {
        PhotonNetwork.NickName = nickNameInputField.text;
    }
    public override void OnMasterClientSwitched(Player newMasterClient)  // 방장이 나가서 방장이 바뀌었을 때
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);  // 방장만 게임시작 버튼 누르기 가능
    }

    public override void OnCreateRoomFailed(short returnCode, string message)  // 방 만들기 실패시 작동
    {
        Debug.Log("룸 입장 실패");
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.Instance.OpenMenu("error");  // 에러 메뉴 열기
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);  // 1인 이유는 빌드에서 scene 번호가 1번씩이기 때문이다. 0은 초기 씬
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();  // 방 떠나기 포톤 네트워크 기능
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);  // 포톤 네트워크의 JoinRoom 기능 해당이름을 가진 방으로 접속한다. 
        MenuManager.Instance.OpenMenu("loading");  // 로딩창 열기
    }

    public override void OnLeftRoom()  // 방을 떠나면 호출
    {
        Debug.Log("Leave Room");
        MenuManager.Instance.OpenMenu("title");  // 방 떠나기 성공 시 타이틀 메뉴 호출
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)  // 포톤의 룸 리스트 기능
    {
        Debug.Log("룸 리스트 업데이트 목록 : " + roomList.Count);

        GameObject tempRoom;

        foreach (var room in roomList)  
        {
            if (room.RemovedFromList == true)  // 룸이 삭제된 경우
            {
                roomDict.TryGetValue(room.Name, out tempRoom);
                Destroy(tempRoom);
                roomDict.Remove(room.Name);
            }
            // 룸 정보가 갱신(변경)된 경우
            else
            {
                if(roomDict.ContainsKey(room.Name) == false)  // 룸이 처음 생성된 경우
                {
                    GameObject _room = Instantiate(roomListItemPrefab, roomListContent);
                    _room.GetComponent<RoomListItem>().SetUp(room);
                    roomDict.Add(room.Name, _room);
                }
                else  // 룸 정보를 갱신하는 경우
                {
                    roomDict.TryGetValue(room.Name, out tempRoom);
                    tempRoom.GetComponent<RoomListItem>().SetUp(room);
                }
                // instantiate로 prefab을 roomListContent위치에 만들어주고 그 프리펩은 i번째 룸리스트가 된다. 
                //Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
            }
            
            //Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }

        /*
        foreach (Transform trans in roomListContent)  // 존재하는 모든 roomListContent
        {
            Destroy(trans.gameObject);  // 룸리스트 업데이트가 될때마다 싹지우기
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

    public override void OnPlayerEnteredRoom(Player newPlayer)  // 다른 플레이어가 방에 들어오면 작동
    {
        // instantiate로 prefab을 playerListContent위치에 만들어주고 그 프리펩을 이름 받아서 표시. 
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
