using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Encripte
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            txtEncrypt.Text = Eramake.eCryptography.Encrypt(txtValue.Text);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            txtDecrypt.Text = Eramake.eCryptography.Decrypt(txtValue.Text);
        }
    }
}
