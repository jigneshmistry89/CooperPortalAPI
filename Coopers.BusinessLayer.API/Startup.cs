using Microsoft.Owin;
using Owin;
using Autofac;
using System.Web.Http;
using Autofac.Integration.WebApi;
using System.Reflection;
using Coopers.BusinessLayer.Services.Services;
using Coopers.BusinessLayer.NotifEye.APIClient;
using Coopers.BusinessLayer.Database.APIClient.Location;
using AutoMapper;
using Coopers.BusinessLayer.Services.DTO;
using Coopers.BusinessLayer.Database.APIClient.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.DTO;
using System.Web;
using Coopers.BusinessLayer.API.Providers;
using Coopers.BusinessLayer.Model.Interface;
using Coopers.BusinessLayer.Model.DTO;
using Coopers.BusinessLayer.NotifEye.APIClient.HttpService;
using Coopers.BusinessLayer.Database.APIClient;
using Coopers.BusinessLayer.Utility;

[assembly: OwinStartup(typeof(Coopers.BusinessLayer.API.Startup))]

namespace Coopers.BusinessLayer.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType(typeof(LocationApplicationService)).As<ILocationApplicationService>();
            builder.RegisterType(typeof(SensorApplicationService)).As<ISensorApplicationService>();
            builder.RegisterType(typeof(GatewayApplicationService)).As<IGatewayApplicationService>();
            builder.RegisterType(typeof(AccountApplicationService)).As<IAccountApplicationService>();
            builder.RegisterType(typeof(NetworkApplicationService)).As<INetworkApplicationService>();
            builder.RegisterType(typeof(NotificationApplicationService)).As<INotificationApplicationService>();
            builder.RegisterType(typeof(LookupApplicationService)).As<ILookupApplicationService>();
            builder.RegisterType(typeof(AuthenticationApplicationService)).As<IAuthenticationApplicationService>();
            builder.RegisterType(typeof(PaymentApplicationService)).As<IPaymentApplicationService>();
            builder.RegisterType(typeof(TranscationCacheApplicationService)).As<ITranscationCacheApplicationService>();
            builder.RegisterType(typeof(PDFExportApplicationService)).As<IPDFExportApplicationService>();
            builder.RegisterType(typeof(UserApplicationService)).As<IUserApplicationService>();
            builder.RegisterType(typeof(EmailApplicationService)).As<IEmailApplicationService>();
            builder.RegisterType(typeof(PaymentHistoryApplicationService)).As<IPaymentHistoryApplicationService>();

            builder.RegisterType(typeof(SensorClient)).As<ISensorClient>();
            builder.RegisterType(typeof(GatewayClient)).As<IGatewayClient>();
            builder.RegisterType(typeof(AccountClient)).As<IAccountClient>();
            builder.RegisterType(typeof(NetworkClient)).As<INetworkClient>();
            builder.RegisterType(typeof(NotificationClient)).As<INotificationClient>();
            builder.RegisterType(typeof(LookupClient)).As<ILookupClient>();
            builder.RegisterType(typeof(AuthenticationClient)).As<IAuthenticationClient>();
            builder.RegisterType(typeof(NetworkLocationClient)).As<INetworkLocationClient>();
            builder.RegisterType(typeof(NotifEye.APIClient.HttpService.HttpService)).As<NotifEye.APIClient.HttpService.IHttpService>();
            builder.RegisterType(typeof(PaymentHistoryClient)).As<IPaymentHistoryClient>();
            builder.RegisterType(typeof(TaxableStateClient)).As<ITaxableStateClient>();
            builder.RegisterType(typeof(AccountLocationClient)).As<IAccountLocationClient>();
            builder.RegisterType(typeof(Coopers.BusinessLayer.Database.APIClient.HttpService)).As<Database.APIClient.IHttpService>();

            builder.RegisterType(typeof(UserClient)).As<IUserClient>();

           

            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerRequest();
            builder.RegisterType<HttpContextProvider>().As<IHttpContextProvider>().InstancePerRequest();

            builder.Register(context =>
            {
                var configure = new MapperConfiguration(x =>
                {
                    x.CreateMap<LocationSummary, NetworkLocation>().ReverseMap();
                    x.CreateMap<LocationSummary, LocationDTO>().ReverseMap();
                    x.CreateMap<LocationDetails, LocationDTO>().ReverseMap();
                    x.CreateMap<SensorDetails, NotifEye.APIClient.DTO.SensorDetail>().ReverseMap()
                     .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.MonnitApplicationID))
                     .ForMember(dest => dest.Scale, opt => opt.MapFrom(src => src.CurrentReading.Contains("C") ? "C" : "F"));
                    x.CreateMap<SensorInfo, NotifEye.APIClient.DTO.SensorDetail>().ReverseMap()
                                .ForMember(dest => dest.SensorType, opt => opt.MapFrom(src => src.MonnitApplicationID));
                    x.CreateMap<GatewayDetails, GatewayDTO>().ReverseMap();
                    x.CreateMap<GatewayInfo, GatewayDTO>().ReverseMap()
                                .ForMember(dest => dest.CSNetID, opt => opt.MapFrom(src => src.NetworkID));
                    x.CreateMap<LocationDetails, NetworkLocation>().ReverseMap()
                                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.CSNetID));
                    x.CreateMap<AccountLocation, UpdateAccount>().ReverseMap()
                                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName));
                    x.CreateMap<LocationDetails, Model.DTO.Network>().ReverseMap()
                                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.CSNetID));
                    x.CreateMap<AccountLocation, AccountInfo>().ReverseMap()
                                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName));
                    x.CreateMap<UserLocationSummary, NetworkLocation>().ReverseMap()
                                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Name))
                                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.CSNetID));
                    x.CreateMap<UserLocationSummary, SensorSummary>().ReverseMap();
                    x.CreateMap<UserLocationSummary, NotifEye.APIClient.DTO.Network>().ReverseMap()
                                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.NetworkID))
                                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.NetworkName));
                    x.CreateMap<CreateNetwork, NetworkLocation>().ReverseMap();
                    x.CreateMap<SensorCreate, Model.DTO.CreateSensor>().ReverseMap()
                                .ForMember(dest => dest.ReportInterval, opt => opt.MapFrom(src => src.HeartBeat))
                                .ForMember(dest => dest.ActiveStateInterval, opt => opt.MapFrom(src => src.HeartBeat));
                    x.CreateMap<NetworkLocation,UpdateNetwork>().ReverseMap()
                                .ForMember(dest => dest.CSNetID, opt => opt.MapFrom(src => src.NetworkID));
                    x.CreateMap<Model.DTO.Network, UpdateNetwork>().ReverseMap()
                                .ForMember(dest => dest.CSNetID, opt => opt.MapFrom(src => src.NetworkID));
                    x.CreateMap<Model.DTO.SensorDetail, NotifEye.APIClient.DTO.SensorDetail>().ReverseMap();
                    x.CreateMap<Model.DTO.SensorDetail, SensorExtendedDetail>().ReverseMap()
                                .ForMember(dest => dest.SensorType, opt => opt.MapFrom(src => src.MonnitApplicationID));
                    x.CreateMap<Customer, Account>().ReverseMap()
                                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.AccountID))
                                .ForMember(dest => dest.ExpiryDate, opt => opt.MapFrom(src => src.SubscriptionExpiry))
                                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.FullName))
                                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.EmailAddress))
                                .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.SMSNumber))
                                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CompanyName));
                    x.CreateMap<PaymentHistoryInfo, NotifEyeTransactionInfo>().ReverseMap()
                                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => AppConstant.Online))
                                .ForMember(dest => dest.NewRenewalDate, opt => opt.MapFrom(src => src.NewRenewalDate.ToString("yyyy-MM-dd")))
                                .ForMember(dest => dest.OldRenewalDate, opt => opt.MapFrom(src => src.OldRenewalDate.ToString("yyyy-MM-dd")));
                    x.CreateMap<PaymentHistoryInfo, ManualPaymentHistory>().ReverseMap()
                                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => AppConstant.Manual))
                                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.UserName))
                                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => AppConstant.NotifEye))
                                .ForMember(dest => dest.OldRenewalDate, opt => opt.MapFrom(src => src.OldExpirationDate.ToString("yyyy-MM-dd")))
                                .ForMember(dest => dest.NewRenewalDate, opt => opt.MapFrom(src => src.NewExpirationDate.ToString("yyyy-MM-dd")))
                                .ForMember(dest => dest.HistoryDate, opt => opt.MapFrom(src => src.ChangeDate.ToString("yyyy-MM-dd")));


                });
                return configure;
            }).SingleInstance() // We only need one instance
           .AutoActivate() // Create it on ContainerBuilder.Build()
           .AsSelf(); // Bind it to its own type


            // HACK: IComponentContext needs to be resolved again as 'tempContext' is only temporary. See http://stackoverflow.com/a/5386634/718053 
            builder.Register(tempContext =>
            {
                var ctx = tempContext.Resolve<IComponentContext>();
                var confg = ctx.Resolve<MapperConfiguration>();
                // Create our mapper using our configuration above
                return confg.CreateMapper();
            }).As<IMapper>(); // Bind it to the IMapper interface


            var container = builder.Build();


            ConfigureAuth(app);

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
