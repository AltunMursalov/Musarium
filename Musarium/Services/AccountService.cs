using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Services {
    public class AccountService : IAccountService {
        private readonly IMusariumRepository musariumMusariumRepository;
        private readonly ICityRepository cityRepository;
        private AppData AppData = AppData.GetInstance();

        public AccountService(IMusariumRepository musariumMusariumRepository, ICityRepository cityRepository) {
            this.musariumMusariumRepository = musariumMusariumRepository;
            this.cityRepository = cityRepository;
        } 

        public AuthenticationResult Login(string login, string password) {
            var result = musariumMusariumRepository.OpenConnection();
            cityRepository.OpenConnection();
            if (result != false) {
                var museum = musariumMusariumRepository.GetMuseumByLogin(login);
                if (museum == null) {
                    musariumMusariumRepository.CloseConnection();
                    return AuthenticationResult.IsNotExist;
                } else if (museum.Login != login) {
                    musariumMusariumRepository.CloseConnection();
                    return AuthenticationResult.IncorrectLogin;
                } else if (museum.Password != password) {
                    musariumMusariumRepository.CloseConnection();
                    return AuthenticationResult.IncorrectPassword;
                } else {
                    AppData.CurrentMuseum = museum;
                    museum.CityName = cityRepository.GetMuseumCityById(museum.CityId).Name;
                    musariumMusariumRepository.CloseConnection();
                    return AuthenticationResult.Successful;
                }
            } else {
                cityRepository.CloseConnection();
                musariumMusariumRepository.CloseConnection();
                return AuthenticationResult.InvalidConnection;
            }
        }

        public void EditProfile(Museum museum) {
            musariumMusariumRepository.Update(museum);
        }

        public RegistrationResult Registration(RegisterData data) {
            var result = musariumMusariumRepository.OpenConnection();
            if (result != false) {
                var museum = musariumMusariumRepository.CreateMuseum(data.Login, data.Password, data.City);
                if (data.Password.Length < 6 && data.Password == data.ValidatePassword) {
                    musariumMusariumRepository.CloseConnection();
                    return RegistrationResult.ShortPassword;
                } else if (data.ValidatePassword != data.Password) {
                    musariumMusariumRepository.CloseConnection();
                    return RegistrationResult.PasswordsIsNotMatch;
                } else if (museum == null) {
                    musariumMusariumRepository.CloseConnection();
                    return RegistrationResult.LoginIsExist;
                } else {
                    musariumMusariumRepository.CloseConnection();
                    return RegistrationResult.Successful;
                }
            } else {
                return RegistrationResult.InvalidConnection;
            }
        }
    }
}