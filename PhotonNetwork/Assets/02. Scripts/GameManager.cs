using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    // 플레이어가 생성될 위치들

    public Transform[] spawnPoints;
    
    // 네트워크로 빌드해서 게임을 해보면 해상도 설정을 해주는게 편하다
    private void Awake()
    {
        // 해상도 설정
        Screen.SetResolution(800, 600, FullScreenMode.Windowed);
    }
    // 게임매니저의 역할
    // 사용자가 게임세상에 들어오면 플레이어를 생성해준다
    // 프리팹으로 플레이어가 만들어져 있을때
    // 프리팹은 반드시 Resources 폴더 경로안에 들어 있어야 한다


    // Start is called before the first frame update
    void Start()
    {
        // 기본 네트워크 세팅이 아닌 데이터 전송률을 조금더 높여주자
        // 전송 속도 관련 세팅하기 ( 안해주면 디폴트값으로 세팅 )
        // 기본 디폴트 값은 10프레임으로 설정
        // 하지만 네트워크 지연율은 네트워크 상황에따라 다르고 아무도모름
        // 1. RPC
        PhotonNetwork.SendRate = 30;
        // 2. Socket Send, Receive
        // 참고로 SendRate보다 크면 안된다고 되있음, 따라서 같은값으로 맞추자
        PhotonNetwork.SerializationRate = 30;

        // 플레이어 생성
        CreatePlayer();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    private void CreatePlayer()
    {
        // 포톤네트워크를 사용해서 만들어야 한다.
        int index = Random.Range(0, spawnPoints.Length);
        PhotonNetwork.Instantiate("Player", spawnPoints[index].position,spawnPoints[index].rotation);
    }


}
