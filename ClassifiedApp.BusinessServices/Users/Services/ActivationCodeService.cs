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
using System.Net;
using System.Net.Http;
using System.Net.Mail;

namespace ClassifiedApp.BusinessServices.Users.Services
{
    public class ActivationCodeService : IActivationCodeService
    {
        private UnitOfWork _unitOfWork;

        public ActivationCodeService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActivationCodeModel GenerateCode(int userId, string reason)
        {
            _unitOfWork.ActivationCodeRepository.Delete(x => x.UserId == userId && x.Reason == reason);
            _unitOfWork.Save();
            string code = RandomString(6);
            DateTime issuedOn = BusinessSettings.ServerNow;

            DateTime expiredOn = BusinessSettings.ServerNow.AddDays(1);
            var activationCode = new ActivationCode
            {
                UserId = userId,
                Code = code,
                IssuedOn = issuedOn,
                ExpiresOn=expiredOn,
                Reason=reason
            };

            Mapper.CreateMap<User, UserModel>();
            var userModel = Mapper.Map<User, UserModel>(_unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId));

            _unitOfWork.ActivationCodeRepository.Add(activationCode);
            _unitOfWork.Save();
            var activatopnCodeModel = new ActivationCodeModel()
            {
                UserId = userId,
                IssuedOn = issuedOn,
                Code = code,
                ExpiresOn=expiredOn,
                Reason=reason,
                User = userModel
            };

            return activatopnCodeModel;
        }

        public bool ValidateActivationCode(string code, int userId, string reason)
        {
            //int intUserId;   
            //if (!int.TryParse(userId, out intUserId))
            //    return false;
            var activationCode = _unitOfWork.ActivationCodeRepository.FindSingleBy(t => t.Code == code && t.UserId == userId && t.Reason == reason && t.ExpiresOn >= BusinessSettings.ServerNow);
            if (activationCode != null)
            {
                return true;
            }
            return false;
        }

        public bool DeleteByUserId(int userId, string reason)
        {
            _unitOfWork.ActivationCodeRepository.Delete(x => x.UserId == userId && x.Reason==reason);
            _unitOfWork.Save();

            var isNotDeleted = _unitOfWork.ActivationCodeRepository.FindBy(x => x.UserId == userId).Any();
            return !isNotDeleted;
        }
        
        private string RandomString(int Size)
        {
            return "1A2B3C";
            //Random random = new Random((int)DateTime.Now.Ticks);
            //string input = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //StringBuilder builder = new StringBuilder();
            //char ch;
            //for (int i = 0; i < Size; i++)
            //{
            //    ch = input[random.Next(0, input.Length)];
            //    builder.Append(ch);
            //}
            //return builder.ToString();
        }

        public bool Kill(string activationCodeId)
        {
            //Guid guidTokenId;
            //if (!Guid.TryParse(tokenId, out guidTokenId))
            //    return false;
            //_unitOfWork.TokenRepository.Delete(x => x.AuthToken == guidTokenId);
            //_unitOfWork.Save();
            //var isNotDeleted = _unitOfWork.TokenRepository.FindBy(x => x.AuthToken == guidTokenId).Any();
            //if (isNotDeleted) { return false; }
            return true;
        }


        public async Task<SyriatelWebService.SendSMSResponse> SendCode(int userId, ActivationCodeModel codeModel)
        {
            SyriatelWebService.APISoapClient client = new SyriatelWebService.APISoapClient();
            SyriatelWebService.SendSMSResponse response = await client.SendSMSAsync(BusinessSettings.SyriatelJobName,
                BusinessSettings.SyriatelUserName,
                BusinessSettings.SyriatelPassword,
                codeModel.Code,
                BusinessSettings.SyriatelSender,
                _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId).GSM,
                //"041111",
                null,
                null,
                null);

            return response;
        }

        public bool SendEmailCode(int userId, ActivationCodeModel codeModel)
        {
            SmtpClient client = GetSmtpClient();
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);

            string emailFrom = BusinessSettings.SmtpUserName;
            string emailTo = user.Email;
            string subject = "Classified Activation";
            string body = codeModel.Code;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        private SmtpClient GetSmtpClient()
        {
            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential(BusinessSettings.SmtpUserName, BusinessSettings.SmtpPassword);
            client.Host = BusinessSettings.SmtpHost;
            client.Port = BusinessSettings.SmtpPort;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            return client;
        }
        public async Task<SyriatelWebService.SendSMSResponse> ReSendCode(int userId, string reason)
        {
            var userObj = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            var codeObj = _unitOfWork.ActivationCodeRepository.FindSingleBy(c => c.UserId == userId && c.Reason == reason);
            SyriatelWebService.APISoapClient client = new SyriatelWebService.APISoapClient();
            SyriatelWebService.SendSMSResponse response = await client.SendSMSAsync(BusinessSettings.SyriatelJobName,
                BusinessSettings.SyriatelUserName,
                BusinessSettings.SyriatelPassword,
                codeObj.Code,
                BusinessSettings.SyriatelSender,
                _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId).GSM,
                //"041111",
                null,
                null,
                null);

            return response;
        }

        public bool ReSendEmailCode(int userId, string reason)
        {
            SmtpClient client = GetSmtpClient();
            var user = _unitOfWork.UserRepository.FindSingleBy(u => u.Id == userId);
            var codeModel = _unitOfWork.ActivationCodeRepository.FindSingleBy(c => c.UserId == userId && c.Reason == reason);

            string emailFrom = BusinessSettings.SmtpUserName;
            string emailTo = user.Email;
            string subject = "Classified Activation";
            string body = codeModel.Code;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                try
                {
                    client.Send(mail);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }
    }
}
