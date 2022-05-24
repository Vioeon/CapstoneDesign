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
        if (other.gameObject.tag == "StartPortal")  // MainPlace���� ��Ż Ż ��
        {
            MM.playercount++;
            Debug.Log("Enterplayercount : " + MM.playercount);

            if(MM.playercount == 2)  // �θ��� �÷��̾ ���  �浹 ���϶� �۵�
            {
                MM.playercount = 0;

                //MM.RankObj = GameObject.Find(other.gameObject.name).GetComponentsInChildren<GameObject>();  // ��Ż ������Ʈ�� ���� ������Ʈ�� �迭�� ����
                MM.Stage = other.gameObject.name;  // ��Ż�� Ÿ�� ���������� �̸� ����

                // �� ���������� �̸� ����
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

            if (MM.playercount == 2)  // �θ��� �÷��̾ ���  �浹 ���϶� �۵�
            {
                MM.playercount = 0;

                MenuManager.Instance.OpenMenu("loading");
                PhotonNetwork.LoadLevel(other.gameObject.name);
            }
        }
        else if (other.gameObject.tag == "ClearPortal")  // ���������ʿ��� Clear ��Ż Ż ��
        {
            // �� Ŭ���� ���� ++
            // ȹ���� ��޺��� ����

            MM.playercount++;
            Debug.Log("Enterplayercount : " + MM.playercount);

            if (MM.playercount == 2)   // �θ��� �÷��̾ ���  �浹 ���϶� �۵�
            {
                MM.playercount = 0;  // ���� �ʱ�ȭ

                // Ŭ���� ����++, ȹ���� �� ��޾����� ����
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
