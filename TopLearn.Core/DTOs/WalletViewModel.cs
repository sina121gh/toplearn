using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.DTOs
{
	public class TransactionsListViewModel
	{
        public int Id { get; set; }
        public bool Type { get; set; }
        public int Amout { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
