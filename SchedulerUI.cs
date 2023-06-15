// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.SchedulerUI
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using MuhammedYuksel.EventScheduler;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MuhammedYuksel
{
  public class SchedulerUI : Form
  {
    private static SchedulerUI singletonInstance = (SchedulerUI) null;
    private bool exitFromContextMenuButton = false;
    private ListView SchedulesView;
    private ColumnHeader NameCol;
    private ColumnHeader TypeCol;
    private Button DeleteBtn;
    private Button CloseBtn;
    private Button AddBtn;
    private Label label2;
    private Label label3;
    private DateTimePicker m_startTimePicker;
    private GroupBox groupBox1;
    private CheckBox m_sat;
    private CheckBox m_fri;
    private CheckBox m_thu;
    private CheckBox m_wed;
    private CheckBox m_tue;
    private CheckBox m_mon;
    private CheckBox m_sun;
    private Button UpdateBtn;
    private RadioButton RunWeekly;
    private RadioButton RunDaily;
    private RadioButton RunIntervals;
    private RadioButton RunOnce;
    private RadioButton RunMonthly;
    private GroupBox GroupBox_RunOnlyOn;
    private GroupBox GroupBox_RunBetween;
    private Label SecsLabel;
    private Label IntervalLabel;
    private Label label5;
    private TextBox NameTxt;
    private TextBox IntervalTxt;
    private Button CreateBtn;
    private Label label1;
    private DateTimePicker m_fromTime;
    private DateTimePicker m_toTime;
    private ColumnHeader NextTime;
    private IContainer components;
    private Label label4;
    private ComboBox kullaniciComboBox;
    private ComboBox curFromComboBox;
    private Label label6;
    private ComboBox curToComboBox;
    private Label label7;
    private ComboBox curTypeComboBox;
    private Label label8;
    private GroupBox groupBox2;
    private ColumnHeader StartTime;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem dosyaToolStripMenuItem;
    private ToolStripMenuItem seçeneklerToolStripMenuItem;
    private ToolStripMenuItem ayarlarToolStripMenuItem;
    private ColumnHeader GunlukKur;
    private Button simdiCalistirbutton;
    private OracleConnection connection;
    private OracleCommand command;
    private NotifyIcon notifyIcon1;
    private ContextMenuStrip contextMenuStrip1;
    private ToolStripMenuItem gorevlerPenceresiButton;
    private ToolStripMenuItem ayarlarButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem cikisButton;
    private Label label10;
    private ComboBox curFromAlinacakKurComboBox;
    private Label label9;
    private ComboBox curToAlinacakKurComboBox;
    private OracleDataReader rdr;

    public SchedulerUI()
    {
      this.InitializeComponent();
      Control.CheckForIllegalCrossThreadCalls = false;
      Program.program_bilgi_oku();
      if (Program.settings.Username == null || Program.settings.Username == "")
      {
        Program.settings.ReCreateSettingsFile();
        int num = (int) new DizinAyarla().ShowDialog();
      }
      this.connection = new OracleConnection(Program.ConnectionString());
      DateTimePicker fromTime = this.m_fromTime;
      DateTime dateTime1 = DateTime.Today;
      DateTime dateTime2 = dateTime1.AddSeconds(1.0);
      fromTime.Value = dateTime2;
      DateTimePicker toTime = this.m_toTime;
      dateTime1 = DateTime.Today;
      DateTime dateTime3 = dateTime1.AddMinutes(1439.0);
      toTime.Value = dateTime3;
      Scheduler.OnSchedulerEvent += new SchedulerEventDelegate(this.OnSchedulerEvent);
      DateTimePicker startTimePicker = this.m_startTimePicker;
      dateTime1 = DateTime.Now;
      DateTime dateTime4 = dateTime1.AddHours(1.0);
      startTimePicker.Value = dateTime4;
      this.RunIntervals.Checked = true;
      this.RunOnce.Checked = true;
      this.SchedulesView.Items.Clear();
      for (int index = 0; index < Scheduler.Count(); ++index)
        this.OnSchedulerEvent(SchedulerEventType.CREATED, Scheduler.GetScheduleAt(index).Name);
      this.LoadComboBoxes();
      Scheduler.LoadScheduler();
    }

    private void ayarlarToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new DizinAyarla().ShowDialog();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SchedulerUI));
      this.SchedulesView = new ListView();
      this.NameCol = new ColumnHeader();
      this.TypeCol = new ColumnHeader();
      this.StartTime = new ColumnHeader();
      this.NextTime = new ColumnHeader();
      this.GunlukKur = new ColumnHeader();
      this.DeleteBtn = new Button();
      this.AddBtn = new Button();
      this.CloseBtn = new Button();
      this.label2 = new Label();
      this.SecsLabel = new Label();
      this.IntervalTxt = new TextBox();
      this.IntervalLabel = new Label();
      this.m_sat = new CheckBox();
      this.m_fri = new CheckBox();
      this.m_thu = new CheckBox();
      this.m_wed = new CheckBox();
      this.m_tue = new CheckBox();
      this.m_mon = new CheckBox();
      this.m_sun = new CheckBox();
      this.label3 = new Label();
      this.RunMonthly = new RadioButton();
      this.RunWeekly = new RadioButton();
      this.RunDaily = new RadioButton();
      this.RunIntervals = new RadioButton();
      this.RunOnce = new RadioButton();
      this.m_startTimePicker = new DateTimePicker();
      this.UpdateBtn = new Button();
      this.groupBox1 = new GroupBox();
      this.GroupBox_RunOnlyOn = new GroupBox();
      this.GroupBox_RunBetween = new GroupBox();
      this.label1 = new Label();
      this.m_toTime = new DateTimePicker();
      this.m_fromTime = new DateTimePicker();
      this.label5 = new Label();
      this.NameTxt = new TextBox();
      this.CreateBtn = new Button();
      this.label4 = new Label();
      this.kullaniciComboBox = new ComboBox();
      this.curFromComboBox = new ComboBox();
      this.label6 = new Label();
      this.curToComboBox = new ComboBox();
      this.label7 = new Label();
      this.curTypeComboBox = new ComboBox();
      this.label8 = new Label();
      this.groupBox2 = new GroupBox();
      this.label10 = new Label();
      this.curFromAlinacakKurComboBox = new ComboBox();
      this.label9 = new Label();
      this.curToAlinacakKurComboBox = new ComboBox();
      this.menuStrip1 = new MenuStrip();
      this.dosyaToolStripMenuItem = new ToolStripMenuItem();
      this.seçeneklerToolStripMenuItem = new ToolStripMenuItem();
      this.ayarlarToolStripMenuItem = new ToolStripMenuItem();
      this.simdiCalistirbutton = new Button();
      this.notifyIcon1 = new NotifyIcon(this.components);
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.gorevlerPenceresiButton = new ToolStripMenuItem();
      this.ayarlarButton = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.cikisButton = new ToolStripMenuItem();
      this.groupBox1.SuspendLayout();
      this.GroupBox_RunOnlyOn.SuspendLayout();
      this.GroupBox_RunBetween.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.SchedulesView.Columns.AddRange(new ColumnHeader[5]
      {
        this.NameCol,
        this.TypeCol,
        this.StartTime,
        this.NextTime,
        this.GunlukKur
      });
      this.SchedulesView.FullRowSelect = true;
      this.SchedulesView.GridLines = true;
      this.SchedulesView.Location = new Point(4, 28);
      this.SchedulesView.MultiSelect = false;
      this.SchedulesView.Name = "SchedulesView";
      this.SchedulesView.Size = new Size(522, 275);
      this.SchedulesView.TabIndex = 0;
      this.SchedulesView.UseCompatibleStateImageBehavior = false;
      this.SchedulesView.View = View.Details;
      this.SchedulesView.DoubleClick += new EventHandler(this.OnScheduleDblClk);
      this.NameCol.Text = "İsim";
      this.TypeCol.Text = "Tip";
      this.StartTime.Text = "Başlangıç Zamanı";
      this.StartTime.Width = 107;
      this.NextTime.Text = "Sıradaki çalışacağı zaman";
      this.NextTime.Width = 135;
      this.GunlukKur.Text = "Kur bilgisi";
      this.GunlukKur.Width = 155;
      this.DeleteBtn.Location = new Point(4, 319);
      this.DeleteBtn.Name = "DeleteBtn";
      this.DeleteBtn.Size = new Size(78, 22);
      this.DeleteBtn.TabIndex = 1;
      this.DeleteBtn.Text = "Sil";
      this.DeleteBtn.Click += new EventHandler(this.OnDeleteSchedule);
      this.AddBtn.Location = new Point(20, 14);
      this.AddBtn.Name = "AddBtn";
      this.AddBtn.Size = new Size(80, 22);
      this.AddBtn.TabIndex = 4;
      this.AddBtn.Text = "Add";
      this.CloseBtn.Location = new Point(11, 776);
      this.CloseBtn.Name = "CloseBtn";
      this.CloseBtn.Size = new Size(116, 37);
      this.CloseBtn.TabIndex = 21;
      this.CloseBtn.Text = "Kapat";
      this.CloseBtn.Click += new EventHandler(this.OnCloseClick);
      this.label2.Location = new Point(8, 344);
      this.label2.Name = "label2";
      this.label2.Size = new Size(237, 18);
      this.label2.TabIndex = 6;
      this.label2.Text = "Zamanlanmış görevi düzenlemek için çift tıklayın";
      this.SecsLabel.Location = new Point(345, 487);
      this.SecsLabel.Name = "SecsLabel";
      this.SecsLabel.Size = new Size(46, 14);
      this.SecsLabel.TabIndex = 22;
      this.SecsLabel.Text = "saniye";
      this.IntervalTxt.Location = new Point(291, 484);
      this.IntervalTxt.Name = "IntervalTxt";
      this.IntervalTxt.RightToLeft = RightToLeft.Yes;
      this.IntervalTxt.Size = new Size(48, 20);
      this.IntervalTxt.TabIndex = 11;
      this.IntervalTxt.Text = "60";
      this.IntervalLabel.Location = new Point(252, 487);
      this.IntervalLabel.Name = "IntervalLabel";
      this.IntervalLabel.Size = new Size(48, 14);
      this.IntervalLabel.TabIndex = 20;
      this.IntervalLabel.Text = "Aralık :";
      this.m_sat.Location = new Point(361, 26);
      this.m_sat.Name = "m_sat";
      this.m_sat.Size = new Size(48, 18);
      this.m_sat.TabIndex = 18;
      this.m_sat.Text = "Cts";
      this.m_fri.Checked = true;
      this.m_fri.CheckState = CheckState.Checked;
      this.m_fri.Location = new Point(293, 26);
      this.m_fri.Name = "m_fri";
      this.m_fri.Size = new Size(48, 18);
      this.m_fri.TabIndex = 17;
      this.m_fri.Text = "Cum";
      this.m_thu.Checked = true;
      this.m_thu.CheckState = CheckState.Checked;
      this.m_thu.Location = new Point(225, 26);
      this.m_thu.Name = "m_thu";
      this.m_thu.Size = new Size(48, 18);
      this.m_thu.TabIndex = 16;
      this.m_thu.Text = "Per";
      this.m_wed.Checked = true;
      this.m_wed.CheckState = CheckState.Checked;
      this.m_wed.Location = new Point(157, 26);
      this.m_wed.Name = "m_wed";
      this.m_wed.Size = new Size(48, 18);
      this.m_wed.TabIndex = 15;
      this.m_wed.Text = "Çar";
      this.m_tue.Checked = true;
      this.m_tue.CheckState = CheckState.Checked;
      this.m_tue.Location = new Point(89, 26);
      this.m_tue.Name = "m_tue";
      this.m_tue.Size = new Size(48, 18);
      this.m_tue.TabIndex = 14;
      this.m_tue.Text = "Sal";
      this.m_mon.Checked = true;
      this.m_mon.CheckState = CheckState.Checked;
      this.m_mon.Location = new Point(21, 26);
      this.m_mon.Name = "m_mon";
      this.m_mon.Size = new Size(48, 18);
      this.m_mon.TabIndex = 13;
      this.m_mon.Text = "Pts";
      this.m_sun.Location = new Point(429, 26);
      this.m_sun.Name = "m_sun";
      this.m_sun.Size = new Size(48, 18);
      this.m_sun.TabIndex = 12;
      this.m_sun.Text = "Paz";
      this.label3.Location = new Point(12, 486);
      this.label3.Name = "label3";
      this.label3.Size = new Size(78, 16);
      this.label3.TabIndex = 11;
      this.label3.Text = "Başl. Zamanı :";
      this.RunMonthly.Location = new Point(370, 19);
      this.RunMonthly.Name = "RunMonthly";
      this.RunMonthly.Size = new Size(62, 16);
      this.RunMonthly.TabIndex = 9;
      this.RunMonthly.Text = "Aylık";
      this.RunMonthly.CheckedChanged += new EventHandler(this.OnScheduleTypeChange);
      this.RunWeekly.Location = new Point(256, 20);
      this.RunWeekly.Name = "RunWeekly";
      this.RunWeekly.Size = new Size(75, 16);
      this.RunWeekly.TabIndex = 7;
      this.RunWeekly.Text = "Haftalık";
      this.RunWeekly.CheckedChanged += new EventHandler(this.OnScheduleTypeChange);
      this.RunDaily.Location = new Point(155, 20);
      this.RunDaily.Name = "RunDaily";
      this.RunDaily.Size = new Size(62, 16);
      this.RunDaily.TabIndex = 6;
      this.RunDaily.Text = "Günlük";
      this.RunDaily.CheckedChanged += new EventHandler(this.OnScheduleTypeChange);
      this.RunIntervals.Location = new Point(22, 40);
      this.RunIntervals.Name = "RunIntervals";
      this.RunIntervals.Size = new Size(120, 16);
      this.RunIntervals.TabIndex = 8;
      this.RunIntervals.Text = "Belirlenen Aralıklarla";
      this.RunIntervals.CheckedChanged += new EventHandler(this.OnScheduleTypeChange);
      this.RunOnce.Checked = true;
      this.RunOnce.Location = new Point(22, 20);
      this.RunOnce.Name = "RunOnce";
      this.RunOnce.Size = new Size(94, 16);
      this.RunOnce.TabIndex = 5;
      this.RunOnce.TabStop = true;
      this.RunOnce.Text = "Yanlızca bir kere çalıştır";
      this.RunOnce.CheckedChanged += new EventHandler(this.OnScheduleTypeChange);
      this.m_startTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
      this.m_startTimePicker.Format = DateTimePickerFormat.Custom;
      this.m_startTimePicker.Location = new Point(92, 484);
      this.m_startTimePicker.Name = "m_startTimePicker";
      this.m_startTimePicker.Size = new Size(154, 20);
      this.m_startTimePicker.TabIndex = 10;
      this.m_startTimePicker.Value = new DateTime(2004, 9, 21, 0, 0, 0, 0);
      this.UpdateBtn.Location = new Point(90, 319);
      this.UpdateBtn.Name = "UpdateBtn";
      this.UpdateBtn.Size = new Size(76, 22);
      this.UpdateBtn.TabIndex = 2;
      this.UpdateBtn.Text = "Güncelle";
      this.UpdateBtn.Click += new EventHandler(this.UpdateBtn_Click);
      this.groupBox1.Controls.Add((Control) this.RunMonthly);
      this.groupBox1.Controls.Add((Control) this.RunWeekly);
      this.groupBox1.Controls.Add((Control) this.RunDaily);
      this.groupBox1.Controls.Add((Control) this.RunIntervals);
      this.groupBox1.Controls.Add((Control) this.RunOnce);
      this.groupBox1.Location = new Point(11, 410);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(503, 64);
      this.groupBox1.TabIndex = 4;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Zamanlanmış Görev Tipi";
      this.GroupBox_RunOnlyOn.Controls.Add((Control) this.m_thu);
      this.GroupBox_RunOnlyOn.Controls.Add((Control) this.m_sat);
      this.GroupBox_RunOnlyOn.Controls.Add((Control) this.m_sun);
      this.GroupBox_RunOnlyOn.Controls.Add((Control) this.m_mon);
      this.GroupBox_RunOnlyOn.Controls.Add((Control) this.m_wed);
      this.GroupBox_RunOnlyOn.Controls.Add((Control) this.m_tue);
      this.GroupBox_RunOnlyOn.Controls.Add((Control) this.m_fri);
      this.GroupBox_RunOnlyOn.Location = new Point(12, 515);
      this.GroupBox_RunOnlyOn.Name = "GroupBox_RunOnlyOn";
      this.GroupBox_RunOnlyOn.Size = new Size(502, 50);
      this.GroupBox_RunOnlyOn.TabIndex = 23;
      this.GroupBox_RunOnlyOn.TabStop = false;
      this.GroupBox_RunOnlyOn.Text = "  Sadece seçili günlerde çalışıtır";
      this.GroupBox_RunBetween.Controls.Add((Control) this.label1);
      this.GroupBox_RunBetween.Controls.Add((Control) this.m_toTime);
      this.GroupBox_RunBetween.Controls.Add((Control) this.m_fromTime);
      this.GroupBox_RunBetween.Location = new Point(11, 571);
      this.GroupBox_RunBetween.Name = "GroupBox_RunBetween";
      this.GroupBox_RunBetween.Size = new Size(503, 50);
      this.GroupBox_RunBetween.TabIndex = 24;
      this.GroupBox_RunBetween.TabStop = false;
      this.GroupBox_RunBetween.Text = "  Bu saatler arasında çalışıtır";
      this.label1.Location = new Point(225, 28);
      this.label1.Name = "label1";
      this.label1.Size = new Size(26, 14);
      this.label1.TabIndex = 2;
      this.label1.Text = "ve";
      this.m_toTime.CustomFormat = "hh:mm tt";
      this.m_toTime.Format = DateTimePickerFormat.Custom;
      this.m_toTime.Location = new Point(267, 24);
      this.m_toTime.Name = "m_toTime";
      this.m_toTime.Size = new Size(74, 20);
      this.m_toTime.TabIndex = 1;
      this.m_fromTime.CustomFormat = "hh:mm tt";
      this.m_fromTime.Format = DateTimePickerFormat.Custom;
      this.m_fromTime.Location = new Point(133, 24);
      this.m_fromTime.Name = "m_fromTime";
      this.m_fromTime.Size = new Size(76, 20);
      this.m_fromTime.TabIndex = 0;
      this.label5.Location = new Point(12, 381);
      this.label5.Name = "label5";
      this.label5.Size = new Size(63, 16);
      this.label5.TabIndex = 25;
      this.label5.Text = "Görev Adı :";
      this.NameTxt.Location = new Point(84, 377);
      this.NameTxt.Name = "NameTxt";
      this.NameTxt.Size = new Size(273, 20);
      this.NameTxt.TabIndex = 26;
      this.CreateBtn.Location = new Point(381, 776);
      this.CreateBtn.Name = "CreateBtn";
      this.CreateBtn.Size = new Size(133, 37);
      this.CreateBtn.TabIndex = 27;
      this.CreateBtn.Text = "Görev Ekle";
      this.CreateBtn.Click += new EventHandler(this.OnCreateSchedule);
      this.label4.Location = new Point(15, 23);
      this.label4.Name = "label4";
      this.label4.Size = new Size(118, 18);
      this.label4.TabIndex = 28;
      this.label4.Text = "Kur Ekleyen Kullanıcı :";
      this.label4.TextAlign = ContentAlignment.MiddleRight;
      this.kullaniciComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.kullaniciComboBox.FormattingEnabled = true;
      this.kullaniciComboBox.Location = new Point(137, 22);
      this.kullaniciComboBox.Name = "kullaniciComboBox";
      this.kullaniciComboBox.Size = new Size(121, 21);
      this.kullaniciComboBox.TabIndex = 29;
      this.curFromComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.curFromComboBox.FormattingEnabled = true;
      this.curFromComboBox.Location = new Point(137, 49);
      this.curFromComboBox.Name = "curFromComboBox";
      this.curFromComboBox.Size = new Size(121, 21);
      this.curFromComboBox.TabIndex = 31;
      this.label6.Location = new Point(15, 52);
      this.label6.Name = "label6";
      this.label6.Size = new Size(118, 18);
      this.label6.TabIndex = 30;
      this.label6.Text = "Dönüştürülecek Kur :";
      this.label6.TextAlign = ContentAlignment.MiddleRight;
      this.curToComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.curToComboBox.FormattingEnabled = true;
      this.curToComboBox.Location = new Point(137, 78);
      this.curToComboBox.Name = "curToComboBox";
      this.curToComboBox.Size = new Size(121, 21);
      this.curToComboBox.TabIndex = 33;
      this.label7.Location = new Point(15, 79);
      this.label7.Name = "label7";
      this.label7.Size = new Size(118, 18);
      this.label7.TabIndex = 32;
      this.label7.Text = "Dönüşecek Kur :";
      this.label7.TextAlign = ContentAlignment.MiddleRight;
      this.curTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.curTypeComboBox.FormattingEnabled = true;
      this.curTypeComboBox.Location = new Point(137, 108);
      this.curTypeComboBox.Name = "curTypeComboBox";
      this.curTypeComboBox.Size = new Size(121, 21);
      this.curTypeComboBox.TabIndex = 35;
      this.label8.Location = new Point(15, 109);
      this.label8.Name = "label8";
      this.label8.Size = new Size(118, 18);
      this.label8.TabIndex = 34;
      this.label8.Text = "Eklenecek Kur Tipi :";
      this.label8.TextAlign = ContentAlignment.MiddleRight;
      this.groupBox2.Controls.Add((Control) this.label10);
      this.groupBox2.Controls.Add((Control) this.curFromAlinacakKurComboBox);
      this.groupBox2.Controls.Add((Control) this.label9);
      this.groupBox2.Controls.Add((Control) this.curToAlinacakKurComboBox);
      this.groupBox2.Controls.Add((Control) this.label4);
      this.groupBox2.Controls.Add((Control) this.curTypeComboBox);
      this.groupBox2.Controls.Add((Control) this.kullaniciComboBox);
      this.groupBox2.Controls.Add((Control) this.label8);
      this.groupBox2.Controls.Add((Control) this.label6);
      this.groupBox2.Controls.Add((Control) this.curToComboBox);
      this.groupBox2.Controls.Add((Control) this.curFromComboBox);
      this.groupBox2.Controls.Add((Control) this.label7);
      this.groupBox2.Location = new Point(4, 627);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(510, 143);
      this.groupBox2.TabIndex = 36;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Kur Satır Bilgileri";
      this.label10.Location = new Point(264, 82);
      this.label10.Name = "label10";
      this.label10.Size = new Size(134, 18);
      this.label10.TabIndex = 39;
      this.label10.Text = "M. Bankasi Alınacak Kur :";
      this.label10.TextAlign = ContentAlignment.MiddleRight;
      this.curFromAlinacakKurComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.curFromAlinacakKurComboBox.FormattingEnabled = true;
      this.curFromAlinacakKurComboBox.Items.AddRange(new object[4]
      {
        (object) "Döviz Alış",
        (object) "Döviz Satış",
        (object) "Efektif Alış",
        (object) "Efektif Satış"
      });
      this.curFromAlinacakKurComboBox.Location = new Point(404, 49);
      this.curFromAlinacakKurComboBox.Name = "curFromAlinacakKurComboBox";
      this.curFromAlinacakKurComboBox.Size = new Size(100, 21);
      this.curFromAlinacakKurComboBox.TabIndex = 38;
      this.label9.Location = new Point(264, 52);
      this.label9.Name = "label9";
      this.label9.Size = new Size(134, 18);
      this.label9.TabIndex = 37;
      this.label9.Text = "M. Bankasi Alınacak Kur :";
      this.label9.TextAlign = ContentAlignment.MiddleRight;
      this.curToAlinacakKurComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
      this.curToAlinacakKurComboBox.FormattingEnabled = true;
      this.curToAlinacakKurComboBox.Items.AddRange(new object[4]
      {
        (object) "Döviz Alış",
        (object) "Döviz Satış",
        (object) "Efektif Alış",
        (object) "Efektif Satış"
      });
      this.curToAlinacakKurComboBox.Location = new Point(404, 76);
      this.curToAlinacakKurComboBox.Name = "curToAlinacakKurComboBox";
      this.curToAlinacakKurComboBox.Size = new Size(100, 21);
      this.curToAlinacakKurComboBox.TabIndex = 36;
      this.menuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.dosyaToolStripMenuItem,
        (ToolStripItem) this.seçeneklerToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(526, 24);
      this.menuStrip1.TabIndex = 38;
      this.menuStrip1.Text = "menuStrip1";
      this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
      this.dosyaToolStripMenuItem.Size = new Size(54, 20);
      this.dosyaToolStripMenuItem.Text = "Dosya ";
      this.seçeneklerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
      {
        (ToolStripItem) this.ayarlarToolStripMenuItem
      });
      this.seçeneklerToolStripMenuItem.Name = "seçeneklerToolStripMenuItem";
      this.seçeneklerToolStripMenuItem.Size = new Size(75, 20);
      this.seçeneklerToolStripMenuItem.Text = "Seçenekler";
      this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
      this.ayarlarToolStripMenuItem.Size = new Size(111, 22);
      this.ayarlarToolStripMenuItem.Text = "Ayarlar";
      this.ayarlarToolStripMenuItem.Click += new EventHandler(this.ayarlarToolStripMenuItem_Click);
      this.simdiCalistirbutton.Location = new Point(172, 319);
      this.simdiCalistirbutton.Name = "simdiCalistirbutton";
      this.simdiCalistirbutton.Size = new Size(128, 22);
      this.simdiCalistirbutton.TabIndex = 39;
      this.simdiCalistirbutton.Text = "Seçileni şimdi çalıştır";
      this.simdiCalistirbutton.Click += new EventHandler(this.button2_Click);
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      this.notifyIcon1.Icon = (Icon) componentResourceManager.GetObject("notifyIcon1.Icon");
      this.notifyIcon1.Text = "Zamanlanmış Görevler";
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.MouseDoubleClick += new MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.gorevlerPenceresiButton,
        (ToolStripItem) this.ayarlarButton,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.cikisButton
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(172, 76);
      this.gorevlerPenceresiButton.Name = "gorevlerPenceresiButton";
      this.gorevlerPenceresiButton.Size = new Size(171, 22);
      this.gorevlerPenceresiButton.Text = "Görevler Penceresi";
      this.gorevlerPenceresiButton.Click += new EventHandler(this.gorevlerPenceresiButton_Click);
      this.ayarlarButton.Name = "ayarlarButton";
      this.ayarlarButton.Size = new Size(171, 22);
      this.ayarlarButton.Text = "Database Ayarları";
      this.ayarlarButton.Click += new EventHandler(this.ayarlarButton_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(168, 6);
      this.cikisButton.Name = "cikisButton";
      this.cikisButton.Size = new Size(171, 22);
      this.cikisButton.Text = "Çıkış";
      this.cikisButton.Click += new EventHandler(this.cikisButton_Click);
      this.AutoScaleBaseSize = new Size(5, 13);
      this.ClientSize = new Size(526, 822);
      this.Controls.Add((Control) this.simdiCalistirbutton);
      this.Controls.Add((Control) this.menuStrip1);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.CreateBtn);
      this.Controls.Add((Control) this.NameTxt);
      this.Controls.Add((Control) this.IntervalTxt);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.GroupBox_RunBetween);
      this.Controls.Add((Control) this.GroupBox_RunOnlyOn);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.DeleteBtn);
      this.Controls.Add((Control) this.SchedulesView);
      this.Controls.Add((Control) this.CloseBtn);
      this.Controls.Add((Control) this.UpdateBtn);
      this.Controls.Add((Control) this.SecsLabel);
      this.Controls.Add((Control) this.IntervalLabel);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.m_startTimePicker);
      this.FormBorderStyle = FormBorderStyle.FixedSingle;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SchedulerUI";
      this.SizeGripStyle = SizeGripStyle.Hide;
      this.Text = "Zamanlanmış Görevler";
      this.Closed += new EventHandler(this.OnClosed);
      this.FormClosing += new FormClosingEventHandler(this.SchedulerUI_FormClosing);
      this.groupBox1.ResumeLayout(false);
      this.GroupBox_RunOnlyOn.ResumeLayout(false);
      this.GroupBox_RunBetween.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private void LoadComboBoxes()
    {
      List<KurItem> kurItemList1 = new List<KurItem>();
      List<KurItem> kurItemList2 = new List<KurItem>();
      List<KurItem> kurItemList3 = new List<KurItem>();
      List<KurItem> kurItemList4 = new List<KurItem>();
      try
      {
        this.connection.Open();
        this.command = new OracleCommand("select US_ID, US_USERNAME FROM users", this.connection);
        this.rdr = this.command.ExecuteReader();
        while (this.rdr.Read())
          kurItemList1.Add(new KurItem(Convert.ToInt32(this.rdr["US_ID"]), this.rdr["US_USERNAME"].ToString()));
        this.kullaniciComboBox.DataSource = (object) kurItemList1;
        this.kullaniciComboBox.ValueMember = "CUR_ID";
        this.kullaniciComboBox.DisplayMember = "CUR_CODE";
        this.rdr.Close();
        this.command = new OracleCommand("select CUR_ID,CUR_CODE FROM GNLD_CURRENCY", this.connection);
        this.rdr = this.command.ExecuteReader();
        while (this.rdr.Read())
        {
          kurItemList2.Add(new KurItem(Convert.ToInt32(this.rdr["CUR_ID"]), this.rdr["CUR_CODE"].ToString()));
          kurItemList3.Add(new KurItem(Convert.ToInt32(this.rdr["CUR_ID"]), this.rdr["CUR_CODE"].ToString()));
        }
        this.curFromComboBox.DataSource = (object) kurItemList2;
        this.curFromComboBox.ValueMember = "CUR_ID";
        this.curFromComboBox.DisplayMember = "CUR_CODE";
        this.curToComboBox.DataSource = (object) kurItemList3;
        this.curToComboBox.ValueMember = "CUR_ID";
        this.curToComboBox.DisplayMember = "CUR_CODE";
        this.rdr.Close();
        this.command = new OracleCommand("select CUR_RATE_TYPE_ID, CUR_RATE_TYPE_CODE FROM GNLD_CUR_RATE_TYPE", this.connection);
        this.rdr = this.command.ExecuteReader();
        while (this.rdr.Read())
          kurItemList4.Add(new KurItem(Convert.ToInt32(this.rdr["CUR_RATE_TYPE_ID"]), this.rdr["CUR_RATE_TYPE_CODE"].ToString()));
        this.curTypeComboBox.DataSource = (object) kurItemList4;
        this.curTypeComboBox.ValueMember = "CUR_ID";
        this.curTypeComboBox.DisplayMember = "CUR_CODE";
        this.rdr.Close();
        this.curFromAlinacakKurComboBox.SelectedIndex = 0;
        this.curToAlinacakKurComboBox.SelectedIndex = 0;
      }
      catch
      {
      }
      finally
      {
        this.connection.Close();
      }
    }

    private void OnCloseClick(object sender, EventArgs e)
    {
      this.Close();
    }

    private void OnScheduleTypeChange(object sender, EventArgs e)
    {
      this.GroupBox_RunOnlyOn.Enabled = this.GroupBox_RunBetween.Enabled = this.IntervalLabel.Enabled = this.IntervalTxt.Enabled = this.SecsLabel.Enabled = false;
      if (sender == this.RunDaily || sender == this.RunIntervals)
        this.GroupBox_RunOnlyOn.Enabled = ((RadioButton) sender).Checked;
      if (sender != this.RunIntervals)
        return;
      this.GroupBox_RunBetween.Enabled = ((RadioButton) sender).Checked;
      this.IntervalLabel.Enabled = this.IntervalTxt.Enabled = this.SecsLabel.Enabled = true;
    }

    private void OnCreateSchedule(object sender, EventArgs e)
    {
      try
      {
        Schedule schedule = (Schedule) null;
        if (this.RunOnce.Checked)
          schedule = (Schedule) new OneTimeSchedule(this.NameTxt.Text, this.m_startTimePicker.Value, new MuhammedYuksel.GunlukKur((int) this.kullaniciComboBox.SelectedValue, (KurItem) this.curFromComboBox.SelectedItem, (KurItem) this.curToComboBox.SelectedItem, (KurItem) this.curTypeComboBox.SelectedItem, (string) this.curFromAlinacakKurComboBox.SelectedItem, (string) this.curToAlinacakKurComboBox.SelectedItem));
        if (this.RunDaily.Checked)
          schedule = (Schedule) new DailySchedule(this.NameTxt.Text, this.m_startTimePicker.Value, new MuhammedYuksel.GunlukKur((int) this.kullaniciComboBox.SelectedValue, (KurItem) this.curFromComboBox.SelectedItem, (KurItem) this.curToComboBox.SelectedItem, (KurItem) this.curTypeComboBox.SelectedItem, (string) this.curFromAlinacakKurComboBox.SelectedItem, (string) this.curToAlinacakKurComboBox.SelectedItem));
        if (this.RunWeekly.Checked)
          schedule = (Schedule) new WeeklySchedule(this.NameTxt.Text, this.m_startTimePicker.Value, new MuhammedYuksel.GunlukKur((int) this.kullaniciComboBox.SelectedValue, (KurItem) this.curFromComboBox.SelectedItem, (KurItem) this.curToComboBox.SelectedItem, (KurItem) this.curTypeComboBox.SelectedItem, (string) this.curFromAlinacakKurComboBox.SelectedItem, (string) this.curToAlinacakKurComboBox.SelectedItem));
        if (this.RunMonthly.Checked)
          schedule = (Schedule) new MonthlySchedule(this.NameTxt.Text, this.m_startTimePicker.Value, new MuhammedYuksel.GunlukKur((int) this.kullaniciComboBox.SelectedValue, (KurItem) this.curFromComboBox.SelectedItem, (KurItem) this.curToComboBox.SelectedItem, (KurItem) this.curTypeComboBox.SelectedItem, (string) this.curFromAlinacakKurComboBox.SelectedItem, (string) this.curToAlinacakKurComboBox.SelectedItem));
        if (this.RunIntervals.Checked)
        {
          string text = this.NameTxt.Text;
          DateTime startTime = this.m_startTimePicker.Value;
          int secs = int.Parse(this.IntervalTxt.Text);
          DateTime dateTime = this.m_fromTime.Value;
          TimeSpan timeOfDay1 = dateTime.TimeOfDay;
          dateTime = this.m_toTime.Value;
          TimeSpan timeOfDay2 = dateTime.TimeOfDay;
          MuhammedYuksel.GunlukKur gKur = new MuhammedYuksel.GunlukKur((int) this.kullaniciComboBox.SelectedValue, (KurItem) this.curFromComboBox.SelectedItem, (KurItem) this.curToComboBox.SelectedItem, (KurItem) this.curTypeComboBox.SelectedItem, (string) this.curFromAlinacakKurComboBox.SelectedItem, (string) this.curToAlinacakKurComboBox.SelectedItem);
          schedule = (Schedule) new IntervalSchedule(text, startTime, secs, timeOfDay1, timeOfDay2, gKur);
        }
        this.SetScheduleWeekDays(schedule);
        Scheduler.AddSchedule(schedule);
        Scheduler.SaveScheduler();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
    }

    private void OnClosed(object sender, EventArgs e)
    {
      SchedulerUI.singletonInstance = (SchedulerUI) null;
    }

    private void SetScheduleWeekDays(Schedule sc)
    {
      sc.SetWeekDay(DayOfWeek.Sunday, this.m_sun.Checked);
      sc.SetWeekDay(DayOfWeek.Monday, this.m_mon.Checked);
      sc.SetWeekDay(DayOfWeek.Tuesday, this.m_tue.Checked);
      sc.SetWeekDay(DayOfWeek.Wednesday, this.m_wed.Checked);
      sc.SetWeekDay(DayOfWeek.Thursday, this.m_thu.Checked);
      sc.SetWeekDay(DayOfWeek.Friday, this.m_fri.Checked);
      sc.SetWeekDay(DayOfWeek.Saturday, this.m_sat.Checked);
    }

    private void GetScheduleWeekDays(Schedule sc)
    {
      this.m_sun.Checked = sc.WeekDayActive(DayOfWeek.Sunday);
      this.m_mon.Checked = sc.WeekDayActive(DayOfWeek.Monday);
      this.m_tue.Checked = sc.WeekDayActive(DayOfWeek.Tuesday);
      this.m_wed.Checked = sc.WeekDayActive(DayOfWeek.Wednesday);
      this.m_thu.Checked = sc.WeekDayActive(DayOfWeek.Thursday);
      this.m_fri.Checked = sc.WeekDayActive(DayOfWeek.Friday);
      this.m_sat.Checked = sc.WeekDayActive(DayOfWeek.Saturday);
    }

    private void OnScheduleDblClk(object sender, EventArgs e)
    {
      Schedule schedule = Scheduler.GetSchedule(this.SchedulesView.SelectedItems[0].Text);
      if (schedule == null)
      {
        int num = (int) MessageBox.Show("Görev Bulunamadı !");
      }
      else
      {
        this.NameTxt.Text = schedule.Name;
        this.m_startTimePicker.Value = schedule.NextInvokeTime;
        this.GetScheduleWeekDays(schedule);
        this.IntervalTxt.Text = schedule.Interval.ToString();
        switch (schedule.Type)
        {
          case ScheduleType.TEKSEFER:
            this.RunOnce.Checked = true;
            break;
          case ScheduleType.ARALIKLI:
            this.RunIntervals.Checked = true;
            break;
          case ScheduleType.GUNLUK:
            this.RunDaily.Checked = true;
            break;
          case ScheduleType.HAFTALIK:
            this.RunWeekly.Checked = true;
            break;
          case ScheduleType.AYLIK:
            this.RunMonthly.Checked = true;
            break;
        }
        this.kullaniciComboBox.SelectedIndex = Engine.FindIndexOf(schedule.GunlukKur.userID, this.kullaniciComboBox);
        this.curFromComboBox.SelectedIndex = Engine.FindIndexOf(schedule.GunlukKur.curFrom.CUR_ID, this.curFromComboBox);
        this.curToComboBox.SelectedIndex = Engine.FindIndexOf(schedule.GunlukKur.curTo.CUR_ID, this.curToComboBox);
        this.curTypeComboBox.SelectedIndex = Engine.FindIndexOf(schedule.GunlukKur.curType.CUR_ID, this.curTypeComboBox);
        this.curFromAlinacakKurComboBox.SelectedIndex = Engine.FindIndexOf(schedule.GunlukKur.curFromAlinacakKur, this.curFromAlinacakKurComboBox);
        this.curToAlinacakKurComboBox.SelectedIndex = Engine.FindIndexOf(schedule.GunlukKur.curToAlinacakKur, this.curToAlinacakKurComboBox);
      }
    }

    public void OnSchedulerEvent(SchedulerEventType type, string scheduleName)
    {
      try
      {
        switch (type)
        {
          case SchedulerEventType.CREATED:
            ListViewItem listViewItem = this.SchedulesView.Items.Add(scheduleName);
            Schedule schedule1 = Scheduler.GetSchedule(scheduleName);
            listViewItem.SubItems.Add(schedule1.Type.ToString());
            listViewItem.SubItems.Add(schedule1.StartTime.ToString("dd/MM/yyyy HH:mm:ss"));
            listViewItem.SubItems.Add(schedule1.NextInvokeTime.ToString("dd/MM/yyyy HH:mm:ss"));
            listViewItem.SubItems.Add(schedule1.GunlukKur.ToString());
            break;
          case SchedulerEventType.DELETED:
            for (int index = 0; index < this.SchedulesView.Items.Count; ++index)
            {
              if (this.SchedulesView.Items[index].Text == scheduleName)
                this.SchedulesView.Items.RemoveAt(index);
            }
            break;
          case SchedulerEventType.INVOKED:
            for (int index = 0; index < this.SchedulesView.Items.Count; ++index)
            {
              if (this.SchedulesView.Items[index].Text == scheduleName)
              {
                Schedule schedule2 = Scheduler.GetSchedule(scheduleName);
                this.SchedulesView.Items[index].SubItems[3].Text = schedule2.NextInvokeTime.ToString("dd/MM/yyyy HH:mm:ss");
              }
            }
            break;
        }
        this.SchedulesView.Refresh();
      }
      catch
      {
      }
    }

    private void OnDeleteSchedule(object sender, EventArgs e)
    {
      if (this.SchedulesView.SelectedItems.Count > 0)
      {
        Schedule schedule = Scheduler.GetSchedule(this.SchedulesView.SelectedItems[0].Text);
        if (schedule == null)
        {
          int num = (int) MessageBox.Show("Silmek için önce bir görev seçin");
          return;
        }
        Scheduler.RemoveSchedule(schedule);
      }
      Scheduler.SaveScheduler();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.SchedulesView.SelectedItems.Count <= 0)
        return;
      Schedule schedule = Scheduler.GetSchedule(this.SchedulesView.SelectedItems[0].Text);
      schedule.WriteGunlukKurToDB(schedule.GunlukKur);
    }

    private void SchedulerUI_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (!this.exitFromContextMenuButton)
      {
        e.Cancel = true;
        this.notifyIcon1.BalloonTipTitle = "Zamanlanmış Görevler";
        this.notifyIcon1.BalloonTipText = "Program hala çalışıyor.";
        this.notifyIcon1.Visible = true;
        this.notifyIcon1.ShowBalloonTip(500);
        this.Hide();
      }
      else
        Scheduler.SaveScheduler();
    }

    private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      this.Show();
      this.WindowState = FormWindowState.Normal;
    }

    private void gorevlerPenceresiButton_Click(object sender, EventArgs e)
    {
      this.Show();
      this.WindowState = FormWindowState.Normal;
    }

    private void cikisButton_Click(object sender, EventArgs e)
    {
      this.exitFromContextMenuButton = true;
      Application.Exit();
    }

    private void ayarlarButton_Click(object sender, EventArgs e)
    {
      int num = (int) new DizinAyarla().ShowDialog();
    }

    private void UpdateBtn_Click(object sender, EventArgs e)
    {
      Scheduler.SaveScheduler();
    }
  }
}
