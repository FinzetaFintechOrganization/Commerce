using AutoMapper;

public class GeneralMapping : Profile
{
    public GeneralMapping()
    {
        CreateMap<Category,ResultCategoryDto>().ReverseMap();
        CreateMap<Category,CreateCategoryDto>().ReverseMap(); 
        CreateMap<Category,UpdateCategoryDto>().ReverseMap(); 
        CreateMap<Category,GetByIdCategoryDto>().ReverseMap();

        CreateMap<Product,GetByIdProductDto>().ReverseMap(); 
        CreateMap<Product,CreateProductDto>().ReverseMap(); 
        CreateMap<Product,UpdateProductDto>().ReverseMap(); 
        CreateMap<Product,ResultProductDto>().ReverseMap(); 

        CreateMap<ProductDetail,GetByIdProductDetailDto>().ReverseMap(); 
        CreateMap<ProductDetail,CreateProductDetailDto>().ReverseMap(); 
        CreateMap<ProductDetail,UpdateProductDto>().ReverseMap(); 
        CreateMap<ProductDetail,ResultProductDto>().ReverseMap(); 

        CreateMap<ProductImage,GetByIdProductImageDto>().ReverseMap(); 
        CreateMap<ProductImage,CreateProductImageDto>().ReverseMap(); 
        CreateMap<ProductImage,UpdateProductImageDto>().ReverseMap(); 
        CreateMap<ProductImage,ResultProductImageDto>().ReverseMap(); 

    }
}