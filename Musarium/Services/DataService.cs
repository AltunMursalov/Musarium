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

        public bool CreateQuest(Quest quest, IEnumerable<Question> questions, IEnumerable<Answer> answers, Prize prize) {
            try {
                this.questRepository.OpenConnection();
                var questResult = questRepository.CreateQuest(quest, prize, this.AppData.CurrentMuseum);
                this.questionRepository.OpenConnection();
                this.answerRepository.OpenConnection();
                foreach (var question in questions) {
                    question.QuestId = questResult.Id;
                    var questionResult = this.questionRepository.CreateQuestion(question);
                    foreach (var answer in answers) {
                        if (question.QuestionType == answer.Type) {
                            answer.QuestionID = questionResult.Id;
                            this.answerRepository.CreateAnswer(answer);
                        }
                    }
                }
                this.questionRepository.CloseConnection();
                this.answerRepository.CloseConnection();
                this.prizeRepository.OpenConnection();
                var isPrizeExist = this.prizeRepository.IsPrizeExist(prize.Id);
                if (isPrizeExist) {
                    questResult.PrizeId = prize.Id;
                    this.questRepository.SetPrize(questResult.Id, prize.Id);
                } else {
                    var insertedPrize = this.prizeRepository.CreatePrize(prize);
                    questResult.PrizeId = insertedPrize.Id;
                    this.questRepository.SetPrize(questResult.Id, insertedPrize.Id);
                }
                this.questRepository.CloseConnection();
                this.prizeRepository.CloseConnection();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public bool CreateManyAnswerQuestion(IEnumerable<Question> question, IEnumerable<Answer> answer) {
            this.questionRepository.OpenConnection();
            int questionId = 0;
            foreach (var item in question) {
                var result = this.questionRepository.CreateQuestion(item);
                questionId = result.Id;
            }
            if (questionId != 0) {
                this.questionRepository.CloseConnection();
                foreach (var item in answer) {
                    item.QuestionID = questionId;
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

        public bool CreateTextQuestion(Question question, Answer answer) {
            this.questionRepository.OpenConnection();
            var result = this.questionRepository.CreateQuestion(question);
            if (result != null) {
                this.questionRepository.CloseConnection();
                this.answerRepository.OpenConnection();
                answer.QuestionID = result.Id;
                var answerResult = this.answerRepository.CreateAnswer(answer);
                this.answerRepository.CloseConnection();
                if (answerResult != null) {
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }
    }
}