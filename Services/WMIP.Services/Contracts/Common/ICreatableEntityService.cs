using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Contracts.Common
{
    public interface ICreatableEntityService<TDto>
    {
        bool Create(TDto creationInfo);
    }
}
