using PX.Data;
using PX.Objects.AP;

namespace AcuUnifiers
{
    public class CDAPPaymentEntryExt : PXGraphExtension<APPaymentEntry>
    {
        public static bool IsActive() => true;

        protected void _(Events.FieldDefaulting<APPayment.paymentMethodID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APPayment payment = e.Row as APPayment;
                e.NewValue = payment.PaymentMethodID;
            }
        }

        protected void _(Events.FieldDefaulting<APPayment.aPAccountID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APPayment payment = e.Row as APPayment;
                e.NewValue = payment.APAccountID;
            }
        }

        protected void _(Events.FieldDefaulting<APPayment.aPSubID> e, PXFieldDefaulting del)
        {
            if (!MergeVendorScope.IsActive)
            {
                del?.Invoke(e.Cache, e.Args);
            }
            else
            {
                APPayment payment = e.Row as APPayment;
                e.NewValue = payment.APSubID;
            }
        }

    }
}
