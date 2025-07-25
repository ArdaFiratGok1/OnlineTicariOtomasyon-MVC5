﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Fatura
    {
        [Key]
        public int FaturaID { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(1)]
        public string FaturaSeriNo { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string FaturaSiraNo { get; set; }
        public DateTime FaturaTarih { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(60)]
        public string VergiDairesi { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(5)]
        public string Saat { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimEden { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimAlan { get; set; }

        public decimal Toplam { get; set; }
        public ICollection<FaturaKalem> FaturaKalems { get; set; }
    }
}