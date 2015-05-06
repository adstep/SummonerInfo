using HtmlAgilityPack;
using mshtml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace LolNexusScraper.Model
{
    public class LolNexusMatch
    {
        private HtmlAgilityPack.HtmlDocument _Document;
        private HtmlAgilityPack.HtmlDocument Document
        {
            get
            {
                if (_Document == null)
                {
                    _Document = new HtmlAgilityPack.HtmlDocument();
                }

                return _Document;
            }
            set
            {
                _Document = value;
            }
        }

        private List<Player> _Team1;
        private List<Player> _Team2;

        public List<Player> Team1
        {
            get
            {
                if(_Team1 == null)
                {
                    _Team1 = new List<Player>();
                }

                return _Team1;
            }
            set
            {
                _Team1 = value;
            }
        }

        public List<Player> Team2
        {
            get
            {
                if(_Team2 == null)
                {
                    _Team2 = new List<Player>();
                }

                return _Team2;
            }
            set
            {
                _Team2 = value;
            }
        }

        public LolNexusMatch(string region, string name)
        {
            LolNexusMatchJsonResult result;
            using (WebClient client = new WebClient())
            {
                var json = new WebClient().DownloadString("http://www.lolnexus.com/ajax/get-game-info/" + region + ".json?name=" + HttpUtility.HtmlEncode(name));
                result = JsonConvert.DeserializeObject<LolNexusMatchJsonResult>(json);
            }

            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html.OptionOutputAsXml = true;
            html.LoadHtml(result.html);
            HtmlNode document = html.DocumentNode;

            Initialize(html.DocumentNode);
        }

        public void waitTillLoad(WebBrowser webBrControl)
        {
            WebBrowserReadyState loadStatus;
            int waittime = 100000;
            int counter = 0;
            while (true)
            {
                loadStatus = webBrControl.ReadyState;
                Application.DoEvents();
                if ((counter > waittime) || (loadStatus == WebBrowserReadyState.Uninitialized) || (loadStatus == WebBrowserReadyState.Loading) || (loadStatus == WebBrowserReadyState.Interactive))
                {
                    break;
                }
                counter++;
            }

            counter = 0;
            while (true)
            {
                loadStatus = webBrControl.ReadyState;
                Application.DoEvents();
                if (loadStatus == WebBrowserReadyState.Complete && webBrControl.IsBusy != true)
                {
                    break;
                }
                counter++;
            }
        }

        public void Initialize(HtmlNode document)
        {
            HtmlNode team1Node = document.SelectSingleNode("//div[@class='team-1']");
            HtmlNodeCollection team1Collection = team1Node.SelectNodes(".//tr[1]");
            Team1.Add(new Player(team1Collection[1]));
            Team1.Add(new Player(team1Node.SelectSingleNode(".//tr[2]")));
            Team1.Add(new Player(team1Node.SelectSingleNode(".//tr[3]")));
            Team1.Add(new Player(team1Node.SelectSingleNode(".//tr[4]")));
            Team1.Add(new Player(team1Node.SelectSingleNode(".//tr[5]")));

            HtmlNode team2Node = document.SelectSingleNode("//div[@class='team-2']");
            HtmlNodeCollection team2Collection = team2Node.SelectNodes(".//tr[1]");
            Team2.Add(new Player(team2Collection[1]));
            Team2.Add(new Player(team1Node.SelectSingleNode(".//tr[2]")));
            Team2.Add(new Player(team1Node.SelectSingleNode(".//tr[3]")));
            Team2.Add(new Player(team1Node.SelectSingleNode(".//tr[4]")));
            Team2.Add(new Player(team1Node.SelectSingleNode(".//tr[5]")));
        }
    }
}
 