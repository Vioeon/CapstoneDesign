using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MapManager : MonoBehaviourPun
{
    public int playercount = 0;

    PhotonView PV;
    Launcher NM;

    public string Stage = null;

    public static MapManager Instance;//Room Manager ��ũ��Ʈ�� �޼���� ����ϱ� ���� ����

    void Awake()
    {
        if (Instance)//�ٸ� �ʸŴ��� ����Ȯ��
        {
            Destroy(gameObject);//������ �ı�
            return;
        }
        DontDestroyOnLoad(gameObject);//�ʸŴ��� ��ȥ�ڸ� �״�� 
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;

        //RankObj = GameObject.Find(Stage).GetComponentsInChildren<GameObject>();  // ��Ż ������Ʈ�� ���� ������Ʈ�� �迭�� ����
    }

    // Update is called once per frame
    void Update()
    {
        //if (!PV.IsMine) return;
        //isClear();
    }
}
