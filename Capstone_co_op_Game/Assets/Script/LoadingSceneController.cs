using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    private Transform imgPos;
    private float rotSpeed = -300f;

    public GameObject loadingcircle;

    // Start is called before the first frame update
    void Start()
    {
        imgPos = loadingcircle.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        imgPos.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
    }
}
