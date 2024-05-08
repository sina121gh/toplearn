using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Questions
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsTrue { get; set; } = false;

        #region Relations

        [ForeignKey("QuestionId")]
        public Question? Question { get; set; }

        [ForeignKey("UserId")]
        public User.User? User { get; set; }

        #endregion

    }
}
