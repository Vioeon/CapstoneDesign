using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks  // �ٸ� ���� ���� �޾Ƶ��̱�
{
    [SerializeField] TMP_Text text;

    Player player;  // ���� ����Ÿ���� Player�� ���� �� �� �ְ� ���ش�.

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;  // �÷��̾� �̸� �޾Ƽ� �׻�� �̸��� ��Ͽ� �߰� ������ش�. 
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)  // �÷��̾ �涰������ ȣ��
    {
        if (player == otherPlayer)  // ���� �÷��̾ ����?
        {
            Destroy(gameObject);  // �̸�ǥ ����
        }
    }

    public override void OnLeftRoom()  // �� ������ ȣ��
    {
        Destroy(gameObject);  // �̸�ǥ ȣ��
    }
}