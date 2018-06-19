using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Musarium.Repositories {
    public class AnswerRepository : IAnswerRepository {
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

        public Answer CreateAnswer(Answer answer) {
            try {
                DbCommand command = connection.CreateCommand();
                var _answer = AppData.GetParameter("Answer", answer.QuestionAnswer, System.Data.DbType.String, "Answer", command);
                var _isRight = AppData.GetParameter("IsRight", true, System.Data.DbType.Boolean, "IsRight", command);
                var _questionId = AppData.GetParameter("QuestionId", answer.QuestionID, System.Data.DbType.Int32, "QuestionId", command);
                command.Parameters.Add(_answer);
                command.Parameters.Add(_questionId);
                command.Parameters.Add(_isRight);
                command.CommandText = "INSERT INTO Answers (Answer, IsRight, QuestionId) VALUES (@Answer, @IsRight, @QuestionId)";
                command.ExecuteNonQuery();
                answer.Id = AppData.GetLastId("Answers", connection);
                return answer;
            }
            catch (DbException) {
                return null;
            }
        }

        public IEnumerable<Answer> GetAnswersByQuestion(int questionId) {
            try {
                DbCommand command = connection.CreateCommand();
                var _questionId = AppData.GetParameter("QuestionId", questionId, System.Data.DbType.Int32, "QuestionId", command);
                command.Parameters.Add(_questionId);
                command.CommandText = "SELECT * FROM Answers WHERE QuestionId = @QuestionId";
                var reader = command.ExecuteReader();
                IList<Answer> answers = new List<Answer>();
                while (reader.Read()) {
                    answers.Add(new Answer {
                        Id = Convert.ToInt32(reader["Id"]),
                        IsRight = Convert.ToBoolean(reader["IsRight"]),
                        QuestionAnswer = Convert.ToString(reader["Answer"]),
                        QuestionID = Convert.ToInt32(reader["QuestionId"])
                    });
                }
                reader.Close();
                return answers;
            }
            catch (DbException) {
                return null;
            }
        }
    }
}
