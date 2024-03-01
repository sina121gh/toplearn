using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.Core.DTOs
{
	public class UsersForAdminViewModel
	{
        public IEnumerable<User>? Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set;}
    }
}
