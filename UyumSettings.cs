// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.UyumSettings
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MuhammedYuksel
{
  [Serializable]
  internal class UyumSettings
  {
    private string databaseName;
    private string password;
    private string username;

    public string AssemblyFolder
    {
      get
      {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MuhammedYuksel\\";
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);
        return path;
      }
    }

    public string DatabaseName
    {
      get
      {
        return this.databaseName;
      }
      set
      {
        this.databaseName = value;
      }
    }

    public string Password
    {
      get
      {
        return this.password;
      }
      set
      {
        this.password = value;
      }
    }

    public string Username
    {
      get
      {
        return this.username;
      }
      set
      {
        this.username = value;
      }
    }

    public UyumSettings Load()
    {
      object obj = (object) null;
      string path = this.AssemblyFolder + "uyumSettings.dat";
      if (!File.Exists(path))
        return new UyumSettings();
      try
      {
        Stream serializationStream = (Stream) new FileStream(path, FileMode.Open);
        obj = new BinaryFormatter()
        {
          Binder = ((SerializationBinder) new CatalogueBinder())
        }.Deserialize(serializationStream);
        serializationStream.Close();
      }
      catch (Exception ex)
      {
        File.Delete(path);
        this.Load();
      }
      return obj as UyumSettings;
    }

    public void ReCreateSettingsFile()
    {
      File.Delete(this.AssemblyFolder + "uyumSettings.dat");
      this.Load();
    }

    public void Save()
    {
      Stream serializationStream = (Stream) new FileStream(this.AssemblyFolder + "uyumSettings.dat", FileMode.Create);
      new BinaryFormatter().Serialize(serializationStream, (object) this);
      serializationStream.Close();
    }
  }
}
