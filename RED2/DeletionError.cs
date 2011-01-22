using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RED2
{
    public partial class DeletionError : Form
    {
        public DeletionError()
        {
            InitializeComponent();
        }

        private void DeletionError_Load(object sender, EventArgs e)
        {

        }

        internal void SetPath(string p)
        {
            this.tbPath.Text = p;
        }

        internal void SetErrorMessage(string p)
        {
            this.tbErrorMessage.Text = p;
        }
    }
}
