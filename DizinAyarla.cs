// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.DizinAyarla
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using Oracle.DataAccess.Client;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MuhammedYuksel
{
    public class DizinAyarla : Form
    {
        private IContainer components = (IContainer)null;
        private Label label10;
        private Label label6;
        private Label label1;
        private TextBox DatabaseNameTextBox;
        private Button TestButton;
        private TextBox KullaniciTextBox;
        private TextBox SifreTextBox;
        private Button KaydetButton;
        private Button button2;

        public DizinAyarla()
        {
            this.InitializeComponent();
            Program.program_bilgi_oku();
            if (Program.DatabaseName == null)
                return;
            this.DatabaseNameTextBox.Text = Program.DatabaseName;
            this.KullaniciTextBox.Text = Program.DatabaseUserName;
            this.SifreTextBox.Text = Program.Password;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            string result;
            if (this.BaglantiTestEt(out result))
            {
                int num1 = (int)MessageBox.Show(result);
            }
            else
            {
                int num2 = (int)MessageBox.Show(result, "Bağlantı Hatası");
            }
        }

        private bool BaglantiTestEt(out string result)
        {
            bool flag = false;
            result = "";
            if (this.KullaniciTextBox.Text != "" && this.SifreTextBox.Text != "")
            {
                try
                {

                    OracleConnection oracleConnection = new OracleConnection("Data Source=" + this.DatabaseNameTextBox.Text + ";User Id=" + this.KullaniciTextBox.Text + ";Password=" + this.SifreTextBox.Text + ";");

                    //OracleConnection oracleConnection =
                    //    new OracleConnection(
                    //        "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=7.242.244.114)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=uyumsoft)));User Id=uyumsoft;Password=uyumsoft");
                    oracleConnection.Open();
                    result = "Bağlantı Başarılı";
                    oracleConnection.Close();
                    flag = true;
                }
                catch (OracleException ex)
                {
                    Console.WriteLine("Oracle Exception Message");
                    Console.WriteLine("Exception Message: " + ex.Message);
                    Console.WriteLine("Exception Source: " + ex.Source);
                }
                catch (Exception ex)
                {
                    result = ex.ToString() + " \n\n " + ex.Message;
                    flag = false;
                }

            }
            return flag;
        }

        private void KaydetButton_Click(object sender, EventArgs e)
        {
            if (!(this.DatabaseNameTextBox.Text != ""))
                return;
            Program.DatabaseName = this.DatabaseNameTextBox.Text;
            Program.DatabaseUserName = this.KullaniciTextBox.Text;
            Program.Password = this.SifreTextBox.Text;
            Program.program_bilgi_yaz();
            this.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.label10 = new Label();
            this.label6 = new Label();
            this.label1 = new Label();
            this.DatabaseNameTextBox = new TextBox();
            this.TestButton = new Button();
            this.KullaniciTextBox = new TextBox();
            this.SifreTextBox = new TextBox();
            this.KaydetButton = new Button();
            this.button2 = new Button();
            this.SuspendLayout();
            this.label10.AutoSize = true;
            this.label10.ImeMode = ImeMode.NoControl;
            this.label10.Location = new Point(60, 74);
            this.label10.Name = "label10";
            this.label10.Size = new Size(34, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Şifre :";
            this.label6.AutoSize = true;
            this.label6.ImeMode = ImeMode.NoControl;
            this.label6.Location = new Point(17, 45);
            this.label6.Name = "label6";
            this.label6.Size = new Size(70, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Kullanıcı Adı :";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new Size(80, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Database Yolu:";
            this.DatabaseNameTextBox.BackColor = Color.PeachPuff;
            this.DatabaseNameTextBox.Location = new Point(102, 12);
            this.DatabaseNameTextBox.Margin = new Padding(3, 2, 3, 2);
            this.DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            this.DatabaseNameTextBox.Size = new Size(208, 20);
            this.DatabaseNameTextBox.TabIndex = 17;
            this.TestButton.BackColor = Color.PeachPuff;
            this.TestButton.ImeMode = ImeMode.NoControl;
            this.TestButton.Location = new Point(102, 92);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new Size(208, 27);
            this.TestButton.TabIndex = 21;
            this.TestButton.Text = "Bağlantıyı Test Et";
            this.TestButton.UseVisualStyleBackColor = false;
            this.TestButton.Click += new EventHandler(this.TestButton_Click);
            this.KullaniciTextBox.BackColor = Color.PeachPuff;
            this.KullaniciTextBox.Location = new Point(102, 37);
            this.KullaniciTextBox.Name = "KullaniciTextBox";
            this.KullaniciTextBox.Size = new Size(208, 20);
            this.KullaniciTextBox.TabIndex = 19;
            this.SifreTextBox.BackColor = Color.PeachPuff;
            this.SifreTextBox.Location = new Point(102, 66);
            this.SifreTextBox.Name = "SifreTextBox";
            this.SifreTextBox.Size = new Size(208, 20);
            this.SifreTextBox.TabIndex = 20;
            this.SifreTextBox.UseSystemPasswordChar = true;
            this.KaydetButton.BackColor = Color.Green;
            this.KaydetButton.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte)162);
            this.KaydetButton.ForeColor = Color.White;
            this.KaydetButton.ImeMode = ImeMode.NoControl;
            this.KaydetButton.Location = new Point(12, 143);
            this.KaydetButton.Name = "KaydetButton";
            this.KaydetButton.Size = new Size(149, 35);
            this.KaydetButton.TabIndex = 26;
            this.KaydetButton.Text = "Kaydet";
            this.KaydetButton.UseVisualStyleBackColor = false;
            this.KaydetButton.Click += new EventHandler(this.KaydetButton_Click);
            this.button2.BackColor = Color.Tomato;
            this.button2.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Bold, GraphicsUnit.Point, (byte)162);
            this.button2.ImeMode = ImeMode.NoControl;
            this.button2.Location = new Point(167, 143);
            this.button2.Name = "button2";
            this.button2.Size = new Size(143, 35);
            this.button2.TabIndex = 27;
            this.button2.Text = "İptal";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new EventHandler(this.button2_Click);
            this.AutoScaleDimensions = new SizeF(6f, 13f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(321, 182);
            this.Controls.Add((Control)this.button2);
            this.Controls.Add((Control)this.KaydetButton);
            this.Controls.Add((Control)this.label10);
            this.Controls.Add((Control)this.label6);
            this.Controls.Add((Control)this.label1);
            this.Controls.Add((Control)this.DatabaseNameTextBox);
            this.Controls.Add((Control)this.TestButton);
            this.Controls.Add((Control)this.KullaniciTextBox);
            this.Controls.Add((Control)this.SifreTextBox);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Name = "DizinAyarla";
            this.Text = "Dizin Ayarla";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
