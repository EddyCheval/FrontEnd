using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketIO.Client;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using MarcassinMobile.JSONObject;
using MarcassinMobile.Utilitaire;
using System.Collections.ObjectModel;

namespace MarcassinMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessagerieGroupe : ContentPage
	{
        public List<JSMessage> Messages = new List<JSMessage>();
        public ObservableCollection<JSMessage> ObMessages;
        Socket socket = IO.Socket(App.Url);
        public int Id_Groupe;
       
		public MessagerieGroupe (int id_groupe)
		{
			InitializeComponent();
            Log(id_groupe);
            Id_Groupe = id_groupe;
        }
        private async void Log(int id_groupe)
        {
            var groupe = await HttpRequest.getRequest(App.Url + "api/Groupes?filter[where][Id_Groupe]=" + id_groupe);
            var message = await HttpRequest.getRequest(App.Url + "api/Messageries?filter[include][employe]&filter[where][Id_Groupe]=" + id_groupe+"&filter[limit]=10");
            Messages = JsonConvert.DeserializeObject<List<JSMessage>>(message);
            System.Diagnostics.Debug.WriteLine(message);
            Settings.ActualGroupe = JsonConvert.DeserializeObject<List<JSGroupe>>(groupe).FirstOrDefault();
            ObMessages = ObservableCollectionConvert.ObservableCollectionConvertion(Messages);
            ListMessages.ItemsSource = ObMessages;
            socket.Connect();
            var JSUser = JsonConvert.SerializeObject(Settings.ActualUser);
            var JSGroupe = JsonConvert.SerializeObject(Settings.ActualGroupe);
            socket.Emit("nouv_client", JSUser, JSGroupe);
            socket.On("retour", (data) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    System.Diagnostics.Debug.WriteLine(data.ToString());
                    JSMessage jSMessage = JsonConvert.DeserializeObject<JSMessage>(data.First().ToString());
                    Messages.Add(jSMessage);
                    ObMessages = ObservableCollectionConvert.ObservableCollectionConvertion(Messages);

                    ListMessages.ItemsSource = ObMessages;
                    ListMessages.ScrollTo(ObMessages.Last(), ScrollToPosition.End, false);
                });
            });
            socket.On("Error",(data) => 
            {
                socket.Emit("nouv_client", JSUser, JSGroupe);
                Device.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Erreur", "Renvoyer votre message", "ok");
                });
            });
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {

            if (StringCheck(MessageEntry.Text))
            {
                await DisplayAlert("Error:", "Le message ne peut être vide", "Ok");
            }
            else if (socket.IsConnected)
            {
                Send(MessageEntry.Text);
            }
            else
            {
                await DisplayAlert("Error:", "Connection Au Serveu Impossible veuillez réessayer","Ok");
                Log(Id_Groupe);
            }
        }
        public void Send(string Data)
        {
            try
            {
                var message = new JSMessage
                {
                    contenu = Data,
                    date = DateTime.Now,
                    id_Groupe = Id_Groupe,
                    id_Expediteur = Settings.ActualUser.id_Employe,
                    Employe = Settings.ActualUser
                };
                var val = JsonConvert.SerializeObject(message);
                socket.Emit("send", val);
                Messages.Add(message);
                ObMessages = ObservableCollectionConvert.ObservableCollectionConvertion(Messages);
                ListMessages.ItemsSource = ObMessages;

                ListMessages.ScrollTo(ObMessages.Last(),ScrollToPosition.End, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                socket.Disconnect();
            }
        }
        private bool StringCheck(string s)
        {
            foreach (var x in s)
            {
                if (x != ' ')
                {
                    return false;
                }
            }
            return true;
        }
    }
}