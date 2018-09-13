using Common;
using Photon.SocketServer;

namespace MyGameServer.Handler
{
    public abstract class HandlerBase
    {
        public OperationCode opCode;
        public abstract void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer);
    }
}