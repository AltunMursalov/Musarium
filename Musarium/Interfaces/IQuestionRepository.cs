using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Interfaces {
    public interface IQuestionRepository : IRepository {
        IList<Question> GetQuestions();
        Question CreateQuestion(Question question);
        IEnumerable<Question> GetQuestQuestions(int questId);
        int GetQuestQuestionsCount(int questId);
    }
} 