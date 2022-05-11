using MyNotes.Data;
using MyNotes.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNotes
{
    public partial class App : Application
    {
        static MyNotesDB myNoteDB;
        public static MyNotesDB MyNoteDB
        {
            get
            {
                if (myNoteDB == null)
                {
                    myNoteDB = new MyNotesDB(MyConstants.DatabasePath );
                }
                return myNoteDB;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage ( new NotesPage () );
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
