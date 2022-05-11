using MyNotes.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyNotes.Views
{
    
    [QueryProperty(nameof(NoteID), nameof(NoteID))]
    public partial class NoteEntryPage : ContentPage
    {
        int notehash;
        public string NoteID
        {
            set
            {
                LoadNote(value);
            }
        }
       
        public NoteEntryPage()
        {
            InitializeComponent();
            BindingContext = new MyNote();

         
            btnSave.Clicked += BtnSave_Clicked;
            btnDelete.Clicked += BtnDelete_Clicked;
        }


        /// <summary>
        /// overide Device back button
        /// Navigation Back Button은 해결하지 못함
        /// </summary>
        /// <returns></returns>

        protected override bool OnBackButtonPressed()
        {
            int hash = txtNotes.Text.GetHashCode();
            // 변경이 되지 않았어면 그냥 Back 
            if (hash == notehash) {
                base.OnBackButtonPressed();
                return false; 
            
            }
            Device.BeginInvokeOnMainThread(
                async () =>
                {
                    var action = await DisplayAlert("Question?", "Would you like to save changes ?", "Yes", "No");
                    // Save Changes
                    if (action)
                    {
                        var note = (MyNote)BindingContext;
                        MessagingCenter.Send<MyNote>(note, "SaveChanges");
                    }
                }
            );

            // Back 
            base.OnBackButtonPressed();
            return false;

            
        }


        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Question?", "Are you sure to delete ?", "Yes", "No");
            //Debug.WriteLine("Answer: " + answer);

            if (answer == false)
            {
                return;
            }

            var note = (MyNote)BindingContext;
            await App.MyNoteDB.DeleteMyNoteAsync(note);

            // Navigate backwards
            await Navigation.PopAsync();
        }

        private async void BtnSave_Clicked(object sender, EventArgs e)
        {
            var note = (MyNote)BindingContext;
            note.LastUpdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(note.Subject))
            {
                await App.MyNoteDB.SaveMyNoteAsync(note);
            }

            // Navigate backwards
            await Navigation.PopAsync();

        }

        async void LoadNote(string noteId)
        {
            try
            {

                int  noteID = Convert.ToInt32(noteId);

                MyNote myNote = await App.MyNoteDB.GetMyNoteAsync(noteID);
                notehash = myNote.NoteText.GetHashCode();

                BindingContext = myNote;
            }
            catch (Exception)
            {

                Console.WriteLine("Failed to load note."); 
            }
        }

    }
}