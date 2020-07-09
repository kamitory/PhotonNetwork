using UnityEngine;

using UnityEngine.UI;
using Photon.Pun;       //포톤네트워크 핵심기능
using Photon.Realtime;  //포톤 서비스관련(룸옵션,디스커넥션등)

//네트워크 매니저 : 룸(게임공간)으로 연결시켜주는 역할
//포톤네트워크 : 마스터서버 -> 로비(대기실) -> 룸

// MonoBehaviourPunCallbacks  : 포톤서버 접속, 로비 접속, 룸 접속 등 이벤트를 받아올수있다.

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public Text infoText; //네트워크상태를 보여줄 텍스트
    public Button connectButton; //룸 접속버튼

    string gameVersion = "1"; //게임버전

    private void Awake()
    {
        //해상도 설정
        Screen.SetResolution(800, 600, FullScreenMode.Windowed);
    }

    // Start is called before the first frame update
    void Start()
    {
        //접속에 필요한 정보 ( 게임버전 ) 설정 
        PhotonNetwork.GameVersion = gameVersion;
        //마스터 서버에 접속하는 함수 ( 제일 중요 )
        PhotonNetwork.ConnectUsingSettings();
        //접속 시도중임으로 텍스트로 표시
        infoText.text = "마스터 서버에 접속중...";
        //룸(게임공간) 접속 버튼 비활성화
        connectButton.interactable = false; 
        

    }

    public override void OnConnectedToMaster()
    {
        //접속 정보 표기
        infoText.text = "마스터 서버와 연결됨";
        //룸(게임공간) 접속 버튼 활성화
        connectButton.interactable = true;
    }

    //혹시나 시작하면서 마스터 서버에 접속 실패했을시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        connectButton.interactable = false;
        //접속 시도중임으로 텍스트로 표시
        infoText.text = "마스터 서버와 연결실패 \n";
        //마스터 서버에 접속하는 함수 ( 제일 중요 )
        PhotonNetwork.ConnectUsingSettings();
    }

    //접속버튼 클릭시 이함수 발돌
    public void OnConnect()
    {
        //중복 접속 차단하기 위해 접속버튼 비활성화
        connectButton.interactable = false;

        //마스터 서버에 접속중인지?
        if(PhotonNetwork.IsConnected)
        {
            //룸(게임공간)으로 바로 접속실행
            infoText.text = "랜덤방으로 접속...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {  
            //마스터 서버에 접속중이 아니라면 다시 마스터 서버에 접속시도
            //접속 정보 표기
            infoText.text = "오프라인 : 마스터 서버와 연결 실패 \n 다시연결중...";
            //룸(게임공간) 접속 버튼 활성화
            PhotonNetwork.ConnectUsingSettings();
        }
    }


    //룸에 참가 완료된경우 자동실행
    public override void OnJoinedRoom()
    {
        //접속 정보 표기
        infoText.text = "랜덤 방 참가 완료";
        //모든 룸 참가자들이 "GameScene"을 로드
        PhotonNetwork.LoadLevel("GameScene");
    }

    //(빈 방이 없어)랜덤룸 참가에 실패한 경우 자동실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //접속 정보 표기
        infoText.text = "빈 방이 없어 새로운방 생성중...";
        //빈방을 생성
        PhotonNetwork.CreateRoom(null,new RoomOptions { MaxPlayers = 4 });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
