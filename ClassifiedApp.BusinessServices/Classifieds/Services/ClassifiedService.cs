using AutoMapper;
using ClassifiedApp.BusinessServices.Classifieds.Interfaces;
using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using ClassifiedApp.BusinessServices.Notifications.Interfaces;
using ClassifiedApp.BusinessServices.Notifications.ViewModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ClassifiedApp.BusinessServices.Classifieds.Services
{
    
    public class ClassifiedService : IClassifiedService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;
        public ClassifiedService(UnitOfWork unitOfWork,INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }
        public int CreateNormalClassified(InputClassifiedModel classifiedModel)
        {
            using (var scope = new TransactionScope())
            {
                var classified = new Classified()
                {
                    UserId = classifiedModel.UserId,
                    SubCategoryId = classifiedModel.SubCategoryId,
                    Title = classifiedModel.Title,
                    Description = classifiedModel.Description,
                    Country = classifiedModel.Country,
                    City = classifiedModel.City,
                    AdType = classifiedModel.AdType,
                    AddingDate = BusinessSettings.ServerNow,
                    IsFeatured = false,
                    ViewCount = 0,
                    Status = AdStatusList.Pending
                };
                if (classified.AdType == AdTypeList.Fixed)
                    classified.AdPrice = classifiedModel.AdPrice;
                else
                    classified.AdPrice = null;
                _unitOfWork.ClassifiedRepository.Add(classified);
                _unitOfWork.Save();
                if(classifiedModel.PropertyValues.Any())
                    foreach (var p in classifiedModel.PropertyValues)
                    {
                        var property = new PropertyValue()
                        {
                            ClassifiedId = classified.Id,
                            PropertyDefinitionId = p.PropertyDefinitionId,
                            Value = p.Value
                        };
                        _unitOfWork.PropertyValueRepository.Add(property);
                        _unitOfWork.Save();
                    }
                scope.Complete();
                return classified.Id;
            }
        }
        public bool UserCanAddNormalClassified(int userId)
        {
            var NormalClassifieds = _unitOfWork.ClassifiedRepository.FindBy(c => c.UserId == userId && c.Status == AdStatusList.Active);
            if (!NormalClassifieds.Any())
                return true;
            var lastNormalClassified = NormalClassifieds.OrderByDescending(c => c.PostingDate).First();
            if (lastNormalClassified.PostingDate.Value.AddDays(BusinessSettings.DaysBetweenClassifieds) > BusinessSettings.ServerNow)
                return false;
            else
                return true;
        }
        public ClassifiedModel GetPublicById(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId && c.Status== AdStatusList.Active);
            if(model!=null)
            {
                var classifiedModel = Mapper.Map<Classified, ClassifiedModel>(model);
                return classifiedModel;
            }
            return null;
        }
        public ClassifiedDetailsModel GetPublicDetailsById(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId && c.Status == AdStatusList.Active);
            if (model != null)
            {
                var classifiedModel = Mapper.Map<Classified, ClassifiedDetailsModel>(model);
                return classifiedModel;
            }
            return null;
        }
        public void IncreaseViewCount(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId);
            if (model != null)
            {
                model.ViewCount++;
                _unitOfWork.ClassifiedRepository.Edit(model);
                _unitOfWork.Save();
            }
            
        }
        public void SetClassifiedActive(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId);
            if (model != null)
            {
                model.Status = AdStatusList.Active;
                model.PostingDate = BusinessSettings.ServerNow;
                _unitOfWork.ClassifiedRepository.Edit(model);
                _unitOfWork.Save();
            }
        }
        public void SetClassifiedPending(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId);
            if (model != null)
            {
                model.Status = AdStatusList.Pending;
                model.PostingDate = null;
                _unitOfWork.ClassifiedRepository.Edit(model);
                _unitOfWork.Save();
            }
        }
        public void SetClassifiedExpired(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId);
            if (model != null)
            {
                model.Status = AdStatusList.Expired;
                _unitOfWork.ClassifiedRepository.Edit(model);
                _unitOfWork.Save();
            }
        }
        public void SetClassifiedRejected(int classifiedId, string rejectionNotes)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId);
            if (model != null)
            {
                model.Status = AdStatusList.Rejected;
                model.Notes = rejectionNotes;
                _unitOfWork.ClassifiedRepository.Edit(model);
                _unitOfWork.Save();
            }
        }
        public ClassifiedModel GetById(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId);
            if (model != null)
            {
                var classifiedModel = Mapper.Map<Classified, ClassifiedModel>(model);
                return classifiedModel;
            }
            return null;
        }
        public ClassifiedDetailsModel GetDetailsById(int classifiedId)
        {
            var model = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId);
            if (model != null)
            {
                var classifiedModel = Mapper.Map<Classified, ClassifiedDetailsModel>(model);
                return classifiedModel;
            }
            return null;
        }
        public bool IfExists(int classifiedId)
        {
            return _unitOfWork.ClassifiedRepository.GetById(classifiedId) != null;
        }
        public List<ClassifiedModel> GetClassifieds(int userId, int status)
        {
            AdStatusList st = (AdStatusList)status;
            var list = _unitOfWork.ClassifiedRepository.FindBy(c => c.UserId == userId && c.Status == st);
            if (list != null)
                return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(list.ToList()).ToList();
            return null;

        }
        public ClassifiedImageModel AddClassifiedImage(InputClassifiedImageModel inputModel)
        {
            using (var scope = new TransactionScope())
            {
                var classifiedImage = new ClassifiedImage()
                {
                    ClassifiedId = inputModel.ClassifiedId,
                    ImageExtension = inputModel.ImageExtension,
                    ImageGuid = inputModel.ImageGuid,
                    IsMainImage = inputModel.IsMainImage
                };

                _unitOfWork.ClassifiedImageRepository.Add(classifiedImage);
                _unitOfWork.Save();
                scope.Complete();
                return Mapper.Map<ClassifiedImage, ClassifiedImageModel>(classifiedImage);
            }
        }
        public List<ClassifiedModel> GetAllForPagingTest(int pageSize, int pageNumber, out int totalCount,out int totalPages)
        {
            totalCount = this._unitOfWork.ClassifiedRepository.GetAll().Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
            var clubQuery = this._unitOfWork.ClassifiedRepository.GetAll();

            

            var clubs = clubQuery.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(clubs.ToList()).ToList();

        }
        public List<ClassifiedModel> GetClassifiedBySubCategory(int subCategoryId, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            var list = this._unitOfWork.ClassifiedRepository.FindBy(c => c.SubCategoryId == subCategoryId && c.Status== AdStatusList.Active);
            if(list==null || !list.Any())
            {
                totalCount = 0;
                totalPages = 0;
                return null;
            }
            totalCount = list.Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));
            
            var models = list.OrderByDescending(c=>c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();

        }
        public List<ClassifiedModel> GetFeaturedClassifiedBySubCategory(int subCategoryId, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            var list = this._unitOfWork.ClassifiedRepository.FindBy(c => c.SubCategoryId == subCategoryId && c.Status == AdStatusList.Active &&  c.IsFeatured);
            if (list == null || !list.Any())
            {
                totalCount = 0;
                totalPages = 0;
                return null;
            }
            totalCount = list.Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            var models = list.OrderByDescending(c => c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();

        }
        public bool ValidateUserClassified(int userId, int classifiedId)
        {
            return _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId && c.UserId == userId) != null;
        }
        public bool EditClassified(EditClassifiedModel model)
        {
            using (var scope = new TransactionScope())
            {
                bool SetAsPending = false;
                var classified = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == model.ClassifiedId);
                if(model.Country!=null)
                {
                    classified.Country = model.Country;
                    classified.City = model.City;
                }
                if(model.AdType.HasValue)
                {
                    classified.AdType = model.AdType.Value;
                    if (classified.AdType == AdTypeList.Fixed)
                        classified.AdPrice = model.AdPrice.Value;
                    else
                        classified.AdPrice = null;
                }
                if(model.Description!=null)
                {
                    classified.Description = model.Description;
                    SetAsPending = true;
                }
                if (model.Title != null)
                {
                    classified.Title = model.Title;
                    SetAsPending = true;
                }
                if(model.SubCategoryId.HasValue)
                {
                    classified.SubCategoryId = model.SubCategoryId.Value;
                    SetAsPending = true;
                }
                if(SetAsPending)
                {
                    classified.Status = AdStatusList.Pending;
                    classified.PostingDate = null;
                }
                _unitOfWork.ClassifiedRepository.Edit(classified);
                _unitOfWork.Save();
                scope.Complete();
                return true;
            }
        }
        public bool CheckIfActive(int classifiedId)
        {
            return _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId && c.Status== AdStatusList.Active) != null;
        }
        public bool IsPreviouslyFavorited(int userId, int classifiedId)
        {
            var previousFavs = _unitOfWork.FavoriteClassifiedRepository.FindBy(f => f.UserId == userId && f.ClassifiedId == classifiedId);
            if (previousFavs.Any())
                return true;

            return false;
        }
        public bool FavoriteClassified(int userId, int classifiedId)
        {
            var fav = new FavoriteClassified()
            {
                UserId = userId,
                ClassifiedId = classifiedId,
                CreationDate = BusinessSettings.ServerNow
            };
            _unitOfWork.FavoriteClassifiedRepository.Add(fav);
            _unitOfWork.Save();

            var classifiedOwner = _unitOfWork.ClassifiedRepository.FindSingleBy(c => c.Id == classifiedId).User;
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            // NOTIFICATION
            InputNotificationModel notification = new InputNotificationModel()
            {
                Message = String.Format("{0} favorited your ad", user.Username),
                Type = NotificationType.ClassifiedAddedToFavorite,
                UserId = classifiedOwner.Id
            };
            _notificationService.Notify(notification, classifiedOwner.Id);


            return true;
        }
        public bool UnFavoriteClassified(int userId, int classifiedId)
        {
            var previousFav = _unitOfWork.FavoriteClassifiedRepository.FindSingleBy(f => f.UserId == userId && f.ClassifiedId == classifiedId
                );
            _unitOfWork.FavoriteClassifiedRepository.Delete(previousFav);
            _unitOfWork.Save();
            return true;
        }
        public List<ClassifiedModel> GetClassifiedByUser(int userId, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            var list = this._unitOfWork.ClassifiedRepository.FindBy(c => c.UserId == userId && c.Status == AdStatusList.Active);
            if (list == null || !list.Any())
            {
                totalCount = 0;
                totalPages = 0;
                return null;
            }
            totalCount = list.Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            var models = list.OrderByDescending(c => c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();
        }
        public List<ClassifiedModel> GetClassifiedFavoritedByUser(int userId, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
                var list = (from c in user.FavoriteClassifieds
                              where c.Classified.Status== AdStatusList.Active
                              select c.Classified
                             ).OrderBy(c=>c.PostingDate).ToList();
                //var list = this._unitOfWork.ClassifiedRepository.FindBy(c => c.UserId == userId && c.Status == AdStatusList.Active);
                if (list == null || !list.Any())
                {
                    totalCount = 0;
                    totalPages = 0;
                    return null;
                }
                totalCount = list.Count();
                totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

                var models = list.OrderByDescending(c => c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();
           
        }
        public bool LikeClassified(int userId, int classifiedId)
        {
            var like = new Like()
            {
                UserId = userId,
                ClassifiedId = classifiedId,
                CreationDate=BusinessSettings.ServerNow
            };
            _unitOfWork.LikeRepository.Add(like);
            _unitOfWork.Save();

            var classifiedOwner = _unitOfWork.ClassifiedRepository.FindSingleBy(c=>c.Id==classifiedId).User;
            var user = _unitOfWork.UserRepository.FindSingleBy(u=>u.Id==userId);
            // NOTIFICATION
            InputNotificationModel notification = new InputNotificationModel()
            {
                Message = String.Format("{0} liked your ad", user.Username),
                Type = NotificationType.UserClassifiedLiked,
                UserId = classifiedOwner.Id
            };
            _notificationService.Notify(notification, classifiedOwner.Id);

            return true;
        }
        public bool IsPreviouslyLiked(int userId, int classifiedId)
        {
            var previousLikes = _unitOfWork.LikeRepository.FindBy(f => f.UserId == userId && f.ClassifiedId == classifiedId);
            if (previousLikes.Any())
                return true;

            return false;
        }
        public bool UnLikeClassified(int userId, int classifiedId)
        {
            var previousLikes = _unitOfWork.LikeRepository.FindSingleBy(f => f.UserId == userId && f.ClassifiedId == classifiedId
                );
            _unitOfWork.LikeRepository.Delete(previousLikes);
            _unitOfWork.Save();
            return true;
        }
        public bool RateClassified(int userId, int classifiedId, byte rateValue)
        {
            var previousRates = _unitOfWork.RateRepository.FindBy(f => f.UserId == userId && f.ClassifiedId == classifiedId);
            if(previousRates.Any())
            {
                foreach (Rate r in previousRates)
                {
                    _unitOfWork.RateRepository.Delete(r);
                }
            }
            var rate = new Rate()
            {
                UserId = userId,
                ClassifiedId = classifiedId,
                CreationDate = BusinessSettings.ServerNow,
                Value = rateValue
            };
            _unitOfWork.RateRepository.Add(rate);
            _unitOfWork.Save();
            return true;
        }
        public List<ClassifiedModel> GetTopRated(int fetchCount)
        {
            var list = _unitOfWork.ClassifiedRepository.FindBy(c => c.Status == AdStatusList.Active).ToList().OrderByDescending(c => c.AverageRate).Take(fetchCount).ToList();
            if(list.Any())
                return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(list).ToList();

            return null;
        }


        public List<ClassifiedModel> SearchByCatId(int categoryId, string keyword, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            //string[] keys = keywords.Split(',');
            //var list = _unitOfWork.ClassifiedRepository.FindBy(c => c.Status == AdStatusList.Active).ToList();
            //var result = new List<Classified>();
            //foreach (var item in keys)
            //{
            //    result.Union(list.Where(c => c.Title.Contains(item)));
            //}
            //if(result.Any())
            //    return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(list).ToList();
            //return null;
            var list = this._unitOfWork.ClassifiedRepository.FindBy(c => (c.Title.Contains(keyword) || (c.Description.Contains(keyword))) && c.Status == AdStatusList.Active && c.SubCategory.CategoryId == categoryId);
            if (list == null || !list.Any())
            {
                totalCount = 0;
                totalPages = 0;
                return null;
            }
            totalCount = list.Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            var models = list.OrderByDescending(c => c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            if (models.Any())
                return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();
            return null;

        }
        public List<ClassifiedModel> SearchBySubCatId(int subcategoryId, string keyword, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            var list = this._unitOfWork.ClassifiedRepository.FindBy(c => (c.Title.Contains(keyword)||(c.Description.Contains(keyword))) && c.Status == AdStatusList.Active && c.SubCategoryId == subcategoryId);
            if (list == null || !list.Any())
            {
                totalCount = 0;
                totalPages = 0;
                return null;
            }
            totalCount = list.Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            var models = list.OrderByDescending(c => c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            if (models.Any())
                return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();
            return null;

        }


        public List<ClassifiedModel> SearchByKeyword(string keyword, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            var list = this._unitOfWork.ClassifiedRepository.FindBy(c => (c.Title.Contains(keyword) || (c.Description.Contains(keyword))) && c.Status == AdStatusList.Active);
            if (list == null || !list.Any())
            {
                totalCount = 0;
                totalPages = 0;
                return null;
            }
            totalCount = list.Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            var models = list.OrderByDescending(c => c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            if (models.Any())
                return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();
            return null;
        }


        public List<ClassifiedModel> AdvancedSearch(SearchClassifiedModel model, int pageSize, int pageNumber, out int totalCount, out int totalPages)
        {
            var list = this._unitOfWork.ClassifiedRepository.FindBy(
                c =>  (model.AdType==null || c.AdType==model.AdType)
                    &&(model.Country==null || c.Country==model.Country)
                    &&(model.CategoryId==null || c.SubCategory.CategoryId==model.CategoryId)
                    &&(model.City==null || c.City==model.City)
                    &&(model.Lang==null || c.SubCategory.Category.Language==model.Lang)
                    &&(model.SubCategoryId==null || c.SubCategoryId== model.SubCategoryId)
                    &&(model.WithImage==null || (model.WithImage.Value && c.ClassifiedImages.Count>0) ||(!model.WithImage.Value && c.ClassifiedImages.Count==0))
                    &&(model.MinPrice==null || (c.AdPrice.Value>=model.MinPrice && c.AdPrice.Value<=model.MaxPrice))
                    &&(model.MinPostingDate==null ||(c.PostingDate>=model.MinPostingDate && c.PostingDate<=model.MaxPostingDate))
                    &&(model.keyword==null || (c.Title.Contains(model.keyword) || c.Description.Contains(model.keyword)))
                    && c.Status == AdStatusList.Active);
            if (list == null || !list.Any())
            {
                totalCount = 0;
                totalPages = 0;
                return null;
            }
            totalCount = list.Count();
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / pageSize));

            var models = list.OrderByDescending(c => c.PostingDate).Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();
            if (models.Any())
                return Mapper.Map<IList<Classified>, IList<ClassifiedModel>>(models).ToList();
            return null;
        }
    }
}
