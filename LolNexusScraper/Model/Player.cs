using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LolNexusScraper.Model
{
    public class Player
    {
        private HtmlNode PlayerNode { get; set; }

        public string Name { get; set; }
        public int GamesPlayedOnChampion { get; private set; }
        public string CurrentSeasonRanking { get; private set; }
        public string NormalWins { get; private set; }
        public string RankedWinsLosses { get; private set; }
        public string ChampionKills { get; private set; }
        public string ChampionDeaths { get; private set; }
        public string ChampionAssists { get; private set; }
        public string KDA { get; private set; }

        private TalentTree masteriesTree;
        private Bitmap _MasteriesImage;

        public Bitmap MasteriesImage
        {
            get { return _MasteriesImage; }
        }


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


            new Thread(
                () =>
                {
                    masteriesTree = new TalentTree(masteriesNode, ref _MasteriesImage);
                }).Start();
        }
    }
}
    