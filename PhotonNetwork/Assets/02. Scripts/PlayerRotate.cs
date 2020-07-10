using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRotate : MonoBehaviourPun
{
    //카메라를 마우스움직이는 방향으로 회전하기
    public float speed = 150;
    //회전각도를 직접 제어하자
    float angleX;

    Transform cam;

    private void Start()
    {
        // 내 카메라는 제거 할필요 x
        if (photonView.IsMine) return;

        // 내가 아니면 상대방이니 상대방 카메라 태그변경
        // 또한 상대방의 카메라,오디오 비활성화
        cam = transform.Find("Main Camera");
        cam.tag = "Untagged";
        cam.GetComponent<Camera>().enabled = false;
        cam.GetComponent<AudioListener>().enabled = false;
        cam.parent.gameObject.layer = LayerMask.NameToLayer("Enemy");
    }


    void Update()
    {
        // 이 사용자가 나라면 회전하고 아니면 하지말라
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Mouse X");
        angleX += h * speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, angleX, 0);
    }
}
