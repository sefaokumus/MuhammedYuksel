// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.KurItem
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System;

namespace MuhammedYuksel
{
  [Serializable]
  public class KurItem
  {
    public int CUR_ID { get; set; }

    public string CUR_CODE { get; set; }

    public string CUR_SYMBOL { get; set; }

    public string DESCRIPTION { get; set; }

    public int UNIT { get; set; }

    public float FOREX_BUYING { get; set; }

    public float FOREX_SELLING { get; set; }

    public float BANKNOTE_BUYING { get; set; }

    public float BANKNOTE_SELLING { get; set; }

    public KurItem()
    {
      this.CUR_ID = -1;
      this.CUR_CODE = "";
      this.CUR_SYMBOL = "";
      this.DESCRIPTION = "";
    }

    public KurItem(int curID, string cur_Code)
    {
      this.CUR_ID = curID;
      this.CUR_CODE = cur_Code;
    }

    public KurItem(int curID, string cur_Code, string cur_symbol, string desc)
    {
      this.CUR_ID = curID;
      this.CUR_CODE = cur_Code;
      this.CUR_SYMBOL = cur_symbol;
      this.DESCRIPTION = desc;
    }
  }
}
