using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.Interfaces {
    public interface IStatisticRepository {
        IList<Statistic> GetStatistics();
        Statistic GetStatisticByQuest(Quest quest);
    }
}