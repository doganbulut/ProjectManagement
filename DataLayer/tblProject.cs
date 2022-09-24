//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class tblProject
    {
        public tblProject()
        {
            this.DURUM_ID = 0;
            this.tblDetails = new HashSet<tblDetail>();
            this.tblTeams = new HashSet<tblTeam>();
        }

        [Display(Name = "Proje ID")]
        public int ID { get; set; }

        [Display(Name = "Birim")]
        public Nullable<int> UNIT_ID { get; set; }

        [Display(Name = "Fırsat Id")]
        public string FIRSAT_ID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Müşteri Adı")]
        public string MUSTERI_ADI { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Proje Adı")]
        public string PROJE_ADI { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Proje Açıklama")]
        public string PROJE_ACIKLAMA { get; set; }


        [Display(Name = "Durum")]
        [DefaultValue(0)]
        public int DURUM_ID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Yüzde Durum")]
        public int YUZDE_DURUM { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Proje Yöneticisi")]
        public string PROJE_YONETICISI { get; set; }


        [Display(Name = "Teknik Sorumlu")]
        public string TEKNIK_SORUMLU { get; set; }


        [Display(Name = "Proje Kabul ve Ödeme Şartları")]
        public string ODEME_KOSULLARI { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Proje Ekibi")]
        public string PROJE_EKIBI { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Üretici Firma")]
        public string URETICI_FIRMA { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Satıştan Hizmete Devir Durumu")]
        public bool DEVIR_DURUM { get; set; }


        [Display(Name = "İstekte Bulunan")]
        public string TALEPTE_BULUNAN_PERSONEL { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "Proje Bedeli")]
        public double PROJE_BEDELI { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "Planlanan Başlangıç Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime PLANLANAN_BASLANGIC_TARIH { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "Planlanan Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime PLANLANAN_BITIS_TARIH { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "Gerçekleşen Başlangıç Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public System.DateTime GERCEKLESEN_BASLANGIC_TARIH { get; set; }


        [Display(Name = "Gerçekleşen Bitiş Tarihi")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> GERCEKLESEN_BITIS_TARIH { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Teknik Planlanan Efor (Adam gün)")]

        public int TEKNİK_PLAN { get; set; }


        [Display(Name = "Npv Planlanan Efor (Adam gün)")]
        public int NPV_PLAN_EFOR { get; set; }


        [Display(Name = "Gerçekleşen Efor (Adam gün)")]
        public string GERCEKLESEN_EFOR { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Proje Tarih Sapma")]
        public Nullable<int> PROJE_TARIH_SAPMA { get; set; }

        [Display(Name = "Proje Efor Sapma")]
        public Nullable<int> PROJE_EFOR_SAPMA { get; set; }


        [Display(Name = "Toplam Uç Sayısı")]
        public Nullable<int> TOPLAM_UC_SAYISI { get; set; }


        [Display(Name = "Kuruluş Uç Sayısı")]

        public Nullable<int> KURULUS_UC_SAYISI { get; set; }

        [Display(Name = "Proje Bedeli Para Birimi")]
        public string PARA_BIRIMI { get; set; }

        [Display(Name = "Şirket İçi")]
        public bool SIRKET_ICI { get; set; }
        public virtual ICollection<tblDetail> tblDetails { get; set; }

        public virtual tblUnit tblUnit { get; set; }

        public virtual ICollection<tblTeam> tblTeams { get; set; }

    }
}