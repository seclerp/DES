using System;
using System.Collections.Generic;
using System.Linq;
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

            try
            {
                var encryptionResult =
                    Encryptor.encrypt(textBox2.Text, EncryptionUtils.formatBinaryString(textBox1.Text, 4));
                textBox3.Text = EncryptionUtils.fromBinaryString(encryptionResult.Item1);
                listView1.Items.Clear();
                listView1.Items.AddRange(encryptionResult.Item2
                    .Select((item, index) => new ListViewItem($"Block {index} entropy: {item}")).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
