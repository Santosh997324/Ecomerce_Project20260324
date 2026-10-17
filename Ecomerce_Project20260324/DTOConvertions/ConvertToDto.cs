using Ecomerce_Project20260324.DTOs.BaseProductDtos.CustomModel;
using Ecomerce_Project20260324.DTOs.ProductVariantDtos.CustomModels;
using Ecomerce_Project20260324.Models.Product;

namespace Ecomerce_Project20260324.DTOConvertions
{
    public static class ConvertToDto
    {
        public static IEnumerable<Model_BaseProductCustom> ConvertToDtoListCustomProduct(this IEnumerable<BaseProduct> baseProducts)
        {
            var BaseProductCustomReturn = new List<Model_BaseProductCustom>();

            foreach (var baseProduct in baseProducts)
            {
                List<Model_BaseImagesCustom> images = new List<Model_BaseImagesCustom>();

                foreach (var imageBase in baseProduct.imageBases)
                {
                    var baseImageCustom = new Model_BaseImagesCustom()
                    {
                        Id = imageBase.Id,
                        BaseProductId = imageBase.BaseProductId,
                        AddedOn = imageBase.AddedOn,
                        staticPath = imageBase.staticPath
                    };
                    images.Add(baseImageCustom);
                }

                List<Model_ProductVarientCustom> productVariants = new List<Model_ProductVarientCustom>();

                foreach (var productVariant in baseProduct.productVariants)
                {
                    var productColor = new Model_ProductColorCustom()
                    {
                        Id = productVariant.productColor.Id,
                        Name = productVariant.productColor.Name
                    };

                    var productSize = new Model_ProductSizeCustom()
                    {
                        Id = productVariant.productSize.Id,
                        Name = productVariant.productSize.Name
                    };

                    var productVariantCustom = new Model_ProductVarientCustom()
                    {
                        Id = productVariant.Id,
                        BaseProductId = productVariant.BaseProductId,
                        ProductColor = productColor,
                        ProductSize = productSize,
                        Quantity = productVariant.Quantity
                    };
                    productVariants.Add(productVariantCustom);
                }

                var baseProductCustom = new Model_BaseProductCustom()
                {
                    Id = baseProduct.Id,
                    Name = baseProduct.Name,
                    Description = baseProduct.Description,
                    mainCategory = baseProduct.MainCategory,
                    material = baseProduct.Material,
                    productVariants = productVariants,
                    ImagesBases = images,
                    Price = baseProduct.Price,
                    Discount = baseProduct.Discount,
                    TotalPrice = baseProduct.TotalPrice,
                };

                BaseProductCustomReturn.Add(baseProductCustom);
            }
            return BaseProductCustomReturn;
        }


        public static Model_BaseProductCustom ConvertToDtoCustomProduct(
               this BaseProduct baseProduct)
        {
            var BaseProductCustom = new Model_BaseProductCustom();

            List<Model_BaseImagesCustom> images = new List<Model_BaseImagesCustom>();

            foreach (var imageBase in baseProduct.imageBases)
            {
                var imageBaseCustom = new Model_BaseImagesCustom()
                {
                    Id = imageBase.Id,
                    BaseProductId = imageBase.BaseProductId,
                    AddedOn = imageBase.AddedOn,
                    staticPath = imageBase.staticPath
                };

                images.Add(imageBaseCustom);
            }

            List<Model_ProductVarientCustom> productVariants = new List<Model_ProductVarientCustom>();

            foreach (var productVariant in baseProduct.productVariants)
            {
                var productColor = new Model_ProductColorCustom()
                {
                    Id = productVariant.productColor.Id,
                    Name = productVariant.productColor.Name
                };

                var productSize = new Model_ProductSizeCustom()
                {
                    Id = productVariant.productSize.Id,
                    Name = productVariant.productSize.Name
                };

                var productVariantCustom = new Model_ProductVarientCustom()
                {
                    Id = productVariant.Id,
                    BaseProductId = productVariant.BaseProductId,
                    ProductColor = productColor,
                    ProductSize = productSize,
                    Quantity = productVariant.Quantity
                };

                productVariants.Add(productVariantCustom);
            }

            var baseProductCustom = new Model_BaseProductCustom()
            {
                Id = baseProduct.Id,
                Name = baseProduct.Name,
                Description = baseProduct.Description,
                mainCategory = baseProduct.MainCategory,
                material = baseProduct.Material,
                productVariants = productVariants,
                ImagesBases = images,
                Price = baseProduct.Price,
                Discount = baseProduct.Discount,
                TotalPrice = baseProduct.TotalPrice,
            };
            return baseProductCustom;
        }


        public static Model_BaseProductWithNoExtraInfo ConvertToDtoProductNoInfo(
             this BaseProduct baseProduct)
        {
            var baseProductNoInfo = new Model_BaseProductWithNoExtraInfo()
            {
                Id = baseProduct.Id,
                Name = baseProduct.Name,
                Description = baseProduct.Description,
                MaterialId = baseProduct.MaterialId,
                MainCategoryId = baseProduct.MainCategoryId,
                Price = baseProduct.Price,
                Discount = baseProduct.Discount,
                TotalPrice = baseProduct.TotalPrice
            };

            return baseProductNoInfo;
        }


        public static IEnumerable<Model_ProductVariantReturn> ConvertToDtoProductVariant
     (this IEnumerable<ProductVariant> productVariants)
        {
            var returnProductVariantCustom = (from productVariant in productVariants
                                              select new Model_ProductVariantReturn
                                              {
                                                  Id = productVariant.Id,
                                                  BaseProductId = productVariant.BaseProductId,
                                                  productColor = productVariant.productColor,
                                                  productSize = productVariant.productSize,
                                                  Quantity = productVariant.Quantity
                                              });
            return returnProductVariantCustom;
        }

        public static Model_ProductVariantWithoutObj ConvertToDtoWithoutObj
      (this ProductVariant productVariant)
        {
            var productVariantWithoutObj = new Model_ProductVariantWithoutObj()
            {
                Id = productVariant.Id,
                BaseProductId = productVariant.BaseProductId,
                ProductColorId = productVariant.ProductColorId,
                ProductSizeId = productVariant.ProductSizeId,
                Quantity = productVariant.Quantity
            };

            return productVariantWithoutObj;
        }


        public static Model_ProductVariantReturn ConvertToDtoWithObj
        (this ProductVariant productVariant)
        {
            var productVariantWIthObj = new Model_ProductVariantReturn()
            {
                Id = productVariant.Id,
                BaseProductId = productVariant.BaseProductId,
                productColor = productVariant.productColor,
                productSize = productVariant.productSize,
                Quantity = productVariant.Quantity
            };

            return productVariantWIthObj;
        }


    }
}
