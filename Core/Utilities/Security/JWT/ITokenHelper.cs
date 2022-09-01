using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, 
                                List<OperationClaim> operationClaims); // Kullanıcı claimlerini buluşturacak sonra jvt üretecek bunu tekrar gönderecek 
    }
}
//(User user, List<OperationClaim> operationClaim);