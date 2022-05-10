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
        PhotonNetwork.AutomaticallySyncScene = true;  // �ϳ��� Ŭ���̾�Ʈ�� �볻�� ��� Ŭ���̾�Ʈ�鿡�� �ε��ؾ��� ������ ����
    }

    // Start is called before the first frame update
    void Start()
    {
        OnLogin();
    }

    void OnLogin()
    {
        PhotonNetwork.GameVersion = this.gameVersion;  // ���� ���ӹ����� ����(���� ���ӹ������� ������ ��)
        PhotonNetwork.NickName = this.nickName;  // Ŭ���̾�Ʈ�� �г��� ����
        PhotonNetwork.ConnectUsingSettings();  // PUN2  ������ �̿��� �¶��� ����
    }
    public override void OnConnectedToMaster()  // ���� ���� ������ �ݹ� �޼ҵ�
    {
        Debug.Log("Connected !!!");
        PhotonNetwork.JoinRandomRoom();  // �����Ǿ��ִ� �뿡 ���� ����
    }
    public override void OnJoinRandomFailed(short returnCode, string message)  // �������� ���н� �ݹ� �޼ҵ�
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
        
        // CreateRoom(���̸�, ��ɼ�)
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });  // ���� �����ϰ� ���� �ɼ� ����
    }
    // Update is called once per frame
    void Update()
    {

    }
}