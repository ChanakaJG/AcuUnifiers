using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.CR;
using System;
using PX.Data.BQL.Fluent;

namespace AcuUnifiers
{
    [PXCacheName("Vendor Merge Filter")]
    public class CDVendorMergeFilter : PXBqlTable, IBqlTable
    {
        #region VendorID
        public abstract class vendorID : PX.Data.BQL.BqlInt.Field<vendorID> { }
        [VendorActive(DisplayName = "Vendor",
            Visibility = PXUIVisibility.SelectorVisible,
            Required = true,
            DescriptionField = typeof(Vendor.acctName))]
        [PXDefault()]
        public virtual int? VendorID { get; set; }
        #endregion

        #region VendorLocationID
        public abstract class vendorLocationID : PX.Data.BQL.BqlInt.Field<vendorLocationID> { }
        [LocationActive(typeof(Where<Location.bAccountID, Equal<Current<CDVendorMergeFilter.vendorID>>,
            And<MatchWithBranch<Location.vBranchID>>>), DisplayName = "Vendor Location", DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible, Required = true)]
        [PXDefault(typeof(SearchFor<Location.locationID>.In<SelectFrom<Location>
            .Where<Location.bAccountID.IsEqual<CDVendorMergeFilter.vendorID.FromCurrent>
                .And<Location.isActive.IsEqual<True>
                    .And<Location.isDefault.IsEqual<True>>>>>))]
        [PXFormula(typeof(Default<CDVendorMergeFilter.vendorID>))]
        public virtual int? VendorLocationID { get; set; }
        #endregion

        #region VendorClassID
        public abstract class vendorClassID : PX.Data.BQL.BqlString.Field<vendorClassID> { }
        [PXDBString(10, IsUnicode = true)]
        [PXSelector(typeof(VendorClass.vendorClassID), DescriptionField = typeof(VendorClass.descr), CacheGlobal = true)]
        [PXUIField(DisplayName = "Vendor Class")]
        public virtual String VendorClassID { get; set; }
        #endregion
    }
}
