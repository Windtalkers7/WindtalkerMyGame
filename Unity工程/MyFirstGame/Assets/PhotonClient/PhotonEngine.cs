using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Common;
using Common.Tools;

public class PhotonEngine : MonoBehaviour, IPhotonPeerListener
{
    public static PhotonEngine Instance;

    private static PhotonPeer peer;
    public static PhotonPeer Peer
    {
        get { return peer; }
    }

    public static string username;

    private Dictionary<EventCode, EventBase> eventDic;
    private Dictionary<OperationCode, RequestBase> requestDic;

    #region  UnityAPI

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        eventDic = new Dictionary<EventCode, EventBase>();
        requestDic = new Dictionary<OperationCode, RequestBase>();
    }

    private void Start()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        peer.Connect("127.0.0.1:5055", "MyGameFirst");

        Application.runInBackground = true;
    }

    private void Update()
    {
        //必须实时调用
        peer.Service();
    }

    private void OnDestroy()
    {
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect();
        }
    }

    #endregion



    #region PhotonAPI

    public void DebugReturn(DebugLevel level, string message)
    {
        throw new System.NotImplementedException();
    }


    public void OnEvent(EventData eventData)
    {
        EventCode code = (EventCode)eventData.Code;
        EventBase e = DicTool.GetValue<EventCode, EventBase>(eventDic, code);
        if (e)
            e.OnEvent(eventData);
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        OperationCode code = (OperationCode)operationResponse.OperationCode;
        RequestBase request = DicTool.GetValue<OperationCode, RequestBase>(requestDic, code);
        request.OnOperationRespons(operationResponse);
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
       
    }

    #endregion

    public void AddEvent(EventBase e)
    {
        if (eventDic.ContainsKey(e.eventCode))
            RemoveEvent(e);
        eventDic.Add(e.eventCode, e);
    }

    public void RemoveEvent(EventBase e)
    {
        if (eventDic.ContainsKey(e.eventCode))
            eventDic.Remove(e.eventCode);
    }


    public void AddRequest(RequestBase request)
    {
        if (requestDic.ContainsKey(request.operationCode))
            RemoveRequest(request);

        requestDic.Add(request.operationCode, request);
    }

    public void RemoveRequest(RequestBase request)
    {
        if (requestDic.ContainsKey(request.operationCode))
            requestDic.Remove(request.operationCode);
    }
}
