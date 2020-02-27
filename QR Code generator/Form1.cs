using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QRCoder;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace QR_Code_generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void Form1_MouseDown_1(object sender, MouseEventArgs e)
        {
            //Przesuwanie Okien PART 3  
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Generate();
        }
        private void Generate()
        {
            QRCodeGenerator codeGenerator = new QRCodeGenerator();
            var Data = codeGenerator.CreateQrCode(textBox1.Text, QRCodeGenerator.ECCLevel.H);
            var code = new QRCode(Data);

            pictureBox1.Image = code.GetGraphic(150);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Generate();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = "jpeg";
            dlg.AddExtension = true;
            dlg.Filter = "Data Files (*.jpeg)|*.jpeg";
            dlg.FileName = textBox1.Text;        
            dlg.ShowDialog();
            string path = dlg.FileName;

            pictureBox1.Image.Save(path, ImageFormat.Jpeg);
        }
    }
}
