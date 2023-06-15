// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.Engine
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace MuhammedYuksel
{
  public class Engine
  {
    private static OracleConnection connection;
    private static OracleCommand command;
    private static OracleDataReader rdr;

    internal static bool GunlukKurExists(GunlukKur gunlukKur)
    {
      bool flag = false;
      try
      {
        Engine.connection = new OracleConnection(Program.ConnectionString());
        Engine.connection.Open();
        Engine.command = new OracleCommand("SELECT DAILY_CUR_RATE_ID FROM GNLD_DAILY_CUR_RATE  WHERE CUR_TO_ID = " + (object) gunlukKur.curTo.CUR_ID + " AND CUR_RATE_TYPE_ID = " + (object) gunlukKur.curType.CUR_ID + " AND CUR_FROM_ID = " + (object) gunlukKur.curFrom.CUR_ID + " AND EXTRACT(year from DOC_DATE) = " + (object) DateTime.Now.Year + " AND EXTRACT(month from DOC_DATE) = " + (object) DateTime.Now.Month + " AND EXTRACT(day from DOC_DATE) = " + (object) DateTime.Now.Day, Engine.connection);
        Engine.rdr = Engine.command.ExecuteReader();
        if (Engine.rdr.Read())
          flag = true;
      }
      catch
      {
      }
      finally
      {
        Engine.connection.Close();
      }
      return flag;
    }

    internal static List<KurItem> ParseXML(string xmlPath)
    {
        Console.WriteLine(xmlPath);
      List<KurItem> kurItemList = new List<KurItem>();
      KurItem kurItem = new KurItem();
      XmlTextReader xmlTextReader = new XmlTextReader(xmlPath);
      while (xmlTextReader.Read())
      {
        if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == "Currency")
        {
          while (xmlTextReader.MoveToNextAttribute())
          {
            if (xmlTextReader.Name == "Kod" && kurItem.CUR_CODE != xmlTextReader.Value)
            {
              kurItem = new KurItem(-1, xmlTextReader.Value);
              kurItemList.Add(kurItem);
              Console.WriteLine(kurItem);
            }
          }
        }
        if (xmlTextReader.Depth == 2 && xmlTextReader.Name == "Unit" && xmlTextReader.NodeType == XmlNodeType.Element && !xmlTextReader.IsEmptyElement)
        {
          xmlTextReader.Read();
          Console.WriteLine(xmlTextReader.Value);
          kurItem.UNIT = int.Parse(xmlTextReader.Value);
        }
        if (xmlTextReader.Depth == 2 && xmlTextReader.Name == "Isim" && xmlTextReader.NodeType == XmlNodeType.Element && !xmlTextReader.IsEmptyElement)
        {
          xmlTextReader.Read();
          Console.WriteLine(xmlTextReader.Value);
          kurItem.DESCRIPTION = xmlTextReader.Value;
        }
        if (xmlTextReader.Depth == 2 && xmlTextReader.Name == "ForexBuying" && xmlTextReader.NodeType == XmlNodeType.Element && !xmlTextReader.IsEmptyElement)
        {
          xmlTextReader.Read();
          Console.WriteLine(xmlTextReader.Value);
          if (xmlTextReader.Value != "")
            kurItem.FOREX_BUYING = float.Parse(xmlTextReader.Value.Replace(".", ","));
        }
        if (xmlTextReader.Depth == 2 && xmlTextReader.Name == "ForexSelling" && xmlTextReader.NodeType == XmlNodeType.Element && !xmlTextReader.IsEmptyElement)
        {
          xmlTextReader.Read();
          Console.WriteLine(xmlTextReader.Value);
          if (xmlTextReader.Value != "")
              kurItem.FOREX_SELLING = float.Parse(xmlTextReader.Value.Replace(".", ","));
        }
        if (xmlTextReader.Depth == 2 && xmlTextReader.Name == "BanknoteBuying" && xmlTextReader.NodeType == XmlNodeType.Element && !xmlTextReader.IsEmptyElement)
        {
          xmlTextReader.Read();
          Console.WriteLine(xmlTextReader.Value);
          if (xmlTextReader.Value != "")
            kurItem.BANKNOTE_BUYING = float.Parse(xmlTextReader.Value.Replace(".", ","));
        }
        if (xmlTextReader.Depth == 2 && xmlTextReader.Name == "BanknoteSelling" && xmlTextReader.NodeType == XmlNodeType.Element && !xmlTextReader.IsEmptyElement)
        {
          xmlTextReader.Read();
          Console.WriteLine(xmlTextReader.Value);
          if (xmlTextReader.Value != "")
            kurItem.BANKNOTE_SELLING = float.Parse(xmlTextReader.Value.Replace(".", ","));
        }
      }
      return kurItemList;
    }

    internal static string GetPreviousDayXMLPath(DateTime d)
    {
      bool flag = false;
      DateTime dateTime = d.AddDays(-1.0);
      string requestUriString = "http://www.tcmb.gov.tr/kurlar/" + (object) dateTime.Year + dateTime.Month.ToString("00") + "/" + dateTime.Day.ToString("00") + dateTime.Month.ToString("00") + (object) dateTime.Year + ".xml";
      HttpWebResponse httpWebResponse = (HttpWebResponse) null;
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(requestUriString);
      httpWebRequest.Timeout = 25000;
      httpWebRequest.Method = "HEAD";
      try
      {
        httpWebResponse = (HttpWebResponse) httpWebRequest.GetResponse();
        flag = true;
      }
      catch (WebException ex)
      {
      }
      finally
      {
        if (httpWebResponse != null)
          httpWebResponse.Close();
      }
      if (flag)
        return requestUriString;
      return "data yok";
    }

    internal static void InsertNewGunlukKur(GunlukKur gunlukKur, List<KurItem> merkezBankasiKurlari)
    {
      try
      {
        float num1 = 0.0f;
        float num2 = 0.0f;
        int num3 = 0;
        int num4 = 0;
        foreach (KurItem kurItem in merkezBankasiKurlari)
        {
          if (gunlukKur.curFrom.CUR_CODE == "TRY")
          {
            num1 = 1f;
            num3 = 1;
          }
          else if (kurItem.CUR_CODE == gunlukKur.curFrom.CUR_CODE)
          {
            num3 = kurItem.UNIT;
            num1 = !(gunlukKur.curFromAlinacakKur == "Döviz Alış") ? (!(gunlukKur.curFromAlinacakKur == "Döviz Satış") ? (!(gunlukKur.curFromAlinacakKur == "Efektif Alış") ? kurItem.BANKNOTE_SELLING : kurItem.BANKNOTE_BUYING) : kurItem.FOREX_SELLING) : kurItem.FOREX_BUYING;
          }
          if (gunlukKur.curTo.CUR_CODE == "TRY")
          {
            num2 = 1f;
            num4 = 1;
          }
          else if (kurItem.CUR_CODE == gunlukKur.curTo.CUR_CODE)
          {
            num4 = kurItem.UNIT;
            num2 = !(gunlukKur.curToAlinacakKur == "Döviz Alış") ? (!(gunlukKur.curToAlinacakKur == "Döviz Satış") ? (!(gunlukKur.curToAlinacakKur == "Efektif Alış") ? kurItem.BANKNOTE_SELLING : kurItem.BANKNOTE_BUYING) : kurItem.FOREX_SELLING) : kurItem.FOREX_BUYING;
          }
        }
        if ((double) num1 == 0.0 || (double) num2 == 0.0)
          return;
        string cmdText = "INSERT INTO GNLD_DAILY_CUR_RATE (CREATE_USER_ID, UPDATE_USER_ID, CREATE_DATE, UPDATE_DATE, DELETED, DOC_DATE, CUR_TO_ID, CUR_RATE_TYPE_ID, CUR_FROM_ID, CUR_RATE_TRA) VALUES (" + (object) gunlukKur.userID + ",0,sysdate, TO_DATE('0001/01/01 00:00', 'yyyy/mm/dd hh24:mi'),1, TRUNC(SYSDATE)," + (object) gunlukKur.curTo.CUR_ID + ", " + (object) gunlukKur.curType.CUR_ID + "," + (object) gunlukKur.curFrom.CUR_ID + "," + ((float) ((double) num1 / (double) num3 / ((double) num2 / (double) num4))).ToString().Replace(",", ".") + ")";
        Engine.connection.Open();
        Engine.command = new OracleCommand(cmdText, Engine.connection);
        Engine.command.ExecuteNonQuery();
      }
      catch
      {
      }
      finally
      {
        Engine.connection.Close();
      }
    }

    internal static void InsertKurFromPreviousDay(GunlukKur gunlukKur, DateTime dateTime)
    {
      string stringForGunlukKur = Engine.GetInsertStringForGunlukKur(gunlukKur, dateTime.AddDays(-1.0));
      if (!(stringForGunlukKur != ""))
        return;
      try
      {
        Engine.connection = new OracleConnection(Program.ConnectionString());
        Engine.connection.Open();
        Engine.command = new OracleCommand(stringForGunlukKur, Engine.connection);
        Engine.command.ExecuteNonQuery();
      }
      catch
      {
      }
      finally
      {
        Engine.connection.Close();
      }
    }

    private static string GetInsertStringForGunlukKur(GunlukKur gunlukKur, DateTime dateTime)
    {
      string str = "";
      try
      {
        Engine.connection = new OracleConnection(Program.ConnectionString());
        Engine.connection.Open();
        Engine.command = new OracleCommand("SELECT * FROM GNLD_DAILY_CUR_RATE  WHERE CUR_TO_ID = " + (object) gunlukKur.curTo.CUR_ID + " AND CUR_RATE_TYPE_ID = " + (object) gunlukKur.curType.CUR_ID + " AND CUR_FROM_ID = " + (object) gunlukKur.curFrom.CUR_ID + " AND EXTRACT(year from DOC_DATE) = " + (object) dateTime.Year + " AND EXTRACT(month from DOC_DATE) = " + (object) dateTime.Month + " AND EXTRACT(day from DOC_DATE) = " + (object) dateTime.Day, Engine.connection);
        Engine.rdr = Engine.command.ExecuteReader();
        if (Engine.rdr.Read())
          str = "INSERT INTO GNLD_DAILY_CUR_RATE (CREATE_USER_ID, UPDATE_USER_ID, CREATE_DATE, UPDATE_DATE, DELETED, DOC_DATE, CUR_TO_ID, CUR_RATE_TYPE_ID, CUR_FROM_ID, CUR_RATE_TRA) VALUES (" + (object) gunlukKur.userID + ", 0, sysdate, TO_DATE('0001/01/01 00:00', 'yyyy/mm/dd hh24:mi'), 1 , TRUNC(SYSDATE)," + (object) gunlukKur.curTo.CUR_ID + ", " + (object) gunlukKur.curType.CUR_ID + "," + (object) gunlukKur.curFrom.CUR_ID + "," + Engine.rdr["CUR_RATE_TRA"].ToString().Replace(',', '.') + ")";
      }
      catch
      {
      }
      finally
      {
        Engine.connection.Close();
      }
      return str;
    }

    internal static int FindIndexOf(int p, ComboBox kullaniciComboBox)
    {
      int num = 0;
      for (int index = 0; index < kullaniciComboBox.Items.Count; ++index)
      {
        if (p == ((KurItem) kullaniciComboBox.Items[index]).CUR_ID)
        {
          num = index;
          break;
        }
      }
      return num;
    }

    internal static int FindIndexOf(string p, ComboBox curToAlinacakKurComboBox)
    {
      int num = 0;
      for (int index = 0; index < curToAlinacakKurComboBox.Items.Count; ++index)
      {
        if ((object) p == curToAlinacakKurComboBox.Items[index])
        {
          num = index;
          break;
        }
      }
      return num;
    }
  }
}
