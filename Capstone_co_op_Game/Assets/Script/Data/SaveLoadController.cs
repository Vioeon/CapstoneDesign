using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
    private void Awake()
    {
        SaveData loadData = SaveSystem.Load("save_001");
        SaveData savedata = new SaveData(null, 0, 0, 0);
        SaveSystem.Save(savedata, "save_001");
    }
    /*void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            SaveData character = new SaveData("왼손잡이 개발자", 30, 100f);

            SaveSystem.Save(character, "save_001");
        }

        if (Input.GetKeyDown("l"))
        {
            SaveData loadData = SaveSystem.Load("save_001");
            Debug.Log(string.Format("LoadData Result => name : {0}, age : {1}, power : {2}", loadData.name, loadData.age, loadData.power));
        }
    }*/
}
