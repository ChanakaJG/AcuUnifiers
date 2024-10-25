using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcuUnifiers
{
    [PXLocalizable]
    public class Messages
    {
        public const string MergingOptionLabelAllTransactions = "All Transactions";
        public const string MergingOptionLabelOpenPurchaseOrders = "Open Purchase Orders";

        public const string FilterValidationMsg = "Vendor, Vendor Location and Merging Option should be defined";

    }

    public class Constants
    {
        public const string MergingOptionValueAllTransactions = "AT";
        public const string MergingOptionValueOpenPurchaseOrders = "OP";
    }
}
