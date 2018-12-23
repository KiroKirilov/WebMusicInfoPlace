using System;
using System.Collections.Generic;
using System.Text;

namespace WMIP.Data.Models.Common
{
    public abstract class BaseModel<T>
    {
        public BaseModel()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
