using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Repositories {
    public class CityRepository : ICityRepository {
        private DbConnection connection;
        private DbProviderFactory factory;
        private AppData AppData = AppData.GetInstance();

        public bool OpenConnection() {
            try {
                factory = DbProviderFactories.GetFactory(AppData.MyConnection.ProviderName);
                connection = factory.CreateConnection();
                connection.ConnectionString = AppData.MyConnection.ConnectionString;
                connection.Open();
                return true;
            }
            catch (DbException) {
                return false;
            }
        }

        public void CloseConnection() {
            if (connection != null)
                this.connection.Close();
        }

        public City GetMuseumCityById(int id) {
            try {
                DbCommand command = connection.CreateCommand();
                var museumId = AppData.GetParameter("MuseumId", id, System.Data.DbType.Int32, "Id", command);
                command.Parameters.Add(museumId);
                command.CommandText = "SELECT * FROM Cities WHERE Id = @MuseumId";
                var reader = command.ExecuteReader();
                City city = new City();
                if (reader.Read()) {
                    city.Id = Convert.ToInt32(reader["Id"]);
                    city.Name = Convert.ToString(reader["CityName"]);
                }
                reader.Close();
                return city;
            }
            catch (DbException) {
                return null;
            }
        }
    }
}
