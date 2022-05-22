using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public SaveData(string _Stage, int _ClearNum, int _getRankItem, int _Tot_rank)
    {
        Stage = _Stage;
        ClearNum = _ClearNum;
        getRankItem = _getRankItem;
        Tot_rank = _Tot_rank;
    }

    public string Stage = null;  // 현재 플레이중인 스테이지
    public int ClearNum = 0;     // 클리어한 스테이지 수
    public int getRankItem = 0;  // 현재 스테이지에서 먹은 등급아이템 수
    public int Tot_rank = 0;     // 획득한 총 등급아이템 수

}
