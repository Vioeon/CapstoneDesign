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

        SaveData loadData = SaveSystem.Load("save_001");
        Debug.Log("GetRankItem:  " + loadData.getRankItem);

        if (loadData.getRankItem == 0)
        {
            Debug.Log("GetRankItem ZERO");
            return;  // �⺻ ��ǰ���� ����
        }
        else
        {
            GameObject.Find(loadData.Stage + "_g").transform.GetChild(0).gameObject.SetActive(false);  // ��Ż ��Ȱ��ȭ
            GameObject.Find(loadData.Stage + "_g").transform.GetChild(1).gameObject.SetActive(false);  // �⺻ ��ǰ ��Ȱ��ȭ
            GameObject.Find(loadData.Stage + "_g").transform.GetChild((loadData.getRankItem)/2 + 1).gameObject.SetActive(true); // ��޾������� ���� ������ ���� ��ǰ Ȱ��ȭ

            // ��������, ȹ���� ��޾����� ���� �ʱ�ȭ
            SaveData savedata = new SaveData(null, loadData.ClearNum, 0, loadData.Tot_rank);
            SaveSystem.Save(savedata, "save_001");
        }

        if (loadData.ClearNum == 2)
        {
            MenuManager.Instance.OpenMenu("loading");
            PhotonNetwork.LoadLevel("EndScene");  // ���� ������ �̵�
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
