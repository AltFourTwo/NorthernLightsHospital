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
    
    public partial class Sejour
    {
        public int ID { get; set; }
        public Nullable<byte> PreDispo { get; set; }
        public int LitID { get; set; }
        public int PatientID { get; set; }
        public int MedID { get; set; }
        public System.DateTime DateDebut { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
        public Nullable<bool> Television { get; set; }
        public Nullable<bool> Telephone { get; set; }
        public Nullable<decimal> TotalFacture { get; set; }
    
        public virtual Lit Lit { get; set; }
        public virtual Medecin Medecin { get; set; }
        public virtual Patient Patient { get; set; }
    }
}