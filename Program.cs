// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.Program
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using MuhammedYuksel.Properties;
using System;
using System.Windows.Forms;

namespace MuhammedYuksel
{
  internal static class Program
  {
    private static string databaseName = Resources.database;
    private static string databaseUsername = Resources.username;
    private static string password = Resources.password;
    public static UyumSettings settings = new UyumSettings();

    internal static string DatabaseName
    {
      get
      {
        return Program.databaseName;
      }
      set
      {
        Program.databaseName = value;
      }
    }

    internal static string DatabaseUserName
    {
      get
      {
        return Program.databaseUsername;
      }
      set
      {
        Program.databaseUsername = value;
      }
    }

    internal static string Password
    {
      get
      {
        return Program.password;
      }
      set
      {
        Program.password = value;
      }
    }

    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run((Form) new SchedulerUI());
    }

    public static string ConnectionString()
    {
      return "Data Source=" + Program.databaseName + ";User Id=" + Program.databaseUsername + ";Password=" + Program.password + ";";
    }

    public static void program_bilgi_oku()
    {
      try
      {
        Program.settings = Program.settings.Load();
        Program.databaseName = Program.settings.DatabaseName;
        Program.databaseUsername = Program.settings.Username;
        Program.password = Program.settings.Password;
      }
      catch (Exception ex)
      {
      }
    }

    public static void program_bilgi_yaz()
    {
      try
      {
        Program.settings.DatabaseName = Program.databaseName;
        Program.settings.Username = Program.databaseUsername;
        Program.settings.Password = Program.password;
        Program.settings.Save();
      }
      catch (Exception ex)
      {
      }
    }
  }
}
