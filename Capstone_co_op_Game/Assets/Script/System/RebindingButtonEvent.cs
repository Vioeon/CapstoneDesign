using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebindingButtonEvent : MonoBehaviour
{
    private RebindingDisplay rbd;
    [Tooltip("FIRE = 0, INTERACT = 1, MOVE = 2")]
    [SerializeField] private int index;
    [Tooltip("up = 0, down = 1, left = 2, right = 3, none = -1")]
    [SerializeField] private int subindex;
    Text txt;

    // Start is called before the first frame update
    void Start()
    {
        rbd = gameObject.GetComponentInParent<RebindingDisplay>();
        //rbd = gameObject.transform.parent.GetComponent<RebindingDisplay>();
        txt = gameObject.transform.Find("Text").GetComponent<Text>();
        //a.text = 
    }

    // Update is called once per frame
    void Update()
    {
        if (rbd != null) return;

        rbd = gameObject.GetComponentInParent<RebindingDisplay>();
        Debug.Log("Updated");
    }

    public void Clicked()
    {
        if (rbd == null) 
        {
            Debug.Log("isnull");
            return;
        }
        rbd.StartRebinding(index, subindex, gameObject);
    }
}
