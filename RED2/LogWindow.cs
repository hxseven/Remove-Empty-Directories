using System;
using System.Windows.Forms;

namespace RED2
{
    public partial class LogWindow : Form
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void LogWindow_Load(object sender, EventArgs e)
        {

        }

        public void SetLog(string log) {
            this.tbLog.Text = log;
        }

        private void tbLog_DoubleClick(object sender, EventArgs e)
        {
            this.tbLog.SelectAll();
        }
    }
}
