// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.Properties.Resources
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MuhammedYuksel.Properties
{
  [CompilerGenerated]
  [DebuggerNonUserCode]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) MuhammedYuksel.Properties.Resources.resourceMan, (object) null))
          MuhammedYuksel.Properties.Resources.resourceMan = new ResourceManager("MuhammedYuksel.Properties.Resources", typeof (MuhammedYuksel.Properties.Resources).Assembly);
        return MuhammedYuksel.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return MuhammedYuksel.Properties.Resources.resourceCulture;
      }
      set
      {
        MuhammedYuksel.Properties.Resources.resourceCulture = value;
      }
    }

    internal static string database
    {
      get
      {
        return MuhammedYuksel.Properties.Resources.ResourceManager.GetString("database", MuhammedYuksel.Properties.Resources.resourceCulture);
      }
    }

    internal static string password
    {
      get
      {
        return MuhammedYuksel.Properties.Resources.ResourceManager.GetString("password", MuhammedYuksel.Properties.Resources.resourceCulture);
      }
    }

    internal static string username
    {
      get
      {
        return MuhammedYuksel.Properties.Resources.ResourceManager.GetString("username", MuhammedYuksel.Properties.Resources.resourceCulture);
      }
    }

    internal Resources()
    {
    }
  }
}
