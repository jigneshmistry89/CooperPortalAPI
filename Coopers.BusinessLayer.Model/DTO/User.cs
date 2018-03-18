using System;
using System.Collections.Generic;

namespace Coopers.BusinessLayer.Model.DTO
{
    public class User
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class UserDetail
    {
        public UserWithAccountInfo User { get; set; }
    }

    public class UserDetailWithPaymentHistory
    {
        public UserWithAccountInfo User { get; set; }
        public List<PaymentHistoryInfo> PaymentHistories { get; set; }
        public List<UserInfo> Users { get; set; }
        //public List<long> NetworkPermissions { get; set; }

    }

    public class UserWithAccountInfo
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string SMSNumber { get; set; }
        public string Email { get; set; }
        public string VoiceNumber { get; set; }
        public bool DirectSMS { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }
        public bool RecievesMaintenanceByPhone { get; set; }
        public bool RecievesMaintenanceByEmail { get; set; }
        public string ExternalSMSProvider { get; set; }
        public List<AccountInfo> Account { get; set; }
    }

    public class AccountInfo
    {
        public long AccountID { get; set; }

        public int TimeZoneID { get; set; }

        public string TimeZone { get; set; }

        public int ResellerID { get; set; }

        public DateTime ActivationDate { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public long SubscriptionID { get; set; }

        public string SubscriptionType { get; set; }

        public DateTime SubscriptionExpiry { get; set; }

        public long UserID { get; set; }

        public string UserName { get; set; }

        public string UserFullName { get; set; }

        public string SMSNumber { get; set; }

        public string EmailAddress { get; set; }

        public long ExternalSMSProviderID { get; set; }

        public string ExternalSMSProvider { get; set; }

        public int NumSensors { get; set; }

        public int NumGateways { get; set; }
    }

    public class UserRegistration
    {
        public string DashboardUserName { get; set; }
        public string DashboardPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProductName { get; set; }
        public string Email { get; set; }
        public string NotifeyeUserName { get; set; }
        public string NotifeyePassword { get; set; }
        public long AccountID { get; set; }

    }

    public class UserNewRegistration
    {

        public string DashboardUserName { get; set; }
        public string DashboardPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProductName { get; set; }

        public string Email { get; set; }
        public string SMSNumber { get; set; }

        public int SMSCarrierID { get; set; }

        public AccountRegistration Account { get; set; }

    }

    public class UserNotifEyeRegistration
    {
        public string DashboardUserName { get; set; }

        public string DashboardPassword { get; set; }

        public string ProductName { get; set; }

        public string Email { get; set; }

        public string NotifeyeUserName { get; set; }

        public string NotifeyePassword { get; set; }

        //public double Latitude { get; set; }

        //public double Longitude { get; set; }
    }

    public class AccountRegistration
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Address2 { get; set; }

        public string PurchaseLocation { get; set; }

        public int IndustryType { get; set; }

        public int BusinessType { get; set; }

        public int TimeZone { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string SubscriptionExpirationdate { get; set; }

    }

    public class UserCreate
    {
        public string DashboardUserName { get; set; }

        public string DashboardPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProductName { get; set; }

        public string Email { get; set; }

        public string SMSNumber { get; set; }

        public int SMSCarrierID { get; set; }

        public int IsAdmin { get; set; }

        public int DirectSMS { get; set; }

        public int RecievesMaintenanceByPhone { get; set; }

        public int RecievesMaintenanceByEmail { get; set; }

        public int RecievesSensorNotificationByVoice { get; set; }

        public int RecievesSensorNotificationByText { get; set; }

        public string VoiceNumber { get; set; }

        public CreateUserAccount Account { get; set; }

    }

    public class CreateUserAccount
    {
        public long AccountID { get; set; }
    }

    public class UpdateUser
    {
        public string DashboardUserName { get; set; }

        public string DashboardPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string SMSNumber { get; set; }

        public int SMSCarrierID { get; set; }

        public int IsAdmin { get; set; }

        public int DirectSMS { get; set; }

        public int RecievesMaintenanceByPhone { get; set; }

        public int RecievesMaintenanceByEmail { get; set; }

        public int RecievesSensorNotificationByVoice { get; set; }

        public int RecievesSensorNotificationByText { get; set; }

        public string VoiceNumber { get; set; }

        public CreateUserAccount Account { get; set; }

        public long UserID { get; set; }

        public List<long> NetworkPermissions { get; set; }
    }
}
