using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MapManager : MonoBehaviourPun
{
    public int playercount = 0;

    public int ClearNum = 0;

    public int Gpu_rank = 0;
    public int Cpu_rank = 0;
    public int Cooler_rank = 0;
    public int Power_rank = 0;

    public int getRankItem = 0;
    public int Tot_rank = 0;

    PhotonView PV;
    Launcher NM;

    public string Stage = null;

    public GameObject[] RankObj = null;


    public static MapManager Instance;//Room Manager 스크립트를 메서드로 사용하기 위해 선언

    void Awake()
    {
        if (Instance)//다른 맵매니저 존재확인
        {
            Destroy(gameObject);//있으면 파괴
            return;
        }
        DontDestroyOnLoad(gameObject);//맵매니저 나혼자면 그대로 
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PV = photonView;

        //RankObj = GameObject.Find(Stage).GetComponentsInChildren<GameObject>();  // 포탈 오브젝트의 하위 오브젝트를 배열로 만듬
    }

    // Update is called once per frame
    void Update()
    {
        //if (!PV.IsMine) return;

        if(getRankItem == 0)  // 등급아이템을 하나도 못먹으면
        {
            return;
        }
        else
        {
            RankObj[0].SetActive(false);  // 기본 부품 false
            RankObj[getRankItem].SetActive(true);  // 등급아이템을 먹은 갯수에 따라 부품 교체
        }

        isClear();
    }

    void isClear()
    {
        if(ClearNum == 4)
        {
            //MenuManager.Instance.OpenMenu(" ");  // 클리어 이펙트 효과
            PhotonNetwork.LoadLevel("EndScene");  // 엔딩 씬으로 이동
        }
    }
}
