using ClassifiedApp.DataAccess.UnitOfWork;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassifiedApp.DataAccess.Extensions;
using System.Transactions;
//using ClassifiedApp.BusinessModels;
using ClassifiedApp.BusinessServices.Users;
using ClassifiedApp.BusinessServices.Users.ViewModels;
using ClassifiedApp.BusinessServices.Users.Interfaces;
using System.Net.Mail;
using System.Net;
using AutoMapper;

namespace ClassifiedApp.BusinessServices.Users.Services
{
    /// <summary>
    /// Offers services for user specific operations
    /// </summary>
    public class UserService : IUserService
    {
        
        private readonly UnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenService _tokenService;
        private readonly IActivationCodeService _activationCodeService;
        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserService(UnitOfWork unitOfWork, IEncryptionService encryptionService, ITokenService tokenService, IActivationCodeService activationCodeService)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
            _tokenService = tokenService;
            _activationCodeService = activationCodeService;
        }

        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Authenticate(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Username.ToLower() == userName.ToLower());
            if (user != null && isUserValid(user, password))
            {
                return user.Id;
            }
            return 0;
        }

        private bool isPasswordValid(User user, string password)
        {
            string str = _encryptionService.EncryptPassword(password, user.Salt);
            return string.Equals(str, user.HashedPassword);
        }

        private bool isUserValid(User user, string password)
        {
            if (isPasswordValid(user, password))
            {
                return !user.IsBlocked;
            }

            return false;
        }


        public int CreateUser(RegisterUserModel registerUserModel)
        {
            using (var scope = new TransactionScope())
            {
                string salt = _encryptionService.CreateSalt();
                string hashedPassword = _encryptionService.EncryptPassword(registerUserModel.Password, salt);
                var user = new User
                {
                    Username = registerUserModel.Username,
                    Salt = salt,
                    HashedPassword = hashedPassword,
                    CreationDate = BusinessSettings.ServerNow,
                    GSM = registerUserModel.GSM,
                    Country = registerUserModel.Country,
                    City = registerUserModel.City,
                    IsBlocked = false,
                    IsEmailApproved = false,
                    IsGpsEnabled = registerUserModel.IsGpsEnabled,
                    IsGsmApproved = false,
                    UserGuid = Guid.NewGuid()
                };
                _unitOfWork.UserRepository.Add(user);
                _unitOfWork.Save();
                scope.Complete();
                return user.Id;
            }
        }

        public bool ValidateUniqueUsername(string userName)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Username == userName);
            if (user == null)
                return true;
            else
                return false;
        }
        public bool IsGsmApproved(int userId)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            if (user != null && user.IsGsmApproved)
                return true;
            else
                return false;
        }

        public bool ActivateUserGSM(int userId)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            if (user == null)
                return false;
            user.IsGsmApproved = true;
            _unitOfWork.UserRepository.Edit(user);
            _unitOfWork.Save();
            return true;
        }
        public bool ActivateUserEmail(int userId)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            if (user == null)
                return false;
            user.IsEmailApproved = true;
            _unitOfWork.UserRepository.Edit(user);
            _unitOfWork.Save();
            return true;
        }


        public bool UserHasEmail(int userId)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            if (user.Email != null && user.Email != "")
                return true;
            return false;
        }

        



        public bool FavoriteAnotherUser(int favSenderId, int favReceiverId)
        {
            var fav = new FavoriteUser()
            {
                FavoriteSenderId = favSenderId,
                FavoriteReceiverId = favReceiverId,
                CreationDate=BusinessSettings.ServerNow
            };
            _unitOfWork.FavoriteUserRepository.Add(fav);
            _unitOfWork.Save();
            return true;
        }

        public bool UnFavoriteAnotherUser(int favSenderId, int favReceiverId)
        {
            var previousFav = _unitOfWork.FavoriteUserRepository.FindSingleBy(f => f.FavoriteSenderId == favSenderId && f.FavoriteReceiverId == favReceiverId);
            _unitOfWork.FavoriteUserRepository.Delete(previousFav);
            _unitOfWork.Save();
            return true;
        }


        public bool CheckIfExists(int userId)
        {
            return _unitOfWork.UserRepository.GetById(userId) != null;
        }


        public bool IsPreviouslyFavorited(int favSenderId, int favReceiverId)
        {
            var previousFavs = _unitOfWork.FavoriteUserRepository.FindBy(f => f.FavoriteSenderId == favSenderId && f.FavoriteReceiverId == favReceiverId);
            if (previousFavs.Any())
                return true;

            return false;
        }


        public bool Logout(int userId)
        {
            return _tokenService.DeleteByUserId(userId);
        }


        public UserModel GetUserInfo(int userId)
        {
            var user= _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            if (user != null)
                return Mapper.Map<User, UserModel>(user);
            return null;
        }


        public bool ValidatePassword(int userId, string password)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            return isPasswordValid(user, password);
        }


        public bool ChangePassword(int userId, ChangePasswordModel model)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            using (var scope = new TransactionScope())
            {
                string salt = user.Salt;
                string hashedPassword = _encryptionService.EncryptPassword(model.NewPassword, salt);
                user.HashedPassword = hashedPassword;
                _unitOfWork.UserRepository.Edit(user);
                _unitOfWork.Save();
                scope.Complete();
                return true;
            }
        }


        public List<ClassifiedOwnerModel> GetUserFavoriteOwners(int userId)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            var list = user.FavoritesSender;

            if(list.Any())
            {
                var models = (from u in list
                              select u.FavoriteReceiver
                             ).OrderBy(u => u.Username).ToList();
                

                return Mapper.Map<IList<User>, IList<ClassifiedOwnerModel>>(models).ToList();
            }
            return null;
        }


        public List<TokenModel> Test()
        {
            var tokens = _unitOfWork.TokenRepository.FindBy(t => t.User.IsGsmApproved && !t.User.IsBlocked).ToList();
            Mapper.CreateMap<Token, TokenModel>();
            return Mapper.Map<IList<Token>, IList<TokenModel>>(tokens).ToList();
        }


        public List<UserModel> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.All;
            if (users.Any())
                return Mapper.Map<IList<User>, IList<UserModel>>(users.ToList()).ToList();
            return null;
        }


        public IEnumerable<UserModel> All()
        {
            var users = _unitOfWork.UserRepository.All;
            if (users.Any())
                return Mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(users);
            return null;
        }


        public UserModel GetByID(int id)
        {
            var user= _unitOfWork.UserRepository.FindSingleBy(u => u.Id == id);
            return Mapper.Map<User, UserModel>(user);
        }


        public bool EditProfile(int userId, EditProfileModel model)
        {
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            if(user!=null)
            {
                if(model.Country!=null)
                    user.Country = model.Country;
                if(model.City !=null)
                    user.City = model.City;
                if(model.NewEmail!=null)
                {
                    if (user.Email==null || user.Email.ToLower() != model.NewEmail.ToLower())
                    {
                        user.Email = model.NewEmail.ToLower();
                        user.IsEmailApproved = false;
                    }
                    
                }
                if (model.NewGSM != null && user.GSM != model.NewGSM)
                {
                    user.GSM = model.NewGSM.ToLower();
                    user.IsGsmApproved = false;
                }
                _unitOfWork.UserRepository.Edit(user);
                _unitOfWork.Save();

                return true;
            }
            return false;
        }
    }
}
