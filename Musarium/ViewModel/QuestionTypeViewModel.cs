using Musarium.Common;
using Musarium.Interfaces;
using System.Windows.Input;
using Autofac;
using System;

namespace Musarium.ViewModel {
    public class QuestionTypeViewModel : NotifyableObject, IQuestionTypeViewModel {
        private AppData AppData;
        public QuestionTypeViewModel(IQuestionTypeView view) {
            this.View = view;
            this.View.BindDataContext(this);
            this.AppData = AppData.GetInstance();
        }

        public IQuestionTypeView View { get; private set; }

        private ICommand choose;
        public ICommand Choose {
            get {
                if (choose is null) {
                    choose = new RelayCommand(
                        (param) => {
                            int type = Convert.ToInt32(param);
                            this.View.Hide();
                            if (type == 1) {
                                var textQuestion = this.AppData.Container.Resolve<ITextQuestionViewModel>();
                                textQuestion.Question.QuestionType = type;
                                textQuestion.View.ShowDialog();
                            } else if (type == 2) {
                                var manyAnswerQuestion = this.AppData.Container.Resolve<IManyAnswerQuestionViewModel>();
                                manyAnswerQuestion.Question.QuestionType = type;
                                manyAnswerQuestion.View.ShowDialog();
                            }
                        });
                }
                return choose;
            }
        }

    }
}