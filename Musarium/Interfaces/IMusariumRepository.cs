using Musarium.Model;
using System.Collections.Generic;

namespace Musarium.Interfaces {
    public interface IMusariumRepository : IRepository {
        IList<Museum> GetMuseums();
        Museum GetMuseumById(int id);
        bool Update(Museum museum);
        Museum GetMuseumByLogin(string login);
        Museum CreateMuseum(string login, string password, string city);
    }
}