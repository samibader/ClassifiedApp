using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ClassifiedApp.BusinessModels;
using ClassifiedApp.DataAccess.UnitOfWork;
using System.Configuration;
using ClassifiedApp.Models;
using AutoMapper;
using ClassifiedApp.BusinessServices.Users.Interfaces;
using ClassifiedApp.BusinessServices.Users.ViewModels;

namespace ClassifiedApp.BusinessServices.Users.Services
{
    public class TokenService : ITokenService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public TokenService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TokenModel GenerateToken(int userId)
        {
            Guid tokenId = Guid.NewGuid();
            DateTime issuedOn = DateTime.Now;
            //DateTime expiredOn = DateTime.Now.AddSeconds(
            //                                  Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            DateTime expiredOn = BusinessSettings.AuthTokenExpiry;
            var token = new Token
            {
                UserId = userId,
                AuthToken = tokenId,
                IssuedOn = issuedOn
            };

            Mapper.CreateMap<User, UserModel>();
                var userModel = Mapper.Map<User, UserModel>(_unitOfWork.UserRepository.FindSingleBy(u=>u.Id==userId));
                DeleteByUserId(userId);
            _unitOfWork.TokenRepository.Add(token);
            _unitOfWork.Save();
            var tokenModel = new TokenModel()
            {
                UserId = userId,
                IssuedOn = issuedOn,
                AuthToken = tokenId,
                User= userModel
            };

            return tokenModel;
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public bool ValidateToken(string tokenId, string userId)
        {
            Guid guidTokenId;
            int intUserId;
            if (!Guid.TryParse(tokenId, out guidTokenId))
                return false;
            if (!int.TryParse(userId, out intUserId))
                return false;
            var token = _unitOfWork.TokenRepository.FindSingleBy(t => t.AuthToken == guidTokenId && t.UserId == intUserId);
            if (token != null)
            {
                return true;
            }
            return false;
        }

        public bool Kill(string tokenId)
        {
            Guid guidTokenId;
            if (!Guid.TryParse(tokenId, out guidTokenId))
                return false;
            _unitOfWork.TokenRepository.Delete(x => x.AuthToken == guidTokenId);
            _unitOfWork.Save();
            var isNotDeleted = _unitOfWork.TokenRepository.FindBy(x => x.AuthToken == guidTokenId).Any();
            if (isNotDeleted) { return false; }
            return true;
        }

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>true for successful delete</returns>
        public bool DeleteByUserId(int userId)
        {
            _unitOfWork.TokenRepository.Delete(x => x.UserId == userId);
            _unitOfWork.Save();

            var isNotDeleted = _unitOfWork.TokenRepository.FindBy(x => x.UserId == userId).Any();
            return !isNotDeleted;
        }

    }
}
