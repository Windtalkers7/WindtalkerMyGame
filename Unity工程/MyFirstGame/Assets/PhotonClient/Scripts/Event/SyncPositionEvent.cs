using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ExitGames.Client.Photon;
using Common.Tools;
using Common;
using System.Xml.Serialization;

public class SyncPositionEvent : EventBase
{
    private PlayerController playerController;

    protected override void Init()
    {
        eventCode = EventCode.SyncPosition;
    }

    protected override void Start()
    {
        base.Start();
        playerController = PlayerController.Instance;
    }
    public override void OnEvent(EventData eventData)
    {
        string playerDataListString = (string)DicTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.PlayerDataList);

        using (StringReader reader = new StringReader(playerDataListString))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
            List<PlayerData> playerDataList = (List<PlayerData>)serializer.Deserialize(reader);
            playerController.OnSyncPositionEvent(playerDataList);
        }
    }


}