using INF_SUSK_Project_BusinessLayer;
using INF_SUSK_Project_Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using static INF_SUSK_Project_BusinessLayer.INF_SUSK_Manager;



namespace INF_SUSK_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public INF_SUSK_Manager inf_SUSK_Manager = new INF_SUSK_Manager();
        public List<SUSKONCESI_HAZIRLIK_MKA> transferListesi = new List<SUSKONCESI_HAZIRLIK_MKA>();
        public List<SUSKONCESI_HAZIRLIK_MKA> yariMamulList = new List<SUSKONCESI_HAZIRLIK_MKA>();
        public List<YARIMAMUL> yariMamuller = new List<YARIMAMUL>();
        public List<YARIMAMULISEMRIMKA> yariMamulIsemirleri = new List<YARIMAMULISEMRIMKA>();
        public List<SUSK_LISTESI> Anamamul_GenelList = new List<SUSK_LISTESI>();
        public List<TBLISEMRI_MKA> acilacakIsemirleri = new List<TBLISEMRI_MKA>();

        //ilk gelen SUSK listesi program sonuna kadar tutulacak
        private void btnExcelOku_Click(object sender, EventArgs e)
        {
            Anamamul_GenelList.Clear();
            OpenFileDialog OFD = new OpenFileDialog()
            {
                Filter = "Excel Dosyası |*.xlsx| Excel Dosyası|*.xls| Excel Dosyası|*.xlsm",
                // open file dialog açıldığında sadece excel dosyalarınu görecek
                Title = "Excel Dosyası Seçiniz..",
                // open file dialog penceresinin başlığı
                RestoreDirectory = true,
                // en son açtığı klasörü gösterir. Örn en son excel dosyasını D://Exceller adlı
                // bir klasörden çekmiş olsun. Bir sonraki open file dialog açıldığında yine aynı 
                // klasörü gösterecektir.
            };
            // bu da bir kullanım şeklidir. Aslında bu şekilde kullanmak daha faydalıdır. 
            // bir çok intelligence aracı bu şekilde kullanılmasını tavsiye ediyor.
            if (OFD.ShowDialog() == DialogResult.OK)
            // perncere açıldığında dosya seçildi ise yapılacak. Bunu yazmazsak dosya seçmeden 
            // kapandığında program kırılacaktır.
            {
                var DosyaYolu = OFD.FileName;// dosya yolu
                var DosyaAdi = OFD.SafeFileName; // dosya adı

                OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + DosyaYolu + "; Extended Properties='Excel 12.0 xml;HDR=YES;'");
                // excel dosyasına access db gibi bağlanıyoruz.
                baglanti.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [Sheet2$]", baglanti);
                // burada FROM dan sonra sayfa1$ kısmı önemlidir.sayfa adı faklı ise örn
                // sheet ise program hata verecektir.
                // NOT: Excel dosyanızın ilk satır başlık olsun. Yani sistem öyle algıladığından 
                // ilk satırdaki bilgileri başlık olarak tanımlayıp almıyor. Ne yazarsanız yazın
                // sorun teşkil etmiyor. Tabi db için özel olan karakterleri kullanmayın.
                DataTable DTexcel = new DataTable();
                da.Fill(DTexcel);
                // select sorgusu ile okunan verileri datatable'ye aktarıyoruz.
                dgvListe.Columns.Clear();
                dgw_Ekran.DataSource = DTexcel;
                // datatable'ı da gridcontrol'ün datasource'una atıyoruz.

                baglanti.Close();
                // bağlantıyı kapatıyoruz.
            }

            label2.Text = dgvListe.RowCount.ToString();

            for (int i = 0; i < dgvListe.RowCount; i++)
            {
                SUSK_LISTESI suskEleman = new SUSK_LISTESI();
                suskEleman.TARIH = Convert.ToDateTime(dgvListe.GetRowCellValue(i, "TARIH"));
                suskEleman.ISEMRINO = (dgvListe.GetRowCellValue(i, "ISEMRINO").ToString());
                suskEleman.STOK_KODU = (dgvListe.GetRowCellValue(i, "STOK_KODU").ToString());
                suskEleman.MIKTAR = Convert.ToDecimal(dgvListe.GetRowCellValue(i, "MIKTAR"));
                suskEleman.ACIKLAMA = (dgvListe.GetRowCellValue(i, "ACIKLAMA").ToString());
                if (dgvListe.GetRowCellValue(i, "SERI_NO") == null)
                    suskEleman.SERI_NO = "";
                else
                    suskEleman.SERI_NO = (dgvListe.GetRowCellValue(i, "SERI_NO").ToString());
                Anamamul_GenelList.Add(suskEleman);
            }

            //foreach (var item in susk_GenelList)
            //{
            //    var kontrol = inf_SUSK_Manager.SUSK_KONTROL(item.ISEMRINO);
            //    if (kontrol.BAKIYE==0)
            //    {
            //        AutoClosingMessageBox.Show("Daha önceden SUSK Yapılmıştır. Netsisten kontrol ediniz.", "Sistem Uyarısı", 3000);
            //        //this.Close();
            //    }
            //}
            //btnSUSK_Kaydet.Enabled = true;
            //btnExcelOku.Enabled = false;
            btnDurumKontrol.Visible = true;
            txtIsemriNo.Visible = true;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Text = "Serbest Üretim Sonu Kaydı v.2.1.3 - İnform Elektronik...   Aktif Şirket:" + ConfigurationManager.AppSettings["SIRKET"].ToString();
            //btnSUSK_Kaydet.Enabled = false;
            //btnSUSK_Kaydet.Visible = false;
            //txtSUSK.Visible = false;
            //label1.Visible = false;
            btnDepoTransfer.Visible = false;
            btnDurumKontrol.Visible = false;
            txtIsemriNo.Visible = true;
            txtParola.Visible = false;
            btnAspirin.Visible = false;
            btnYariMamulSUSK.Visible = false;
            //txtSUSK.Visible = false;
            //btnSUSK_Kaydet.Visible = false;
            txtSeriNo.Visible = false;
            label1.Visible = false;

            Anamamul_GenelList.Clear();
        }

        private void btnSUSK_Kaydet_Click(object sender, EventArgs e)
        {
            if (txtSUSK.Text == "")
            {
                AutoClosingMessageBox.Show("SUSK için harf girmelisiniz..!", "SUSK Kontrol", 2000);
            }
            else
            {
                List<SUSK_LISTESI> anaMamulSusk = new List<SUSK_LISTESI>();
                foreach (var item in Anamamul_GenelList)
                {
                    SUSK_LISTESI suskEleman = new SUSK_LISTESI();
                    suskEleman.ISEMRINO = item.ISEMRINO;
                    suskEleman.STOK_KODU = item.STOK_KODU;
                    suskEleman.MIKTAR = item.MIKTAR;
                    suskEleman.TARIH = item.TARIH;
                    suskEleman.SUSKDEPOGIRIS = Convert.ToInt32(txtSUSKGirisDepo.Text);
                    suskEleman.SUSKDEPOCIKIS = Convert.ToInt32(txtSUSKCikisDepo.Text);
                    suskEleman.SERI_NO = item.SERI_NO;
                    anaMamulSusk.Add(suskEleman);
                }
                foreach (var item in anaMamulSusk)
                {
                    if (true)
                    {
                        inf_SUSK_Manager.SUSK_Kaydet(item, txtSUSK.Text);
                    }
                }
            }
            // prgsBar.Value = 100;
        }

        private void btnDepoTransfer_Click(object sender, EventArgs e)
        {
            AutoClosingMessageBox.Show("Hammadde Depo Transfer işlemi başlatılacaktır...", "Depo Transfer Kontrol", 1000);
            foreach (var item in Anamamul_GenelList)
            {
                var list = inf_SUSK_Manager.Hammadde_Bul(item.STOK_KODU, item.ISEMRINO, item.MIKTAR, item.TARIH);
                transferListesi.AddRange(list);
            }

            dgvListe.Columns.Clear();//Grid temizleme işlemi, yoksa doğru çalışmıyor.
            dgw_Ekran.DataSource = transferListesi;
            btnDepoTransfer.Text = "Depo Transferi Yap";
            AutoClosingMessageBox.Show("Sistem Transfere hazırdır, Depo Transfer işlemi başlatılacak..!", "Depo Transfer Kontrol", 2000);

            List<SUSKONCESI_HAZIRLIK_MKA> suskOnList = new List<SUSKONCESI_HAZIRLIK_MKA>();
            foreach (var item in transferListesi)
            {
                SUSKONCESI_HAZIRLIK_MKA suskEleman = new SUSKONCESI_HAZIRLIK_MKA();
                suskEleman.STOK_KODU = item.STOK_KODU;
                suskEleman.TRANSFERMIKTAR = item.TRANSFERMIKTAR;
                suskEleman.BAKIYE114 = item.BAKIYE114;
                suskEleman.BAKIYE115 = item.BAKIYE115;
                suskEleman.BAKIYE117 = item.BAKIYE117;
                suskEleman.GIRISDEPOKODU = Convert.ToInt32(txtSUSKGirisDepo.Text);
                suskEleman.CIKISDEPOKODU = Convert.ToInt32(txtSUSKCikisDepo.Text);
                suskEleman.REFISEMRINO = item.REFISEMRINO;
                suskOnList.Add(suskEleman);
            }
            if (suskOnList.Count != 0)
            {
                //inf_SUSK_Manager.LokalDepolarArasiTransferBelgesi(suskOnList);//Depo transferini yapan kod
                AutoClosingMessageBox.Show("Depo transferi tamamlanmıştır..!", "Depo Transfer İşlemi", 1000);
            }
            else
            {
                AutoClosingMessageBox.Show("Hammaddelerin daha önceden transferi tamamlanmıştır..!", "Depo Transfer İşlemi", 1000);
            }

            //bu aşamada anamamul altındaki yarı mamuller bulunur
            //sonra yarımamul işemirleri ana mamule bağlantılı olanlar bulunur

            label2.Text = dgvListe.RowCount.ToString();
            //btnDepoTransfer.Visible = true;
            //btnDurumKontrol.Enabled = false;
            //btnYariMamulSUSK.Visible = true;
            ////btnDepoTransfer.Enabled = false;
            //btnSUSK_Kaydet.Visible = true;
        }

        private void btnDurumKontrol_Click(object sender, EventArgs e)
        {

            yariMamulList.Clear();

            //List<string> isemirleri = new List<string>();
            //for (int i = 0; i < dgvListe.RowCount; i++)
            //{
            //    DataRow row = dgvListe.GetDataRow(i);
            //    isemirleri.Add("'" + (row.ItemArray[1].ToString()) + "'");
            //}

            //dgw_Ekran.DataSource= inf_SUSK_Manager.ACILANISEMIRLERI(isemirleri);

            //ilk kontrol->Anamamül işemrino SUSK tablosunda var mı

            //Önce yarımamüller 115ten 118 e çek, sonra eksik miktar kadar işemri aç.
            //Hammaddeleri depo transferi yap,
            //Yarımamül SUSK  yap ->118
            //en son Anamamul SUSK yap
            //AutoClosingMessageBox.Show("Yarımamul işemirleri kontrol ediliyor..!", "İşemri Kontrol", 3000);

            if (txtIsemriNo.Text == "İşemri No Giriniz")
            {
                //MessageBox.Show("İşemri başlangıç harfini giriniz..!");
                AutoClosingMessageBox.Show("İşemri başlangıç harfini giriniz..!", "İşemri Kontrol", 2000);
            }
            else
            {
                foreach (var anaMamul in Anamamul_GenelList)
                {
                    var list = inf_SUSK_Manager.YariMamul_Bul(anaMamul.STOK_KODU, anaMamul.ISEMRINO, anaMamul.MIKTAR, anaMamul.TARIH,5,anaMamul.ACIKLAMA);
                    yariMamulList.AddRange(list);
                }

                var grupYariMamul = yariMamulList.GroupBy(a => a.STOK_KODU).Select(y => new YARIMAMULGRUP
                {
                    STOK_KODU =y.First().STOK_KODU,
                    REFISEMRINO=y.First().REFISEMRINO,
                    TARIH=y.First().TARIH,
                    ANAMAMUL=y.First().ANAMAMUL,
                    MIKTAR = y.Sum(x => x.TRANSFERMIKTAR),
                    ACIKLAMA=y.First().ACIKLAMA
                });

                dgvListe.Columns.Clear();
                dgw_Ekran.DataSource = grupYariMamul;
                label2.Text = dgvListe.RowCount.ToString();

                //List<YARIMAMULGRUP> result = yariMamulList.GroupBy(l => l.STOK_KODU).Select(cl => new YARIMAMULGRUP
                //{
                //    STOK_KODU = cl.First().STOK_KODU,
                //    MIKTAR = cl.Sum(x => x.TRANSFERMIKTAR)
                //}).ToList();
                //dgvListe.Columns.Clear();
                //dgw_Ekran.DataSource = result;
                //label2.Text = dgvListe.RowCount.ToString();

                //yarıMamulList->Genel Liste
                //Yarımamullerin transferini yaptık, sonrasınra 118=0 olanlar için işemri açılacak
                //115 in hepsini neden transfer ettik ?

                //yarımamulleri tekrar transfer etmek isterken, içerde aynı refisemri nolu depo transfer fişi var mı bak
                //List<SUSKONCESI_HAZIRLIK_MKA> yariMamulTrnsfrList = new List<SUSKONCESI_HAZIRLIK_MKA>();

                //foreach (var item in yariMamulList)
                //{
                //    if (item.BAKIYE117 > 0 && (item.SEVIYE == 1 || item.SEVIYE == 2 || item.SEVIYE == 3 || item.SEVIYE == 4))
                //    {
                //        SUSKONCESI_HAZIRLIK_MKA yariMamulSuskHz = new SUSKONCESI_HAZIRLIK_MKA();
                //        yariMamulSuskHz.STOK_KODU = item.STOK_KODU;
                //        yariMamulSuskHz.GIRISDEPOKODU = Convert.ToInt32(txtSUSKGirisDepo.Text);
                //        yariMamulSuskHz.CIKISDEPOKODU = Convert.ToInt32(txtSUSKCikisDepo.Text);


                //        if (item.TRANSFERMIKTAR > item.BAKIYE117)
                //        {
                //            yariMamulSuskHz.TRANSFERMIKTAR = item.BAKIYE117;
                //        }
                //        else
                //        {
                //            yariMamulSuskHz.TRANSFERMIKTAR = item.TRANSFERMIKTAR;
                //        }

                //        yariMamulSuskHz.REFISEMRINO = item.REFISEMRINO;
                //        yariMamulSuskHz.BAKIYE117 = item.BAKIYE117;
                //        yariMamulTrnsfrList.Add(yariMamulSuskHz);
                //    }
                //}


                //if (yariMamulTrnsfrList.Count == 0)
                //{
                //    AutoClosingMessageBox.Show("Yarımamüller depoda bulunamamıştır. Sistemde ihtiyaç kadar işemri açılacaktır.", "Yarı Mamul Kontrol", 2000);
                //}
                //else
                //{
                //    //yarımamülleri transfer ederken ekrandaki text boxları kullan
                //    AutoClosingMessageBox.Show("Yarımamüllerin depo transferi yapılacaktır.", "YarıMamul Transfer", 2000);
                //    inf_SUSK_Manager.LokalDepolarArasiTransferBelgesi(yariMamulTrnsfrList);
                //    AutoClosingMessageBox.Show("Yarımamüllerin depo transferi yapılmıştır.", "Yarı Mamul Kontrol", 2000);//Otomatiğe çevrilecek
                //}


                //var listIsemriOlmayanlar = new List<YARIMAMULISEMRIMKA>();
                //foreach (var anaMamul in Anamamul_GenelList)
                //{
                //    var list2 = inf_SUSK_Manager.YARIMAMUL_ISEMRIBUL(anaMamul.STOK_KODU, anaMamul.ISEMRINO, anaMamul.MIKTAR, anaMamul.TARIH);
                //    listIsemriOlmayanlar.AddRange(list2);
                //}
                //if (listIsemriOlmayanlar.Count != 0)
                //{
                //    AutoClosingMessageBox.Show("Açılması gereken " + listIsemriOlmayanlar.Count + " adet işemri mevcuttur. İşemirleri otomatik olarak açılacaktır..!", "İşemri Kontrol", 2000);

                //    dgvListe.Columns.Clear();//Grid temizleme işlemi, yoksa doğru çalışmıyor.
                //    dgw_Ekran.DataSource = listIsemriOlmayanlar;
                //    label2.Text = dgw_Ekran.DefaultView.RowCount.ToString();


                foreach (var item in grupYariMamul)
                 {
                     TBLISEMRI_MKA isemri = new TBLISEMRI_MKA();
                     isemri.ISEMRINO = inf_SUSK_Manager.ISEMRINO_OLUSTUR(txtIsemriNo.Text);
                     isemri.STOK_KODU = item.STOK_KODU;
                     isemri.TARIH = DateTime.Now;
                     isemri.MIKTAR = item.MIKTAR;
                     isemri.TESLIM_TARIHI = item.TARIH.Date;//excelden gelen tarih alınacak
                     isemri.ACIKLAMA = item.ACIKLAMA;
                     isemri.ANAMAMUL = item.ANAMAMUL; //Yarımamuller gruplanıp toplandığı içinde bağlantı kısmı pasif yapıldı.
                     isemri.REFISEMRINO = item.REFISEMRINO;

                     inf_SUSK_Manager.ACILACAK_ISEMIRLERI(isemri);//işemri açan kod
                     //inf_SUSK_Manager.IsEmriEkalan_Guncelle(isemri.ISEMRINO, Convert.ToInt32(item.BAKIYE118KULLANILAN));
                     AutoClosingMessageBox.Show(isemri.ISEMRINO + " nolu işemri açılmıştır.", "İşemri Kontrol", 1000);
                     acilacakIsemirleri.Add(isemri);
                 }

                //    AutoClosingMessageBox.Show("Sistemde " + acilacakIsemirleri.Count + " işemri açılmıştır.", "Otomatik açılan işemirleri", 2000);
                //}

                //else
                //{
                //    AutoClosingMessageBox.Show("Yarımamül işemirleri daha önceden açılmıştır.", "İşemri Kontrol", 2000);
                //}


                //dgvListe.Columns.Clear();//Grid temizleme işlemi, yoksa doğru çalışmıyor.
                //dgw_Ekran.DataSource = yariMamulIsemirleri;
                //label2.Text = dgvListe.RowCount.ToString();
                //btnDepoTransfer.Visible = true;
                //btnDurumKontrol.Enabled = false;
                //btnYariMamulSUSK.Visible = true;
                //btnSUSK_Kaydet.Visible = true;

                yariMamulList.Clear();
            }
        }

        private void btnYariMamulSUSK_Click(object sender, EventArgs e)
        {
            if (txtSUSKCikisDepo.Text == "118")
            {
                List<SUSK_LISTESI> yariMamulSuskList = new List<SUSK_LISTESI>();
                foreach (var anaMamul in Anamamul_GenelList)
                {
                    var list = inf_SUSK_Manager.YARIMAMULSUSK_LISTESI(anaMamul.STOK_KODU, anaMamul.ISEMRINO, anaMamul.MIKTAR, anaMamul.TARIH);
                    yariMamulSuskList.AddRange(list);


                    List<SUSK_LISTESI> yariMamulSuskListDepolu = new List<SUSK_LISTESI>();
                    foreach (var item in yariMamulSuskList)
                    {
                        SUSK_LISTESI suskEleman = new SUSK_LISTESI();
                        suskEleman.ISEMRINO = item.ISEMRINO;
                        suskEleman.STOK_KODU = item.STOK_KODU;
                        suskEleman.MIKTAR = item.MIKTAR;
                        suskEleman.TARIH = item.TARIH;
                        suskEleman.SUSKDEPOGIRIS = Convert.ToInt32(txtSUSKGirisDepo.Text);
                        suskEleman.SUSKDEPOCIKIS = Convert.ToInt32(txtSUSKGirisDepo.Text);
                        suskEleman.REFISEMRINO = item.REFISEMRINO;
                        yariMamulSuskListDepolu.Add(suskEleman);
                    }
                    dgw_Ekran.DataSource = yariMamulSuskListDepolu;
                    label2.Text = dgvListe.RowCount.ToString();
                    AutoClosingMessageBox.Show("SUSK işlemi başlatılacaktır..!", "SUSK Uyarı", 2000);

                    foreach (var item in yariMamulSuskListDepolu)
                    {
                        inf_SUSK_Manager.SUSK_Kaydet(item, txtSUSK.Text);
                    }
                }
            }
            else
            {
                MessageBox.Show("Depo Kodu 118 olmalı");
            }
        }

        private void txtIsemriNo_Enter(object sender, EventArgs e)
        {
            if (txtIsemriNo.Text == "İşemri için Harf Giriniz")
                txtIsemriNo.Text = "";
        }

        private void txtIsemriNo_Leave(object sender, EventArgs e)
        {
            if (txtIsemriNo.Text == "")
                txtIsemriNo.Text = "İşemri için Harf Giriniz";
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            txtParola.Visible = true;
        }

        private void txtParola_TextChanged(object sender, EventArgs e)
        {
            if (txtParola.Text == "bmw")
            {
                btnAspirin.Visible = true;
            }
        }

        private void btnAspirin_Click(object sender, EventArgs e)
        {

        }

        private void txtIsemriBarkod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtIsemriNo.Text == "İşemri için Harf Giriniz")
                {
                    MessageBox.Show("İşemri için Harf Girmelisiniz");
                }
                else
                {
                    TBLISEMRI anaMamulisemri = new TBLISEMRI();
                    anaMamulisemri = inf_SUSK_Manager.IsEmriGetir(txtIsemriBarkod.Text);

                    SUSK_LISTESI suskEleman = new SUSK_LISTESI();
                    suskEleman.ISEMRINO = anaMamulisemri.ISEMRINO;
                    suskEleman.STOK_KODU = anaMamulisemri.STOK_KODU;
                    suskEleman.MIKTAR = anaMamulisemri.MIKTAR;
                    suskEleman.TARIH = anaMamulisemri.TARIH;
                    suskEleman.SUSKDEPOGIRIS = Convert.ToInt32(txtSUSKGirisDepo.Text);
                    suskEleman.SUSKDEPOCIKIS = Convert.ToInt32(txtSUSKCikisDepo.Text);

                    Anamamul_GenelList.Add(suskEleman);


                    if (txtIsemriNo.Text == "İşemri Barkod Okutunuz")
                    {
                        //MessageBox.Show("İşemri başlangıç harfini giriniz..!");
                        AutoClosingMessageBox.Show("İşemri başlangıç harfini giriniz..!", "İşemri Kontrol", 2000);
                    }
                    else
                    {
                        foreach (var yariMamul in Anamamul_GenelList)
                        {
                            var list = inf_SUSK_Manager.YariMamul_Bul(yariMamul.STOK_KODU, yariMamul.ISEMRINO, yariMamul.MIKTAR, yariMamul.TARIH,2, yariMamul.ACIKLAMA);
                            yariMamulList.AddRange(list);
                        }

                        //yarıMamulList->Genel Liste
                        //Yarımamullerin transferini yaptık, sonrasınra 118=0 olanlar için işemri açılacak
                        //115 in hepsini neden transfer ettik ?

                        //yarımamulleri tekrar transfer etmek isterken, içerde aynı refisemri nolu depo transfer fişi var mı bak
                        List<SUSKONCESI_HAZIRLIK_MKA> yariMamulTrnsfrList = new List<SUSKONCESI_HAZIRLIK_MKA>();
                        foreach (var item in yariMamulList)
                        {
                            if (item.BAKIYE117 >= item.TRANSFERMIKTAR && (item.SEVIYE == 1 || item.SEVIYE == 2 || item.SEVIYE == 3 || item.SEVIYE == 4))
                            {
                                SUSKONCESI_HAZIRLIK_MKA yariMamulSuskHz = new SUSKONCESI_HAZIRLIK_MKA();
                                yariMamulSuskHz.STOK_KODU = item.STOK_KODU;
                                yariMamulSuskHz.GIRISDEPOKODU = Convert.ToInt32(txtSUSKGirisDepo.Text);
                                yariMamulSuskHz.CIKISDEPOKODU = Convert.ToInt32(txtSUSKCikisDepo.Text);
                                yariMamulSuskHz.TRANSFERMIKTAR = item.TRANSFERMIKTAR;
                                yariMamulSuskHz.REFISEMRINO = item.REFISEMRINO;
                                yariMamulSuskHz.BAKIYE117 = item.BAKIYE117;
                                yariMamulTrnsfrList.Add(yariMamulSuskHz);
                            }
                        }

                        if (yariMamulTrnsfrList.Count == 0)
                        {
                            AutoClosingMessageBox.Show("Yarımamüller depoda bulunamamıştır. Sistemde ihtiyaç kadar işemri açılacaktır.", "Yarı Mamul Kontrol", 2000);
                        }
                        else
                        {
                            //yarımamülleri transfer ederken ekrandaki text boxları kullan
                            AutoClosingMessageBox.Show("Yarımamüllerin depo transferi yapılacaktır.", "YarıMamul Transfer", 2000);
                            inf_SUSK_Manager.LokalDepolarArasiTransferBelgesi(yariMamulTrnsfrList);
                            AutoClosingMessageBox.Show("Yarımamüllerin depo transferi yapılmıştır.", "Yarı Mamul Kontrol", 2000);//Otomatiğe çevrilecek
                        }
                        var listIsemriOlmayanlar = new List<YARIMAMULISEMRIMKA>();
                        foreach (var item in Anamamul_GenelList)
                        {
                            var list = inf_SUSK_Manager.YARIMAMUL_ISEMRIBUL(item.STOK_KODU, item.ISEMRINO, item.MIKTAR, item.TARIH);
                            listIsemriOlmayanlar.AddRange(list);
                        }
                        if (listIsemriOlmayanlar.Count != 0)
                        {
                            AutoClosingMessageBox.Show("Açılması gereken " + listIsemriOlmayanlar.Count + " adet işemri mevcuttur. İşemirleri otomatik olarak açılacaktır..!", "İşemri Kontrol", 2000);

                            dgvListe.Columns.Clear();//Grid temizleme işlemi, yoksa doğru çalışmıyor.
                            dgw_Ekran.DataSource = listIsemriOlmayanlar;
                            label2.Text = dgw_Ekran.DefaultView.RowCount.ToString();
                            foreach (var item in listIsemriOlmayanlar)
                            {
                                TBLISEMRI_MKA isemri = new TBLISEMRI_MKA();
                                isemri.ISEMRINO = inf_SUSK_Manager.ISEMRINO_OLUSTUR(txtIsemriNo.Text);
                                isemri.STOK_KODU = item.STOK_KODU;
                                isemri.TARIH = DateTime.Now.Date;//excelden gelen tarih alınacak
                                isemri.MIKTAR = item.ACILACAK_ISEMRIMIKTAR;
                                isemri.TESLIM_TARIHI = DateTime.Now.Date;
                                //isemri.ACIKLAMA = "B";
                                isemri.ANAMAMUL = item.ANAMAMUL;
                                isemri.REFISEMRINO = item.REFISEMRINO;

                                inf_SUSK_Manager.ACILACAK_ISEMIRLERI(isemri);//işemri açan kod
                                AutoClosingMessageBox.Show(isemri.ISEMRINO + " nolu işemri açılmıştır.", "İşemri Kontrol", 1000);
                                acilacakIsemirleri.Add(isemri);
                            }

                            AutoClosingMessageBox.Show("Sistemde " + acilacakIsemirleri.Count + " işemri açılmıştır.", "Otomatik açılan işemirleri", 2000);
                        }
                        else
                        {
                            AutoClosingMessageBox.Show("Yarımamül işemirleri daha önceden açılmıştır.", "İşemri Kontrol", 2000);
                        }
                        //dgvListe.Columns.Clear();//Grid temizleme işlemi, yoksa doğru çalışmıyor.
                        //dgw_Ekran.DataSource = yariMamulIsemirleri;
                        //label2.Text = dgvListe.RowCount.ToString();
                        //btnDepoTransfer.Visible = true;
                        //btnDurumKontrol.Enabled = false;
                        //btnYariMamulSUSK.Visible = true;
                        //btnSUSK_Kaydet.Visible = true;
                    }
                }

            }
        }

        private void txtIsemriBarkod_Enter(object sender, EventArgs e)
        {
            if (txtIsemriBarkod.Text == "İşemri Barkod Okutunuz")
                txtIsemriBarkod.Text = "";
        }

        private void txtIsemriBarkod_Leave(object sender, EventArgs e)
        {
            if (txtIsemriBarkod.Text == "")
                txtIsemriBarkod.Text = "İşemri Barkod Okutunuz";
        }

        private void txtSeriNo_Leave(object sender, EventArgs e)
        {
            if (txtSeriNo.Text == "")
                txtSeriNo.Text = "Seri No Okutunuz";

        }

        private void txtSeriNo_Enter(object sender, EventArgs e)
        {
            if (txtSeriNo.Text == "Seri No Okutunuz")
                txtSeriNo.Text = "";
        }

        private void txtSeriNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtIsemriNo.Text == "Seri No Okutunuz")
                {
                    MessageBox.Show("Seri No Okutunuz");

                }
                else
                {

                }
            }
        }

        private void btnDirekIsemriAc_Click(object sender, EventArgs e)
        {
            List<TBLISEMRI> acilacakIsemriList = new List<TBLISEMRI>();

            for (int i = 0; i < dgvListe.RowCount; i++)
            {
                TBLISEMRI isemri = new TBLISEMRI();
                isemri.TARIH = Convert.ToDateTime(dgvListe.GetRowCellValue(i, "TARIH"));
                isemri.ISEMRINO = (dgvListe.GetRowCellValue(i, "ISEMRINO").ToString());
                isemri.STOK_KODU = (dgvListe.GetRowCellValue(i, "STOK_KODU").ToString());
                isemri.MIKTAR = Convert.ToDecimal(dgvListe.GetRowCellValue(i, "MIKTAR"));
                isemri.TESLIM_TARIHI= Convert.ToDateTime(dgvListe.GetRowCellValue(i, "TESLIM_TARIHI"));
                isemri.ACIKLAMA = (dgvListe.GetRowCellValue(i, "ACIKLAMA").ToString());
                isemri.SIPARIS_NO = (dgvListe.GetRowCellValue(i, "SIPARIS_NO").ToString());

                if ((dgvListe.GetRowCellValue(i, "SIRA").ToString() == ""))
                    isemri.SIRA = 0;
                else
                    isemri.SIRA = Convert.ToInt32(dgvListe.GetRowCellValue(i, "SIRA").ToString() == "");

                acilacakIsemriList.Add(isemri);
            }
            inf_SUSK_Manager.DIREKACILACAK_ISEMIRLERI(acilacakIsemriList);
        }

        private void ilkSeviyeIsemriAc_Click(object sender, EventArgs e)
        {
            foreach (var anaMamul in Anamamul_GenelList)
            {
                var list = inf_SUSK_Manager.YariMamul_Bul(anaMamul.STOK_KODU, anaMamul.ISEMRINO, anaMamul.MIKTAR, anaMamul.TARIH, 2,anaMamul.ACIKLAMA);//buradaki 2 seviye anlamına geliyor, seviye 2 nin altı açılıyor.
                yariMamulList.AddRange(list);
            }
            
            foreach (var item in yariMamulList)
            {
                TBLISEMRI_MKA isemri = new TBLISEMRI_MKA();
                isemri.ISEMRINO = inf_SUSK_Manager.ISEMRINO_OLUSTUR(txtIsemriNo.Text);
                isemri.STOK_KODU = item.STOK_KODU;
                isemri.TARIH = DateTime.Now;
                isemri.MIKTAR = item.TRANSFERMIKTAR;
                isemri.TESLIM_TARIHI = item.TARIH.Date;//excelden gelen tarih alınacak
                //isemri.ACIKLAMA = "B";
                isemri.ANAMAMUL = item.ANAMAMUL; //Yarımamuller gruplanıp toplandığı içinde bağlantı kısmı pasif yapıldı.
                isemri.REFISEMRINO = item.REFISEMRINO;

                inf_SUSK_Manager.ACILACAK_ISEMIRLERI(isemri);//işemri açan kod
                //inf_SUSK_Manager.IsEmriEkalan_Guncelle(isemri.ISEMRINO, Convert.ToInt32(item.BAKIYE118KULLANILAN));
                AutoClosingMessageBox.Show(isemri.ISEMRINO + " nolu işemri açılmıştır.", "İşemri Kontrol", 1000);
                acilacakIsemirleri.Add(isemri);
            }

            dgw_Ekran.DataSource = acilacakIsemirleri;
        }
    }
}







