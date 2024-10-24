using PX.Data;
using PX.Objects.PO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuUnifiers
{
    public class CDPOOrderEntryExt : PXGraphExtension<POOrderEntry>
    {
        public static bool IsActive() => true;

        public delegate void RaiseVendorIDExceptionDel(PXCache sender, POOrder order, object newVendorID, string error);
        [PXOverride]
        public virtual void RaiseVendorIDException(PXCache sender, POOrder order, object newVendorID, string error, RaiseVendorIDExceptionDel baseInvoke)
        {
            if (!MergeVendorScope.IsActive)
                baseInvoke?.Invoke(sender, order, newVendorID, error);
        }
    }
}
