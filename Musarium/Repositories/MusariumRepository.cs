using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Musarium.Repositories {
    public class MusariumRepository : IMusariumRepository, IRepository {
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
                this.connection.Close();
        }

        public Museum GetMuseumByLogin(string login) {
            DbCommand command = connection.CreateCommand();
            var _login = AppData.GetParameter("Login", login, DbType.String, "Login", command);
            command.Parameters.Add(_login);
            command.CommandText = "SELECT * FROM Museums WHERE Login = @Login";
            var reader = command.ExecuteReader();
            Museum museum = new Museum();
            if (reader.Read()) {
                museum.Id = Convert.ToInt32(reader["Id"]);
                museum.Title = Convert.ToString(reader["Title"]);
                museum.Adress = Convert.ToString(reader["Address"]);
                museum.CityId = Convert.ToInt32(reader["CityId"]);
                museum.Description = Convert.ToString(reader["Description"]);
                museum.Latitude = Convert.ToSingle(reader["Latitude"]);
                museum.Login = Convert.ToString(reader["Login"]);
                museum.Password = Convert.ToString(reader["Password"]);
                museum.Longitude = Convert.ToSingle(reader["Longitude"]);
                museum.Phone = Convert.ToString(reader["Phone"]);
                museum.PictureSrc = Convert.ToString(reader["PictureSrc"]);
                museum.Radius = Convert.ToSingle(reader["Radius"]);
                museum.WebSite = Convert.ToString(reader["WebSite"]);
                return museum;
            } else {
                return null;
            }
        }

        public Museum GetMuseumById(int id) {
            try {
                DbCommand command = connection.CreateCommand();
                var _id = AppData.GetParameter("Id", id, DbType.Int32, "Id", command);
                command.Parameters.Add(_id);
                command.CommandText = "SELECT * FROM Museums WHERE Id = @Id";
                DbDataReader reader = command.ExecuteReader();
                Museum museum = null;
                if (reader.Read()) {
                    museum = new Museum {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = Convert.ToString(reader["Title"]),
                        Adress = Convert.ToString(reader["Address"]),
                        CityId = Convert.ToInt32(reader["CityId"]),
                        Description = Convert.ToString(reader["Description"]),
                        Latitude = Convert.ToSingle(reader["Latitude"]),
                        Login = Convert.ToString(reader["Login"]),
                        Password = Convert.ToString(reader["Password"]),
                        Longitude = Convert.ToSingle(reader["Longitude"]),
                        Phone = Convert.ToString(reader["Phone"]),
                        PictureSrc = Convert.ToString(reader["PictureSrc"]),
                        Radius = Convert.ToSingle(reader["Radius"]),
                        WebSite = Convert.ToString(reader["WebSite"])
                    };
                }
                reader.Close();
                return museum;
            }
            catch (DbException) {
                return null;
            }
        }

        public IList<Museum> GetMuseums() {
            try {
                DbCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Museums";
                DbDataReader reader = command.ExecuteReader();
                IList<Museum> museums = new List<Museum>();
                while (reader.Read()) {
                    Museum museum = new Museum {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = Convert.ToString(reader["Title"]),
                        Adress = Convert.ToString(reader["Adress"]),
                        CityId = Convert.ToInt32(reader["CityId"]),
                        Description = Convert.ToString(reader["Description"]),
                        Latitude = Convert.ToSingle(reader["Latitude"]),
                        Login = Convert.ToString(reader["Login"]),
                        Password = Convert.ToString(reader["Password"]),
                        Longitude = Convert.ToSingle(reader["Longitude"]),
                        Phone = Convert.ToString(reader["Phone"]),
                        PictureSrc = Convert.ToString(reader["PictureSrc"]),
                        Radius = Convert.ToSingle(reader["Radius"]),
                        WebSite = Convert.ToString(reader["WebSite"])
                    };
                    museums.Add(museum);
                }
                reader.Close();
                return museums;
            }
            catch (DbException) {
                return null;
            }

        }

        public bool Update(Museum museum) {
            try {
                DbCommand command = connection.CreateCommand();
                var _id = AppData.GetParameter("id", museum.Id, DbType.Int32, "Id", command);
                var _name = AppData.GetParameter("title", museum.Title, DbType.String, "Title", command);
                var _address = AppData.GetParameter("address", museum.Adress, DbType.String, "Adress", command);
                var _desc = AppData.GetParameter("description", museum.Description, DbType.String, "Description", command);
                var _cityName = AppData.GetParameter("cityName", museum.CityName, DbType.String, "CityName", command);
                var _phone = AppData.GetParameter("phone", museum.Phone, DbType.String, "Phone", command);
                var _website = AppData.GetParameter("webSite", museum.WebSite, DbType.String, "WebSite", command);
                var _latitude = AppData.GetParameter("latitude", museum.Point.Latitude, DbType.Single, "Latitude", command);
                var _longitude = AppData.GetParameter("longitude", museum.Point.Longitude, DbType.Single, "Longitude", command);
                var _radius = AppData.GetParameter("radius", museum.Radius, DbType.Single, "Radius", command);
                command.Parameters.Add(_name);
                command.Parameters.Add(_address);
                command.Parameters.Add(_desc);
                command.Parameters.Add(_phone);
                command.Parameters.Add(_website);
                command.Parameters.Add(_cityName);
                command.Parameters.Add(_id);
                command.Parameters.Add(_latitude);
                command.Parameters.Add(_longitude);
                command.Parameters.Add(_radius);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "UpdateMuseum";
                command.ExecuteNonQuery();
                return true;
            }
            catch (DbException) {
                return false;
            }
            
            //DbTransaction transaction = connection.BeginTransaction();
            //try {
            //    DbCommand getId = connection.CreateCommand();
            //    DbCommand command = connection.CreateCommand();
            //    command.Transaction = transaction;
            //    getId.Transaction = transaction;
            //    command.Transaction = transaction;
            //    var _cityName = AppData.GetParameter("CityName", museum.CityName, DbType.String, "CityName", getId);
            //    getId.Parameters.Add(_cityName);
            //    getId.CommandText = "SELECT Id FROM Cities WHERE CityName = @CityName";
            //    var result = getId.ExecuteScalar();
                
            //    if (result != null) {
            //        command.ExecuteNonQuery();
            //        var _id = AppData.GetParameter("Id", museum.Id, DbType.Int32, "Id", command);
            //        var _name = AppData.GetParameter("Name", museum.Title, DbType.String, "Title", command);
            //        var _address = AppData.GetParameter("Adress", museum.Adress, DbType.String, "Adress", command);
            //        var _desc = AppData.GetParameter("Description", museum.Description, DbType.String, "Description", command);
            //        var _cityId = AppData.GetParameter("CityId", (int)result, DbType.Int32, "CityId", command);
            //        var _phone = AppData.GetParameter("Phone", museum.Phone, DbType.String, "Phone", command);
            //        var _website = AppData.GetParameter("WebSite", museum.WebSite, DbType.String, "WebSite", command);
            //        var _latitude = AppData.GetParameter("Latitude", museum.Point.Latitude, DbType.Single, "Latitude", command);
            //        var _longitude = AppData.GetParameter("Longitude", museum.Point.Longitude, DbType.Single, "Longitude", command);
            //        var _radius = AppData.GetParameter("Radius", museum.Radius, DbType.Single, "Radius", command);
            //        command.Parameters.Add(_name);
            //        command.Parameters.Add(_address);
            //        command.Parameters.Add(_desc);
            //        command.Parameters.Add(_phone);
            //        command.Parameters.Add(_website);
            //        command.Parameters.Add(_id);
            //        command.Parameters.Add(_latitude);
            //        command.Parameters.Add(_longitude);
            //        command.Parameters.Add(_radius);
            //        command.CommandText = "UPDATE Museums SET Title = @Name, Address = @Adress, Description = @Description, Phone = @Phone, WebSite = @WebSite, " +
            //                              "Latitude = @Latitude, Longitude = @Longitude, Radius = @Radius WHERE Id = @Id";
            //        transaction.Commit();
            //        return true;
            //    } else {
            //        DbCommand createCity = connection.CreateCommand();
            //        DbCommand getLastId = connection.CreateCommand();
            //        getLastId.Transaction = transaction;
            //        getLastId.CommandText = "SELECT MAX(Id) FROM Cities";
            //        int lastId = (int)getLastId.ExecuteScalar();
            //        var __cityName = AppData.GetParameter("CityName", museum.CityName, DbType.String, "CityName", createCity);
            //        var _id = AppData.GetParameter("Id", museum.Id, DbType.Int32, "Id", command);
            //        var _name = AppData.GetParameter("Name", museum.Title, DbType.String, "Title", command);
            //        var _address = AppData.GetParameter("Adress", museum.Adress, DbType.String, "Adress", command);
            //        var _desc = AppData.GetParameter("Description", museum.Description, DbType.String, "Description", command);
            //        var _cityId = AppData.GetParameter("CityId", lastId + 1, DbType.Int32, "CityId", command);
            //        var _phone = AppData.GetParameter("Phone", museum.Phone, DbType.String, "Phone", command);
            //        var _website = AppData.GetParameter("WebSite", museum.WebSite, DbType.String, "WebSite", command);
            //        var _latitude = AppData.GetParameter("Latitude", museum.Point.Latitude, DbType.Single, "Latitude", command);
            //        var _longitude = AppData.GetParameter("Longitude", museum.Point.Longitude, DbType.Single, "Longitude", command);
            //        var _radius = AppData.GetParameter("Radius", museum.Radius, DbType.Single, "Radius", command);
            //        command.Parameters.Add(_name);
            //        command.Parameters.Add(_address);
            //        command.Parameters.Add(_desc);
            //        command.Parameters.Add(_cityId);
            //        command.Parameters.Add(_phone);
            //        command.Parameters.Add(_website);
            //        command.Parameters.Add(_id);
            //        command.Parameters.Add(_latitude);
            //        command.Parameters.Add(_longitude);
            //        command.Parameters.Add(_radius);
            //        command.CommandText = "UPDATE Museums SET Title = @Name, Address = @Adress, Description = @Description, Phone = @Phone, WebSite = @WebSite, " +
            //                              "Latitude = @Latitude, Longitude = @Longitude, Radius = @Radius, CityId = @CityId WHERE Id = @Id";
            //        createCity.Parameters.Add(__cityName);
            //        createCity.Transaction = transaction;
            //        createCity.CommandText = "INSERT INTO Cities (CityName) VALUES (@CityName)";
            //        createCity.ExecuteNonQuery();
            //        command.ExecuteNonQuery();
            //        transaction.Commit();
            //        return true;
            //    }
            //}
            //catch (DbException ex) {
            //    transaction.Rollback();
            //    return false;
            //}
        }

        public Museum CreateMuseum(string login, string password, string city) {
            try {
                DbCommand getId = connection.CreateCommand();
                DbTransaction transaction = connection.BeginTransaction();
                getId.Transaction = transaction;
                int cityId = 0;
                var _city = AppData.GetParameter("CityName", city, DbType.String, "CityName", getId);
                getId.Parameters.Add(_city);
                getId.CommandText = "SELECT Id FROM Cities WHERE CityName = @CityName";
                var result = getId.ExecuteScalar();
                if (result != null) {
                    cityId = (int)result;
                } else {
                    transaction.Rollback();
                    throw new Exception();
                }
                if (cityId != 0) {
                    DbCommand add = connection.CreateCommand();
                    add.Transaction = transaction;
                    var _cityId = AppData.GetParameter("CityId", cityId, DbType.Int32, "Id", add);
                    var _login = AppData.GetParameter("Login", login, DbType.String, "Login", getId);
                    var _password = AppData.GetParameter("Password", password, DbType.String, "Password", getId);
                    add.Parameters.Add(_cityId);
                    add.Parameters.Add(_login);
                    add.Parameters.Add(_password);
                    add.CommandText = "INSERT INTO Museums (Title, Description, Address, Phone, PictureSrc, WebSite, Login, Password, Latitude, Longitude, Radius, CityId) " +
                        "VALUES (N'Default', N'Default', N'Default', N'Default', N'Default', N'Default', @Login, @Password, 0, 0, 0, @CityId)";
                    add.ExecuteNonQuery();
                    transaction.Commit();
                    return this.GetMuseumById(AppData.GetLastId("Museums", connection));
                } else {
                    transaction.Rollback();
                    throw new Exception();
                }
            }
            catch (Exception) {
                return null;
            }
        }
    }
}