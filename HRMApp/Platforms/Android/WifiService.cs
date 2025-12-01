using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net.Wifi;
using AndroidX.Core.Content;
using HRMApp.Services;
using HRMApp.Services.Wifi;
using Microsoft.Maui.ApplicationModel;
using System.Security;
using Android;
using Application = Android.App.Application;

namespace HRMApp.Platforms.Android
{
    public class WifiService : IWifiService
    {
        public async Task<(string ssid, string bssid)> GetWifiInfoAsync()
        {
            try
            {
                // ✅ Kiểm tra quyền location trước khi đọc Wi-Fi info
                var context = Application.Context;
                
                if (ContextCompat.CheckSelfPermission(context, Manifest.Permission.AccessFineLocation) 
                    != Permission.Granted)
                {
                    System.Diagnostics.Debug.WriteLine("❌ Không có quyền ACCESS_FINE_LOCATION để đọc BSSID");
                    return ("Cần quyền location", "Không có quyền");
                }

                var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);

                if (wifiManager?.ConnectionInfo != null)
                {
                    var ssid = wifiManager.ConnectionInfo.SSID?.Replace("\"", "");
                    var bssid = wifiManager.ConnectionInfo.BSSID;
                    
                    System.Diagnostics.Debug.WriteLine($"✅ Wi-Fi Info: SSID={ssid}, BSSID={bssid}");
                    
                    return (ssid ?? "Unknown", bssid ?? "Unknown");
                }
            }
            catch (SecurityException secEx)
            {
                System.Diagnostics.Debug.WriteLine($"🔒 SecurityException: {secEx.Message}");
                return ("Lỗi bảo mật", "Cần cấp quyền location");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Wi-Fi error: {ex.Message}");
            }

            return ("Không xác định", "");
        }
    }
}
