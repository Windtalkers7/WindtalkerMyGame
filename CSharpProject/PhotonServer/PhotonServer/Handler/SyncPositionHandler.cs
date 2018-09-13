using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;

namespace MyGameServer.Handler
{
    public class SyncPositionHandler : HandlerBase
    {

        public SyncPositionHandler()
        {
            opCode = Common.OperationCode.SyncPosition;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            Vector3Data pos = (Vector3Data)DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Position);
            float x = (float)DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.X);
            float y = (float)DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Y);
            float z = (float)DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Z);
            peer.x = x; peer.y = y; peer.z = z;
        }
    }
}
