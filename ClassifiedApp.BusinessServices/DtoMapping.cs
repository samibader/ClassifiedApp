using AutoMapper;
using ClassifiedApp.BusinessServices.Categories.ViewModels;
using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using ClassifiedApp.BusinessServices.SubCategories.ViewModels;
using ClassifiedApp.BusinessServices.Users.ViewModels;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices
{
    public class CustomResolver : ValueResolver<Category, int>
    {
        protected override int ResolveCore(Category source)
        {
            int classifiedCount = 0;
            foreach (SubCategory sub in source.SubCategories)
            {
                classifiedCount += sub.Classifieds.Where(c=>c.Status== AdStatusList.Active).Count();
            }
            return classifiedCount;
        }
    }
    public class RateResolver : ValueResolver<Classified, double>
    {
        protected override double ResolveCore(Classified source)
        {
            if (!source.Rates.Any())
                return 0;
            int count = source.Rates.Count;
            double value = 0;
            foreach (Rate rate in source.Rates)
            {
                value += rate.Value;
            }
            return Math.Round(value / count, 2);
        }
    }
    internal static class DtoMappings
    {
        public static void Map()
        {
            //I specified mapping for AssignedPersonId since NHibernate does not fill Task.AssignedPersonId
            //If you will just use EF, then you can remove ForMember definition.

            //////////////////////////////////// User Mapping
            Mapper.CreateMap<User, ClassifiedOwnerModel>();
            Mapper.CreateMap<PropertyValue, PropertyValueModel>()
                .ForMember(dest => dest.PropertyValueId, opt =>
                    opt.MapFrom(src => src.Id)
                )
                .ForMember(dest => dest.PropertyDefinitionType, opt =>
                    opt.MapFrom(src => src.PropertyDefinition.Type)
                )
                .ForMember(dest => dest.PropertyDefinitionTypeString, opt =>
                    opt.MapFrom(src => src.PropertyDefinition.Type.ToString())
                )
                .ForMember(dest => dest.PropertyDefinitionName, opt =>
                    opt.MapFrom(src => src.PropertyDefinition.Name)
                );
            Mapper.CreateMap<User, UserModel>();

            Mapper.CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.PropertyDefinitionsCount, opt =>
                    opt.MapFrom(src => src.PropertyDefinitions.Count)
                )
                .ForMember(dest => dest.SubCategoriesCount, opt => opt.MapFrom(
                    src => src.SubCategories.Count
                    ))
                .ForMember(dest => dest.ClassifiedCount, opt => opt.ResolveUsing<CustomResolver>())
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => string.Join("/", BusinessSettings.BaseUrl, BusinessSettings.CategoryImageFolderBase, src.ImageUrl)))
                    ;

            Mapper.CreateMap<PropertyDefinition, PropertyDefinitionModel>()
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category.Name)
                )
                .ForMember(dest => dest.TypeString, opt =>
                    opt.MapFrom(src => src.Type.ToString())
                );
            Mapper.CreateMap<SubCategory, SubCategoryModel>()
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.Category.Name)
                )
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom(src => string.Join("/", BusinessSettings.BaseUrl, BusinessSettings.SubCategoryImageFolderBase, src.ImageUrl))
                )
                .ForMember(dest => dest.ClassifiedCount, opt =>
                    opt.MapFrom(src => src.Classifieds.Where(c=>c.Status== AdStatusList.Active).Count())
                )
                ;
            // classified Image Mapping
            Mapper.CreateMap<ClassifiedImage, ClassifiedImageModel>()               
                .ForMember(dest => dest.ImageUrl, opt =>
                    opt.MapFrom(src => string.Join("/", BusinessSettings.BaseUrl, BusinessSettings.ClassifiedImageFolderBase, src.ImageGuid + src.ImageExtension))
                )
                ;
            // classified Mapping
            Mapper.CreateMap<Classified, ClassifiedModel>()
                .ForMember(dest => dest.ShortDescription, opt =>
                    opt.MapFrom(src => string.Join(" ", src.Description.Length >= 10 ? src.Description.Substring(0, 10) : src.Description, "..."))
                )
                .ForMember(dest => dest.SubCategoryName, opt =>
                    opt.MapFrom(src => src.SubCategory.Name)
                )
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.SubCategory.Category.Name)
                )

                .ForMember(dest => dest.MainImage, opt =>
                    opt.MapFrom(src => src.ClassifiedImages.Select(i => i.IsMainImage == true).Count() > 0
                        ? Mapper.Map<ClassifiedImage, ClassifiedImageModel>(src.ClassifiedImages.SingleOrDefault(i => i.IsMainImage == true))
                        : new ClassifiedImageModel() { ImageUrl=BusinessSettings.ClassifiedImagePlaceHolder })
                )
                .ForMember(dest => dest.Status, opt =>
                    opt.MapFrom(src => src.Status.ToString())
                )
                .ForMember(dest => dest.AverageRate, opt =>
                    opt.MapFrom(src => Math.Round(src.AverageRate, 2))
                )
                //.ForMember(dest => dest.AdditionalImages, opt =>
                //    opt.MapFrom(src => Mapper.Map<IList<ClassifiedImage>, IList<ClassifiedImageModel>>(src.ClassifiedImages.Where(i=>!i.IsMainImage).ToList()))
                //)
                ;
            Mapper.CreateMap<Classified, ClassifiedDetailsModel>()
                .ForMember(dest => dest.SubCategoryName, opt =>
                    opt.MapFrom(src => src.SubCategory.Name)
                )
                .ForMember(dest => dest.CategoryName, opt =>
                    opt.MapFrom(src => src.SubCategory.Category.Name)
                )

                .ForMember(dest => dest.MainImage, opt =>
                    opt.MapFrom(src => src.ClassifiedImages.Select(i => i.IsMainImage == true).Count() > 0
                        ? Mapper.Map<ClassifiedImage, ClassifiedImageModel>(src.ClassifiedImages.SingleOrDefault(i => i.IsMainImage == true))
                        : new ClassifiedImageModel() { ImageUrl = BusinessSettings.ClassifiedImagePlaceHolder })
                )
                .ForMember(dest => dest.ClassifiedImageGallery, opt =>
                    opt.MapFrom(src => Mapper.Map<IList<ClassifiedImage>, IList<ClassifiedImageModel>>(src.ClassifiedImages.ToList()))
                )
                .ForMember(dest => dest.Owner, opt =>
                    opt.MapFrom(src => Mapper.Map<User, ClassifiedOwnerModel>(src.User))
                )
                .ForMember(dest => dest.TotalCountFavorited, opt =>
                    opt.MapFrom(src => src.FavoriteUsers.Count)
                )
                .ForMember(dest => dest.TotalCountLikes, opt =>
                    opt.MapFrom(src => src.Likes.Count)
                )
                .ForMember(dest => dest.Properties, opt =>
                    opt.MapFrom(src => Mapper.Map<IList<PropertyValue>, IList<PropertyValueModel>>(src.PropertyValues.ToList()))
                )
                .ForMember(dest => dest.Rate, opt => opt.ResolveUsing<RateResolver>())
                ;
                
        }
    }
}
