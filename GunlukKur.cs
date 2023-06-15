// Decompiled with JetBrains decompiler
// Type: MuhammedYuksel.GunlukKur
// Assembly: MuhammedYuksel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3D104369-FD4E-44BD-8956-4BCB13E72DF2
// Assembly location: D:\MuhammedYuksel\MuhammedYuksel.exe

using System;

namespace MuhammedYuksel
{
  [Serializable]
  public class GunlukKur
  {
    public int userID { get; set; }

    public KurItem curFrom { get; set; }

    public KurItem curTo { get; set; }

    public KurItem curType { get; set; }

    public string curToAlinacakKur { get; set; }

    public string curFromAlinacakKur { get; set; }

    public GunlukKur()
    {
      this.userID = -1;
      this.curFrom = new KurItem();
      this.curTo = new KurItem();
      this.curType = new KurItem();
    }

    public GunlukKur(int userID, KurItem curFrom, KurItem curTo, KurItem curType, string curFromAlinacakKur, string curToAlinacakKur)
    {
      this.userID = userID;
      this.curFrom = curFrom;
      this.curTo = curTo;
      this.curType = curType;
      this.curFromAlinacakKur = curFromAlinacakKur;
      this.curToAlinacakKur = curToAlinacakKur;
    }

    public override string ToString()
    {
      return "Cur From: " + this.curFrom.CUR_CODE + " - Cur To: " + this.curTo.CUR_CODE;
    }
  }
}
