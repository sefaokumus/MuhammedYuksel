// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.EventScheduler.Schedule
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System;
using System.Collections.Generic;

namespace MuhammedYuksel.EventScheduler
{
  [Serializable]
  public abstract class Schedule : IComparable
  {
    private bool[] m_workingWeekDays = new bool[7]
    {
      true,
      true,
      true,
      true,
      true,
      true,
      true
    };
    private long m_interval = 0;
    protected string m_name;
    protected ScheduleType m_type;
    protected bool m_active;
    protected DateTime m_startTime;
    protected DateTime m_stopTime;
    protected DateTime m_nextTime;
    protected TimeSpan m_fromTime;
    protected TimeSpan m_toTime;
    protected GunlukKur gunlukKur;

    public ScheduleType Type
    {
      get
      {
        return this.m_type;
      }
    }

    public GunlukKur GunlukKur
    {
      get
      {
        return this.gunlukKur;
      }
      set
      {
        this.gunlukKur = value;
      }
    }

    public string Name
    {
      get
      {
        return this.m_name;
      }
    }

    public bool Active
    {
      get
      {
        return this.m_active;
      }
      set
      {
        this.m_active = value;
      }
    }

    public DateTime NextInvokeTime
    {
      get
      {
        return this.m_nextTime;
      }
      set
      {
        this.m_nextTime = value;
      }
    }

    public DateTime StartTime
    {
      get
      {
        return this.m_startTime;
      }
      set
      {
        if (value.CompareTo(DateTime.Now) <= 0)
          throw new SchedulerException("Başlangıç zamanı ileriki bir tarih olmalı");
        this.m_startTime = value;
      }
    }

    public DateTime StartTimeProgrammatically
    {
      get
      {
        return this.m_startTime;
      }
      set
      {
        this.m_startTime = value;
      }
    }

    public TimeSpan FromTime
    {
      get
      {
        return this.m_fromTime;
      }
      set
      {
        this.m_fromTime = value;
      }
    }

    public TimeSpan ToTime
    {
      get
      {
        return this.m_toTime;
      }
      set
      {
        this.m_toTime = value;
      }
    }

    public long Interval
    {
      get
      {
        return this.m_interval;
      }
      set
      {
        if (value < 30L)
          throw new SchedulerException("Tekrarlanacak olan aralık 60 sn den az olmamaz.");
        this.m_interval = value;
      }
    }

    public event Invoke OnTrigger;

    public Schedule(string name, DateTime startTime, ScheduleType type, GunlukKur gkur)
    {
      this.m_startTime = startTime;
      this.m_nextTime = startTime;
      this.m_type = type;
      this.m_name = name;
      this.gunlukKur = gkur;
    }

    protected bool NoFreeWeekDay()
    {
      bool flag = false;
      for (int index = 0; index < 7; ++index)
        flag |= this.m_workingWeekDays[index];
      return flag;
    }

    public void SetWeekDay(DayOfWeek day, bool On)
    {
      this.m_workingWeekDays[(int) day] = On;
      this.Active = true;
      if (!this.NoFreeWeekDay())
        return;
      this.Active = false;
    }

    public bool WeekDayActive(DayOfWeek day)
    {
      return this.m_workingWeekDays[(int) day];
    }

    public void TriggerEvents()
    {
      this.CalculateNextInvokeTime();
      if (this.OnTrigger != null)
        this.OnTrigger(this.Name);
      if (this.gunlukKur == null)
        return;
      this.WriteGunlukKurToDB(this.gunlukKur);
    }

    private void KickOffEvents()
    {
      if (this.OnTrigger != null)
        this.OnTrigger(this.Name);
      if (this.gunlukKur == null)
        return;
      this.WriteGunlukKurToDB(this.gunlukKur);
    }

    public void WriteGunlukKurToDB(GunlukKur gunlukKur)
    {
      string previousDayXmlPath = Engine.GetPreviousDayXMLPath(DateTime.Now);
      if (Engine.GunlukKurExists(gunlukKur))
        return;
      if (previousDayXmlPath != "data yok")
      {
        List<KurItem> xml = Engine.ParseXML(previousDayXmlPath);
        Engine.InsertNewGunlukKur(gunlukKur, xml);
      }
      else
        Engine.InsertKurFromPreviousDay(gunlukKur, DateTime.Now);
    }

    internal abstract void CalculateNextInvokeTime();

    protected bool CanInvokeOnNextWeekDay()
    {
      return this.m_workingWeekDays[(int) this.m_nextTime.DayOfWeek];
    }

    protected bool IsInvokeTimeInTimeRange()
    {
      if (this.m_fromTime < this.m_toTime)
        return this.m_nextTime.TimeOfDay > this.m_fromTime && this.m_nextTime.TimeOfDay < this.m_toTime;
      return this.m_nextTime.TimeOfDay > this.m_toTime && this.m_nextTime.TimeOfDay < this.m_fromTime;
    }

    public int CompareTo(object obj)
    {
      if (obj is Schedule)
        return this.m_nextTime.CompareTo(((Schedule) obj).m_nextTime);
      throw new Exception("Not a Schedule object");
    }
  }
}
