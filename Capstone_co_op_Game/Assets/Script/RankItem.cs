using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")  // 등급아이템을 먹으면
        {
            SaveData loadData = SaveSystem.Load("save_001");
            SaveData savedata = new SaveData(loadData.Stage, loadData.ClearNum, loadData.getRankItem + 1, loadData.Tot_rank);
            Debug.Log("GetRankItem:  " + loadData.getRankItem);
            SaveSystem.Save(savedata, "save_001");

            Destroy(this.gameObject);  // 등급아이템 삭제
        }
    }
}
