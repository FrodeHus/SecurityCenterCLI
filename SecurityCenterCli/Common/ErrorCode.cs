using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCenterCli.Common
{
    public enum ErrorCode
    {
        NoError = 0,
        InvalidArgument = 100,
        InvalidOperation = 101,
        AuthenticationError = 200,
        CredentialNotSet = 201,
    }
}
