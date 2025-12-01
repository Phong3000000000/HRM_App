using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApp.Model
{
    public enum RequestCategory
    {
        ot = 0,
        leave = 1,
        resignation = 2,
        business_trip = 3,
        incident = 4,
        proposal = 5,
        other = 6
    }

    public enum RequestStatus
    {
        pending = 0,
        approved = 1,
        rejected = 2,
        cancelled = 3
    }
}
