using System;
using System.Windows.Input;
using TriviaPorts;
using Xamarin.Forms;

namespace ISDCompanion
{
    public class ExercisesViewModel
    {
        public ICommand NavigateCommand { get; set; }
        public ICommand NetworksQuizCommand { get; set; }
        public ICommand OpSysQuizCommand { get; set; }

        //TODO: Refactor this in a future release
        readonly IQuestion[] networksQuestions = new YesNoQuestion[] {
            new YesNoQuestion()
            {
                Text = @"Die Mindestlänge von Rahmen dient dazu, Kollisionen erkennen zu können.",
                Answer = true
            }, 
            new YesNoQuestion()
            {
                Text = @"Bei bekannter IP-Adresse eines Servers können wir mit dessen Prozessen kommunizieren.",
                Answer = false
            }, 
            new YesNoQuestion()
            {
                Text = @"UDP ist für Videotelefonie besser geeignet als TCP.",
                Answer = true
            }, 
            new YesNoQuestion()
            {
                Text = @"Alle Netzwerkgeräte im gleichen physischen Netz empfangen Rahmen mit der Zieladresse FF-FF-FF-FF-FF-FF.",
                Answer = true
            }, 
            new YesNoQuestion()
            {
                Text = @"Die Netzwerktopologie Maschen enthält einen Single Point of Failure.",
                Answer = false
            }, 
            new YesNoQuestion()
            {
                Text = @"Router untersuchen Rahmen mit Prüfsummen auf Korrektheit.",
                Answer = false
            }, 
            new YesNoQuestion()
            {
                Text = @"Bridges vermeiden Kreise mit dem Spanning Tree Protocol.",
                Answer = true
            }, 
            new YesNoQuestion()
            {
                Text = @"Der Leitungscode Non-Return-To-Zero (NRZ) verursacht Probleme bei der Taktwiederherstellung.",
                Answer = true
            }, 
            new YesNoQuestion()
            {
                Text = @"Gateways in der Vermittlungsschicht von Computernetzen werden häufig eingesetzt.",
                Answer = false
            }, 
            new YesNoQuestion()
            {
                Text = @"Telnet ermöglicht die verschlüsselte Fernsteuerung von Computern.",
                Answer = false
            }, 
            new YesNoQuestion()
            {
                Text = @"DHCP ermöglicht die Zuweisung der Netzwerkkonfiguration an Netzwerkgeräte.",
                Answer = true
            }, 
            new YesNoQuestion()
            {
                Text = @"ICMP dient dazu, Diagnose- und Fehlermeldungen auszutauschen.",
                Answer = true
            }, 
            new YesNoQuestion()
            {
                Text = @"Im Ethernet-Rahmen stehen die MAC-Adressen von Sender und Empfänger.",
                Answer = true
            }
            };

        readonly IQuestion[] opSysQuestions = new YesNoQuestion[] {
            new YesNoQuestion()
            {
                Text = @"Journaling-Dateisysteme grenzen die bei der Konsistenzprüfung zu überprüfenden Daten ein.",
                Answer = true                
            },
            new YesNoQuestion()
            {
                Text = @"Bei Dateisystemen mit Journal sind Datenverluste garantiert ausgeschlossen.",
                Answer = false
            },
            new YesNoQuestion()
            {
                Text = @"Der Zugriff auf Festplatten erfolgt blockorientiert und kann wahlfrei geschehen.",
                Answer = true
            },
            new YesNoQuestion()
            {
                Text = @"Round Robin kann als faires Schedulingverfahren bezeichnet werden.",
                Answer = true
            },
            new YesNoQuestion()
            {
                Text = @"Mit Prioritätengesteuertem Scheduling lässt sich die geringste Gesamtlaufzeit für Aufgaben erreichen.",
                Answer = false
            },
            new YesNoQuestion()
            {
                Text = @"Shortest Job First ist ein in der Praxis eingesetztes Schedulingverfahren.",
                Answer = false
            },
            new YesNoQuestion()
            {
                Text = @"Monolithische Betriebssystemkerne sind leichter zu erweitern und zu warten.",
                Answer = false
            },
            new YesNoQuestion()
            {
                Text = @"Defragmentieren ist auch bei SSDs nötig.",
                Answer = false
            },
            new YesNoQuestion()
            {
                Text = @"Die Funktion eines Mutex lässt sich auch mit Semaphoren erreichen.",
                Answer = true
            }
        };

        public ExercisesViewModel(INavigation navigation)
        {
            NavigateCommand = new Command<Type>(async (Type pageType) =>
            {
                Page page = (Page)Activator.CreateInstance(pageType);
                await navigation.PushAsync(page);
            });
            NetworksQuizCommand = new Command(async () =>
            {                
                await navigation.PushAsync(new QuizPage(networksQuestions));
            });
            OpSysQuizCommand = new Command(async () =>
            {
                await navigation.PushAsync(new QuizPage(opSysQuestions));
            });
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
