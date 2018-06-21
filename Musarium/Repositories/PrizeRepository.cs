using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Musarium.Repositories {
    public class PrizeRepository : IPrizeRepository {
        private DbConnection connection;
        private DbProviderFactory factory;
        private AppData AppData = AppData.GetInstance();

        public bool OpenConnection() {
            try {
                factory = DbProviderFactories.GetFactory(AppData.ItstepAcademy.ProviderName);
                connection = factory.CreateConnection();
                connection.ConnectionString = AppData.ItstepAcademy.ConnectionString;
                connection.Open();
                return true;
            }
            catch (DbException) {
                return false;
            }
        }

        public bool IsPrizeExist(int id) {
            DbCommand command = connection.CreateCommand();
            var _id = this.AppData.GetParameter("Id", id, System.Data.DbType.Int32, "Id", command);
            command.Parameters.Add(_id);
            command.CommandText = "SELECT Id FROM Prizes WHERE Id = @Id";
            var result = command.ExecuteScalar();
            if(result != null) {
                return true;
            } else {
                return false;
            }
        }

        public void CloseConnection() {
            if (connection != null)
                this.connection.Close();
        }

        public IList<Prize> GetPrizes() {
            try {
                DbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Prizes";
                IList<Prize> prizes = new List<Prize>();
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    Prize prize = new Prize {
                        Id = Convert.ToInt32(reader["Id"]),
                        PictureSrc = Convert.ToString(reader["PictureSrc"]),
                        PrizeName = Convert.ToString(reader["PrizeName"])
                    };
                    prizes.Add(prize);
                }
                reader.Close();
                return prizes;
            }
            catch (DbException) {
                return null;
            }

        }

        public Prize CreatePrize(Prize prize) {
            try {
                DbCommand command = connection.CreateCommand();
                var _prizeName = AppData.GetParameter("PrizeName", prize.PrizeName, System.Data.DbType.String, "PrizeName", command);
                var _prizeSrc = AppData.GetParameter("PictureSrc", prize.PictureSrc, System.Data.DbType.String, "PictureSrc", command);
                command.Parameters.Add(_prizeName);
                command.Parameters.Add(_prizeSrc);
                command.CommandText = "INSERT INTO Prizes (PrizeName, PictureSrc) VALUES (@PrizeName, @PictureSrc)";
                command.ExecuteNonQuery();
                prize.Id = AppData.GetLastId("Prizes", this.connection);
                return prize;
            }
            catch (DbException ex) {
                return null;
            }
        } 

        public IEnumerable<Prize> GetMuseumPrizes(int museumId) {
            DbCommand command = connection.CreateCommand();
            var _museumId = AppData.GetParameter("MuseumId", museumId, System.Data.DbType.Int32, "MuseumId", command);
            command.Parameters.Add(_museumId);
            command.CommandText = "SELECT DISTINCT Prizes.Id, Prizes.PrizeName, Prizes.PictureSrc FROM Prizes JOIN Quests"
                                  + " ON Quests.PrizeId = Prizes.Id"
                                  +" WHERE Quests.MuseumId = @MuseumId";
            IList<Prize> prizes = new List<Prize>();
            var reader = command.ExecuteReader();
            while (reader.Read()) {
                Prize prize = new Prize {
                    Id = Convert.ToInt32(reader["Id"]),
                    PictureSrc = Convert.ToString(reader["PictureSrc"]),
                    PrizeName = Convert.ToString(reader["PrizeName"])
                };
                prizes.Add(prize);
            }
            reader.Close();
            return prizes;

        }
    }
}