using Coopers.BusinessLayer.NotifEye.APIClient;
using System.Threading.Tasks;
using System.Linq;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.Localizer;
using System.Configuration;
using System.IO;
using System.Web;
using System;
using AutoMapper;
using Coopers.BusinessLayer.Database.APIClient.Location;
using System.Collections.Generic;
using System.Linq;

namespace Coopers.BusinessLayer.Services.Services
{
    public class UserApplicationService : IUserApplicationService
    {

        #region PRIVATE MEMBERS

        private readonly IUserClient _userClient;
        private readonly IEmailApplicationService _emailApplicationService;
        private readonly IPaymentHistoryApplicationService _paymentHistoryApplicationService;
        private readonly IAccountApplicationService _accountApplicationService;
        private readonly INetworkApplicationService _networkApplicationService;
        private readonly IMapper _mapper;
        private readonly IAccountLocationClient _accountLocationClient;

        #endregion


        #region CONSTRUCTOR

        public UserApplicationService(IAccountLocationClient accountLocationClient,IMapper mapper,INetworkApplicationService networkApplicationService,IAccountApplicationService accountApplicationService,IUserClient userClient,IEmailApplicationService emailApplicationService, IPaymentHistoryApplicationService paymentHistoryApplicationService)
        {
            _userClient = userClient;
            _emailApplicationService = emailApplicationService;
            _paymentHistoryApplicationService = paymentHistoryApplicationService;
            _accountApplicationService = accountApplicationService;
            _networkApplicationService = networkApplicationService;
            _mapper = mapper;
            _accountLocationClient = accountLocationClient;
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

            //Get the User details
            userDetail.User = await _userClient.GetUserInfo();

            //If User is Admin 
            if(userDetail.User.Admin)
            {
                //Get the users list
                userDetail.Users = await _accountApplicationService.GetAccountUserList(userDetail.User.Account[0].AccountID);
            }

            return userDetail;
        }

        /// <summary>
        /// Get the NetworPermissions for a given User
        /// </summary>
        /// <param name="UserID">Unique Indentifier for the User</param>
        /// <returns>List of networkIds</returns>
        public async Task<List<long>> GetNetworkPermissionsByUserID(long UserID)
        {
            List<long> netPermissions = new List<long>();

            //Get the UserInfo for a given user ID
            var user = await _userClient.GetUserInfoByID(UserID);

            //Get the available NetworkList for a given user Name
            var netWorkList = await _networkApplicationService.GetNetworkListByUser(user.UserName);

            //prepare the NetworkList IDs
            if (netWorkList != null)
            {
                netPermissions = netWorkList.Select(x => x.NetworkID).ToList();
            }

            //Return network permission IDs
            return netPermissions;
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
            template = template.Replace("{%EmailID%}", UserEmail);

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
          
            //Create the User and get the token
            var registrationToken = await _userClient.RegisterNewUser(User);

            //Get the User Info using the received token 
            var user = await _userClient.GetUserInfoWithRegistrationToken(registrationToken);

            if (user.Account != null && user.Account.Count > 0  )
            {
                await CreateAccountLocation(registrationToken, user.Account[0], User.Account.Latitude, User.Account.Longitude);
            }

            //Retrun the token
            return registrationToken;
        }

        /// <summary>
        /// Register the existing notifeye user to the Dashboard portal
        /// </summary>
        /// <param name="User">User Model</param>
        /// <returns>Authentication Token</returns>
        public async Task<string> RegisterNotifEyeUser(UserNotifEyeRegistration User)
        {
            //RegisterUser and get the get the Token
            return await _userClient.RegisterNotifEyeUser(User);

            //Get the User Info using the received token 
            //var user = await _userClient.GetUserInfoWithRegistrationToken(registrationToken);

            //if (user.Account != null && user.Account.Count > 0)
            //{
            //    await CreateAccountLocation(registrationToken, user.Account[0], User.Latitude, User.Longitude);
            //}

            //return registrationToken;
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
            //UserPermission userPermissions = new UserPermission { CustomerID = User.UserID, NetworkList = User.NetworkPermissions };
            //await _userClient.UpdateUserPermissions(userPermissions);
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

        /// <summary>
        /// Get the UserPermissions for a given User
        /// </summary>
        /// <param name="UserID">Unique indetofier for the User</param>
        /// <returns>UserPermissions</returns>
        public async Task<List<string>> GetUserPermissons(long UserID)
        {
            return await _userClient.GetUserPermissons(UserID);
        }

        /// <summary>
        /// Update the User permissions
        /// </summary>
        /// <param name="UserPermission">Update Permissions info</param>
        /// <returns>Success/Failure</returns>
        public async Task<string> UpdateUserPermissions(UserPermission UserPermission)
        {
            return await _userClient.UpdateUserPermissions(UserPermission);
        }

        #endregion


        #region PRIVATE MEMBERS     

        private async Task<long> CreateAccountLocation(string registrationToken,AccountInfo Account,double Latitude,double Longitude)
        {

            //Create the AccountLocation model. 
            var AccountLocation = _mapper.Map<AccountLocation>(Account);

            AccountLocation.IsActive = true;
            AccountLocation.Longitude = Longitude;
            AccountLocation.Latitude = Latitude;

            //Save the AccountLocation Record in DB
            return await _accountLocationClient.CreateAccountLocation(AccountLocation);

        }

        #endregion

    }
}
