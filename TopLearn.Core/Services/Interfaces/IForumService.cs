using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Questions;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IForumService
    {      

        #region Question

        int AddQuestion(Question question);


        #endregion

        #region Answer

        #endregion
    }
}
