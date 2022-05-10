using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public string gameVersion = "1.0";
    public string nickName = "Eony";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;  // 하나의 클라이언트가 룸내의 모든 클라이언트들에게 로드해야할 레벨을 정의
    }

    // Start is called before the first frame update
    void Start()
    {
        OnLogin();
    }

    void OnLogin()
    {
        PhotonNetwork.GameVersion = this.gameVersion;  // 포톤 게임버전을 설정(같은 게임버전끼리 공유가 됨)
        PhotonNetwork.NickName = this.nickName;  // 클라이언트의 닉네임 설정
        PhotonNetwork.ConnectUsingSettings();  // PUN2  포톤을 이용한 온라인 연결
    }
    public override void OnConnectedToMaster()  // 포톤 연결 성공시 콜백 메소드
    {
        Debug.Log("Connected !!!");
        PhotonNetwork.JoinRandomRoom();  // 생성되어있는 룸에 랜덤 접속
    }
    public override void OnJoinRandomFailed(short returnCode, string message)  // 랜덤접속 실패시 콜백 메소드
    {
        Debug.Log("Failed join room !!!");
        this.CreateRoom();
        //base.OnJoinRandomFailed(returnCode, message);
    }
    public int who;
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        //base.OnJoinedLobby();
        //if(PhotonView.viewID)

        CreatePlayer();
    }

    void CreatePlayer()
    {
        Transform[] points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        int idx = Random.Range(1, points.Length);

        PhotonNetwork.Instantiate("Player", points[idx].position, Quaternion.identity);
    }

    void CreateRoom()
    {
        
        // CreateRoom(방이름, 방옵션)
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });  // 방을 생성하고 방의 옵션 정의
    }
    // Update is called once per frame
    void Update()
    {

    }
}