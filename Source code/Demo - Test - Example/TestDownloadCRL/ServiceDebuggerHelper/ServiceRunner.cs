using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ServiceDebuggerHelper
{
    public partial class ServiceRunner : Form
    {
        private readonly IDebuggableService _theService;
        public ServiceRunner(IDebuggableService service)
        {
            InitializeComponent();
            //
            _theService = service;
            ServiceBase winService = _theService as ServiceBase;
            if (winService != null) Text = winService.ServiceName + " Controler";
            Show();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            _theService.Start(new string[] { });
            lblResult.Text = "Started";
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            _theService.Pause();
            lblResult.Text = "Stopped";
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            _theService.Continue();
            lblResult.Text = "Started";
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            _theService.StopService();
            lblResult.Text = "Stopped";
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {

        }
    }
}
