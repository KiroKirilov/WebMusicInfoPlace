using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Services.Contracts.Common
{
    public interface ICrudableEntityService<TCreateDto, TEditDto, TEntity, TKey> :
        ICreatableEntityService<TCreateDto>, IEditableEntityService<TEditDto>, IDeleteableEntityService, IGettableEntityService<TEntity, TKey>
    {
    }
}
