using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ISDCompanion.Interfaces;
using ISDCompanion.Models;
using Italbytz.Ports.Trivia;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class ExercisesViewModel : ViewModel
    {
        public ICommand NavigateCommand { get; set; }
        public ICommand NetworksQuizCommand { get; set; }
        public ICommand OpSysQuizCommand { get; set; }

        private INavigation _navigation;

        public Command<Exercise> ExerciseTapped { get; }

        public ExercisesViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _semesters = new TrulyObservableCollection<Semester>();

            PopulateData();

            ExerciseTapped = new Command<Exercise>(OnExerciseSelected);

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

        private async void OnExerciseSelected(Exercise obj)
        {
            if (obj == null)
            {
                return;
            }
            Page page = null;


            page = (Page)Activator.CreateInstance(Type.GetType(obj.CommandParameter));


            IAfterRender afterRender = null;
            if (page is IAfterRender)
            {
                afterRender = (IAfterRender)page;
            }
            await _navigation.PushAsync(page).ContinueWith(result =>
            {
                if (afterRender != null)
                {
                    afterRender.AfterRender();
                }
            });
        }


        private void PopulateData()
        {
            TrulyObservableCollection<Exercise> networks = new TrulyObservableCollection<Exercise>();

            networks.Add(new Exercise
            {
                Exercise_Title = "Bitencodings",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.BitencodingsPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "CRC",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.CRCPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "MST",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.MSTPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "ShortestPaths",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.ShortestPathsPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            networks.Add(new Exercise
            {
                Exercise_Title = "Netmasks",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.NetmaskPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });



            TrulyObservableCollection<Exercise> opsys = new TrulyObservableCollection<Exercise>();

            opsys.Add(new Exercise
            {
                Exercise_Title = "PageReplacement",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.PageReplacementPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            opsys.Add(new Exercise
            {
                Exercise_Title = "Buddy",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.BuddyPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            opsys.Add(new Exercise
            {
                Exercise_Title = "Scheduling",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.SchedulingPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            opsys.Add(new Exercise
            {
                Exercise_Title = "RealtimeScheduling",
                Command = "{Binding NavigateCommand}",
                CommandParameter = "ISDCompanion.RealtimeSchedulingPage, ISDCompanion",
                InitExpanded = Settings.RealSemester == 4
            });

            TrulyObservableCollection<Topic> temp_Topics = new TrulyObservableCollection<Topic>();

            temp_Topics.Add(new Topic
            {
                Topic_Title = "Betriebssysteme",
                Exercises = opsys,
                InitExpanded = Settings.RealSemester == 4
            });

            temp_Topics.Add(new Topic
            {
                Topic_Title = "Netzwerke",
                Exercises = networks,
                InitExpanded = Settings.RealSemester == 4
            });

            TrulyObservableCollection<Module> temp_Modules = new TrulyObservableCollection<Module>();

            temp_Modules.Add(new Module
            {
                Module_Title = "Betriebssysteme & Netzwerke",
                Topics = temp_Topics,
                InitExpanded = Settings.RealSemester == 4
            });


            _semesters.Add(new Semester
            {
                Semester_Title = "Fachsemester 4",
                Modules = temp_Modules,
                InitExpanded = Settings.RealSemester == 4
            });



        }

        private TrulyObservableCollection<Semester> _semesters { get; set; }

        //public ObservableCollection<Exercise> Exercises
        //{
        //    get 
        //    { 
        //        return _exercises; 
        //    }
        //    private set
        //    {
        //        _exercises = value;
        //    }
        //}

        public TrulyObservableCollection<Semester> Semesters
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
