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

        protected void _(Events.FieldDefaulting<POOrder.termsID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.TermsID;
            }
        }
        protected void _(Events.FieldDefaulting<POOrder.ownerID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.OwnerID;
            }
        }
        protected void _(Events.FieldDefaulting<POOrder.orderDate> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.OrderDate;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.branchID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.BranchID;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.taxCalcMode> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.TaxCalcMode;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.shipVia> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.ShipVia;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.fOBPoint> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.FOBPoint;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.expectedDate> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.ExpectedDate;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.siteID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.SiteID;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.approved> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.Approved;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.dontPrint> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.DontPrint;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.dontEmail> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.DontEmail;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.emailed> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.Emailed;
            }
        }
        protected void _(Events.FieldDefaulting<POOrder.printed> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.Printed;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.shipDestType> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.ShipDestType;
            }
        }

        protected void _(Events.FieldDefaulting<POOrder.shipToLocationID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                POOrder order = e.Row as POOrder;
                e.NewValue = order.ShipToLocationID;
            }
        }
    }
}
