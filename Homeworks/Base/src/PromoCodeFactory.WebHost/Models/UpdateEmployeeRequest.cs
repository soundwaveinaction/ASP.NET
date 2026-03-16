using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models
{
    public record UpdateEmployeeRequest(string FirstName,
                                        string SecondName,
                                        string Email,
                                        List<Guid> RoleIds,
                                        int AppliedPromocodesCount);
}