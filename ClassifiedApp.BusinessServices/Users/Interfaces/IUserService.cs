//using ClassifiedApp.BusinessModels;
using ClassifiedApp.BusinessServices.Users.ViewModels;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Users.Interfaces
{
    public interface IUserService
    {
        int Authenticate(string userName, string password);

        int CreateUser(RegisterUserModel registerUserModel);
        bool ValidateUniqueUsername(string userName);
        bool IsGsmApproved(int userId);
        bool ActivateUserGSM(int userId);
        bool ActivateUserEmail(int userId);
        bool UserHasEmail(int userId);
        //bool SendEmailActivation(int userId);

        bool FavoriteAnotherUser(int favSenderId, int favReceiverId);
        bool UnFavoriteAnotherUser(int favSenderId, int favReceiverId);
        bool CheckIfExists(int userId);
        bool IsPreviouslyFavorited(int favSenderId, int favReceiverId);
        

        bool ValidatePassword(int userId, string password);

        bool ChangePassword(int userId, ChangePasswordModel model);
        bool EditProfile(int userId, EditProfileModel model);
        bool Logout(int userId);
        UserModel GetUserInfo(int userId);
        List<ClassifiedOwnerModel> GetUserFavoriteOwners(int userId);

        List<TokenModel> Test();

        List<UserModel> GetAllUsers();

        IEnumerable<UserModel> All();
        UserModel GetByID(int id);
    }
}
