//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BrandedKart.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TransactionDetail
    {
        public string NameOnCard { get; set; }
        public short CVV { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public long CardNumber { get; set; }
        public int TransactionId { get; set; }
        public int orderId { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
