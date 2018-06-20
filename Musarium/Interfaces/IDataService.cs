using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.Interfaces {
    public interface IDataService {
        Museum InfoAboutMuseum(int id);
        bool EditMuseum(Museum museum);
        string GetCityName(int id);
        IEnumerable<Prize> GetPrizes();
        IEnumerable<Question> GetQuestQuestions(Quest quest);
        IEnumerable<Quest> GetMuseumQuests(int id);
        IEnumerable<Prize> GetMuseumPrizes(int id);
        IEnumerable<Statistic> GetMuseumStatistics(int id);
        bool DeleteQuest(Quest id);
        bool CreatePrize(Prize prize);
        Museum GetByLogin(string login);
        bool CreateQuest(Quest quest, IEnumerable<Question> questions, IEnumerable<Answer> answers, Prize prize);
        bool CreateTextQuestion(Question question, Answer answer);
    }
}