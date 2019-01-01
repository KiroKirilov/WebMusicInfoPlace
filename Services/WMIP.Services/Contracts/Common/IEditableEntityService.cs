using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Contracts.Common
{
    public interface IEditableEntityService<TDto>
    {
        bool Edit(TDto editInfo);
    }
}
