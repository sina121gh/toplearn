using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Question;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Questions;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IForumService
    {

        #region Question

        ShowCourseQuestionsViewModel GetQuestionsByCourseIdWithIncludes(
            int courseId, string filter = "", int pageId = 1, int take = 5);
        Question? GetQuestionById(int questionId);
        int AddQuestion(Question question);
        ShowSingleQuestionViewModel ShowQuestion(int questionId);

        #endregion

        #region Answer

        bool AddAnswer(Answer answer);
        bool ChangeTrueAnswer(int questionId, int answerId);
        Answer? GetAnswerById(int questionId, int answerId);

        #endregion
    }
}
