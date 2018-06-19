using Autofac;
using Musarium.Interfaces;
using Musarium.Model;
using Musarium.Repositories;
using Musarium.Services;
using Musarium.View;
using Musarium.ViewModel;
using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Musarium.Common {
    public class AppData {
        private static AppData instance;
        public Museum CurrentMuseum { get; set; }

        public ConnectionStringSettings MyConnection {
            get {
                return ConfigurationManager.ConnectionStrings["My Connection"];
            }
        }

        public ConnectionStringSettings AzureDB {
            get {
                return ConfigurationManager.ConnectionStrings["Azure DB"];
            }
        }

        public ConnectionStringSettings ItstepAcademy {
            get {
                return ConfigurationManager.ConnectionStrings["Step Academy"];
            }
        }

        public IContainer Container { get; private set; }

        private AppData() {
            this.CurrentMuseum = new Museum();
            var builder = new ContainerBuilder();
            #region ViewModels
            builder.RegisterType<LogInViewModel>().As<ILogInViewModel>();
            builder.RegisterType<MuseumDeveloperViewModel>().As<IMuseumDeveloperViewModel>().SingleInstance();
            builder.RegisterType<RegistrationViewModel>().As<IRegistrationViewModel>();
            builder.RegisterType<AuthorizationViewModel>().As<IAuthorizationViewModel>();
            builder.RegisterType<AddEditQuestViewModel>().As<IAddEditViewModel>().SingleInstance();
            builder.RegisterType<InfoAboutQuestViewModel>().As<IInfoAboutQuestViewModel>().SingleInstance();
            builder.RegisterType<MuseumEditingViewModel>().As<IMuseumEditingViewModel>().SingleInstance();
            builder.RegisterType<AddPrizeViewModel>().As<IAddPrizeViewModel>().SingleInstance();
            builder.RegisterType<PrizeShowViewModel>().As<IPrizeShowViewModel>().SingleInstance();
            builder.RegisterType<QuestionTypeViewModel>().As<IQuestionTypeViewModel>().SingleInstance();
            builder.RegisterType<CreateQuestionsViewModel>().As<ICreateQuestsViewModel>().SingleInstance();
            builder.RegisterType<TaskInfoAboutQuestViewModel>().As<ITaskInfoAboutQuestViewModel>().SingleInstance();
            builder.RegisterType<MusariumViewModel>().As<IMusariumViewModel>().SingleInstance();
            builder.RegisterType<TextQuestionViewModel>().As<ITextQuestionViewModel>().SingleInstance();
            builder.RegisterType<ManyAnswerQuestionViewModel>().As<IManyAnswerQuestionViewModel>().SingleInstance();
            #endregion
            #region Views
            builder.RegisterType<ManyAnswerQuestion>().As<IManyAnswerQuestionView>();
            builder.RegisterType<Registration>().As<IRegistrationView>();
            builder.RegisterType<Login>().As<ILoginView>();
            builder.RegisterType<Musariume>().As<IMusariumView>();
            builder.RegisterType<CreateQuests>().As<ICreateQuestsView>();
            builder.RegisterType<QuestionType>().As<IQuestionTypeView>();
            builder.RegisterType<TaskInfoAboutQuest>().As<ITaskInfoAboutQuestView>();
            builder.RegisterType<PrizeShow>().As<IPrizeShowView>();
            builder.RegisterType<AddPrize>().As<IAddPrizeView>();
            builder.RegisterType<InfoAboutQuest>().As<IInfoAboutQuestView>();
            builder.RegisterType<AddEditQuest>().As<IAddEditView>(); 
            builder.RegisterType<MuseumDeveloper>().As<IMuseumDeveloperView>();
            builder.RegisterType<MuseumEditing>().As<IMuseumEditingView>();
            builder.RegisterType<Authorization>().As<IAuthorizationView>();
            builder.RegisterType<TextQuestion>().As<ITextQuestionView>();
            #endregion
            #region Repositories
            builder.RegisterType<QuestRepository>().As<IQuestRepository>();
            builder.RegisterType<CityRepository>().As<ICityRepository>();
            builder.RegisterType<AnswerRepository>().As<IAnswerRepository>();
            builder.RegisterType<PrizeRepository>().As<IPrizeRepository>();
            builder.RegisterType<StatisticRepository>().As<IStatisticRepository>();
            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>();
            builder.RegisterType<MusariumRepository>().As<IMusariumRepository>();
            #endregion
            #region Services
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<DataService>().As<IDataService>();
            #endregion
            Container = builder.Build();
        }

        public static AppData GetInstance() {
            if (instance == null) {
                instance = new AppData();
            }
            return instance;
        }

        public DbParameter GetParameter(string parameterName, object value, DbType type, string sourceColumn, DbCommand command) {
            try {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = parameterName;
                parameter.DbType = type;
                parameter.Value = value;
                parameter.SourceColumn = sourceColumn;
                return parameter;
            }
            catch (NullReferenceException ex) {
                throw ex;
            }
        }

        public int GetLastId(string tableName, DbConnection connection) {
            DbCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT MAX(Id) FROM {tableName}";
            var result = command.ExecuteScalar();
            if (result != null) {
                return (int)result;
            } else {
                return -1;
            }
        }
    }
}
