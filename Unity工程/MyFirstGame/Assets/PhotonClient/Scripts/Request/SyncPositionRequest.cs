using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using UnityEngine;
using Common;

public class SyncPositionRequest : RequestBase
{
    [HideInInspector]
    public Vector3 pos;
    protected override void Init()
    {
        operationCode = Common.OperationCode.SyncPosition;
    }

    public override void DefaultRequest()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add((byte)ParameterCode.X, pos.x);
        data.Add((byte)ParameterCode.Y, pos.y);
        data.Add((byte)ParameterCode.Z, pos.z);
        PhotonEngine.Peer.OpCustom((byte)operationCode, data, true);
    }

    public override void OnOperationRespons(OperationResponse operationResponse)
    {
        throw new System.NotImplementedException();
    }
}