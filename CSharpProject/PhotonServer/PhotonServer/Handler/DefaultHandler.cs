using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;

namespace MyGameServer.Handler
{
    public class DefaultHandler : HandlerBase
    {
        public DefaultHandler()
        {
            opCode = OperationCode.Default;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            throw new NotImplementedException();
        }
    }
}
