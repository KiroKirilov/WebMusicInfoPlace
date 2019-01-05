using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WMIP.Data.Models.Common
{
    public abstract class BaseModel<T>
    {
        public BaseModel()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public T Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
