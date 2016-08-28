using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;

namespace RabbitMqLogger.DatabaseWriter
{
    public partial class Service1 : ServiceBase
    {
        private IBus _messageBus;
        private LogDbContext _dbContext;
        private MessageWriter _messageWriter;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var rmqHost = ConfigurationManager.AppSettings["rmqHost"];
            var rmqConnection = new ConnectionConfiguration { Hosts = new[] { new HostConfiguration { Host = rmqHost } } };
            _messageBus = RabbitHutch.CreateBus(rmqConnection, register => { });
            _dbContext = new LogDbContext(ConfigurationManager.ConnectionStrings["logDb"].ConnectionString);

            _messageWriter = new MessageWriter(_messageBus, _dbContext);
        }

        protected override void OnStop()
        {
            _messageBus?.Dispose();
            _dbContext?.Dispose();
        }

        public void Start()
        {
            OnStart(null);
        }
    }
}
