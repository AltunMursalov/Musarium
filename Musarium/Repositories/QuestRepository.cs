using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Musarium.Repositories {
    public class QuestRepository : IQuestRepository {
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

        public void CloseConnection() {
            if (connection != null)
                connection.Close();
        }

        public IList<Quest> GetQuests() {
            try {
                DbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Quests";
                DbDataReader reader = command.ExecuteReader();
                IList<Quest> quests = new List<Quest>();
                while (reader.Read()) {
                    Quest quest = new Quest {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = Convert.ToString(reader["Title"]),
                        Description = Convert.ToString(reader["Description"]),
                        Difficult = Convert.ToInt32(reader["Difficult"]),
                        MuseumId = Convert.ToInt32(reader["MuseumId"]),
                        PrizeId = Convert.ToInt32(reader["PrizeId"]),
                        Point = Convert.ToInt32(reader["Points"]),
                        PictureSrc = Convert.ToString(reader["PictureSrc"])
                    };
                    quests.Add(quest);
                }
                reader.Close();
                return quests;
            }
            catch (DbException) {
                return null;
            }
        }

        public Quest CreateQuest(Quest quest, Prize prize, Museum museum) {
            try {
                DbCommand command = connection.CreateCommand();
                var _questTitle = AppData.GetParameter("QuestTitle", quest.Title, System.Data.DbType.String, "Title", command);
                var _questDescription = AppData.GetParameter("QuestDescription", quest.Description, System.Data.DbType.String, "Description", command);
                var _questDifficult = AppData.GetParameter("QuestDifficult", quest.Difficult, System.Data.DbType.Int32, "Difficult", command);
                var _questPictureSrc = AppData.GetParameter("QuestPictureSrc", quest.PictureSrc, System.Data.DbType.String, "PictureSrc", command);
                var _questPoint = AppData.GetParameter("QuestPoints", quest.Point, System.Data.DbType.Int32, "Points", command);
                var _MuseumId = AppData.GetParameter("MuseumId", museum.Id, System.Data.DbType.Int32, "MuseumId", command);
                var _PrizeId = AppData.GetParameter("PrizeId", prize.Id, System.Data.DbType.Int32, "PrizeId", command);
                command.Parameters.Add(_questTitle);
                command.Parameters.Add(_MuseumId);
                command.Parameters.Add(_PrizeId);
                command.Parameters.Add(_questDescription);
                command.Parameters.Add(_questDifficult);
                command.Parameters.Add(_questPictureSrc);
                command.Parameters.Add(_questPoint);
                command.CommandText = "INSERT INTO Quests (Title, Description, Difficult, PictureSrc, Points, MuseumId, PrizeId) " +
                                      "VALUES (@QuestTitle, @QuestDescription, @QuestDifficult, @QuestPictureSrc, @QuestPoints, @MuseumId, @PrizeId)";
                command.ExecuteNonQuery();
                quest.Id = AppData.GetLastId("Quests", this.connection);
                return quest;
            }
            catch (DbException) {
                return null;
            }
        }

        public IEnumerable<Quest> GetMuseumQuests(int museumId) {
            try {
                DbCommand command = connection.CreateCommand();
                var _museumId = AppData.GetParameter("MuseumId", museumId, System.Data.DbType.Int32, "MuseumId", command);
                command.Parameters.Add(_museumId);
                command.CommandText = "SELECT * FROM Quests WHERE MuseumId = @MuseumId";
                var reader = command.ExecuteReader();
                IList<Quest> quests = new List<Quest>();
                while (reader.Read()) {
                    Quest quest = new Quest {
                        Id = Convert.ToInt32(reader["Id"]),
                        Description = Convert.ToString(reader["Description"]), 
                        Difficult = Convert.ToInt32(reader["Difficult"]),
                        MuseumId = Convert.ToInt32(reader["MuseumId"]),
                        PictureSrc = Convert.ToString(reader["PictureSrc"]),
                        Point = Convert.ToInt32(reader["Points"]),
                        PrizeId = Convert.ToInt32(reader["PrizeId"]),
                        Title = Convert.ToString(reader["Title"])
                    };
                    quests.Add(quest);
                }
                reader.Close();
                return quests;
            }
            catch (DbException) {
                return null;
            }
        }

        public bool RemoveQuest(int questId) {
            DbTransaction transaction = connection.BeginTransaction();
            try {
                DbCommand command = connection.CreateCommand();
                var _questId = factory.CreateParameter();
                _questId.DbType = System.Data.DbType.Int32;
                _questId.ParameterName = "QuestId";
                _questId.Value = questId;
                command.Transaction = transaction;
                command.Parameters.Add(_questId);
                command.CommandText = "DELETE FROM Answers WHERE QuestionId = ANY (SELECT Id FROM Questions WHERE  QuestId = @QuestId) " +
                                             "DELETE FROM Questions WHERE QuestId = @QuestId " +
                                             "DELETE FROM Quests WHERE Id = @QuestId";
                int resuls = (int)command.ExecuteNonQuery();
                transaction.Commit();
                return true;
            }
            catch (DbException) {
                transaction.Rollback();
                return false;
            }
        }
    }
}