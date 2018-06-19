using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Quest CreateQuest(Quest quest, Question question, Prize prize, Museum museum);
        bool DeleteQuest(Quest id);
        bool CreatePrize(Prize prize);
        Museum GetByLogin(string login);
        bool CreateQuestion(Question question, Answer answer);
    }
}