using Musarium.Model;

namespace Musarium.Interfaces {
    public interface ICityRepository : IRepository {
        City GetMuseumCityById(int id);
    }
}