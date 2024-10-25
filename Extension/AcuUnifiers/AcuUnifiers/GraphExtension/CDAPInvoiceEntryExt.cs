using PX.Data;
using PX.Objects.AP;

namespace AcuUnifiers
{
    public class CDAPInvoiceEntryExt : PXGraphExtension<APInvoiceEntry>
    {
        public static bool IsActive() => true;

        //protected void _(Events.FieldDefaulting<APInvoice.aPAccountID> e, PXFieldDefaulting del)
        //{
        //    if (!MergeVendorScope.IsActive)
        //    {
        //        del?.Invoke(e.Cache, e.Args);
        //    }
        //    else
        //    {
        //        APInvoice apInvoice = e.Row as APInvoice;
        //        e.NewValue = apInvoice.APAccountID;
        //    }
        //}

        //protected void _(Events.FieldDefaulting<APInvoice.aPSubID> e, PXFieldDefaulting del)
        //{
        //    if (!MergeVendorScope.IsActive)
        //    {
        //        del?.Invoke(e.Cache, e.Args);
        //    }
        //    else
        //    {
        //        APInvoice apInvoice = e.Row as APInvoice;
        //        e.NewValue = apInvoice.APSubID;
        //    }
        //}

        protected void _(Events.FieldDefaulting<APInvoice.payTypeID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APInvoice apInvoice = e.Row as APInvoice;
                e.NewValue = apInvoice.PayTypeID;
            }
        }

        protected void _(Events.FieldDefaulting<APInvoice.separateCheck> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APInvoice apInvoice = e.Row as APInvoice;
                e.NewValue = apInvoice.SeparateCheck;
            }
        }

        protected void _(Events.FieldDefaulting<APInvoice.taxCalcMode> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APInvoice apInvoice = e.Row as APInvoice;
                e.NewValue = apInvoice.TaxCalcMode;
            }
        }

        protected void _(Events.FieldDefaulting<APInvoice.taxZoneID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APInvoice apInvoice = e.Row as APInvoice;
                e.NewValue = apInvoice.TaxZoneID;
            }
        }

        protected void _(Events.FieldDefaulting<APInvoice.prebookAcctID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APInvoice apInvoice = e.Row as APInvoice;
                e.NewValue = apInvoice.PrebookAcctID;
            }
        }

        protected void _(Events.FieldDefaulting<APInvoice.prebookSubID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APInvoice apInvoice = e.Row as APInvoice;
                e.NewValue = apInvoice.PrebookSubID;
            }
        }

        protected void _(Events.FieldDefaulting<APInvoice.paymentsByLinesAllowed> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APInvoice apInvoice = e.Row as APInvoice;
                e.NewValue = apInvoice.PaymentsByLinesAllowed;
            }
        }
    }
}
