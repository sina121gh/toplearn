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
                Answers = question.Answers
            };
        }
    }
}
