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

        private ICommand questionWithTextBoxCom;
        public ICommand QuestionWithTextBoxCom {
            get {
                if (questionWithTextBoxCom is null) {
                    questionWithTextBoxCom = new RelayCommand(
                        (param) => {
                            int type = Convert.ToInt32(param);
                            this.View.Hide();
                            if (type == 1) {
                                this.AppData.Container.Resolve<ITextQuestionViewModel>().View.ShowDialog();
                            } else if (type == 2) {
                                this.AppData.Container.Resolve<IManyAnswerQuestionViewModel>().View.ShowDialog();
                            }
                        });
                }

                return questionWithTextBoxCom;
            }
        }
         
    }
}