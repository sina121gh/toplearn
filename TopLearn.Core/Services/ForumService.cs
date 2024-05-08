using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs.Question;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.Questions;

namespace TopLearn.Core.Services
{
    public class ForumService : IForumService
    {

        private readonly TopLearnContext _context;

        public ForumService(TopLearnContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public int AddQuestion(Question question)
        {
            question.CreateDate = DateTime.Now;
            question.ModifyDate = DateTime.Now;

            try
            {
                _context.Questions.Add(question);
                _context.SaveChanges();
                return question.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public ShowSingleQuestionViewModel ShowQuestion(int questionId)
        {
            Question? question = _context.Questions
                .Include(q => q.User)
                .Include(q => q.Answers)
                .ThenInclude(a => a.User)
                .SingleOrDefault(q => q.Id == questionId);

            return new ShowSingleQuestionViewModel()
            {
                Question = question,
                Answers = question.Answers.OrderByDescending(a => a.IsTrue).ThenByDescending(a => a.CreateDate),
            };
        }

        public bool AddAnswer(Answer answer)
        {
            try
            {
                _context.Answers.Add(answer);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Question? GetQuestionById(int questionId) => _context.Questions.Find(questionId);

        public bool ChangeTrueAnswer(int questionId, int answerId)
        {
            var answers = _context.Answers.Where(a => a.QuestionId == questionId);
            foreach (var answer in answers)
            {
                answer.IsTrue = false;
            }
            
            answers.SingleOrDefault(a => a.Id == answerId).IsTrue = true;

            try
            {
                _context.UpdateRange(answers);
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Answer? GetAnswerById(int questionId, int answerId)
        {
            return _context.Answers
                .SingleOrDefault(a => a.QuestionId == questionId && 
                a.Id == answerId);
        }

        public ShowCourseQuestionsViewModel GetQuestionsByCourseIdWithIncludes(
            int courseId, string filter = "", int pageId = 1,
            int take = 5)
        {
            IQueryable<Question> result = _context.Questions
                .Where(q => q.CourseId == courseId &&
                EF.Functions.Like(q.Title, $"%{filter}%"))
                .Include(q => q.User);

            int skip = (pageId  - 1) * take;
            int pageCount = (int)Math.Ceiling((double)result.Count() / take);

            return new ShowCourseQuestionsViewModel()
            {
                Qeustions = result.OrderByDescending(q => q.ModifyDate)
                .Skip(skip).Take(take).ToList(),
                CourseId = courseId,
                PageCount = pageCount,
                HasNextPage = pageId < pageCount,
                HasPreviousPage = pageId > 1,
            };
        }
    }
}
