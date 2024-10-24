using System;
using PX.Data;

namespace AcuUnifiers
{
  public class MergeVendor : PXGraph<MergeVendor>
  {

    public PXSave<MasterTable> Save;
    public PXCancel<MasterTable> Cancel;


    public PXFilter<MasterTable> MasterView;
    public PXFilter<DetailsTable> DetailsView;

    [Serializable]
    public class MasterTable : PXBqlTable, IBqlTable
    {

    }

    [Serializable]
    public class DetailsTable : PXBqlTable, IBqlTable
    {

    }


  }
}