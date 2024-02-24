using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {
            
        }

        public int Id { get; set; }
        public int UserId { get; set; }


        #region Relations

        [ForeignKey("UserId")]
        public virtual User.User User { get; set; }
        public virtual IEnumerable<Transaction> Transactions { get; set; }

        #endregion
    }
}
