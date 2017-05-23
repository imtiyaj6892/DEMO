﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCGridView.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class OKEntities : DbContext
    {
        public OKEntities()
            : base("name=OKEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<PersonTable> PersonTables { get; set; }
        public DbSet<Product> Products { get; set; }
    
        public virtual ObjectResult<CrudOperations_Result> CrudOperations(Nullable<int> productid, string productname, Nullable<int> price, string status)
        {
            var productidParameter = productid.HasValue ?
                new ObjectParameter("productid", productid) :
                new ObjectParameter("productid", typeof(int));
    
            var productnameParameter = productname != null ?
                new ObjectParameter("productname", productname) :
                new ObjectParameter("productname", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(int));
    
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CrudOperations_Result>("CrudOperations", productidParameter, productnameParameter, priceParameter, statusParameter);
        }
    }
}
