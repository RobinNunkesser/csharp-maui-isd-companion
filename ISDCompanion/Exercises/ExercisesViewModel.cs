using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ISDCompanion.Models;
using Italbytz.Ports.Trivia;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class ExercisesViewModel
    {
        public ICommand NavigateCommand { get; set; }
        public ICommand NetworksQuizCommand { get; set; }
        public ICommand OpSysQuizCommand { get; set; }

        public Command<Exercise> ExerciseTapped { get; }

        public ExercisesViewModel(INavigation navigation)
        {
            _semesters = new ObservableCollection<Semester>();

            PopulateData();

            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await navigation.PushAsync(page);
            });
            NetworksQuizCommand = new Command(async () =>
            {                
                await navigation.PushAsync(new QuizPage(Italbytz.Adapters.Exam.Networks.YesNoQuestions.Questions));
            });
            OpSysQuizCommand = new Command(async () =>
            {
                await navigation.PushAsync(new QuizPage(Italbytz.Adapters.Exam.OperatingSystems.YesNoQuestions.Questions));
            });
        }

        private void PopulateData()
        {
            ObservableCollection<Exercise> networks = new ObservableCollection<Exercise>();

            networks.Add(new Exercise
            {
                Exercise_Title = "Bitencodings",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:BitencodingsPage}"
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "CRC",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:CRCPage}"
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "MST",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:MSTPage}"
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "ShortestPaths",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:ShortestPathsPage}"
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "Netmasks",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:NetmaskPage}"
            });



            ObservableCollection<Exercise> opsys = new ObservableCollection<Exercise>();

            opsys.Add(new Exercise
            {
                Exercise_Title = "PageReplacement",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:PageReplacementPage}"
            });

            opsys.Add(new Exercise
            {
                Exercise_Title = "Buddy",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:BuddyPage}"
            });

            opsys.Add(new Exercise
            {
                Exercise_Title = "Scheduling",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:SchedulingPage}"
            });

            opsys.Add(new Exercise
            {
                Exercise_Title = "RealtimeScheduling",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "{x:Type local:RealtimeSchedulingPage}"
            });



            ObservableCollection<Topic> temp_Topics = new ObservableCollection<Topic>();

            temp_Topics.Add(new Topic
            {
                Topic_Title = "Betriebssysteme",
                Exercises = networks
            }); 
            
            temp_Topics.Add(new Topic
            {
                Topic_Title = "Netzwerke",
                Exercises = opsys
            });

            ObservableCollection<Module> temp_Modules = new ObservableCollection<Module>();

            temp_Modules.Add(new Module
            {
                Module_Title = "Betriebssysteme & Netzwerke",
                Topics = temp_Topics
            }); 

            _semesters.Add(new Semester
            {
                Semester_Title = "Fachsemester 4",
                Modules = temp_Modules
            }); 
            
            
        }

        private ObservableCollection<Semester> _semesters { get; set; }

        public ObservableCollection<Semester> Semesters
        {
            get
            {
                return _semesters;
            }
            private set
            {
                _semesters = value;
            }
        }

        private class YesNoQuestion : IYesNoQuestion
        {
            public bool Answer { get; set; }
            public string Category { get; set; }
            public QuestionType QuestionType { get; set; }
            public Choices ChoicesType { get; set; }
            public Difficulty Difficulty { get; set; }
            public string Text { get; set; }
            public IQuestion AlternativeQuestion { get; set; }
        }


    }
}
