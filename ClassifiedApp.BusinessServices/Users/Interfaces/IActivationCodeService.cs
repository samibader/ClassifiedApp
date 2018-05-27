//using ClassifiedApp.BusinessModels;
using ClassifiedApp.BusinessServices.Users.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices.Users.Interfaces
{
    public interface IActivationCodeService
    {
        #region Interface member methods.
        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ActivationCodeModel GenerateCode(int userId,string reason);

        Task<SyriatelWebService.SendSMSResponse> SendCode(int userId, ActivationCodeModel codeModel);
        bool SendEmailCode(int userId, ActivationCodeModel codeModel);
        Task<SyriatelWebService.SendSMSResponse> ReSendCode(int userId, string reason);
        bool ReSendEmailCode(int userId, string reason);

        /// <summary>
        /// Function to validate token againt expiry and existance in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        bool ValidateActivationCode(string code,int userId,string reason);

        /// <summary>
        /// Method to kill the provided token id.
        /// </summary>
        /// <param name="tokenId"></param>
        bool Kill(string activationCodeId);

        /// <summary>
        /// Delete tokens for the specific deleted user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool DeleteByUserId(int userId,string reason);
        #endregion
    }
}
