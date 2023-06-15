// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.EventScheduler.Scheduler
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace MuhammedYuksel.EventScheduler
{
  public sealed class Scheduler
  {
    private static Schedule m_nextSchedule = (Schedule) null;
    private static List<Schedule> m_schedulesList = new List<Schedule>();
    private static Timer m_timer = new Timer(new TimerCallback(Scheduler.TimerCallback), (object) null, 0, 5000);
    private static bool callFinished = true;

    public static List<Schedule> ScheduleList
    {
      get
      {
        return Scheduler.m_schedulesList;
      }
      set
      {
        Scheduler.m_schedulesList = value;
      }
    }

    public static event SchedulerEventDelegate OnSchedulerEvent;

    public static Schedule GetScheduleAt(int index)
    {
      if (index < 0 || index >= Scheduler.m_schedulesList.Count)
        return (Schedule) null;
      return Scheduler.m_schedulesList[index];
    }

    public static int Count()
    {
      return Scheduler.m_schedulesList.Count;
    }

    public static Schedule GetSchedule(string scheduleName)
    {
      for (int index = 0; index < Scheduler.m_schedulesList.Count; ++index)
      {
        if (Scheduler.m_schedulesList[index].Name == scheduleName)
          return Scheduler.m_schedulesList[index];
      }
      return (Schedule) null;
    }

    private static void TimerCallback(object o)
    {
      if (Scheduler.m_nextSchedule == null || !Scheduler.callFinished)
        return;
      Scheduler.callFinished = false;
      Scheduler.m_nextSchedule.TriggerEvents();
      Scheduler.callFinished = true;
      if (Scheduler.m_nextSchedule.Type == ScheduleType.TEKSEFER)
      {
        Scheduler.RemoveSchedule(Scheduler.m_nextSchedule);
      }
      else
      {
        if (Scheduler.OnSchedulerEvent != null)
          Scheduler.OnSchedulerEvent(SchedulerEventType.INVOKED, Scheduler.m_nextSchedule.Name);
        Scheduler.m_schedulesList.Sort();
        Scheduler.SetNextEventTime();
        GC.Collect();
      }
    }

    private static void SetNextEventTime()
    {
      if (Scheduler.m_schedulesList.Count == 0)
      {
        Scheduler.m_timer.Change(-1, -1);
      }
      else
      {
        Scheduler.m_nextSchedule = Scheduler.m_schedulesList[0];
        TimeSpan timeSpan = Scheduler.m_nextSchedule.NextInvokeTime.Subtract(DateTime.Now);
        if (timeSpan < TimeSpan.Zero)
          timeSpan = TimeSpan.Zero;
        Scheduler.m_timer.Change((int) timeSpan.TotalMilliseconds, 2000);
      }
    }

    public static void AddSchedule(Schedule s)
    {
      if (Scheduler.GetSchedule(s.Name) != null)
        throw new SchedulerException("Schedule with the same name already exists");
      Scheduler.m_schedulesList.Add(s);
      Scheduler.m_schedulesList.Sort();
      if (Scheduler.m_schedulesList[0] == s)
        Scheduler.SetNextEventTime();
      if (Scheduler.OnSchedulerEvent == null)
        return;
      Scheduler.OnSchedulerEvent(SchedulerEventType.CREATED, s.Name);
    }

    public static void RemoveSchedule(Schedule s)
    {
      Scheduler.m_schedulesList.Remove(s);
      Scheduler.SetNextEventTime();
      if (Scheduler.OnSchedulerEvent == null)
        return;
      Scheduler.OnSchedulerEvent(SchedulerEventType.DELETED, s.Name);
    }

    public static void RemoveSchedule(string name)
    {
      Scheduler.RemoveSchedule(Scheduler.GetSchedule(name));
    }

    public static bool SaveScheduler()
    {
      bool flag = false;
      try
      {
        Stream serializationStream = (Stream) new FileStream("kuralListesi.dat", FileMode.Create);
        IFormatter formatter = (IFormatter) new BinaryFormatter();
        formatter.Binder = (SerializationBinder) new CatalogueBinder();
        formatter.Serialize(serializationStream, (object) Scheduler.ScheduleList);
        serializationStream.Close();
        flag = true;
      }
      catch
      {
      }
      return flag;
    }

    public static void LoadScheduler()
    {
      List<Schedule> scheduleList = new List<Schedule>();
      if (!File.Exists("kuralListesi.dat"))
        return;
      try
      {
        Stream serializationStream = (Stream) new FileStream("kuralListesi.dat", FileMode.Open);
        scheduleList = (List<Schedule>) new BinaryFormatter()
        {
          Binder = ((SerializationBinder) new CatalogueBinder())
        }.Deserialize(serializationStream);
        serializationStream.Close();
      }
      catch (Exception ex)
      {
        File.Delete("kuralListesi.dat");
        Scheduler.LoadScheduler();
      }
      foreach (Schedule schedule in scheduleList)
      {
        Schedule s = (Schedule) null;
        if (schedule.GetType() == typeof (OneTimeSchedule))
        {
          string name = schedule.Name;
          DateTime now = DateTime.Now;
          int year = now.Year;
          now = DateTime.Now;
          int month = now.Month;
          int day = DateTime.Now.Day;
          DateTime startTime1 = schedule.StartTime;
          int hour = startTime1.Hour;
          startTime1 = schedule.StartTime;
          int minute = startTime1.Minute;
          startTime1 = schedule.StartTime;
          int second = startTime1.Second;
          DateTime startTime2 = new DateTime(year, month, day, hour, minute, second);
          GunlukKur gKur = new GunlukKur(schedule.GunlukKur.userID, schedule.GunlukKur.curFrom, schedule.GunlukKur.curTo, schedule.GunlukKur.curType, schedule.GunlukKur.curFromAlinacakKur, schedule.GunlukKur.curToAlinacakKur);
          s = (Schedule) new OneTimeSchedule(name, startTime2, gKur);
        }
        if (schedule.GetType() == typeof (DailySchedule))
        {
          string name = schedule.Name;
          DateTime now = DateTime.Now;
          int year = now.Year;
          now = DateTime.Now;
          int month = now.Month;
          int day = DateTime.Now.Day;
          DateTime startTime1 = schedule.StartTime;
          int hour = startTime1.Hour;
          startTime1 = schedule.StartTime;
          int minute = startTime1.Minute;
          startTime1 = schedule.StartTime;
          int second = startTime1.Second;
          DateTime startTime2 = new DateTime(year, month, day, hour, minute, second);
          GunlukKur gKur = new GunlukKur(schedule.GunlukKur.userID, schedule.GunlukKur.curFrom, schedule.GunlukKur.curTo, schedule.GunlukKur.curType, schedule.GunlukKur.curFromAlinacakKur, schedule.GunlukKur.curToAlinacakKur);
          s = (Schedule) new DailySchedule(name, startTime2, gKur);
        }
        if (schedule.GetType() == typeof (WeeklySchedule))
        {
          string name = schedule.Name;
          DateTime now = DateTime.Now;
          int year = now.Year;
          now = DateTime.Now;
          int month = now.Month;
          int day = DateTime.Now.Day;
          DateTime startTime1 = schedule.StartTime;
          int hour = startTime1.Hour;
          startTime1 = schedule.StartTime;
          int minute = startTime1.Minute;
          startTime1 = schedule.StartTime;
          int second = startTime1.Second;
          DateTime startTime2 = new DateTime(year, month, day, hour, minute, second);
          GunlukKur gKur = new GunlukKur(schedule.GunlukKur.userID, schedule.GunlukKur.curFrom, schedule.GunlukKur.curTo, schedule.GunlukKur.curType, schedule.GunlukKur.curFromAlinacakKur, schedule.GunlukKur.curToAlinacakKur);
          s = (Schedule) new WeeklySchedule(name, startTime2, gKur);
        }
        if (schedule.GetType() == typeof (MonthlySchedule))
        {
          string name = schedule.Name;
          DateTime now = DateTime.Now;
          int year = now.Year;
          now = DateTime.Now;
          int month = now.Month;
          int day = DateTime.Now.Day;
          DateTime startTime1 = schedule.StartTime;
          int hour = startTime1.Hour;
          startTime1 = schedule.StartTime;
          int minute = startTime1.Minute;
          startTime1 = schedule.StartTime;
          int second = startTime1.Second;
          DateTime startTime2 = new DateTime(year, month, day, hour, minute, second);
          GunlukKur gKur = new GunlukKur(schedule.GunlukKur.userID, schedule.GunlukKur.curFrom, schedule.GunlukKur.curTo, schedule.GunlukKur.curType, schedule.GunlukKur.curFromAlinacakKur, schedule.GunlukKur.curToAlinacakKur);
          s = (Schedule) new MonthlySchedule(name, startTime2, gKur);
        }
        if (schedule.GetType() == typeof (IntervalSchedule))
        {
          string name = schedule.Name;
          DateTime now = DateTime.Now;
          int year = now.Year;
          now = DateTime.Now;
          int month = now.Month;
          int day = DateTime.Now.Day;
          DateTime startTime1 = schedule.StartTime;
          int hour = startTime1.Hour;
          startTime1 = schedule.StartTime;
          int minute = startTime1.Minute;
          startTime1 = schedule.StartTime;
          int second = startTime1.Second;
          DateTime startTime2 = new DateTime(year, month, day, hour, minute, second);
          int int32 = Convert.ToInt32(schedule.Interval);
          TimeSpan fromTime = schedule.FromTime;
          TimeSpan toTime = schedule.ToTime;
          GunlukKur gKur = new GunlukKur(schedule.GunlukKur.userID, schedule.GunlukKur.curFrom, schedule.GunlukKur.curTo, schedule.GunlukKur.curType, schedule.GunlukKur.curFromAlinacakKur, schedule.GunlukKur.curToAlinacakKur);
          s = (Schedule) new IntervalSchedule(name, startTime2, int32, fromTime, toTime, gKur);
        }
        Scheduler.AddSchedule(s);
      }
    }
  }
}
