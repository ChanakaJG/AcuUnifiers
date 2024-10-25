using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.CR;
using System;

namespace AcuUnifiers
{
    [PXCacheName("Merge Vendor Audit")]
    public class CDMergeVendorsAudit : PXBqlTable, IBqlTable
    {
        public abstract class noteID : BqlGuid.Field<noteID> { }
        [PXSequentialNote(IsKey = true)]
        public virtual Guid? NoteID { get; set; }

        public abstract class mergeVendorTo : BqlType<IBqlString, string>.Field<mergeVendorTo> { }
        [PXDBString]
        [PXUIField(DisplayName = "Merge Vendor To")]
        public virtual string MergeVendorTo { get; set; }

        public abstract class mergeVendorLocationTo : BqlType<IBqlInt, int>.Field<mergeVendorLocationTo> { }
        [LocationActive(
            DisplayName = "Merging Vendor Location",
            DescriptionField = typeof(Location.locationCD),
            Visibility = PXUIVisibility.SelectorVisible, Enabled = true)]
        [PXUIField(DisplayName = "Merge Vendor Location To")]
        public virtual int? MergeVendorLocationTo { get; set; }

        public abstract class mergeVendorFrom : BqlType<IBqlString, string>.Field<mergeVendorFrom> { }
        [PXDBString]
        [PXUIField(DisplayName = "Merge Vendor From")]
        public virtual string MergeVendorFrom { get; set; }

        public abstract class mergeVendorLocationFrom : BqlType<IBqlInt, int>.Field<mergeVendorLocationFrom> { }
        [LocationActive(
            DisplayName = "Merging Vendor Location",
            DescriptionField = typeof(Location.locationCD),
            Visibility = PXUIVisibility.SelectorVisible, Enabled = true)]
        [PXUIField(DisplayName = "Merge Vendor Location From")]
        public virtual int? MergeVendorLocationFrom { get; set; }

        public abstract class trnDate : BqlType<IBqlDateTime, DateTime>.Field<trnDate> { }
        [PXDBDate]
        [PXUIField(DisplayName = "Transaction Date")]
        public virtual DateTime? TrnDate { get; set; }

        public abstract class trnUser : BqlType<IBqlString, String>.Field<trnUser> { }
        [PXDBString]
        [PXUIField(DisplayName = "User")]
        public virtual string TrnUser { get; set; }

        public abstract class type : BqlType<IBqlString, String>.Field<type> { }
        [PXDBString]
        [PXUIField(DisplayName = "Type")]
        public virtual string Type { get; set; }

        public abstract class batchID : BqlGuid.Field<batchID> { }
        [PXDBGuid]
        public virtual Guid? BatchID { get; set; }

    }
}
