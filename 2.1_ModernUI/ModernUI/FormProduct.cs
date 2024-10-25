using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModernUI
{
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }

        void LoadTheme()
        {
            foreach (Control _btn in this.Controls)
            {
                if (_btn.GetType() == typeof(Button))
                {
                    Button btn = (Button)_btn;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;

                    
                }
            }
            label1.ForeColor = ThemeColor.SecondaryColor;
        }
    }
}
