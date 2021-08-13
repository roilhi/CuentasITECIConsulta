using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace Cuentas_ITECI_Consulta
{
    public partial class LoginForm : MetroForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private void tbUsr_KeyDown(object sender, KeyEventArgs e) 
        {
            if (tbUsr.Text == "admin" && tbPass.Text == "iteciprepa1")
            {
                new LoginForm().Show();
                this.Hide();
            }
            else 
            { 
                

            }
        }
    }
}
