// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.EventScheduler.IntervalSchedule
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System;

namespace MuhammedYuksel.EventScheduler
{
  [Serializable]
  public class IntervalSchedule : Schedule
  {
    public IntervalSchedule(string name, DateTime startTime, int secs, TimeSpan fromTime, TimeSpan toTime, GunlukKur gKur)
      : base(name, startTime, ScheduleType.ARALIKLI, gKur)
    {
      this.m_fromTime = fromTime;
      this.m_toTime = toTime;
      this.Interval = (long) secs;
    }

    internal override void CalculateNextInvokeTime()
    {
      this.m_nextTime = this.m_nextTime.AddSeconds((double) this.Interval);
      if (!this.IsInvokeTimeInTimeRange())
      {
        if (this.m_nextTime.TimeOfDay < this.m_fromTime)
          this.m_nextTime.AddSeconds((double) (this.m_fromTime.Seconds - this.m_nextTime.TimeOfDay.Seconds));
        else
          this.m_nextTime.AddSeconds((double) (86400 - this.m_nextTime.TimeOfDay.Seconds + this.m_fromTime.Seconds));
      }
      while (!this.CanInvokeOnNextWeekDay())
        this.m_nextTime = this.m_nextTime.AddDays(1.0);
    }
  }
}
