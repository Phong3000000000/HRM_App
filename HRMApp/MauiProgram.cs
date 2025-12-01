using HRMApp.Helpers;  // 👈 nhớ import
using HRMApp.Services.Api;
using HRMApp.Services.Wifi;
using HRMApp.Services.Notification; // ✅ THÊM dòng này
using HRMApp.View;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Bundled.Platforms.Android;
using Plugin.Firebase.Bundled.Shared;
using Plugin.Firebase.Crashlytics;
using Refit;
using System.Diagnostics;
using HRMApp.ViewModels;

namespace HRMApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.otf", "FontAwesome");
                    fonts.AddFont("Font Awesome 6 Brands-Regular-400.otf", "FAB");
                    fonts.AddFont("Font Awesome 6 Free-Regular-400.otf", "FAR");
                    fonts.AddFont("Font Awesome 6 Free-Solid-900.otf", "FAS");
                });

            // ✅ URL của Web API
            const string BaseApiUrl = "https://hrmadmin.huynhthanhson.io.vn/";
            const string LocalApiUrl = "http://192.168.1.40:5246/"; 


            // ✅ Cấu hình Refit + JWT token tự động
            var refitSettings = new RefitSettings
            {
                HttpMessageHandlerFactory = () =>
                {
                    return new AuthHeaderHandler();
                }
            };



            // ✅ Đăng ký Refit client (tự động gắn token vào mọi request)
            builder.Services.AddSingleton<IHrmApi>(
                RestService.For<IHrmApi>(BaseApiUrl, refitSettings)
            );

            builder.Services.AddSingleton<ILocalApi>(
                RestService.For<ILocalApi>(LocalApiUrl, refitSettings)
            );

            // ✅ THÊM: Đăng ký SignalR và Notification services
            builder.Services.AddSingleton<ISignalRService, SignalRService>();
            builder.Services.AddSingleton<IDeviceNotificationService, DeviceNotificationService>();

            // ✅ Đăng ký wifi service
            builder.Services.AddSingleton<IWifiService, HRMApp.Platforms.Android.WifiService>();

            // ĐĂNG KÝ SERVICE
            builder.Services.AddSingleton<NotificationStateService>();

            // ✅ Đăng ký ViewModel để BindingContext không bị null
            builder.Services.AddTransient<HRMApp.ViewModels.LoginViewModel>();

            // Thêm vào phần đăng ký services trong CreateMauiApp()
            builder.Services.AddTransient<RequestFormPage>();
            builder.Services.AddTransient<RequestListPage>();

            // Thêm vào phần đăng ký services
            //builder.Services.AddTransient<HRMApp.ViewModels.NotificationViewModel>();
            //builder.Services.AddTransient<NotificationPage>();
            // Đảm bảo chỉ có MỘT ViewModel và MỘT Page trong suốt vòng đời app
            builder.Services.AddSingleton<HRMApp.ViewModels.NotificationViewModel>();
            builder.Services.AddSingleton<NotificationPage>();

            // Thêm vào phần đăng ký services
            builder.Services.AddTransient<NotificationDetailViewModel>();
            builder.Services.AddTransient<NotificationDetailPage>();

            //training
            builder.Services.AddTransient<HRMApp.ViewModel.TrainingListViewModel>();
            builder.Services.AddTransient<HRMApp.ViewModel.TrainingDetailViewModel>();
            builder.Services.AddTransient<HRMApp.View.TrainingListPage>();
            builder.Services.AddTransient<HRMApp.View.TrainingDetailPage>();

            //schedule
            builder.Services.AddTransient<ScheduleViewModel>();
            builder.Services.AddTransient<SchedulePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // ✅ Quan trọng: Gán Provider cho ServiceHelper sau khi Build
            var app = builder.Build();
            ServiceHelper.Provider = app.Services;

            return app;
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events =>
            {
#if IOS
                events.AddiOS(iOS => iOS.FinishedLaunching((app, launchOptions) => {
                    CrossFirebase.Initialize(CreateCrossFirebaseSettings());
                    return false;
                }));
#else
                events.AddAndroid(android => android.OnCreate((activity, _) =>
                    CrossFirebase.Initialize(activity, CreateCrossFirebaseSettings())));
                CrossFirebaseCrashlytics.Current.SetCrashlyticsCollectionEnabled(true);
#endif
            });

            builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
            return builder;
        }

        private static CrossFirebaseSettings CreateCrossFirebaseSettings()
        {
            return new CrossFirebaseSettings(isAuthEnabled: true,
            isCloudMessagingEnabled: true, isAnalyticsEnabled: true);
        }
    }

    // Thêm DelegatingHandler để xử lý token
    public class AuthHeaderHandler : DelegatingHandler
    {
        public AuthHeaderHandler() : base(new HttpClientHandler())
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await SecureStorage.GetAsync("jwt_token");
            Debug.WriteLine($"🧩 Header token gửi đi: {token}");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
