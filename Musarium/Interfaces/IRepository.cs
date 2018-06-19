using System.Data;
using System.Data.Common;

namespace Musarium.Interfaces {
    public interface IRepository {
        bool OpenConnection();
        void CloseConnection();
    }
}