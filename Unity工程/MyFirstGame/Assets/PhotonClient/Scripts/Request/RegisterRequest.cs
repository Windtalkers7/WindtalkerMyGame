using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common;
using UnityEngine;

public class RegisterRequest : RequestBase
{
    [HideInInspector]
    public string username;
    [HideInInspector]
    public string password;

    private RegisterPanel registerPanel;

    protected override void Init()
    {
        operationCode = OperationCode.Register;
    }

    public override void Start()
    {         
        base.Start();       
        registerPanel = GetComponent<RegisterPanel>();
    }
    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.Username, username);
        data.Add((byte)ParameterCode.Password, password);
        PhotonEngine.Peer.OpCustom((byte)operationCode, data,true);
    }

    public override void OnOperationRespons(OperationResponse operationResponse)
    {
        ReturnCode code = (ReturnCode)operationResponse.ReturnCode;
        registerPanel.OnRegisterResponse(code);
    }
}