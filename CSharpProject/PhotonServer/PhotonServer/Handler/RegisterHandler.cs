using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using Common;
using Common.Tools;
using MyGameServer.Manager;
using MyGameServer.Model;

namespace MyGameServer.Handler
{
    public class RegisterHandler : HandlerBase
    {
        public RegisterHandler()
        {
            opCode = Common.OperationCode.Register;
        }

        public override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters, ClientPeer peer)
        {
            string username = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Username) as string;
            string password = DicTool.GetValue<byte, object>(operationRequest.Parameters, (byte)ParameterCode.Password) as string;                        
            UserManager userManager = new UserManager();
            User user = userManager.GetByUsername(username);
            OperationResponse operationResponse = new OperationResponse(operationRequest.OperationCode);
            if (user == null)
            {
                user = new User() { Username = username, Password = password };
                userManager.Add(user);
                operationResponse.ReturnCode = (short)ReturnCode.Success;
            }
            else
            {
                operationResponse.ReturnCode = (short)ReturnCode.Failed;
            }

            peer.SendOperationResponse(operationResponse, sendParameters);
        }
    }
}
