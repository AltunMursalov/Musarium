using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.Interfaces {
    public interface IQuestRepository : IRepository {
        IList<Quest> GetQuests();
        Quest CreateQuest(Quest quest, Prize prize, Museum museum);
        IEnumerable<Quest> GetMuseumQuests(int museumId);
        bool RemoveQuest(int questId);
        void SetPrize(int questId, int prizeId);
    }
} 