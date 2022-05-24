using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PortalManager : MonoBehaviourPun
{
    PhotonView PV;
    MapManager MM;

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;
        MM = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartPortal")  // MainPlace에서 포탈 탈 때
        {
            MM.playercount++;
            Debug.Log("Enterplayercount : " + MM.playercount);

            if(MM.playercount == 2)  // 두명의 플레이어가 모두  충돌 중일때 작동
            {
                MM.playercount = 0;

                //MM.RankObj = GameObject.Find(other.gameObject.name).GetComponentsInChildren<GameObject>();  // 포탈 오브젝트의 하위 오브젝트를 배열로 만듬
                MM.Stage = other.gameObject.name;  // 포탈을 타는 스테이지의 이름 저장

                // 들어간 스테이지의 이름 저장
                SaveData loadData = SaveSystem.Load("save_001");
                SaveData savedata = new SaveData(other.gameObject.name, loadData.ClearNum, loadData.getRankItem, loadData.Tot_rank);
                SaveSystem.Save(savedata, "save_001");

                MenuManager.Instance.OpenMenu("loading");
                PhotonNetwork.LoadLevel(other.gameObject.name);
            }
        }
        else if(other.gameObject.tag == "Portal")
        {
            MM.playercount++;
            Debug.Log("Enterplayercount : " + MM.playercount);

            if (MM.playercount == 2)  // 두명의 플레이어가 모두  충돌 중일때 작동
            {
                MM.playercount = 0;

                MenuManager.Instance.OpenMenu("loading");
                PhotonNetwork.LoadLevel(other.gameObject.name);
            }
        }
        else if (other.gameObject.tag == "ClearPortal")  // 스테이지맵에서 Clear 포탈 탈 때
        {
            // 맵 클리어 숫자 ++
            // 획득한 등급별의 숫자

            MM.playercount++;
            Debug.Log("Enterplayercount : " + MM.playercount);

            if (MM.playercount == 2)   // 두명의 플레이어가 모두  충돌 중일때 작동
            {
                MM.playercount = 0;  // 변수 초기화

                // 클리어 갯수++, 획득한 총 등급아이템 갱신
                SaveData loadData = SaveSystem.Load("save_001");
                SaveData savedata = new SaveData(loadData.Stage, loadData.ClearNum + 1, loadData.getRankItem, loadData.Tot_rank+=loadData.getRankItem);
                SaveSystem.Save(savedata, "save_001");

                MenuManager.Instance.OpenMenu("loading");
                PhotonNetwork.LoadLevel("MainPlace");
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Portal")
        {
            MM.playercount--;
            Debug.Log("Exitplayercount : " + MM.playercount);
        }
        else if (other.gameObject.tag == "ClearPortal")
        {
            MM.playercount--;
            Debug.Log("Exitplayercount : " + MM.playercount);
        }
    }
}
