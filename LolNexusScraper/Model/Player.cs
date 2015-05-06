using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolNexusScraper.Model
{
    public class Player
    {
        private HtmlNode PlayerNode { get; set; }

        public string Name { get; set; }
        public int GamesPlayedOnChampion { get; set; }
        public string CurrentSeasonRanking { get; set; }
        public string NormalWins { get; set; }
        public string RankedWinsLosses { get; set; }

        public string ChampionKills { get; set; }

        public string ChampionDeaths { get; set; }

        public string ChampionAssists { get; set; }
        public string KDA { get; set; }

        public Player(HtmlNode playerNode)
        {
            PlayerNode = playerNode;
            Initialize();
        }

        public void Initialize()
        {
            HtmlNode nameNode =  PlayerNode.SelectSingleNode(".//td[@class='name']");
            Name = nameNode.SelectSingleNode(".//span").InnerHtml;

            HtmlNode currentSeasonNode = PlayerNode.SelectSingleNode(".//td[@class='current-season']");
            HtmlNode currentSeasonRankingNode = currentSeasonNode.SelectSingleNode(".//div[@class='ranking']");
            CurrentSeasonRanking = currentSeasonRankingNode.SelectSingleNode(".//span").InnerHtml.Replace("<b>", "").Replace("</b>","");

            HtmlNode normalWinsNode = PlayerNode.SelectSingleNode(".//td[@class='normal-wins']");
            NormalWins = normalWinsNode.SelectSingleNode(".//span").InnerHtml;

            HtmlNode rankedWinsLossesNode = PlayerNode.SelectSingleNode(".//td[@class='ranked-wins-losses']");
            string rankedWins = rankedWinsLossesNode.SelectSingleNode(".//span[@class='ranked-wins']").InnerHtml;
            string rankedLosses = rankedWinsLossesNode.SelectSingleNode(".//span[@class='ranked-losses']").InnerHtml;
            RankedWinsLosses = string.Format("{0}/{1}", rankedWins, rankedLosses);

            HtmlNode championKDANode = PlayerNode.SelectSingleNode(".//td[@class='champion-kda']");
            ChampionKills = championKDANode.SelectSingleNode(".//span[@class='kills']").InnerHtml;
            ChampionDeaths = championKDANode.SelectSingleNode(".//span[@class='deaths']").InnerHtml;
            ChampionAssists = championKDANode.SelectSingleNode(".//span[@class='assists']").InnerHtml;

            HtmlNode masteriesNode = PlayerNode.SelectSingleNode(".//td[@class='masteries j-masteries-modal-link']");

            var tree = new TalentTree(masteriesNode);

            tree.GetImage().Save(@"C:\Users\Adam\Pictures\Testing\" + Name + ".jpg", ImageFormat.Jpeg);

        }
    }
}
