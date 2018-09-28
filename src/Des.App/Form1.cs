using System;
using System.Windows.Forms;
using Des.Core;

namespace Des.App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                return;
            } 

            textBox3.Text = EncryptionUtils.fromBinaryString(Encryptor.encrypt(textBox2.Text, EncryptionUtils.formatBinaryString(textBox1.Text, 4)));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                return;
            }

            textBox2.Text = EncryptionUtils.fromBinaryString(Encryptor.decrypt(EncryptionUtils.toBinaryString(textBox3.Text), EncryptionUtils.formatBinaryString(textBox1.Text, 4)));
        }
    }
}
