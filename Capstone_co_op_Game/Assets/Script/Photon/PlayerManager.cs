using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;//path�������

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;//����� ����

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)//�� ���� ��Ʈ��ũ�̸�
        {
            CreateController();//�÷��̾� ��Ʈ�ѷ� �ٿ��ش�. 
        }
    }
    void CreateController()//�÷��̾� ��Ʈ�ѷ� �����
    {
        Debug.Log("Instantiated Player Controller");

        int id = PV.ViewID;
        int idx = (id / 1000) % 2;

        Transform[] points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        
        if (idx == 1)
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController1"), points[1].position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController2"), points[2].position, Quaternion.identity);
        //���� �����鿡 �ִ� �÷��̾� ��Ʈ�ѷ��� �� ��ġ�� �� ������ ������ֱ�
    }
}