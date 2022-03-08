using GGPuntoYComa.SSO.Models.Consent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGPuntoYComa.SSO.Models.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}
