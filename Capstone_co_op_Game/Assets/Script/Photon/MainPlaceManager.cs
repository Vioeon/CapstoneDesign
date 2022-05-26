using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MainPlaceManager : MonoBehaviour
{
    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        SetRankItem();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SetRankItem()
    {
        SaveData loadData = SaveSystem.Load("save_001");
        Debug.Log("GetRankItem:  " + loadData.getRankItem);

        if (loadData.getRankItem == 0)
        {
            Debug.Log("GetRankItem ZERO");
            return;  // 기본 부품으로 유지
        }
        else
        {
            GameObject.Find(loadData.Stage + "_g").transform.GetChild(0).gameObject.SetActive(false);  // 포탈 비활성화
            GameObject.Find(loadData.Stage + "_g").transform.GetChild(1).gameObject.SetActive(false);  // 기본 부품 비활성화
            GameObject.Find(loadData.Stage + "_g").transform.GetChild((loadData.getRankItem) + 1).gameObject.SetActive(true); // 등급아이템을 먹은 갯수에 따라 부품 활성화

            // 스테이지, 획득한 등급아이템 갯수 초기화
            SaveData savedata = new SaveData(null, loadData.ClearNum, 0, loadData.Tot_rank);
            SaveSystem.Save(savedata, "save_001");
        }

        if (loadData.ClearNum == 1)
        {
            MenuManager.Instance.OpenMenu("loading");
            PhotonNetwork.LoadLevel("EndScene");  // 엔딩 씬으로 이동
        }
    }
}
