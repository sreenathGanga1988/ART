﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArtWebApp.DataModelAtcWorld
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AtcWorldEntities : DbContext
    {
        public AtcWorldEntities()
             : base("name=AtcWorldEntities")
        {
        }
        public AtcWorldEntities(String EthiopiaConStr)
            : base("name=AtcWorldEthiopiaEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<LocationMaster_tbl> LocationMaster_tbl { get; set; }
        public virtual DbSet<PackingListDetailsAtc> PackingListDetailsAtcs { get; set; }
        public virtual DbSet<PackingListMasterAtcPro> PackingListMasterAtcProes { get; set; }
        public virtual DbSet<StyleSizeMaster> StyleSizeMasters { get; set; }
        public virtual DbSet<ASQShuffleDetailsAtc> ASQShuffleDetailsAtcs { get; set; }
        public virtual DbSet<ASQShuffleMasterAtc> ASQShuffleMasterAtcs { get; set; }
        public virtual DbSet<ArtJobContractMaster> ArtJobContractMasters { get; set; }
        public virtual DbSet<ArtCutPlanASQDet> ArtCutPlanASQDets { get; set; }
        public virtual DbSet<ArtCutPlanMarkerSizeDetail> ArtCutPlanMarkerSizeDetails { get; set; }
        public virtual DbSet<FabricRequest_tbl> FabricRequest_tbl { get; set; }
        public virtual DbSet<ArtLaySheetDetail> ArtLaySheetDetails { get; set; }
        public virtual DbSet<ArtLaySheetMasterData> ArtLaySheetMasterDatas { get; set; }
        public virtual DbSet<Fabricreqforpart> Fabricreqforparts { get; set; }
        public virtual DbSet<CmDozmaster> CmDozmasters { get; set; }
        public virtual DbSet<FactoryCostMaster> FactoryCostMasters { get; set; }
        public virtual DbSet<ASQAllocationMaster_tbl> ASQAllocationMaster_tbl { get; set; }
        public virtual DbSet<ArtJobContractOptionalMaster> ArtJobContractOptionalMasters { get; set; }
        public virtual DbSet<ArtAtcClosingMaster> ArtAtcClosingMasters { get; set; }
    }
}
