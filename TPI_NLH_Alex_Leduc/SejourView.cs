//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPI_NLH_Alex_Leduc
{
    using System;
    using System.Collections.Generic;
    
    public partial class SejourView
    {
        public int ID { get; set; }
        public string Pprenom { get; set; }
        public string Pnom { get; set; }
        public string Eprenom { get; set; }
        public string Enom { get; set; }
        public string NomDeptMed { get; set; }
        public string Type { get; set; }
        public Nullable<byte> PreDispo { get; set; }
        public Nullable<System.DateTime> DateDebut { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
        public Nullable<bool> Television { get; set; }
        public Nullable<bool> Telephone { get; set; }
        public Nullable<decimal> TotalFacture { get; set; }
        public int LitID { get; set; }
        public int ChambreID { get; set; }
    }
}
