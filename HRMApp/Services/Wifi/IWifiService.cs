using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApp.Services.Wifi
{
    public interface IWifiService
    {
        Task<(string ssid, string bssid)> GetWifiInfoAsync();
    }
}
