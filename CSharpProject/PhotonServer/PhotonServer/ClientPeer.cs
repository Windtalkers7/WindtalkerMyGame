using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using MyGameServer.Handler;
using Common.Tools;
using Common;

namespace MyGameServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public float x, y, z;
        public string username;

        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {
        }

        //处理客户端断开连接的后续工作
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            MyGameServer.log.Info("有一个客户端断开连接");
            MyGameServer.Instance.peerList.Remove(this);
        }

        //处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler = (HandlerBase)DicTool.GetValue<OperationCode, HandlerBase>(MyGameServer.Instance.handlerDic, (OperationCode)operationRequest.OperationCode);
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
            }
            else
            {
                HandlerBase defaultHandler = DicTool.GetValue<OperationCode, HandlerBase>(MyGameServer.Instance.handlerDic, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }
        }
    }
}
