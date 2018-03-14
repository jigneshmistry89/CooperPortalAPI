using Coopers.BusinessLayer.NotifEye.APIClient;
using System.Threading.Tasks;
using AutoMapper;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Localizer;
using System.Configuration;
using System.IO;
using System.Web;
using System;

namespace Coopers.BusinessLayer.Services.Services
{
    public class UserApplicationService : IUserApplicationService
    {

        #region PRIVATE MEMBERS

        private readonly IUserClient _userClient;
        private readonly IEmailApplicationService _emailApplicationService;
        private readonly IPaymentHistoryApplicationService _paymentHistoryApplicationService;
        private readonly IAccountApplicationService _accountApplicationService;

        #endregion


        #region CONSTRUCTOR

        public UserApplicationService(IAccountApplicationService accountApplicationService,IUserClient userClient,IEmailApplicationService emailApplicationService, IPaymentHistoryApplicationService paymentHistoryApplicationService)
        {
            _userClient = userClient;
            _emailApplicationService = emailApplicationService;
            _paymentHistoryApplicationService = paymentHistoryApplicationService;
            _accountApplicationService = accountApplicationService;
        }

        #endregion


        #region PUBLIC MEMBERS     

        /// <summary>
        /// Get the currently loggeg in User info. 
        /// </summary>
        /// <returns>User Model</returns>
        public async Task<UserWithAccountInfo> GetUserInfo()
        {
            return await _userClient.GetUserInfo();
        }

        /// <summary>
        /// Get the User details of the currently loggedin user. It will also get the Users List info and the Payment histories if the user is admin
        /// </summary>
        /// <returns>User details</returns>
        public async Task<UserDetailWithPaymentHistory> GetUserDetails()
        {
            var userDetail = new UserDetailWithPaymentHistory();
            userDetail.User = await _userClient.GetUserInfo();

            if(userDetail.User.Admin)
            {

                userDetail.PaymentHistories = await _paymentHistoryApplicationService.GetPaymentHistoryList(0, 50);
                userDetail.Users = await _accountApplicationService.GetAccountUserList(userDetail.User.Account[0].AccountID);
            }

            return userDetail;
        }

        /// <summary>
        /// Get the User info for the given userID
        /// </summary>
        /// <param name="UserID">Unique indetifier for the User</param>
        /// <returns>User Model</returns>
        public async Task<UserWithAccountInfo> GetUserInfoByID(long UserID)
        {
            return await _userClient.GetUserInfoByID(UserID);
        }

        public async Task<string> SendRegisterationLink(string UserEmail)
        {
            //Get the registration token
            var token = await _userClient.GetRegistrationToken(UserEmail);

            //Get the template
            var template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Registration Email.html"));

            //prepare the registration Link.
            var link = string.Format(ConfigurationManager.AppSettings["UserRegisterRedirectionLink"] + "?email={0}&token={1}", UserEmail, token);

            //Replace the link.
            template = template.Replace("{%RegistrationLink%}", link);

            //Send an email.
            _emailApplicationService.SendAnEmail(template, UserEmail, AppLocalizer.UserRegistrationLink);

            //return the registration token
            return token;
        }

        /// <summary>
        /// Register a New Dasboard User
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>Authetication Token</returns>
        public async Task<string> RegisterNewUser(UserNewRegistration User)
        {
            //Calculate the Account Expiry Date
            if(User != null && User.Account != null)
            {
                User.Account.SubscriptionExpirationdate = DateTime.Now.AddDays(365).ToString("yyyy-MM-dd");
            }
          
            return await _userClient.RegisterNewUser(User);
        }

        /// <summary>
        /// Register the existing notifeye user to the Dashboard portal
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>Authentication Token</returns>
        public async Task<string> RegisterNotifEyeUser(UserNotifEyeRegistration User)
        {
            return await _userClient.RegisterNotifEyeUser(User);
        }

        /// <summary>
        /// Create the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>unique indetifier for the created user</returns>
        public async Task<long> CreateUser(UserCreate User)
        {
            return await _userClient.CreateUser(User);
        }

        /// <summary>
        /// Update the user
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>success/failure</returns>
        public async Task<string> UpdateUser(UpdateUser User)
        {
            return await _userClient.UpdateUser(User);
        }

        /// <summary>
        /// Delete the user by the ID
        /// </summary>
        /// <param name="UserID">Unique identifier for the user</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> DeleteUser(long UserID)
        {
            return await _userClient.DeleteUser(UserID);
        }

        #endregion


        #region PRIVATE MEMBERS     

        #endregion

    }
}
