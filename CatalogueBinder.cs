// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.CatalogueBinder
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using MuhammedYuksel.EventScheduler;
using System;
using System.Runtime.Serialization;

namespace MuhammedYuksel
{
  public class CatalogueBinder : SerializationBinder
  {
    public override Type BindToType(string assemblyName, string typeName)
    {
      string[] strArray = typeName.Split('.');
      string str = strArray[strArray.Length - 1];
      if (str.Equals("UyumSettings"))
        return typeof (UyumSettings);
      if (str.Equals("Schedule"))
        return typeof (Schedule);
      if (str.Equals("ScheduleType"))
        return typeof (ScheduleType);
      if (str.Equals("OneTimeSchedule"))
        return typeof (OneTimeSchedule);
      if (str.Equals("IntervalSchedule"))
        return typeof (IntervalSchedule);
      if (str.Equals("DailySchedule"))
        return typeof (DailySchedule);
      if (str.Equals("WeeklySchedule"))
        return typeof (WeeklySchedule);
      if (str.Equals("MonthlySchedule"))
        return typeof (MonthlySchedule);
      return Type.GetType(string.Format("{0}, {1}", (object) typeName, (object) assemblyName));
    }
  }
}
