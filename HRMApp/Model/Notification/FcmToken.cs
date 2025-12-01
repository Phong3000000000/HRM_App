using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApp.Model.Notification
{
    public class FcmToken
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string Token { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
