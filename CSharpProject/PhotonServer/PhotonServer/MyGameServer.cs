using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using System.IO;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MyGameServer.Handler;
using Common;
using MyGameServer.Threads;

namespace MyGameServer
{
    //所有的server端 主类都要集成自ApplicationBase
    class MyGameServer : ApplicationBase
    {

        public static MyGameServer Instance
        {
            get;
            private set;
        }

        public Dictionary<OperationCode, HandlerBase> handlerDic = new Dictionary<OperationCode, HandlerBase>();
        public List<ClientPeer> peerList = new List<ClientPeer>();
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();
        private SyncPositionThread syncPositionThread = new SyncPositionThread(); 

        //当一个客户端请求链接时调用
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("检测到一个客户端请求连接！");
            ClientPeer peer = new ClientPeer(initRequest);
            peerList.Add(peer);
            return peer;
        }

        //初始化
        protected override void Setup()
        {
            Instance = this;         
            InitLog();
            InitHandler();
            syncPositionThread.Run();
        }

        private void InitLog()
        {
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");

            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);//让photon知道
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置文件
            }
            log.Info(log4net.GlobalContext.Properties["Photon: ApplicationLogPath"]);
            log.Info(this.ApplicationRootPath);
            log.Info("初始化日志文件完成!");
        }

        private void InitHandler()
        {
            DefaultHandler defaultHandle = new DefaultHandler();
            handlerDic.Add(defaultHandle.opCode, defaultHandle);
            LoginHandler loginHandler = new LoginHandler();
            handlerDic.Add(loginHandler.opCode, loginHandler);
            RegisterHandler registerHandler = new RegisterHandler();
            handlerDic.Add(registerHandler.opCode, registerHandler);
            SyncPositionHandler syncPositionHandler = new SyncPositionHandler();
            handlerDic.Add(syncPositionHandler.opCode, syncPositionHandler);
            SyncPlayerHandler syncPlayerHander = new SyncPlayerHandler();
            handlerDic.Add(syncPlayerHander.opCode, syncPlayerHander);
        }

        //server关闭的时候
        protected override void TearDown()
        {
            syncPositionThread.Stop();
            log.Info("服务器应用关闭了");
        }      
    }
}
