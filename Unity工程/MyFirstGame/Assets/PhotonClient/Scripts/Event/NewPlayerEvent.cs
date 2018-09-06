using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExitGames.Client.Photon;
using Common.Tools;
using Common;

public class NewPlayerEvent : EventBase
{
    private PlayerController playerController;

    protected override void Init()
    {
        eventCode = EventCode.NewPlayer;
    }
    protected override void Start()
    {
        base.Start();

        playerController = PlayerController.Instance;
    }
    public override void OnEvent(EventData eventData)
    {
        string username = (string)DicTool.GetValue<byte, object>(eventData.Parameters, (byte)ParameterCode.Username);
        playerController.OnNewPlayerEvent(username);
    }


}