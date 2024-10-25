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
        public const string MergingOptionLabelOpenTransactions = "Open Transactions";

        public const string FilterValidationMsg = "Vendor, Vendor Location and Merging Option should be defined";

        public const string MergingOptionATMsg = "Merging vendors is an irreversible action. All transactions will be merged with the criteria set irrespective of the status of the transactions.";
        public const string MergingOptionOpenMsg = "Merging vendors is an irreversible action. Open Purchase Orders will be merge with the criteria set.";

        public const string Warning = "Warning!";
    }

    public class Constants
    {
        public const string MergingOptionValueAllTransactions = "AT";
        public const string MergingOptionValueOpenTransactions = "OT";
    }
}
