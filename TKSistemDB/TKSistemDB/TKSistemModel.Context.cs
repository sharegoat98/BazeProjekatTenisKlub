﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TKSistemDB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TKSistemModelContainer : DbContext
    {
        public TKSistemModelContainer()
            : base("name=TKSistemModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FabrikaReketa> FabrikaReketas { get; set; }
        public virtual DbSet<Reket> Rekets { get; set; }
        public virtual DbSet<Zica> Zice { get; set; }
        public virtual DbSet<Igrac> Igraci { get; set; }
        public virtual DbSet<Klub> Klubovi { get; set; }
        public virtual DbSet<Trener> Treners { get; set; }
        public virtual DbSet<ima_ugovor> ima_ugovors { get; set; }
        public virtual DbSet<Turnir> Turnirs { get; set; }
    }
}
