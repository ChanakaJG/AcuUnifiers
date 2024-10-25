<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormDetail.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="CDAP5010.aspx.cs" Inherits="Page_CDAP5010" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormDetail.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="AcuUnifiers.MergeVendor"
        PrimaryView="CDVendorMergeFilter"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="CDVendorMergeFilter" Width="100%" Height="" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule runat="server" StartRow="True" ID="PXLayoutRule1" ></px:PXLayoutRule>
			<px:PXLayoutRule runat="server" StartGroup="True" GroupCaption="Merge To" ID="CstPXLayoutRule5" ></px:PXLayoutRule>
			<px:PXSegmentMask runat="server" DataField="VendorID" CommitChanges="True" ID="CstPXSegmentMask1" ></px:PXSegmentMask>
			<px:PXSegmentMask runat="server" DataField="VendorLocationID" AutoRefresh="True" CommitChanges="True" ID="CstPXSegmentMask2" ></px:PXSegmentMask>
			<px:PXDropDown CommitChanges="True" runat="server" ID="CstPXDropDown1" DataField="MergingOption" ></px:PXDropDown>
			<px:PXDateTimeEdit CommitChanges="True" runat="server" ID="CstPXDateTimeEdit3" DataField="MergingDate" ></px:PXDateTimeEdit>
			<px:PXCheckBox CommitChanges="True" runat="server" ID="CstPXCheckBox2" DataField="UpdateGLAccounts" ></px:PXCheckBox>
			<px:PXLayoutRule runat="server" StartGroup="True" StartColumn="True" GroupCaption="Filter By" ID="CstPXLayoutRule6" ></px:PXLayoutRule>
			<px:PXSelector runat="server" DataField="VendorClassID" CommitChanges="True" ID="CstPXSelector4" ></px:PXSelector></Template>
	</px:PXFormView>
</asp:Content>
<asp:Content ID="cont3" ContentPlaceHolderID="phG" Runat="Server">
	<px:PXGrid runat="server" SyncPosition="True" Height="150px" SkinID="PrimaryInquire" Width="100%" ID="grid" AllowAutoHide="false" DataSourceID="ds" AllowPaging="true" AdjustPageSize="Auto" AllowSearch="true">
		<AutoSize Enabled="True" Container="Window" MinHeight="150" />
		<Levels>
			<px:PXGridLevel DataMember="VendorsToBeMerged">
				<RowTemplate>
					</RowTemplate>
				<Columns>
					<px:PXGridColumn Type="CheckBox" DataField="Selected" Width="60" AllowCheckAll="True" ></px:PXGridColumn>
					<px:PXGridColumn DataField="AcctCD" Width="140" LinkCommand="ViewVendor" ></px:PXGridColumn>
					<px:PXGridColumn DataField="AcctName" Width="280" ></px:PXGridColumn>
					<px:PXGridColumn LinkCommand="ViewVendorLocation" DataField="VendorLocationCD" Width="70" ></px:PXGridColumn>
					<px:PXGridColumn DataField="VendorClassID" Width="120" ></px:PXGridColumn></Columns></px:PXGridLevel></Levels></px:PXGrid></asp:Content>