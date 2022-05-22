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

    public string Stage = null;  // ���� �÷������� ��������
    public int ClearNum = 0;     // Ŭ������ �������� ��
    public int getRankItem = 0;  // ���� ������������ ���� ��޾����� ��
    public int Tot_rank = 0;     // ȹ���� �� ��޾����� ��

}
