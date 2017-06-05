using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotSpace.Enumerations
{
    public enum StatusCode : int
    {
        OK = 200,
        BAD_REQUEST = 400,
        BAD_RESPONSE = 401,
        NOT_FOUND = 404,
        METHOD_NOT_ALLOWED = 405,
        NOT_IMPLEMENTED = 501

    }
}
