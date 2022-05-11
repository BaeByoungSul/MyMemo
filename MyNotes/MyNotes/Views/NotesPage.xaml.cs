using MyNotes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNotes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();

            MessagingCenter.Unsubscribe<MyNote>(this, "SaveChanges");
            MessagingCenter.Subscribe<MyNote>(this, "SaveChanges", (value) =>
            {
                Device.BeginInvokeOnMainThread(async () => {
                    await App.MyNoteDB.SaveMyNoteAsync(value);
                    noteCollectionView.ItemsSource = await App.MyNoteDB.GetMyNotesAsync();
                });

            });

            toolbarAdd.Clicked += ToolbarAdd_Clicked;

            noteCollectionView.SelectionChanged += NoteCollectionView_SelectionChanged;
            

        }

        private async void NoteCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                MyNote note = (MyNote)e.CurrentSelection.FirstOrDefault();

                await Navigation.PushAsync(new NoteEntryPage() { NoteID = note.NoteID.ToString() });
                //await Navigation.PushModalAsync(new NoteEntryPage() { NoteID = note.NoteID.ToString() });

            }
        }

        private async void ToolbarAdd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteEntryPage());
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            noteCollectionView.ItemsSource = await App.MyNoteDB.GetMyNotesAsync();
            
            Debug.WriteLine("aaaa");
        }
    }
}