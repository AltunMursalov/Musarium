using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.Interfaces {
    public interface IPrizeRepository : IRepository {
        IList<Prize> GetPrizes();
        Prize CreatePrize(Prize prize);
        bool IsPrizeExist(int id);
        IEnumerable<Prize> GetMuseumPrizes(int museumId);
    }
} 