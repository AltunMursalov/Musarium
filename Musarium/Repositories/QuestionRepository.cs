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
    public class QuestionRepository : IQuestionRepository {
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

        public IList<Question> GetQuestions() {
            try {
                DbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Questions";
                DbDataReader reader = command.ExecuteReader();
                IList<Question> questions = new List<Question>();
                while (reader.Read()) {
                    Question question = new Question {
                        Id = Convert.ToInt32(reader["Id"]),
                        Description = Convert.ToString(reader["Description"]),
                        Hint = Convert.ToString(reader["Hint"]),
                        PictureSrc = Convert.ToString(reader["PictureSrc"]),
                        Points = Convert.ToInt32(reader["Points"]),
                        QuestionType = Convert.ToInt32(reader["QuestionType"]),
                        QuestId = Convert.ToInt32(reader["QuestId"])
                    };
                    questions.Add(question);
                }
                reader.Close();
                return questions;
            }
            catch (DbException) {
                return null;
            }
        }

        public Question CreateQuestion(Question question) {
            try {
                DbCommand command = connection.CreateCommand();
                var _desc = AppData.GetParameter("Description", question.Description, System.Data.DbType.String, "Description", command);
                var _picSrc = AppData.GetParameter("PictureSrc", question.Description, System.Data.DbType.String, "PictureSrc", command);
                var _points = AppData.GetParameter("Points", question.Description, System.Data.DbType.Int32, "Points", command);
                var _hint = AppData.GetParameter("Hint", question.Description, System.Data.DbType.String, "Hint", command);
                var _questionType = AppData.GetParameter("QuestionType", question.Description, System.Data.DbType.Int32, "QuestionType", command);
                var _questId = AppData.GetParameter("QuestId", question.Description, System.Data.DbType.Int32, "QuestId", command);
                command.CommandText = "INSERT INTO Questions (Description, PictureSrc, Points, Hint, QuestionType, QuestId) " +
                                      "VALUES (@Description, @PictureSrc, @Points, @Hint, @QuestionType, @QuestId)";
                command.ExecuteNonQuery();
                question.Id = AppData.GetLastId("Questions", connection);
                return question;
            }
            catch (DbException) {
                return null;
            }
        }
        
        public IEnumerable<Question> GetQuestQuestions(int questId) {
            try {
                DbCommand command = connection.CreateCommand();
                var _questId = AppData.GetParameter("QuestId", questId, System.Data.DbType.Int32, "QuestId", command);
                command.Parameters.Add(_questId);
                command.CommandText = "SELECT * FROM Questions WHERE QuestId = @QuestId";
                IList<Question> questions = new List<Question>();
                var reader = command.ExecuteReader();
                while (reader.Read()) {
                    Question question = new Question {
                        Id = Convert.ToInt32(reader["Id"]),
                        Description = Convert.ToString(reader["Description"]),
                        Hint = Convert.ToString(reader["Hint"]),
                        PictureSrc = Convert.ToString(reader["PictureSrc"]),
                        Points = Convert.ToInt32(reader["Points"]),
                        QuestId = Convert.ToInt32(reader["QuestId"]),
                        QuestionType = Convert.ToInt32(reader["QuestionType"])
                    };
                    questions.Add(question);
                }
                reader.Close();
                return questions;
            }
            catch (DbException) {
                return null;
            }
        }

        public int GetQuestQuestionsCount(int questId) {
            DbCommand dbCommand = connection.CreateCommand();
            var _questId = AppData.GetParameter("QuestId", questId, System.Data.DbType.Int32, "QuestId", dbCommand);
            dbCommand.Parameters.Add(_questId);
            dbCommand.CommandText = "SELECT COUNT(Id) FROM Questions WHERE QuestId = @QuestId";
            var result = dbCommand.ExecuteScalar();
            if (result != null) {
                return (int)result;
            } else {
                return 0;
            }
        }
    }
}