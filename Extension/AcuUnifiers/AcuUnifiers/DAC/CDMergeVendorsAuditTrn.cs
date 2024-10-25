using PX.Data;
using PX.Data.BQL;
using System;

namespace AcuUnifiers
{
    [PXCacheName("Merge Vendor Audit Transactions")]
    public class CDMergeVendorsAuditTrn : PXBqlTable, IBqlTable
    {
        public abstract class noteID : BqlGuid.Field<noteID> { }
        [PXSequentialNote(IsKey = true)]
        public virtual Guid? NoteID { get; set; }

        public abstract class affectedEntity : BqlType<IBqlString, string>.Field<affectedEntity> { }
        [PXDBString]
        [PXUIField(DisplayName = "Affected Entity")]
        public virtual string AffectedEntity { get; set; }

        public abstract class docType : BqlType<IBqlString, string>.Field<docType> { }
        [PXDBString]
        [PXUIField(DisplayName = "Doc Type")]
        public virtual string DocType { get; set; }

        public abstract class refNumber : BqlType<IBqlString, string>.Field<refNumber> { }
        [PXDBString]
        [PXUIField(DisplayName = "Ref Number")]
        public virtual string RefNumber { get; set; }

        public abstract class originalVendor : BqlType<IBqlString, string>.Field<originalVendor> { }
        [PXDBString]
        [PXUIField(DisplayName = "Original Vendor")]
        public virtual string OriginalVendor { get; set; }

        public abstract class mergeVendorTo : BqlType<IBqlString, string>.Field<mergeVendorTo> { }
        [PXDBString]
        [PXUIField(DisplayName = "Merged To Vendor")]
        public virtual string MergeVendorTo { get; set; }

        public abstract class batchID : BqlGuid.Field<batchID> { }
        [PXDBGuid]
        public virtual Guid? BatchID { get; set; }

    }
}
