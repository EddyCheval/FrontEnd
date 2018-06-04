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
	public partial class AffichageCompetence : ContentPage
	{
        private List<double> listnote;
        private JSCompetence jsCompetence;
        private List<JSNote> Notes;
        private JSEmploye employe;
        private bool Estnote;
        public AffichageCompetence(JSCompetence jSCompetence, JSEmploye jSEmploye = null)
		{
			InitializeComponent();

            if (jSEmploye == null)
            {
                employe = Settings.ActualUser;
            }
            else
            {
                employe = jSEmploye;
            }
            jsCompetence = jSCompetence;
            JSIntituleCompetence jS = jSCompetence.IntituleCompetences.Where(c => c.id_Langue == Settings.ActualLanguage).First();
            Intitule.Text = jS.intitule;
            Description.Text = jS.description;
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
            var chartDataNull = Estnote ? "" : "<p style=\"color:#999999;text-align:center;Margin-top:40vh;\"><i>Aucune note n'a été donnée !!</i></p>";
            var inlineStyle = "style=\"width:100%;height:97%;\"";
            var chartJsScript = !Estnote ? "" :"<script src=\"https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.bundle.min.js\"></script>";
            var chartConfigJsScript =!Estnote ? "" : $"<script>{chartConfig}</script>";
            var chartContent = !Estnote ? "" : $@"<div id=""chart-container"" {inlineStyle}>
  <canvas id=""chart"" />
</div>";
            var document = $@"<html style=""width:97%;height:100%;"">
  <head>{chartJsScript}</head>
  <body {inlineStyle}>
    {chartContent}
    {chartConfigJsScript}
    {chartDataNull}
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

        private List<Tuple<int, int, int>> GetDefaultColors()
        {
            return new List<Tuple<int, int, int>>
            {
                new Tuple<int, int, int>(255, 99, 132),
                new Tuple<int, int, int>(255, 159, 64),
                new Tuple<int, int, int>(255, 205, 86),
                new Tuple<int, int, int>(75, 192, 192),
                new Tuple<int, int, int>(54, 162, 235),
                new Tuple<int, int, int>(153, 102, 255),
                new Tuple<int, int, int>(201, 203, 207)
            };
        }
        private async void RecupNote()
        {
            var req = await HttpRequest.getRequest(App.Url + "api/Notes?filter[where][Id_Tuteur]=" + employe.id_Employe + "&filter[where][Id_Competence]=" +jsCompetence.id_Competence+"&filter[include][Tutore]");
            var reqdes = new List<JSNote>();
            var Notelist = new List<double>()
                {
                    0,
                    0,
                    0,
                    0,
                    0
                };
            if (req.Count() >2)
            {
                //cahnger en double ?
                 reqdes = JsonConvert.DeserializeObject<List<JSNote>>(req);
                foreach (var note in reqdes)
                {
                    Notelist[0] += note.note;
                    Notelist[1] += note.noteRelationnel;
                    Notelist[2] += note.noteConnaissance;
                    Notelist[3] += note.noteCommunication;
                    Notelist[4] += note.notePedagogie;
                }
                Estnote = true;
                Notelist[0] = Notelist[0] / reqdes.Count();
                Notelist[1] = Notelist[1] / reqdes.Count();
                Notelist[2] = Notelist[2] / reqdes.Count();
                Notelist[3] = Notelist[3] / reqdes.Count();
                Notelist[4] = Notelist[4] / reqdes.Count();
                
            }
            else
            {
                Estnote = false;
            }
            Notes = reqdes;
            listnote = Notelist;

            BuildReportHtml();
           
        }

        private async void Detail_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListNote(Notes));
        }
    }
}