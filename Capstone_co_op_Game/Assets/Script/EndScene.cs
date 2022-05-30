using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    float timer = 0f;

    public void Start()
    {
        Invoke("goTitle", 5f);
    }
    public void goTitle()
    {
        while(timer <= 5f)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }
        SceneManager.LoadScene("Title");
    }
    public void gotoTitle()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Title");
    }
}
