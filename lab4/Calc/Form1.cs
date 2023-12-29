using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly List<string> history = new List<string>();
        int count;
        float a, b;
        bool sign;
        bool onTop = false;

        private void button20_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Вийти з програми?", "Вихід", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button17_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 0;
        private void button13_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 1;
        private void button14_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 2;
        private void button15_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 3;
        private void button9_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 4;
        private void button10_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 5;
        private void button11_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 6;
        private void button5_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 7;
        private void button6_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 8;
        private void button7_Click(object sender, EventArgs e) => textBox1.Text = textBox1.Text + 9;

        private void button2_Click(object sender, EventArgs e)
        {
            int lenght = textBox1.Text.Length - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < lenght; i++)
                textBox1.Text = textBox1.Text + text[i];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sign == true)
            {
                textBox1.Text = "-" + textBox1.Text;
                sign = false;
            } 
            else if (sign == false)
            {
                textBox1.Text = textBox1.Text.Replace("-", "");
                sign = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = 1;
            label1.Text = a.ToString() + "+";
            sign = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = 2;
            label1.Text = a.ToString() + "-";
            sign = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = 3;
            label1.Text = a.ToString() + "*";
            sign = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            a = float.Parse(textBox1.Text);
            textBox1.Clear();
            count = 4;
            label1.Text = a.ToString() + "/";
            sign = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ",";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            calculate();
            label1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            textBox1.Text = "";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false;
            Message m = Message.Create(base.Handle, 0xA1, new IntPtr(2), IntPtr.Zero);
            base.WndProc(ref m);
        }

        private void button20_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTip x = new ToolTip();
            x.SetToolTip(button20, "Вихід");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (onTop == false)
                onTop = true;
            else
                onTop = false;
            onTopCheck();
        }

        private void calculate()
        {
            float secondOperand = float.Parse(textBox1.Text);
            switch (count)
            {
                case 1:
                    b = a + secondOperand;
                    label1.Text = $"{a} + {secondOperand} = {b}";
                    textBox1.Text = b.ToString();
                    break;
                case 2:
                    b = a - secondOperand;
                    label1.Text = $"{a} - {secondOperand} = {b}";
                    textBox1.Text = b.ToString();
                    break;
                case 3:
                    b = a * secondOperand;
                    label1.Text = $"{a} * {secondOperand} = {b}";
                    textBox1.Text = b.ToString();
                    break;
                case 4:
                    b = a / secondOperand;
                    label1.Text = $"{a} / {secondOperand} = {b}";
                    textBox1.Text = b.ToString();
                    break;
                default:
                    break;
            }

            string operationHistory = $"{a} {GetOperationSign()} {secondOperand} = {b}";
            history.Add(operationHistory);
        }


        private string GetOperationSign()
        {
            switch (count)
            {
                case 1: return "+";
                case 2: return "-";
                case 3: return "*";
                case 4: return "/";
                default: return "";
            }
        }

        private void DisplayHistory()
        {
            listBox1.Items.Clear();
            foreach (var operation in history)
            {
                listBox1.Items.Add(operation);
            }
        }

        private void button21_MouseMove(object sender, MouseEventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button21, "Закріпити поверх усіх вікон");
        }

        private void button22_Click(object sender, EventArgs e)
        {

            DisplayHistory();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            history.Clear();
            listBox1.Items.Clear(); 
        }

        public void onTopCheck()
        {
            if (onTop == true)
            {
                TopMost = true;
                button21.BackColor = Color.Purple;
            } 
            else
            {
                TopMost = false;
                button21.BackColor = Color.DimGray;
            }
        }

    }
}
