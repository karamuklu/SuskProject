using INF_SUSK_Project_DataAccessLayer;
using INF_SUSK_Project_Entities;
using NetOpenX50;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INF_SUSK_Project_BusinessLayer
{
    public class INF_SUSK_Manager
    {
        public INF_SUSK_Dal infSusk_Dal = new INF_SUSK_Dal();
        public TBLISEMRI IsEmriGetir(string isemriNo)
        {
            return infSusk_Dal.IsEmriGetir(isemriNo);
        }
        public TBLSTOKURS IsEmriNoGetir(string id)
        {
            return infSusk_Dal.IsEmriNoGetir(id);
        }
        public TBLISEMRI_MKA IsEmriSipNoGetir(string isemriNo)
        {
            return infSusk_Dal.IsEmriSipNoGetir(isemriNo);
        }
        public List<TBLISEMRI> ACILANISEMIRLERI(List<string> isEmirleri)
        {
            string isEmirleri_text = string.Join(",", isEmirleri);//Liste elemanlarını string ifadeye çeviriyor.
            return infSusk_Dal.ACILANISEMIRLERI(isEmirleri_text);
        }
        public List<SUSKONCESI_HAZIRLIK_MKA> Hammadde_Bul(string stok_kodu, string refisemriNo, decimal recMiktar, DateTime tarih)
        {
            return infSusk_Dal.Hammadde_Bul(stok_kodu, refisemriNo, recMiktar,tarih);
        }
        //public List<SUSKONCESI_HAZIRLIK_MKA> TransferList(string mamulKodu, string refIsemriNo, decimal isemriMiktar, DateTime tarih)
        //{
        //    return infSusk_Dal.TransferList(mamulKodu, refIsemriNo, isemriMiktar, tarih);
        //}
        public List<YARIMAMULISEMRIMKA> YARIMAMUL_ISEMRIBUL(string MAMUL_KODU, string ISEMRINO, decimal ISEMRIMIKTAR,DateTime TARIH)
        {
            return infSusk_Dal.YARIMAMULISEMRI_BUL(MAMUL_KODU, ISEMRINO, ISEMRIMIKTAR, TARIH).ToList();
        }
        public List<SUSKONCESI_HAZIRLIK_MKA> YariMamul_Bul(string mamulKodu, string refIsemriNo, decimal isemriMiktar,DateTime tarih,int seviye,string aciklama)
        {
            return infSusk_Dal.YariMamul_Bul(mamulKodu, refIsemriNo, isemriMiktar,tarih,seviye,aciklama);
        }
        public string ISEMRINO_OLUSTUR(string isEmriilkHarf)
        {
            return infSusk_Dal.ISEMRINO_OLUSTUR(isEmriilkHarf);
        }
        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                using (_timeoutTimer)
                    MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }
        public void ACILACAK_ISEMIRLERI(TBLISEMRI_MKA acilacakIsemri)//İşemri Açma metodu
        {
            IsEmriKaydet(acilacakIsemri);
        }
        public void DIREKACILACAK_ISEMIRLERI(List<TBLISEMRI> acilacakIsemri)//İşemri Açma metodu
        {
            IsEmriKaydet(acilacakIsemri);
        }
        public void LokalDepolarArasiTransferBelgesi(List<SUSKONCESI_HAZIRLIK_MKA> transferList)
        {
            //aktarım için lazım olan kalemler, hammadde kodu, transfer miktar
            //Üst bilgiler sabit, kalem bilgilerini foreach ile dönüp aktarımı yaptırabiliriz.


            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe.config");

            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            Fatura fatura = default(Fatura);
            FatUst fatUst = default(FatUst);
            FatKalem fatKalem = default(FatKalem);
            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL,
                                              ConfigurationManager.AppSettings["SIRKET"],
                                              "TEMELSET",
                                              "",
                                              "karamuklu",
                                              "12qw",
                                              1);
                fatura = kernel.yeniFatura(sirket, TFaturaTip.ftLokalDepo);
                fatUst = fatura.Ust();
                fatUst.FATIRS_NO = fatura.YeniNumara("J");
                fatUst.KOD2 = "D";
                fatUst.CariKod = "1";
                fatUst.CARI_KOD2 = "1";
                fatUst.TIPI = TFaturaTipi.ft_Bos;
                fatUst.AMBHARTUR = TAmbarHarTur.htDepolar;
                fatUst.Tarih = DateTime.Now.Date;
                fatUst.FiiliTarih = DateTime.Now.Date;
                fatUst.PLA_KODU = "D";
                fatUst.Proje_Kodu = "G";
                fatUst.Aciklama = "OTO. AKTARIM";
                fatUst.EFatOzelKod = 'D';
                fatUst.KDV_DAHILMI = true;
                fatUst.EKACK1 =transferList.FirstOrDefault().REFISEMRINO+" Otomatik Aktarım - Depo Transfer Fişi";

                foreach (var item in transferList)
                {
                    fatKalem = fatura.kalemYeni(item.STOK_KODU);            //stok kodu lazım
                    fatKalem.Gir_Depo_Kodu = item.GIRISDEPOKODU;            //Depo Kodu lazım Giriş
                    fatKalem.DEPO_KODU = item.CIKISDEPOKODU;                //Depo Kodu lazım Çıkış
                    fatKalem.STra_GCMIK = (double)item.TRANSFERMIKTAR;
                    fatKalem.ProjeKodu = "G";
                    fatKalem.ReferansKodu = item.REFISEMRINO;// Bu kısımda önemli.Bağlı siparişe bakıp çekilebilir
                    //Sadece 114 depodan çekeceğimiz zaman Dinamik depo hareketlerini yaptırabilmemiz lazım. program doğru şekilde çalışıyor.

                    //if (item.TRANSFERMIKTAR > item.BAKIYE115)
                    //{
                    //      //Miktar Lazım
                    //}
                    //fatKalem.DEPO_KODU = 114;
                    //fatKalem.STra_BF = 1;
                    fatKalem.Irsaliyeno = item.REFISEMRINO;
                }
                fatura.kayitYeni();
                MailGonder("Otomatik oluşturulan " + fatUst.FATIRS_NO + " no'lu Depo Transfer Fişi", fatUst.FATIRS_NO + " numaralı Depo Transfer Fişi sistemde oluşturulmuştur.", ConfigurationManager.AppSettings["DepoTransferEmail"]);//,bora.demirkol@inform.com.tr
            }
            finally
            {
                Marshal.ReleaseComObject(fatKalem);
                Marshal.ReleaseComObject(fatUst);
                Marshal.ReleaseComObject(fatura);
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);
            }
        }
        public static void IsEmriKaydet(TBLISEMRI_MKA acilacakIsemri)//İşemri Açma NetOpenx
        {
            //var sirketNedir = ConfigurationManager.AppSettings["SIRKET"];
            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            IsEmri Isemri = default(IsEmri);
            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL,
                                           ConfigurationManager.AppSettings["SIRKET"],
                                           "TEMELSET",
                                           "",
                                           "karamuklu",
                                           "12qw",
                                           1);
                Isemri = kernel.yeniIsEmri(sirket);
                Isemri.IsEmriNo = acilacakIsemri.ISEMRINO;
                Isemri.Tarih = DateTime.Now.Date;
                Isemri.StokKodu = acilacakIsemri.STOK_KODU;
                Isemri.Miktar = (double)acilacakIsemri.MIKTAR;//depo adetine bak, fark kadar aç
                Isemri.SiparisNo = acilacakIsemri.SIPARIS_NO;  // Müşteri sipariş no
                Isemri.TeslimTarihi = acilacakIsemri.TESLIM_TARIHI.Date;
                Isemri.RefIsEmriNo = acilacakIsemri.REFISEMRINO;
                //Isemri.Aciklama = acilacakIsemri.ACIKLAMA;
                Isemri.ProjeKodu = "G";
                
                
                Isemri.kayitYeni();
                MailGonder("Bilgi..." + acilacakIsemri.REFISEMRINO + " " + acilacakIsemri.ANAMAMUL + " Açılmayan Alt İşemirleri", acilacakIsemri.ISEMRINO + " nolu işemri ile " + acilacakIsemri.STOK_KODU + " kodlu ürün sistemde " + Convert.ToInt32(acilacakIsemri.MIKTAR) + " adet olarak açılmıştır.", ConfigurationManager.AppSettings["IsemriEmail"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(Isemri);
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);
            }
        }
        public static void IsEmriKaydet(List<TBLISEMRI> acilacakIsemri)//İşemri Açma NetOpenx
        {
            //var sirketNedir = ConfigurationManager.AppSettings["SIRKET"];
            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            IsEmri Isemri = default(IsEmri);
            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL,
                                           ConfigurationManager.AppSettings["SIRKET"],
                                           "TEMELSET",
                                           "",
                                           "karamuklu",
                                           "12qw",
                                           1);
                foreach (var item in acilacakIsemri)
                {
                Isemri = kernel.yeniIsEmri(sirket);
                Isemri.IsEmriNo = item.ISEMRINO;
                Isemri.Tarih = DateTime.Now.Date;
                Isemri.StokKodu = item.STOK_KODU;
                Isemri.Miktar = (double)item.MIKTAR;//depo adetine bak, fark kadar aç
                //Isemri.SiparisNo = acilacakIsemri.SIPARIS_NO;  // Müşteri sipariş no
                Isemri.TeslimTarihi = item.TESLIM_TARIHI.Date;
                //Isemri.RefIsEmriNo = acilacakIsemri.REFISEMRINO;
                Isemri.SiparisNo = item.SIPARIS_NO;
                 Isemri.SipKont = item.SIRA;
                Isemri.Aciklama = item.ACIKLAMA;//"B";// acilacakIsemri.ACIKLAMA;
                Isemri.ProjeKodu = "G";

                Isemri.kayitYeni();
                MailGonder("Bilgi..." + item.ISEMRINO + " " + item.STOK_KODU + " Açılan İşemirleri", item.ISEMRINO + " nolu işemri ile " + item.STOK_KODU + " kodlu ürün sistemde " + Convert.ToInt32(item.MIKTAR) + " adet olarak açılmıştır.", ConfigurationManager.AppSettings["IsemriEmail"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(Isemri);
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);
            }
        }
        public static bool MailGonder(string konu, string aciklama, string kime)
        {
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("mustafa.karamuklu@inform.com.tr", "Password18");
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("noreply@legrand.com");
            smtpClient.Host = "SMTP.LIMOUSIN.FR.GRPLEG.COM";
            smtpClient.Port = 25; //Gönderici portudur.
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;
            smtpClient.EnableSsl = false;
            message.From = fromAddress;
            message.Subject = konu;
            message.IsBodyHtml = true; // HTML içeriğine izin verir
            message.Body = aciklama; // İçeriği oluşturmaktadır.
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            message.To.Add(kime);
            message.Bcc.Add("mustafa.karamuklu@inform.com.tr");
            smtpClient.Send(message);
            return true;
        }
        public List<SUSK_LISTESI> YARIMAMULSUSK_LISTESI(string MAMUL_KODU, string REFERANSISEMRINO, decimal ISEMRIMIKTAR,DateTime TARIH)
        {
            return infSusk_Dal.YARIMAMULSUSK_LISTESI(MAMUL_KODU, REFERANSISEMRINO, ISEMRIMIKTAR,TARIH).ToList();
        }
        public void SUSK_Kaydet(SUSK_LISTESI suskYarimamulList,string ilkHarf)
        {
            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            SerbestUSK susk = default(SerbestUSK);
            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL,
                   ConfigurationManager.AppSettings["SIRKET"],
                     "TEMELSET",
                     "",
                     "karamuklu",
                     "12qw",
                     1);

                susk = kernel.yeniSerbestUSK(sirket);
                susk.UretSon_FisNo = susk.SonFisNumarasi(ilkHarf);
                susk.UretSon_Mamul = suskYarimamulList.STOK_KODU;
                susk.UretSon_Depo = suskYarimamulList.SUSKDEPOGIRIS; //giriş depo
                susk.I_Yedek1 = suskYarimamulList.SUSKDEPOCIKIS;   //çıkış depo
                susk.UretSon_Miktar = (double)suskYarimamulList.MIKTAR;
                susk.UretSon_Tarih = DateTime.Now.Date;
                susk.BelgeTipi = TBelgeTipi.btIsEmri;
                susk.UretSon_SipNo = suskYarimamulList.ISEMRINO;
                susk.DepoOnceligi = TDepoOnceligi.doStokDepo;
                //susk.S_Yedek1=suskYarimamulList.Refe
                //susk.F_Yedek1 = (double)suskYarimamulList.MIKTAR; //miktar2
                susk.Aciklama = "OTO. SUSK";
                susk.Proje_Kodu = "G";
                susk.S_Yedek1 = suskYarimamulList.REFISEMRINO;
                //susk.S_Yedek2 = "ekalan2 örneği";
                susk.OTO_YMAM_GIRDI_CIKTI = true;
                susk.OTO_YMAM_STOK_KULLAN = false;
                susk.BAKIYE_DEPO = 0;  //0:verilen_depo 1:tüm_depolar

                if (suskYarimamulList.SERI_NO!="" && suskYarimamulList.SERI_NO !=null)
                {
                    susk.SeriEkle(suskYarimamulList.SERI_NO, "SERI1_2", "Açıklama1", "Açıklama2", 1);
                }

                if (susk.FisUret() != true)
                {
                    AutoClosingMessageBox.Show(susk.HataKodu.ToString() + ' ' + susk.HataMesaji, "SUSK Kontrol", 1000);
                    MailGonder("Uyarı... Eksik Malzeme - SUSK YAPILAMAYAN İŞEMRİ " + suskYarimamulList.ISEMRINO + "  " + suskYarimamulList.STOK_KODU, suskYarimamulList.STOK_KODU + " stok kodu için açılmış olan " + suskYarimamulList.ISEMRINO + " nolu işemri " + susk.UretSon_FisNo + " SUSK yapılırken eksik malzemeden dolayı TAMAMLANAMAMIŞTIR..." + " " + "" +
                       susk.HataMesaji, ConfigurationManager.AppSettings["YariMamulSUSKEmail"]);
                    //infSusk_Dal.IsEmriUSK_STATUSGuncelle(suskYarimamulList.ISEMRINO);
                    //MessageBox.Show(susk.HataKodu.ToString() + ' ' + susk.HataMesaji);
                }


                if (susk.Kaydet() != true)
                {
                    AutoClosingMessageBox.Show(susk.HataKodu.ToString() + ' ' + susk.HataMesaji, "SUSK Kontrol", 1000);
                    MailGonder("Uyarı... Eksik Malzeme - SUSK YAPILAMAYAN İŞEMRİ " + suskYarimamulList.ISEMRINO + "  " + suskYarimamulList.STOK_KODU, suskYarimamulList.STOK_KODU + " stok kodu için açılmış olan " + suskYarimamulList.ISEMRINO + " nolu işemri " + susk.UretSon_FisNo + " SUSK yapılırken eksik malzemeden dolayı TAMAMLANAMAMIŞTIR..." + " " + "" +
                   susk.HataMesaji, ConfigurationManager.AppSettings["YariMamulSUSKEmail"]);
                    //infSusk_Dal.IsEmriUSK_STATUSGuncelle(suskYarimamulList.ISEMRINO);
                }


                //MessageBox.Show(susk.HataKodu.ToString() + ' ' + susk.HataMesaji);
                AutoClosingMessageBox.Show(suskYarimamulList.ISEMRINO + " nolu işemri SUSK yapılmıştır.", "SUSK Kontrol", 1000);

                if (suskYarimamulList.ISEMRINO.StartsWith("S"))
                {
                    MailGonder("Uyarı... SUSK Yapılan İşemri " + suskYarimamulList.ISEMRINO + "  " + suskYarimamulList.STOK_KODU, suskYarimamulList.STOK_KODU + " stok kodu için açılmış olan " + suskYarimamulList.ISEMRINO + " nolu işemri " + susk.UretSon_FisNo + " fiş numarası ile  otomatik olarak SUSK yapılmıştır.", ConfigurationManager.AppSettings["AnaMamulSUSKEmail"]);//oto mail gönder ,bora.demirkol@inform.com.tr,hakan.sari@inform.com.tr
                    //infSusk_Dal.IsEmriUSK_STATUSGuncelle(suskYarimamulList.ISEMRINO);
                }
                 else
                {
                    MailGonder("Bilgi... SUSK Yapılan İşemri " + suskYarimamulList.ISEMRINO + "  " + suskYarimamulList.STOK_KODU, suskYarimamulList.STOK_KODU + " stok kodu için açılmış olan " + suskYarimamulList.ISEMRINO + " nolu işemri " + susk.UretSon_FisNo + " fiş numarası ile  otomatik olarak SUSK yapılmıştır.", ConfigurationManager.AppSettings["YariMamulSUSKEmail"]);//oto mail gönder ,bora.demirkol@inform.com.tr,hakan.sari@inform.com.tr
                }

            }
            catch (Exception ex)
            {
                MailGonder("Uyarı... Eksik Malzeme - SUSK YAPILAMAYAN İŞEMRİ " + suskYarimamulList.ISEMRINO + "  " + suskYarimamulList.STOK_KODU, suskYarimamulList.STOK_KODU + " stok kodu için açılmış olan " + suskYarimamulList.ISEMRINO + " nolu işemri " + susk.UretSon_FisNo + " SUSK yapılırken eksik malzemeden dolayı TAMAMLANAMAMIŞTIR..." + " " + "" +
                   ex.Message, ConfigurationManager.AppSettings["YariMamulSUSKEmail"]);
                //infSusk_Dal.IsEmriUSK_STATUSGuncelle(suskYarimamulList.ISEMRINO);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(susk);
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);
            }


            //public decimal DepoBakiyeGetirYariMamul(string stok_kodu)
            //{
            //    return Convert.ToDecimal(infSusk_Dal.DepoBakiyeGetirYariMamul(stok_kodu));
            //}
        }
        public SUSK_KONTROL SUSK_KONTROL(string isemri_no)
        {
            return infSusk_Dal.SUSK_KONTROL(isemri_no);
        }
        public void DinamikDepoHareket()
        {
            Kernel kernel = new Kernel();
            Sirket sirket = default(Sirket);
            DinamikDepo depo = default(DinamikDepo);
            DepoHareket hareket = default(DepoHareket);
            HucreHareketKalem harKalem = default(HucreHareketKalem);

            try
            {
                sirket = kernel.yeniSirket(TVTTipi.vtMSSQL,
                                             "INFORM20",
                                             "TEMELSET",
                                             "",
                                             "karamuklu",
                                             "12qw",
                                             1);
                depo = kernel.yeniDinamikDepo(sirket, TDepoIslemTipi.ditToplama, TDepoBelgeTipi.dbtDepolarArasiTransfer);
                depo.BelgeNumarasi = "A00000000000028";
                depo.CariKodu = "1";
                depo.KalemleriHazirla();
                hareket = depo.Hareket[0];
                hareket.YeniHucreHareketKalem("81 14 02");
                harKalem.NetMiktar = hareket.Miktar;
                harKalem.HareketTipi = TDepoHareketTipi.dhtNormal;
                depo.HareketleriIsle();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Marshal.ReleaseComObject(depo);
                Marshal.ReleaseComObject(hareket);
                Marshal.ReleaseComObject(harKalem);
                Marshal.ReleaseComObject(depo);
                Marshal.ReleaseComObject(sirket);
                kernel.FreeNetsisLibrary();
                Marshal.ReleaseComObject(kernel);

            }
        }
        public TBLISEMRIEK IsEmriEkalan_Guncelle(string isemriNo, int kulMiktar)
        {
            return infSusk_Dal.IsEmriEkalan_Guncelle(isemriNo, kulMiktar);
        }

    }
}
