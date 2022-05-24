using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.Utility;
using TMPro;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviourPunCallbacks, IPunObservable
{
    //[SerializeField] float sprintSpeed, walkSpeed, jumpForce;
    //뛰는속도 걷는속도 점프힘 뛰기걷기바꿀때 가속시간
    bool grounded;//점프를 위한 바닥체크
    Rigidbody rb;

    public PhotonView pv;
    public SpriteRenderer SR;

    private float h, v, r;
    private Transform tr;
    public Animator anim;

    public float movespeed = 15.0f;
    public float rotSpeed = 60.0f;
    public float jumpForce = 300.0f;

    //public TextMeshPro nickName;
    // TextMeshProUGUI resourceText;

    //public Text a;

    private Vector3 currPos;
    private Quaternion currRot;

    MapManager MM;

    //private float timer = 0.0f;
    //private bool Mstate = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();

        pv.ObservedComponents[0] = this; // 동기화 콜백함수 발생 시 필요

        if (pv.IsMine)
        {
            // 자신의 플레이어에게만 카메라 제어권 연결
            Camera.main.GetComponent<SmoothFollow>().target = tr;
        }
        

        //resourceText.text = pv.Owner.NickName;
        //a.text = pv.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            move();
            Jump();
        }
        else
        {
            tr.position = Vector3.Lerp(tr.position, this.currPos, Time.deltaTime * 5);
            tr.rotation = Quaternion.Lerp(tr.rotation, this.currRot, Time.deltaTime * 5);
        }
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Attack1");

        }*/
        /*AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime < 1.0f)
        {
            //anim.CrossFade(애니메이션 이름);  애니메이션 전환을 부드럽게 해줌
        }*/
    }
    void move()
    {
        v = Input.GetAxis("Vertical");
        h = Input.GetAxis("Horizontal");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        if (moveDir != Vector3.zero)
        {
            tr.position += moveDir * movespeed * Time.deltaTime;
            tr.LookAt(transform.position + moveDir);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)//땅위에서 스페이스바 누르면
        {
            rb.AddForce(transform.up * jumpForce);//점프력만큼위로 힘받음
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (!pv.IsMine)
        {
            return;
        }
        else if (other.gameObject.tag == "rankItem")  // 등급아이템을 먹으면
        {
            Destroy(other.gameObject);  // 등급아이템 삭제
            //pv.RPC("DestroyItem", RpcTarget.All, other.gameObject);

            // 획득한 등급아이템 갯수 ++ 증가
            SaveData loadData = SaveSystem.Load("save_001");
            SaveData savedata = new SaveData(loadData.Stage, loadData.ClearNum, loadData.getRankItem + 1, loadData.Tot_rank);
            Debug.Log("GetRankItem:  " + loadData.getRankItem);
            SaveSystem.Save(savedata, "save_001");

        }

        /*
        if (other.gameObject.tag == "Gpu_RankItem")  // 등급아이템을 먹으면
        {
            PhotonNetwork.Destroy(other.gameObject);  // 등급아이템 삭제
            MM.Gpu_rank++;
        }
        else if (other.gameObject.tag == "Cpu_RankItem")  // 등급아이템을 먹으면
        {
            PhotonNetwork.Destroy(other.gameObject);  // 등급아이템 삭제
            MM.Cpu_rank++;
        }
        else if (other.gameObject.tag == "Cooler_RankItem")  // 등급아이템을 먹으면
        {
            PhotonNetwork.Destroy(other.gameObject);  // 등급아이템 삭제
            MM.Cooler_rank++;
        }
        else if (other.gameObject.tag == "Power_RankItem")  // 등급아이템을 먹으면
        {
            PhotonNetwork.Destroy(other.gameObject);  // 등급아이템 삭제
            MM.Power_rank++;
        }*/
    }

    [PunRPC]
    void DestroyItem(GameObject obj)
    {
        if (!pv.IsMine)
            return;
        Destroy(obj);
    }

    // 상태를 동기화 시켜주는 메소드
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 자신의 플레이 정보를 다른 네트워크 사용자에게 송신
        if (stream.IsWriting) 
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
           
        }
        // 다른 사용자의 정보 수신
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }

    /*private void FixedUpdate()
    {
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && Mstate == true)
        {
            anim.SetBool("isMoving", true);

            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
            tr.position += moveDir * movespeed * Time.deltaTime;
            tr.LookAt(transform.position + moveDir);
            
        }
        if (moveDir != Vector3.zero)
        {
            tr.position += moveDir * movespeed * Time.deltaTime;
            tr.LookAt(transform.position + moveDir);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        /*else if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Attack1");
            Mstate = false;
            timer += Time.deltaTime;
            if(timer < 2f)
            {
                Mstate = false;
            }
            else { Mstate = true;
            }
        }
        else
        {
            anim.SetBool("isMmoving", false);
            Mstate = true;
        }
        //anim.SetBool("isMoving", moveDir != Vector3.zero);
        if(moveDir != Vector3.zero)
        {
            anim.SetTrigger("isMoving");
        }
    }*/
}