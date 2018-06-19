using Musarium.Common;
using Musarium.Interfaces;
using Musarium.Model;
using Musarium.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musarium.Services {
    public class DataService : IDataService {
        private readonly IMusariumRepository musariumRepository;
        private readonly IPrizeRepository prizeRepository;
        private readonly IQuestRepository questRepository;
        private readonly IQuestionRepository questionRepository;
        ICityRepository cityRepository; 
        private readonly IStatisticRepository statisticRepository;
        private readonly IAnswerRepository answerRepository;
        private AppData AppData = AppData.GetInstance();

        public DataService(IMusariumRepository musariumRepository, IPrizeRepository prizeRepository, IQuestRepository questRepository,
                           IQuestionRepository questionRepository, IStatisticRepository statisticRepository, IAnswerRepository answerRepository,
                           ICityRepository cityRepository) {
            this.answerRepository = answerRepository;
            this.cityRepository = cityRepository;
            this.musariumRepository = musariumRepository;
            this.prizeRepository = prizeRepository;
            this.questRepository = questRepository;
            this.questionRepository = questionRepository;
            this.statisticRepository = statisticRepository;
        }

        public Quest CreateQuest(Quest quest, Question question, Prize prize, Museum museum) {
            this.questRepository.OpenConnection();
            var _quest = this.questRepository.CreateQuest(quest, question, prize, museum);
            if (quest != null) {
                this.questRepository.CloseConnection();
                return quest;
            } else {
                this.questRepository.CloseConnection();
                return null;
            }
        }

        public bool DeleteQuest(Quest quest) {
            var result = questRepository.OpenConnection();
            if (result != false) {
                var isDeleted = questRepository.RemoveQuest(quest.Id);
                if (isDeleted) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        public bool CreateQuestion(Question question, List<Answer> answer) {
            this.questionRepository.OpenConnection();
            var result = this.questionRepository.CreateQuestion(question);
            if (result != null) {
                this.questionRepository.CloseConnection();
                foreach (var item in answer) {
                    item.QuestionID = result.Id;
                    this.answerRepository.CreateAnswer(item);
                }
                return true;
            } else {
                this.questionRepository.CloseConnection();
                return false;
            }
        }

        public string GetCityName(int id) {
            cityRepository.OpenConnection();
            var city = cityRepository.GetMuseumCityById(id);
            cityRepository.CloseConnection();
            return city.Name;
        }

        public IEnumerable<Prize> GetPrizes() {
            this.prizeRepository.OpenConnection();
            var result = this.prizeRepository.GetPrizes();
            if (result != null) {
                this.prizeRepository.CloseConnection();
                return result;
            } else {
                this.prizeRepository.CloseConnection();
                return null;
            }
        }

        public IEnumerable<Prize> GetMuseumPrizes(int museumId) {
            prizeRepository.OpenConnection();
            IEnumerable<Prize> prizes = prizeRepository.GetMuseumPrizes(museumId);
            if (prizes != null) {
                prizeRepository.CloseConnection();
                return prizes;
            } else {
                prizeRepository.CloseConnection();
                return null;
            }
        }

        public Museum GetByLogin(string login) {
            musariumRepository.OpenConnection();
            var res = musariumRepository.GetMuseumByLogin(login);
            musariumRepository.CloseConnection();
            return res;
        }

        public bool EditMuseum(Museum museum) {
            musariumRepository.OpenConnection();
            var result = musariumRepository.Update(museum);
            if (result != false) {
                musariumRepository.CloseConnection();
                return true;
            } else {
                musariumRepository.CloseConnection();
                return false;
            }
        }

        public IEnumerable<Question> GetQuestQuestions(Quest quest) {
            this.questionRepository.OpenConnection();
            var questions = questionRepository.GetQuestQuestions(quest.Id);
            if (questions != null) {
                questionRepository.CloseConnection();
                return questions;
            } else {
                questionRepository.CloseConnection();
                return null;
            }
        }

        public IEnumerable<Quest> GetMuseumQuests(int id) {
            var result = questRepository.OpenConnection();
            questionRepository.OpenConnection();
            if (result != false) {
                var quests = questRepository.GetMuseumQuests(id);
                if (quests != null) {
                    foreach (var item in quests) {
                        item.QuestionCounts = questionRepository.GetQuestQuestionsCount(item.Id);
                    }
                    questionRepository.CloseConnection();
                    questRepository.CloseConnection();
                    return quests;
                } else {
                    questionRepository.CloseConnection();
                    questRepository.CloseConnection();
                    return null;
                }
            }
            questionRepository.CloseConnection();
            questRepository.CloseConnection();
            return null;
        }

        public IEnumerable<Statistic> GetMuseumStatistics(int id) {
            IEnumerable<Statistic> statistics = statisticRepository.GetStatistics();
            if (statistics != null) {
                return statistics;
            } else {
                return null;
            }
        }

        public Museum InfoAboutMuseum(int id) {
            var result = musariumRepository.OpenConnection();
            if (result != false) {
                var museum = musariumRepository.GetMuseumById(id);
                if (museum != null) {
                    return museum;
                } else {
                    return null;
                }
            } else {
                return null;
            }
        }

        public bool CreatePrize(Prize prize) {
            this.prizeRepository.OpenConnection();
            var result = this.prizeRepository.CreatePrize(prize);
            if (result != null) {
                this.prizeRepository.CloseConnection();
                return true;
            } else {
                this.prizeRepository.CloseConnection();
                return false;
            }
        }
    }
}