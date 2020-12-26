﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Entities.Interfaces
{
    public interface IAudit
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }       
        DateTime? ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
    }
}
