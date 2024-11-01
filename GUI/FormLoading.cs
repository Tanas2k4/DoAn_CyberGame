using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
            // Cấu hình ProgressBar
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.ForeColor = Color.Green; // Đặt màu xanh lá cho ProgressBar
        }

        // Hàm để cập nhật giá trị của ProgressBar
        public void UpdateProgress(int value)
        {
            if (value <= progressBar1.Maximum)
            {
                progressBar1.Value = value;
            }
        }
    }
}
