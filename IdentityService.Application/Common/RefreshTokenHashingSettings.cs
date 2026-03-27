using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Application.Common
{
    public sealed class RefreshTokenHashingSettings
    {
        public string Secret { get; set; } = string.Empty;
    }
}
