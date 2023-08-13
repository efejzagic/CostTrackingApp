using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Entities
{
    public struct OAuthResponse
    {
        public string access_token;
        public int expires_in;
    }
}
