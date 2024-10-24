using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;

namespace AcuUnifiers
{
    [PXCacheName("Vendor Location Detail")]
    [PXProjection(typeof(SelectFrom<Vendor>), Persistent = false)]
    public class CDVendorLocationDetail : PXBqlTable, IBqlTable
    {
        public static bool IsActive() => true;

        #region Selected
        public abstract class selected : BqlBool.Field<selected> { }
        [PXDBBool]
        [PXUIField()]
        public bool? Selected { get; set; }
        #endregion

        #region BAccountID
        public abstract class bAccountID : BqlInt.Field<bAccountID> { }

        /// <inheritdoc cref="BAccount.BAccountID"/>
        [PXDBInt(IsKey = true, BqlField = typeof(Vendor.bAccountID))]
        public int? BAccountID { get; set; }
        #endregion

        #region AcctCD
        public abstract class acctCD : PX.Data.BQL.BqlString.Field<acctCD> { }

        /// <inheritdoc cref="Vendor.AcctCD"/>
        [VendorRaw(BqlField = typeof(Vendor.acctCD))]
        public string AcctCD { get; set; }
        #endregion

        #region VendorLocationID
        public abstract class vendorLocationID : PX.Data.BQL.BqlInt.Field<vendorLocationID> { }
        [LocationActive(typeof(Where<Location.bAccountID, Equal<Current<CDVendorLocationDetail.bAccountID>>,
            And<MatchWithBranch<Location.vBranchID>>>), DescriptionField = typeof(Location.descr), Visibility = PXUIVisibility.SelectorVisible, Enabled = true)]
        [PXDefault(typeof(SearchFor<Location.locationID>.
            In<SelectFrom<Location>.Where<Location.bAccountID.IsEqual<CDVendorLocationDetail.bAccountID.FromCurrent>
                .And<Location.isActive.IsEqual<True>.And<Location.isDefault.IsEqual<True>>>>>))]
        public virtual int? VendorLocationID { get; set; }
        #endregion

        #region VendorClassID
        public abstract class vendorClassID : PX.Data.BQL.BqlString.Field<vendorClassID> { }

        /// <inheritdoc cref="Vendor.VendorClassID"/>
        [PXDBString(10, IsUnicode = true, BqlField = typeof(Vendor.vendorClassID))]
        [PXUIField(DisplayName = "Vendor Class")]
        public virtual string VendorClassID { get; set; }
        #endregion
    }
}
