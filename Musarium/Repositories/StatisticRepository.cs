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
    class StatisticRepository : IRepository, IStatisticRepository {
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
                connection.Close();
        }

        public Statistic GetStatisticByQuest(Quest quest) {
            DbCommand command = connection.CreateCommand();
            var _questId = AppData.GetParameter("QuestId", quest.Id, System.Data.DbType.Int32, "QuestId", command);
            command.Parameters.Add(_questId);
            command.CommandText = "SELECT * FROM Statics WHERE QuestId = @QuestId";
            DbDataReader reader = command.ExecuteReader();
            if (reader.Read()) {
                Statistic statistic = new Statistic {
                    Id = Convert.ToInt32(reader["Id"]),
                    DateTime = Convert.ToDateTime(reader["DateTime"]),
                    Duration = Convert.ToDateTime(reader["Duration"]),
                    Iscomplete = Convert.ToBoolean(reader["IsComplete"]),
                    Points = Convert.ToInt32(reader["Points"]),
                    PrizeId = Convert.ToInt32(reader["PrizeId"]),
                    QuestId = Convert.ToInt32(reader["QuestId"]),
                    UserId = Convert.ToInt32(reader["UserId"]),
                };
                reader.Close();
                return statistic;
            } else {
                reader.Close();
                return null;
            }
        }

        public IList<Statistic> GetStatistics() {
            try {
                DbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Statics";
                DbDataReader reader = command.ExecuteReader();
                IList<Statistic> statistics = new List<Statistic>();
                while (reader.Read()) {
                    Statistic statistic = new Statistic {
                        Id = Convert.ToInt32(reader["Id"]),
                        DateTime = Convert.ToDateTime(reader["DateTime"]),
                        Duration = Convert.ToDateTime(reader["Duration"]),
                        Iscomplete = Convert.ToBoolean(reader["IsComplete"]),
                        Points = Convert.ToInt32(reader["Points"]),
                        PrizeId = Convert.ToInt32(reader["PrizeId"]),
                        QuestId = Convert.ToInt32(reader["QuestId"]),
                        UserId = Convert.ToInt32(reader["UserId"]),
                    };
                    statistics.Add(statistic);
                }
                reader.Close();
                return statistics;
            }
            catch (DbException) {
                return null;                
            }
        }
    }
}