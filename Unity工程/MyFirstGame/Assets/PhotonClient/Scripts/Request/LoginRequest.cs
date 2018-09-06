using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;

public class LoginRequest : RequestBase
{
    [HideInInspector]
    public string username;
    [HideInInspector]
    public string password;

    private LoginPanel loginPanel;


    protected override void Init()
    {
        operationCode = OperationCode.Login;
    }

    public  override void Start()
    {
        base.Start();        
        loginPanel = GetComponent<LoginPanel>();
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username, username);
        data.Add((byte)ParameterCode.Password, password);
        PhotonEngine.Peer.OpCustom((byte)OperationCode.Login, data, true);
    }

    public override void OnOperationRespons(OperationResponse operationResponse)
    {
        ReturnCode code = (ReturnCode)operationResponse.ReturnCode;
        if (code == ReturnCode.Success)
            PhotonEngine.username = username;
        loginPanel.OnPerationResponse(code);
    }


}