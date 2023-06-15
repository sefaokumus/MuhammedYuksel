﻿// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.EventScheduler.WeeklySchedule
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System;

namespace MuhammedYuksel.EventScheduler
{
  [Serializable]
  public class WeeklySchedule : Schedule
  {
    public WeeklySchedule(string name, DateTime startTime, GunlukKur gKur)
      : base(name, startTime, ScheduleType.HAFTALIK, gKur)
    {
    }

    internal override void CalculateNextInvokeTime()
    {
      this.m_nextTime = this.m_nextTime.AddDays(7.0);
    }
  }
}
