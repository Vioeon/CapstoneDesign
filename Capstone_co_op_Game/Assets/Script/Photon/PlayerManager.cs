using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;//path사용위해

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;//포톤뷰 선언

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)//내 포톤 네트워크이면
        {
            CreateController();//플레이어 컨트롤러 붙여준다. 
        }
    }
    void CreateController()//플레이어 컨트롤러 만들기
    {
        Debug.Log("Instantiated Player Controller");

        int id = PV.ViewID;
        int idx = (id / 1000) % 2;

        Transform[] points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        
        if (idx == 1)
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController1"), points[1].position, Quaternion.identity);
        else
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController2"), points[2].position, Quaternion.identity);
        //포톤 프리펩에 있는 플레이어 컨트롤러를 저 위치에 저 각도로 만들어주기
    }
}