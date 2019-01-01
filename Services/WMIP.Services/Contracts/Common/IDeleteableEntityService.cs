using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Contracts.Common
{
    public interface IDeleteableEntityService
    {
        bool Delete(int id);
    }
}
