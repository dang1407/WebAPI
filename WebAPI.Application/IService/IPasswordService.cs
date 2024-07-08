using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public interface IPasswordService
    {
        string GenerateRefreshToken();
        string GenerateVerificationCode();
        string ComputeSha256Hash(string rawData);
    }
}
