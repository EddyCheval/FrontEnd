using MarcassinMobile.JSONObject;
using MarcassinMobile.Utilitaire;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarcassinMobile.Page
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NoteCompetence : ContentPage
	{
        private List<int> listnote;
        private JSNote Note;
        public NoteCompetence (JSNote note)
		{
			InitializeComponent ();
            Note = note;
            RecupNote();
		}

        private void BuildReportHtml()
        {
            var chartConfigScript = GetChartScript();
            var html2 = GetHtmlWithChartConfig(chartConfigScript);
            var html = new HtmlWebViewSource
            {
                Html = html2
            };

            var url = new UrlWebViewSource
            {
                Url = "your url here"
            }; ;
            WebView.Source = html;
            //htmlTest.Text = html;
            System.Diagnostics.Debug.WriteLine(html);
        }

        private string GetHtmlWithChartConfig(string chartConfig)
        {
            var inlineStyle = "style=\"width:100%;height:97%;\"";
            var chartJsScript = "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript = $"<script>{chartConfig}</script>";
            var chartContent = $@"<div id=""chart-container"" {inlineStyle}>
  <canvas id=""chart"" />
</div>";
            var document = $@"<html style=""width:97%;height:100%;Margin:""-10,-10,0,0"">
  <head>{chartJsScript}</head>
  <body {inlineStyle}>
    {chartContent}
    {chartConfigJsScript}
  </body>
</html>";
            return document;
        }

        private string GetChartScript()
        {
            var chartConfig = GetSpendingChartConfig();
            var script = $@"var config = {chartConfig};
window.onload = function() {{
  var canvasContext = document.getElementById(""chart"").getContext(""2d"");
  new Chart(canvasContext, config);
}};";
            return script;
        }

        private string GetSpendingChartConfig()
        {
            var config = new
            {
                type = "polarArea",
                data = GetChartData(),
                options = new
                {
                    responsive = true,
                    maintainAspectRatio = false,
                    legend = new
                    {
                        position = "top"
                    },
                    animation = new
                    {
                        animateScale = true
                    }
                }
            };
            var jsonConfig = JsonConvert.SerializeObject(config);
            return jsonConfig;
        }

        private object GetChartData()
        {

            var colors = GetDefaultColors();
            var labels = new[] { "Général", "Relationnel", "Connaissance", "Communication", "Pédagogie" };
            var randomGen = new Random();
            var dataPoints = listnote;
            var data = new
            {
                datasets = new[]
                {
                    new
                    {
                        label = "Spending",
                        data = dataPoints,
                        backgroundColor = dataPoints.Select((d, i) =>
                        {
                            var color = colors[i % colors.Count];
                            return $"rgba({color.Item1},{color.Item2},{color.Item3},0.5)";
                        })
                    }
                },
                labels
            };
            return data;
        }

        private List<Tuple<int, int, int, double>> GetDefaultColors()
        {
            return new List<Tuple<int, int, int, double>>
            {
                new Tuple<int, int, int, double>(255, 99, 132,0.5),
                new Tuple<int, int, int, double>(255, 159, 64,0.5),
                new Tuple<int, int, int, double>(255, 205, 86,0.5),
                new Tuple<int, int, int, double>(75, 192, 192,0.5),
                new Tuple<int, int, int, double>(54, 162, 235,0.5),
                new Tuple<int, int, int, double>(153, 102, 255,0.5),
                new Tuple<int, int, int, double>(201, 203, 207,0.5)
            };
        }
        private async void RecupNote()
        {
            var req2 = await HttpRequest.getRequest(App.Url + "api/Competences?filter[include][intituleCompetences]&filter[where][Id_Competence]="+Note.id_Competence);
            var req2des = JsonConvert.DeserializeObject<List<JSCompetence>>(req2);
            if(req2des.ToList().Count > 0)
            {
                Competence.Text = req2des.First().IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).First().intitule;
                Employe.Text = Note.tutore.prenom + " " + Note.tutore.prenom;
            }
            else
            {
                await DisplayAlert("Error", "Aucune Compétence correspondante", "Ok");
            }
            var Notelist = new List<int>()
            {
                Note.note,
                Note.noteRelationnel,
                Note.noteConnaissance,
                Note.noteCommunication,
                Note.notePedagogie
            };
            listnote = Notelist;

            BuildReportHtml();
        }

    }
}