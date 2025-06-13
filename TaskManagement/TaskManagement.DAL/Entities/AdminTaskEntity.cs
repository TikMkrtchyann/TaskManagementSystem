using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DAL.Entities
{
    public class AdminTaskEntity : TaskEntity
    {
        public string Username { get; set; } = string.Empty;
    }
}
