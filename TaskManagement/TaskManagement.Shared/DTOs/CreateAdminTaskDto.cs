using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Shared.DTOs
{
    public class CreateAdminTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
