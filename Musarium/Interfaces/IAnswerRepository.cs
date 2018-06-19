using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.Interfaces {
    public interface IAnswerRepository : IRepository {
        Answer CreateAnswer(Answer answer);
        IEnumerable<Answer> GetAnswersByQuestion(int questionId);
    }
}
