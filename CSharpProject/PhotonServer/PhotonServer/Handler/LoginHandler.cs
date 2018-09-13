using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using MyGameServer.Manager;

namespace MyGameServer.Handler
{
    public class LoginHandler : HandlerBase
    {
        public LoginHandler()
        {
            opCode = Common.OperationCode.Login;
        }
        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string username = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;
            UserManager userManager = new UserManager();
            bool isSuccess = userManager.VerifyUser(username, password);

            OperationResponse response = new OperationResponse(operationRequest.OperationCode);
            if (isSuccess)
            {
                response.ReturnCode = (short)Common.ReturnCode.Success;
                peer.username = username;
            }
            else
            {
                response.ReturnCode = (short)Common.ReturnCode.Failed;
            }

            peer.SendOperationResponse(response, sendParameters);
        }
    }
}
