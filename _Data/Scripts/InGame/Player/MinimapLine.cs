﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//미니맵 라인 그려줌
public class MinimapLine : MonoBehaviour
{

    LineRenderer line;
    SpriteRenderer ChampIcon;
    string IconName;
    GameObject Parent;

    private void Awake()
    {
       

        //동기화용. 플레이어 이름, 팀, 스펠 넘겨주기
        //if (PhotonNetwork.player.IsLocal)
        //{
        //    object[] datas = new object[] { (string)playerName, (string)Team, (string)IconName, };

        //    RaiseEventOptions op = new RaiseEventOptions()
        //    {
        //        CachingOption = EventCaching.AddToRoomCache,
        //        Receivers = ReceiverGroup.Others,
        //    };

        //    PhotonNetwork.RaiseEvent((byte)0, datas, true, op);
        //}
    }

    private void OnEnable()
    {
        //포톤 사용시
        //PhotonNetwork.RaiseEvent += SyncChampForMinimap();
    }

    private void Start()
    {
        line = this.GetComponent<LineRenderer>();
        line.positionCount = 0;

        ChampIcon = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Parent = GameObject.FindGameObjectWithTag("Player");
        ChampionData cd = Parent.GetComponent<ChampionData>();
        ChampIcon.sprite = Resources.Load<Sprite>("ChampionIcon/" + cd.ChampionName);
        IconName = cd.ChampionName;
    }

    private void OnDisable()
    {
        //PhotonNetwork.RaiseEvent -= SyncChampForMinimap();
    }
    private void Update()
    {
        var PlayerObj = Parent.transform.GetChild(0);
        this.transform.position = new Vector3(PlayerObj.transform.position.x, transform.position.y, PlayerObj.transform.position.z);
        
    }

    public void drawLine(Vector3[] endpoints)
    {
        line.positionCount = endpoints.Length;
        line.startWidth = 0.5f;
        line.endWidth = 0.5f;
        line.SetPositions(endpoints);
    }

    public void deleteLine()
    {
        line.positionCount = 0;
    }

    //동기화용
    public void SyncChampForMinimap(byte eventCode, object content, int senderId)
    {
        //if (eventCode.Equals(0)) // 이벤트 코드 통합후 변경
        //{
        //    object[] datas = content as object[];
        //    if (datas.Length.Equals(3)) //이름, 팀, 스프라이트 이름
        //    {
        //        if (PhotonNetwork.player.GetTeam().ToString().Equals(datas[1])) // 팀이름이 같으면
        //        {
        //            var playerobj = GameObject.Find((string)datas[0]); // Sender 의 오브젝트 찾기
        //            var ChampIcon = playerobj.transform.GetChild(2).GetComponent<MinimapLine>().ChampIcon;
        //            spSelect.btnOrder(1);
        //            ChampIcon = Resources.Load<Sprite>("ChampionIcon/" + (string)datas[2]);
        //            
        //        }
        //    }
        //}
    }


}
