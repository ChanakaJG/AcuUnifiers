using PX.Data;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuUnifiers
{
    public class CDPOReceiptEntryExt : PXGraphExtension<POReceiptEntry>
    {
        public static bool IsActive() => true;

        protected void _(Events.FieldDefaulting<POReceipt.autoCreateInvoice> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POReceipt receipt = e.Row as POReceipt;
                e.NewValue = receipt.AutoCreateInvoice;
            }
        }

        protected void _(Events.FieldDefaulting<POReceipt.branchID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POReceipt receipt = e.Row as POReceipt;
                e.NewValue = receipt.BranchID;
            }
        }
    }
}
