using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PO;

namespace AcuUnifiers
{
    [PXCacheName("Vendor Location Detail")]
    [PXProjection(typeof(SelectFrom<VendorLocation>), Persistent = false)]
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
        [PXDBInt(BqlField = typeof(VendorLocation.bAccountID))]
        public int? BAccountID { get; set; }
        #endregion

        #region AcctCD
        public abstract class acctCD : PX.Data.BQL.BqlString.Field<acctCD> { }

        /// <inheritdoc cref="Vendor.AcctCD"/>
        [VendorRaw(DisplayName = "Merging Vendor", BqlField = typeof(Vendor.acctCD))]
        public string AcctCD { get; set; }
        #endregion



        #region VendorLocationID
        public abstract class vendorLocationID : PX.Data.BQL.BqlInt.Field<vendorLocationID> { }
        [PXDBInt(BqlField = typeof(VendorLocation.locationID), IsKey = true)]
        public virtual int? VendorLocationID { get; set; }
        #endregion

        #region VendorLocationCD
        public abstract class vendorLocationCD : PX.Data.BQL.BqlString.Field<vendorLocationCD> { }
        [PXDBString()]
        [PXUIField(DisplayName = "Merging Vendor Location")]
        public virtual string VendorLocationCD { get; set; }
        #endregion

        #region AcctName
        public abstract class acctName : PX.Data.BQL.BqlString.Field<acctName> { }
        [PXDBString(255, IsUnicode = true)]
        [PXUIField(DisplayName = "Merging Vendor Name", Visibility = PXUIVisibility.SelectorVisible)]
        public string AcctName { get; set; }
        #endregion

        #region VendorClassID
        public abstract class vendorClassID : PX.Data.BQL.BqlString.Field<vendorClassID> { }

        /// <inheritdoc cref="Vendor.VendorClassID"/>
        [PXDBString(10, IsUnicode = true)]
        [PXUIField(DisplayName = "Vendor Class")]
        public virtual string VendorClassID { get; set; }
        #endregion
    }
}
