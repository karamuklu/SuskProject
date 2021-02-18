using INF_SUSK_Project_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace INF_SUSK_Project_DataAccessLayer
{
    public class INF_SUSK_Dal
    {
        public TBLISEMRI IsEmriGetir(string isemriNo)
        {
            using (INF_SUSK_Context context = new INF_SUSK_Context())
            {
                return context.TBLISEMRI.Where(i => i.ISEMRINO == isemriNo).FirstOrDefault();
            }
        }
        public TBLSTOKURS IsEmriNoGetir(string id)
        {
            using (INF_SUSK_Context context = new INF_SUSK_Context())
            {
                return context.TBLSTOKURS.Where(i => i.URETSON_FISNO.StartsWith(id)).OrderByDescending(i => i.URETSON_FISNO).FirstOrDefault();
            }
        }
        public TBLISEMRI IsEmriUSK_STATUSGuncelle(string isemriNo)
        {
            using (INF_SUSK_Context context = new INF_SUSK_Context())
            {
                var isemri = context.TBLISEMRI.Where(i => i.ISEMRINO == isemriNo).FirstOrDefault();
                isemri.USK_STATUS = "Y";
                context.SaveChanges();
                return isemri;
            }
        }
        public TBLISEMRI IsemriKapat(string isemriNo)
        {
            using (INF_SUSK_Context context = new INF_SUSK_Context())
            {
                var isemri = context.TBLISEMRI.Where(i => i.ISEMRINO == isemriNo).FirstOrDefault();
                isemri.KAPALI = "E";
                context.SaveChanges();
                return isemri;
            }
        }

        public TBLISEMRIEK IsEmriEkalan_Guncelle(string isemriNo, int kulMiktar)
        {
            using (INF_SUSK_Context context = new INF_SUSK_Context())
            {
                var isemri = context.TBLISEMRIEK.Where(i => i.ISEMRI == isemriNo).FirstOrDefault();
                isemri.KT_TEKNIKSERVIS = kulMiktar;
                context.SaveChanges();
                return isemri;
            }
        }

        public TBLISEMRI_MKA IsEmriSipNoGetir(string id)
        {
            using (INF_SUSK_Context context = new INF_SUSK_Context())
            {
                return context.TBLISEMRI_MKA.Where(i => i.ISEMRINO == id).FirstOrDefault();
            }
        }
        public List<SUSKONCESI_HAZIRLIK_MKA> YariMamul_Bul(string mamul_Kodu, string refIsemriNo, decimal isemriMiktar, DateTime tarih,int seviye,string aciklama)
        {
            int day = tarih.Day;
            int month = tarih.Month;
            int year = tarih.Year;
            string tamTarih = month + "-" + day + "-" + year;
            string sqlCumle = "SELECT '" + aciklama + "' ACIKLAMA,B.TARIH,B.REFISEMRINO,b.MAMUL_KODU ANAMAMUL, B.STOK_KODU, B.Seviye, B.MAMULMU, Sum(B.RECETE_MIKTAR)RECETE_MIKTAR, Sum(B.TRANSFERMIKTAR) TRANSFERMIKTAR,B.BAKIYE117, B.BAKIYE118, B.BAKIYE115 FROM (SELECT * FROM     (SELECT TARIH,REFISEMRINO, a.MAMUL_KODU, a.STOK_KODU, Seviye, a.MAMULMU, a.RECETE_MIKTAR, TRANSFERMIKTAR,    ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '117' AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE117,ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM TBLSTOKPH t WHERE t.DEPO_KODU = '118'AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE118, ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '115'  AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE115 FROM SUSKONCESI_HAZIRLIK_MKA('" + mamul_Kodu + "','" + refIsemriNo + "'," + isemriMiktar + ",'E','" + tamTarih + "') A) A)B WHERE SEVIYE<"+seviye+" GROUP BY B.REFISEMRINO,b.MAMUL_KODU, B.STOK_KODU, B.Seviye, B.MAMULMU,B.BAKIYE117, B.BAKIYE118, B.BAKIYE115, B.TARIH  ORDER BY seviye,stok_kodu";
            INF_SUSK_Context context = new INF_SUSK_Context();
            return context.Database.SqlQuery<SUSKONCESI_HAZIRLIK_MKA>(sqlCumle).ToList();
        }
        public List<SUSKONCESI_HAZIRLIK_MKA> Hammadde_Bul(string mamul_Kodu, string refIsemriNo, decimal isemriMiktar, DateTime tarih)
        {
            int day = tarih.Day;
            int month = tarih.Month;
            int year = tarih.Year;
            string tamTarih = month + "-" + day + "-" + year;
            string sqlCumle = " SELECT * from (SELECT REFISEMRINO,a.STOK_KODU, a.MAMULMU, a.RECETE_MIKTAR, TRANSFERMIKTAR, ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '117' AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE117,ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '118'AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE118,ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '115'AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE115 FROM SUSKONCESI_HAZIRLIK_MKA('" + mamul_Kodu + "','" + refIsemriNo + "'," + isemriMiktar + ",'H','" + tamTarih + "') a WHERE a.Stok_kodu IS NOT NULL) A WHERE a.TRANSFERMIKTAR > A.BAKIYE118";
            //"SELECT * FROM TEST1..INF_SUSK_BAKIYE_KONTROL_MKA WHERE ISEMRINO in(" + isemri + ")AND GEREKLI_MIKTAR>BAKIYE115";
            INF_SUSK_Context context = new INF_SUSK_Context();
            return context.Database.SqlQuery<SUSKONCESI_HAZIRLIK_MKA>(sqlCumle).ToList();
        }

        //public List<SUSKONCESI_HAZIRLIK_MKA> TransferList(string mamul_Kodu, string refIsemriNo, decimal isemriMiktar, DateTime tarih)
        //{
        //    int day = tarih.Day;
        //    int month = tarih.Month;
        //    int year = tarih.Year;
        //    string tamTarih = month + "-" + day + "-" + year;
        //    string sqlCumle = " SELECT B.REFISEMRINO, B.STOK_KODU, B.Seviye, B.MAMULMU, Sum(B.RECETE_MIKTAR)RECETE_MIKTAR, case when Sum(B.TRANSFERMIKTAR)> B.BAKIYE117 THEN B.BAKIYE117 ELSE  Sum(B.TRANSFERMIKTAR) END TRANSFERMIKTAR, B.BAKIYE117, B.BAKIYE118, B.BAKIYE115 from(  SELECT* from(SELECT REFISEMRINO, a.STOK_KODU, Seviye, a.MAMULMU, a.RECETE_MIKTAR, TRANSFERMIKTAR,  ISNULL((SELECT t.TOP_GIRIS_MIK -t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '117' AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE117, ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '118'AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE118, ISNULL((SELECT t.TOP_GIRIS_MIK - t.TOP_CIKIS_MIK AS BAKIYE FROM dbo.TBLSTOKPH t WHERE t.DEPO_KODU = '115'  AND t.STOK_KODU = a.stok_kodu), 0) AS BAKIYE115 FROM SUSKONCESI_HAZIRLIK_MKA('" + mamul_Kodu + "','" + refIsemriNo + "'," + isemriMiktar + ",'E','" + tamTarih + "') A) A)B WHERE B.Seviye = 1 or B.Seviye = 2 or B.Seviye = 3 or B.Seviye = 4 GROUP BY  B.REFISEMRINO, B.STOK_KODU, B.Seviye, B.MAMULMU,B.BAKIYE117, B.BAKIYE118, B.BAKIYE115 Having B.BAKIYE117 > 0 ORDER BY seviye";
        //    //"SELECT * FROM TEST1..INF_SUSK_BAKIYE_KONTROL_MKA WHERE ISEMRINO in(" + isemri + ")AND GEREKLI_MIKTAR>BAKIYE115";
        //    INF_SUSK_Context context = new INF_SUSK_Context();
        //    return context.Database.SqlQuery<SUSKONCESI_HAZIRLIK_MKA>(sqlCumle).ToList();
        //}
        public List<YARIMAMULISEMRIMKA> YARIMAMULISEMRI_BUL(string mamul_Kodu, string refIsemriNo, decimal isemriMiktar, DateTime tarih)
        {
            int day = tarih.Day;
            int month = tarih.Month;
            int year = tarih.Year;
            string tamTarih = month + "-" + day + "-" + year;

            //Lot size hesaplanarak 
            //string sqlCumle = "SELECT * FROM MINISEMRIOLUSTUR_MKA('" + mamul_Kodu + "','" + refIsemriNo + "'," + isemriMiktar + ",'" + tamTarih + "') WHERE ACILACAK_ISEMRIMIKTAR>0";

            //Birebir hesaplanarak 
            /**/
            string sqlCumle = "SELECT A.TARIH, A.REFISEMRINO, A.ANAMAMUL, A.SEVIYE, A.STOK_KODU, A.RECETE_MIKTAR,A.IHTIYAC_ISEMRIMIKTAR,(A.IHTIYAC_ISEMRIMIKTAR - A.YMAMUL_ISEMRIMIKTAR) ACILACAK_ISEMRIMIKTAR, A.YMAMUL_ISEMRINO, A.YMAMUL_ISEMRIMIKTAR FROM(   SELECT TARIH, REFISEMRINO, ANAMAMUL, SEVIYE, STOK_KODU, RECETE_MIKTAR, IHTIYAC_ISEMRIMIKTAR, YMAMUL_ISEMRINO, YMAMUL_ISEMRIMIKTAR   FROM YARIMAMULISEMRIMKA('" + mamul_Kodu + "','" + refIsemriNo + "'," + isemriMiktar + ",'" + tamTarih + "'))A   WHERE A.IHTIYAC_ISEMRIMIKTAR > A.YMAMUL_ISEMRIMIKTAR ORDER BY seviye";

            INF_SUSK_Context context = new INF_SUSK_Context();
            return context.Database.SqlQuery<YARIMAMULISEMRIMKA>(sqlCumle).ToList();
        }
        public string ISEMRINO_OLUSTUR(string isEmriilkHarf)
        {
            try
            {
                string sonIsemriNo = "";
                int isno = 0;
                using (INF_SUSK_Context context = new INF_SUSK_Context())
                {
                    sonIsemriNo = context.TBLISEMRI.Where(i => i.ISEMRINO.StartsWith(isEmriilkHarf + "0000000")).OrderByDescending(i => i.ISEMRINO).FirstOrDefault().ISEMRINO;
                    isno = Convert.ToInt32(sonIsemriNo.Substring(6)) + 1;
                }
                string isemriNo = isno.ToString();

                if (isemriNo.Length < 15)
                {
                    if (isemriNo.Length == 1)
                    {
                        isemriNo = isEmriilkHarf + "000000000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 2)
                    {
                        isemriNo = isEmriilkHarf + "000000000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 3)
                    {
                        isemriNo = isEmriilkHarf + "00000000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 4)
                    {
                        isemriNo = isEmriilkHarf + "0000000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 5)
                    {
                        isemriNo = isEmriilkHarf + "000000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 6)
                    {
                        isemriNo = isEmriilkHarf + "00000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 7)
                    {
                        isemriNo = isEmriilkHarf + "0000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 8)
                    {
                        isemriNo = isEmriilkHarf + "000000" + isemriNo;
                    }
                    else if (isemriNo.Length == 9)
                    {
                        isemriNo = isEmriilkHarf + "00000" + isemriNo;
                    }
                }
                //isemriNo = isEmriilkHarf+"00000000" + "" + isemriNo;
                return isemriNo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "İşemri No bulunamadı");
                throw;
            }

        }
        public List<SUSK_LISTESI> YARIMAMULSUSK_LISTESI(string mamul_Kodu, string refIsemriNo, decimal isemriMiktar, DateTime tarih)
        {
            int day = tarih.Day;
            int month = tarih.Month;
            int year = tarih.Year;
            string tamTarih = month + "-" + day + "-" + year;
            string sqlCumle = "SELECT SEVIYE,isemri.ISEMRINO,isnull(susk.URETSON_FISNO,'SUSK YAPILMAMIS')SUSK_NO, isemri.TARIH, isemri.STOK_KODU, isemri.MIKTAR,A.REFISEMRINO  FROM dbo.TBLISEMRI isemri LEFT OUTER JOIN dbo.TBLSTOKURS susk ON isemri.ISEMRINO = susk.URETSON_SIPNO AND isemri.STOK_KODU = susk.URETSON_MAMUL CROSS APPLY(SELECT * from SUSKONCESI_HAZIRLIK_MKA('" + mamul_Kodu + "','" + refIsemriNo + "'," + isemriMiktar + ",'E','" + tamTarih + "'))A WHERE isemri.REFISEMRINO = '" + refIsemriNo + "' AND a.stok_kodu = isemri.STOK_KODU AND isemri.MIKTAR > isnull(susk.URETSON_MIKTAR, 0) ORDER BY seviye DESC";

            INF_SUSK_Context context = new INF_SUSK_Context();
            return context.Database.SqlQuery<SUSK_LISTESI>(sqlCumle).ToList();
        }
        public SUSK_KONTROL SUSK_KONTROL(string isemri_no)
        {
            string sqlCumle = " SELECT A.ISEMRINO, A.STOK_KODU, A.MIKTAR ISEMRIMIKTAR, ISNULL(SUSKMIKTAR,0)SUSKMIKTAR, ISNULL(A.MIKTAR-A.SUSKMIKTAR,-1) BAKIYE FROM (SELECT t.ISEMRINO, t.STOK_KODU, t.MIKTAR,(SELECT sum(susk.URETSON_MIKTAR) FROM TBLSTOKURS susk WHERE susk.URETSON_SIPNO = t.ISEMRINO AND susk.URETSON_MAMUL = t.STOK_KODU)SUSKMIKTAR FROM TBLISEMRI t )A WHERE a.ISEMRINO = '" + isemri_no + "'";

            INF_SUSK_Context context = new INF_SUSK_Context();
            SUSK_KONTROL obj = new SUSK_KONTROL();
            if (obj != null)
            {
                obj = context.Database.SqlQuery<SUSK_KONTROL>(sqlCumle).FirstOrDefault();
                return obj;
            }
            return obj;
        }
        public List<TBLISEMRI> ACILANISEMIRLERI(string isemriList)
        {
            string sqlCumle = " SELECT t.TARIH, t.ISEMRINO, t.STOK_KODU, t.MIKTAR, t.ACIKLAMA, t.TESLIM_TARIHI, t.SIPARIS_NO, t.KAPALI FROM dbo.TBLISEMRI t WHERE t.REFISEMRINO in('" + isemriList + "') ORDER BY t.ISEMRINO";
            INF_SUSK_Context context = new INF_SUSK_Context();
            return context.Database.SqlQuery<TBLISEMRI>(sqlCumle).ToList();
        }

    }
}



