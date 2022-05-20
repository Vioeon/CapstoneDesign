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
    //�ٴ¼ӵ� �ȴ¼ӵ� ������ �ٱ�ȱ�ٲܶ� ���ӽð�
    bool grounded;//������ ���� �ٴ�üũ
    Rigidbody rb;

    public PhotonView pv;
    public SpriteRenderer SR;

    private float h, v, r;
    private Transform tr;
    public Animator anim;

    public float movespeed = 5.0f;
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

        pv.ObservedComponents[0] = this; // ����ȭ �ݹ��Լ� �߻� �� �ʿ�

        if (pv.IsMine)
        {
            // �ڽ��� �÷��̾�Ը� ī�޶� ����� ����
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
            //anim.CrossFade(�ִϸ��̼� �̸�);  �ִϸ��̼� ��ȯ�� �ε巴�� ����
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
        if (Input.GetKeyDown(KeyCode.Space) && grounded)//�������� �����̽��� ������
        {
            rb.AddForce(transform.up * jumpForce);//�����¸�ŭ���� ������
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gpu_RankItem")  // ��޾������� ������
        {
            PhotonNetwork.Destroy(other.gameObject);  // ��޾����� ����
            MM.Gpu_rank++;
        }
        else if (other.gameObject.tag == "Cpu_RankItem")  // ��޾������� ������
        {
            PhotonNetwork.Destroy(other.gameObject);  // ��޾����� ����
            MM.Cpu_rank++;
        }
        else if (other.gameObject.tag == "Cooler_RankItem")  // ��޾������� ������
        {
            PhotonNetwork.Destroy(other.gameObject);  // ��޾����� ����
            MM.Cooler_rank++;
        }
        else if (other.gameObject.tag == "Power_RankItem")  // ��޾������� ������
        {
            PhotonNetwork.Destroy(other.gameObject);  // ��޾����� ����
            MM.Power_rank++;
        }
    }


    [PunRPC]
    void FilpXRPC(float axis)
    {
        SR.flipX = axis == -1;
    }

    // ���¸� ����ȭ �����ִ� �޼ҵ�
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // �ڽ��� �÷��� ������ �ٸ� ��Ʈ��ũ ����ڿ��� �۽�
        if (stream.IsWriting) 
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
           
        }
        // �ٸ� ������� ���� ����
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