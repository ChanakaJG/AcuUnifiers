using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.PO;

namespace AcuUnifiers
{
    public class MergeVendorScope : FlaggedModeScopeBase<MergeVendorScope> { }

    public class MergeVendor : PXGraph<MergeVendor>
    {
        #region Views
        public PXCancel<CDVendorMergeFilter> Cancel;

        public PXFilter<CDVendorMergeFilter> CDVendorMergeFilter;

        public PXFilteredProcessingOrderBy<CDVendorLocationDetail, CDVendorMergeFilter, OrderBy<Asc<CDVendorLocationDetail.acctCD>>> VendorsToBeMerged;

        public PXSelect<CDMergeVendorsAudit> auditView;

        public PXSelect<CDMergeVendorsAuditTrn> auditTrnView;
        #endregion

        #region Constructor
        public MergeVendor()
        {
            VendorsToBeMerged.SetProcessAllVisible(false);
            VendorsToBeMerged.SetProcessCaption("Merge Vendors");
        }
        #endregion

        #region Actions
        public PXAction<CDVendorMergeFilter> viewVendor;
        [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewVendor(PXAdapter adapter)
        {
            VendorMaint graph = CreateInstance<VendorMaint>();
            graph.BAccount.Current = PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Current<Vendor.bAccountID>>>>.Select(this);
            throw new PXRedirectRequiredException(graph, true, "View Vendor") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
        }

        public PXAction<CDVendorMergeFilter> viewVendorLocation;
        [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
        [PXButton]
        public virtual IEnumerable ViewVendorLocation(PXAdapter adapter)
        {
            VendorLocationMaint graph = CreateInstance<VendorLocationMaint>();
            graph.Location.Current = PXSelect<Location, Where<Location.locationID, Equal<Current<Location.locationID>>>>.Select(this);
            throw new PXRedirectRequiredException(graph, true, "View Vendor Location") { Mode = PXBaseRedirectException.WindowMode.NewWindow };
        }
        #endregion

        #region View Delegates
        public virtual IEnumerable vendorsToBeMerged()
        {
            var res = SelectFrom<CDVendorLocationDetail>
                .Where<CDVendorMergeFilter.vendorClassID.FromCurrent.IsNull.Or<CDVendorLocationDetail.vendorClassID.IsEqual<CDVendorMergeFilter.vendorClassID.FromCurrent>>>
                .OrderBy<Asc<CDVendorLocationDetail.acctCD>>
                .View.Select(this, CDVendorMergeFilter.Current.VendorID);
            return res;
        }
        #endregion

        #region Event Handlers
        protected void _(Events.RowSelected<CDVendorMergeFilter> e)
        {
            CDVendorMergeFilter filter = e.Row;

            VendorsToBeMerged.SetProcessDelegate(delegate (List<CDVendorLocationDetail> list)
            {
                MergeVendor graph = CreateInstance<MergeVendor>();
                graph.ExecuteMergeVendors(list, filter);
            });
        }

        protected void _(Events.RowSelected<CDVendorLocationDetail> e)
        {
            CDVendorLocationDetail detail = e.Row;

            PXUIFieldAttribute.SetEnabled<CDVendorLocationDetail.vendorLocationID>(VendorsToBeMerged.Cache, null, true);

        }

        protected virtual void _(Events.RowSelecting<CDVendorLocationDetail> e)
        {
            CDVendorLocationDetail detail = e.Row as CDVendorLocationDetail;

            using (new PXConnectionScope())
            {
                Location res = SelectFrom<Location>.Where<Location.bAccountID.IsEqual<@P.AsInt>
                    .And<Location.isActive.IsEqual<True>.And<Location.isDefault.IsEqual<True>>>>.View.Select(this, detail.BAccountID);

                detail.VendorLocationID = res.LocationID;
            }

        }

        #endregion

        #region Private Methods
        private void ExecuteMergeVendors(List<CDVendorLocationDetail> list, CDVendorMergeFilter filter)
        {
            if (filter.VendorID == null || filter.VendorLocationID == null)
            {
                throw new PXException(Messages.FilterValidationMsg);
            }

            POOrderEntry poOrderEntry = PXGraph.CreateInstance<POOrderEntry>();
            POReceiptEntry purchaseReceiptsEntry = PXGraph.CreateInstance<POReceiptEntry>();
            APInvoiceEntry apInvoiceEntry = PXGraph.CreateInstance<APInvoiceEntry>();
            APPaymentEntry apPaymentEntry = PXGraph.CreateInstance<APPaymentEntry>();

            using (new MergeVendorScope())
            {
                Guid guid = Guid.NewGuid();
                DateTime dateTime = DateTime.Now;

                foreach (CDVendorLocationDetail vendorDetail in list)
                {
                    //Audit Master
                    InsertAuditMaster(vendorDetail, guid, dateTime, filter);

                    if (vendorDetail.VendorLocationID == null)
                    {
                        PXProcessing<CDVendorLocationDetail>.SetWarning("Vendor Location should be defined");
                        continue;
                    }
                    if (vendorDetail.VendorLocationID == filter.VendorLocationID)
                    {
                        PXProcessing<CDVendorLocationDetail>.SetWarning("Can not merge to same location");
                        continue;
                    }

                    //Update POOrders
                    var poOrders = SelectFrom<POOrder>
                                    .Where<POOrder.vendorID.IsEqual<@P.AsInt>
                                        .And<POOrder.vendorLocationID.IsEqual<@P.AsInt>>>.View.Select(this, vendorDetail.BAccountID, vendorDetail.VendorLocationID);

                    foreach (POOrder poOrder in poOrders)
                    {
                        UpdatePOOrders(poOrder, poOrderEntry, filter);
                    }


                    // Update Purchase Receipts
                    var poReceipts = SelectFrom<POReceipt>
                                        .Where<POReceipt.vendorID.IsEqual<@P.AsInt>
                                            .And<POReceipt.vendorLocationID.IsEqual<@P.AsInt>>>.View.Select(this, vendorDetail.BAccountID, vendorDetail.VendorLocationID);

                    foreach (var poReceipt in poReceipts)
                    {
                        UpdatePOReceipts(poReceipt, purchaseReceiptsEntry, filter);
                    }

                    // Update AP Bills

                    var apInvoices = SelectFrom<APInvoice>
                                   .Where<APInvoice.vendorID.IsEqual<@P.AsInt>
                                       .And<APInvoice.vendorLocationID.IsEqual<@P.AsInt>>>.View.Select(this, vendorDetail.BAccountID, vendorDetail.VendorLocationID);

                    foreach (APInvoice apInvoice in apInvoices)
                    {
                        UpdateAPInvoice(apInvoice, apInvoiceEntry, filter);
                    }

                    // Update APPayments
                    var apPayments = SelectFrom<APPayment>
                                        .Where<APPayment.vendorID.IsEqual<@P.AsInt>
                                            .And<APPayment.vendorLocationID.IsEqual<@P.AsInt>>>.View.Select(this, vendorDetail.BAccountID, vendorDetail.VendorLocationID);

                    foreach (APPayment apPayment in apPayments)
                    {
                        UpdateAPPayment(apPayment, apPaymentEntry, filter);
                    }
                }
                ReCalculatevendorBalances(list, filter);
            }
        }

        private void UpdatePOOrders(POOrder poOrder, POOrderEntry poOrderEntry, CDVendorMergeFilter filter)
        {
            poOrderEntry.Clear();
            var vendorRef = poOrder.VendorRefNbr;
            poOrderEntry.Document.Current = poOrder;

            poOrderEntry.Document.Cache.SetValueExt<POOrder.vendorID>(poOrderEntry.Document.Current, filter.VendorID);
            poOrderEntry.Document.Cache.SetValueExt<POOrder.vendorLocationID>(poOrderEntry.Document.Current, filter.VendorLocationID);
            poOrderEntry.Document.UpdateCurrent();
            poOrderEntry.Document.Cache.SetValue<POOrder.vendorRefNbr>(poOrderEntry.Document.Current, vendorRef);

            poOrderEntry.Save.Press();
        }

        private void UpdateAPInvoice(APInvoice apInvoice, APInvoiceEntry apInvoiceEntry, CDVendorMergeFilter filter, bool updateClosed = false, bool updateAcccounts = false)
        {
            apInvoiceEntry.Clear();
            apInvoiceEntry.Document.Current = apInvoice;

            apInvoiceEntry.Document.Cache.SetValueExt<APInvoice.vendorID>(apInvoiceEntry.Document.Current, filter.VendorID);
            apInvoiceEntry.Document.Cache.SetValueExt<APInvoice.vendorLocationID>(apInvoiceEntry.Document.Current, filter.VendorLocationID);
            apInvoiceEntry.Document.UpdateCurrent();

            apInvoiceEntry.Save.Press();
        }

        private void UpdateAPPayment(APPayment apPayment, APPaymentEntry apPaymentEntry, CDVendorMergeFilter filter)
        {
            apPaymentEntry.Clear();
            apPaymentEntry.Document.Current = apPayment;

            apPaymentEntry.Document.Cache.SetValueExt<APPayment.vendorID>(apPaymentEntry.Document.Current, filter.VendorID);
            apPaymentEntry.Document.Cache.SetValueExt<APPayment.vendorLocationID>(apPaymentEntry.Document.Current, filter.VendorLocationID);
            apPaymentEntry.Document.UpdateCurrent();

            apPaymentEntry.Save.Press();
        }

        private void UpdatePOReceipts(POReceipt poReceipt, POReceiptEntry purchaseReceiptsEntry, CDVendorMergeFilter filter)
        {
            purchaseReceiptsEntry.Clear();
            purchaseReceiptsEntry.Document.Current = poReceipt;

            purchaseReceiptsEntry.Document.Cache.SetValueExt<POReceipt.vendorID>(purchaseReceiptsEntry.Document.Current, filter.VendorID);
            purchaseReceiptsEntry.Document.Cache.SetValueExt<POReceipt.vendorLocationID>(purchaseReceiptsEntry.Document.Current, filter.VendorLocationID);
            purchaseReceiptsEntry.Document.UpdateCurrent();

            foreach (POReceiptLine receiptLine in purchaseReceiptsEntry.transactions.Select())
            {
                purchaseReceiptsEntry.transactions.Cache.SetValueExt<POReceiptLine.vendorID>(receiptLine, filter.VendorID);
                purchaseReceiptsEntry.transactions.Update(receiptLine);
            }
            purchaseReceiptsEntry.Save.Press();
        }

        private void ReCalculatevendorBalances(List<CDVendorLocationDetail> list, CDVendorMergeFilter filter)
        {
            APReleaseProcess apReleaseProcess = PXGraph.CreateInstance<APReleaseProcess>();

            //Re Calculate vendor Balances of merged vendors
            foreach (CDVendorLocationDetail vendorDetail in list)
            {
                apReleaseProcess.Clear(PXClearOption.PreserveTimeStamp);
                Vendor vendor = Vendor.PK.Find(this, vendorDetail.BAccountID);
                apReleaseProcess.IntegrityCheckProc(vendor, "201201");
                ReopenDocumentsHavingPendingApplications(apReleaseProcess, vendor, "201201");
            }

            // Re Calculate vendor Balances of base vendor
            apReleaseProcess.Clear();
            apReleaseProcess.Clear(PXClearOption.PreserveTimeStamp);
            Vendor baseVendor = Vendor.PK.Find(this, filter.VendorID);
            apReleaseProcess.IntegrityCheckProc(baseVendor, "201201");
            ReopenDocumentsHavingPendingApplications(apReleaseProcess, baseVendor, "201201");

        }

        private static void ReopenDocumentsHavingPendingApplications(PXGraph graph, Vendor vendor, string finPeriod)
        {
            PXUpdate<Set<APRegister.openDoc, True>,
                APRegister,
                Where<APRegister.openDoc, Equal<False>,
                    And<APRegister.vendorID, Equal<Required<APRegister.vendorID>>,
                    And<APRegister.tranPeriodID, GreaterEqual<Required<APRegister.tranPeriodID>>,
                    And<Exists<Select<APAdjust, Where<
                        APAdjust.released, Equal<False>,
                        And<APAdjust.adjgDocType, Equal<APRegister.docType>,
                        And<APAdjust.adjgRefNbr, Equal<APRegister.refNbr>>>
                        >>>>>>>>
                .Update(graph, vendor.BAccountID, finPeriod);


            PXUpdate<Set<APRegister.status, APDocStatus.open>,
                APRegister,
                Where<APRegister.status, Equal<APDocStatus.closed>,
                    And<APRegister.vendorID, Equal<Required<APRegister.vendorID>>,
                    And<APRegister.tranPeriodID, GreaterEqual<Required<APRegister.tranPeriodID>>,
                    And<APRegister.openDoc, Equal<True>>>>>>
                .Update(graph, vendor.BAccountID, finPeriod);
        }

        public void InsertAuditMaster(CDVendorLocationDetail vendorDetail, Guid guid, DateTime dateTime, CDVendorMergeFilter filter)
        {
            var vendorTo = Vendor.PK.Find(this, filter.VendorID);
            var locationTo = Location.PK.Find(this, vendorDetail.BAccountID, vendorDetail.VendorLocationID); //????
            var locationFrom = Location.PK.Find(this, vendorDetail.BAccountID, vendorDetail.VendorLocationID);


            CDMergeVendorsAudit mergeVendorsAudit = new CDMergeVendorsAudit();
            mergeVendorsAudit.TrnUser = this.Accessinfo.UserName;
            mergeVendorsAudit.TrnDate = dateTime;
            mergeVendorsAudit.MergeVendorFrom = vendorDetail.AcctCD;
            mergeVendorsAudit.MergeVendorLocationFrom = locationFrom.LocationCD;
            mergeVendorsAudit.MergeVendorTo = vendorTo.AcctCD;    
            mergeVendorsAudit.MergeVendorLocationTo = locationTo.LocationCD;
            mergeVendorsAudit.Type = filter.MergingOption;              
            mergeVendorsAudit.BatchID = guid;

            this.auditView.Cache.Update(mergeVendorsAudit);
            this.Actions.PressSave();

        }
        #endregion
    }
}
