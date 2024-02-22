using BarcodeLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BarcodeTest
{
    public partial class BarcodeForm : Form
    {
        public BarcodeForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxCode.Text != "" && textBoxCode.Text.Length == 13 && long.TryParse(textBoxCode.Text, out _))
            {
                pictureBox.Image = Barcode.DrawEAN13(pictureBox.Width, pictureBox.Height, textBoxCode.Text);
            }
            else MessageBox.Show("Для успешной генерации штрихкода код должен состоять из 13 цифр!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
