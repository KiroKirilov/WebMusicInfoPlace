using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Contracts.Common
{
    public interface IGettableEntityService<TEntity, TKey>
    {
        TEntity GetById(TKey id);
    }
}
