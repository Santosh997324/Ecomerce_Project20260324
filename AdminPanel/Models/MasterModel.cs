using AdminPanel.Models.Banner;
using AdminPanel.Models.Product;
using AdminPanel.Models.Product.BaseProduct;
using AdminPanel.Models.Status;

namespace AdminPanel.Models
{
    public class MasterModel
    {

        #region Banner
        public BannerVM BannerObj { get; set; }
        public List<BannerVM> BannerList { get; set; }
        public IFormFile ImagePath { get; set; }

        #endregion

        #region Status
        public StatusVM StatusObj { get; set; }
        public List<StatusVM> StatusList { get; set; }

        #endregion

        #region ProductMaterial

        public MaterialVM Material_Obj { get; set; }

        public List<MaterialVM> Mateial_List { get; set; }

        #endregion

        #region ProductColorVM
        public ProductColorVM ProductColor_Obj { get; set; }
        public List<ProductColorVM> ProductColor_List { get; set; }

        #endregion

        #region ProductSizeVM
        public ProductSizeVM ProductSize_Obj { get; set; }
        public List<ProductSizeVM> ProductSize_List { get; set; }

        #endregion

        #region ProductCategoryVM
        public MainCategoryVM ProductCategory_Obj { get; set; }
        public List<MainCategoryVM> ProductCategoryVM_List { get; set; }

        #endregion

        #region BaseProductVM
        public BaseProductVM BaseProductVM_Obj { get; set; }
        public BaseProductVM_Dtos BaseProductVM_Dtos_Obj { get; set; }
        //public BaseProduct_Business baseProduct_Business_Obj { get; set; }
        public List<BaseProductVM_Dtos> BaseProductVM_Dtos_List { get; set; }

        public BaseProductVM_Dtos response_BaseProduct { get; set; }


        #endregion


    }
}
