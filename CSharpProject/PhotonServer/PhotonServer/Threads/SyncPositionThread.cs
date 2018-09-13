using Common;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyGameServer.Threads
{
    public class SyncPositionThread
    {
        private Thread thread;

        public void Run()
        {
            thread = new Thread(UpdatePosition);
            thread.IsBackground = true;
            thread.Start();
        }

        public void Stop()
        {
            thread.Abort();

        }

        private void UpdatePosition()
        {
            Thread.Sleep(2000);

            while (true)
            {
                Thread.Sleep(20);
                //进行同步
                SendPosition();
            }
        }

        private void SendPosition()
        {
            List<PlayerData> playerDataList = new List<PlayerData>();
            foreach (ClientPeer peer in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.Username = peer.username;
                    playerData.Pos = new Vector3Data() { X = peer.x, Y = peer.y, Z = peer.z };
                    playerDataList.Add(playerData);
                }
            }
            string playerDataListString = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PlayerData>));
                serializer.Serialize(sw, playerDataList);
                playerDataListString = sw.ToString();
            }
            Dictionary<byte, object> data = new Dictionary<byte, object>();
            data.Add((byte)ParameterCode.PlayerDataList, playerDataListString);

            foreach (ClientPeer peer in MyGameServer.Instance.peerList)
            {
                if (string.IsNullOrEmpty(peer.username) == false)
                {
                    EventData ed = new EventData((byte)EventCode.SyncPosition);
                    ed.Parameters = data;
                    peer.SendEvent(ed, new SendParameters());
                }
            }
        }
    }
}
