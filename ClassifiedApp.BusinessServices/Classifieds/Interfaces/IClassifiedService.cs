using ClassifiedApp.BusinessServices.Classifieds.ViewModels;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Classifieds.Interfaces
{
    public interface IClassifiedService
    {
        int CreateNormalClassified(InputClassifiedModel classifiedModel);
        bool UserCanAddNormalClassified(int userId);

        ClassifiedModel GetPublicById(int classifiedId);
        ClassifiedDetailsModel GetPublicDetailsById(int classifiedId);
        ClassifiedModel GetById(int classifiedId);
        ClassifiedDetailsModel GetDetailsById(int classifiedId);
        void IncreaseViewCount(int classifiedId);

        // Extra
        void SetClassifiedActive(int classifiedId);
        void SetClassifiedPending(int classifiedId);
        void SetClassifiedExpired(int classifiedId);
        void SetClassifiedRejected(int classifiedId, string rejectionNotes);
        bool IfExists(int classifiedId);

        List<ClassifiedModel> GetClassifieds(int userId, int status);
        List<ClassifiedModel> GetClassifiedBySubCategory(int subCategoryId, int pageSize, int pageNumber, out int totalCount, out int totalPages);
        List<ClassifiedModel> GetFeaturedClassifiedBySubCategory(int subCategoryId, int pageSize, int pageNumber, out int totalCount, out int totalPages);
        List<ClassifiedModel> GetClassifiedByUser(int userId, int pageSize, int pageNumber, out int totalCount, out int totalPages);
        List<ClassifiedModel> GetClassifiedFavoritedByUser(int userId, int pageSize, int pageNumber, out int totalCount, out int totalPages);
        
        ClassifiedImageModel AddClassifiedImage(InputClassifiedImageModel inputModel);

        List<ClassifiedModel> GetAllForPagingTest(int pageSize, int pageNumber, out int totalCount, out int totalPages);

        bool ValidateUserClassified(int userId, int classifiedId);

        bool EditClassified(EditClassifiedModel model);
        bool CheckIfActive(int classifiedId);
        bool IsPreviouslyFavorited(int userId, int classifiedId);
        bool FavoriteClassified(int userId, int classifiedId);
        bool UnFavoriteClassified(int userId, int classifiedId);

        bool LikeClassified(int userId, int classifiedId);
        bool IsPreviouslyLiked(int userId, int classifiedId);
        bool UnLikeClassified(int userId, int classifiedId);
        bool RateClassified(int userId, int classifiedId,byte rateValue);


        List<ClassifiedModel> GetTopRated(int fetchCount);

        List<ClassifiedModel> SearchByCatId(int categoryId, string keyword, int pageSize, int pageNumber, out int totalCount, out int totalPages);
        List<ClassifiedModel> SearchBySubCatId(int subcategoryId, string keyword, int pageSize, int pageNumber, out int totalCount, out int totalPages);
        List<ClassifiedModel> SearchByKeyword(string keyword, int pageSize, int pageNumber, out int totalCount, out int totalPages);
        List<ClassifiedModel> AdvancedSearch(SearchClassifiedModel model, int pageSize, int pageNumber, out int totalCount, out int totalPages);
    }
}
