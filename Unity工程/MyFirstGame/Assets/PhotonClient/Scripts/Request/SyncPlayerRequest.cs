using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common.Tools;
using Common;
using System.Xml.Serialization;

public class SyncPlayerRequest : RequestBase
{
    protected override void Init()
    {
        operationCode = Common.OperationCode.SyncPlayer;
    }

    public override void DefaultRequest()
    {
        PhotonEngine.Peer.OpCustom((byte)operationCode, null, true);
    }

    public override void OnOperationRespons(OperationResponse operationResponse)
    {
        string usernameListString = (string)DicTool.GetValue<byte, object>(operationResponse.Parameters, (byte)ParameterCode.UsernameList);

        using (StringReader reader = new StringReader(usernameListString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<string>));
            List<string> usernameList = (List<string>)serializer.Deserialize(reader);
            PlayerController.Instance.OnSyncPlayerResponse(usernameList);
        }
    }
}