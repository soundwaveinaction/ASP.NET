using System;
using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models;

public record CreateEmployeeRequest(string FirstName,
                                    string LastName,
                                    string Email,
                                    List<Guid> RoleIds,
                                    int AppliedPromocodesCount);

