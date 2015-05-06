using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace LolNexusScraper.Model
{
    public class TalentTree
    {
        private HtmlNode TalentTreeNode;
        public OffensiveMasteries offensiveMasteries;
        public DefensiveMasteries defensiveMasteries;
        public UtilityMasteries utilityMasteries;
        public static Bitmap MasteryImages = Properties.Resources.offensive_masteries;
        public static Bitmap MasteryBorder = Properties.Resources.MasteryBorder;


        public TalentTree(HtmlNode talentTreeNode)
        {
            TalentTreeNode = talentTreeNode;

            HtmlNodeCollection talentTree = talentTreeNode.SelectNodes(".//div[@class='talent-tree']");

            Debug.Assert(talentTree.Count == 3);

            HtmlNode offensiveMasteriesNode = talentTree[0];
            HtmlNode defensiveMasteriesNode = talentTree[1];
            HtmlNode utilitiyMasteriesNode = talentTree[2];

            offensiveMasteries = new OffensiveMasteries(offensiveMasteriesNode);
            defensiveMasteries = new DefensiveMasteries(defensiveMasteriesNode);
            utilityMasteries = new UtilityMasteries(utilitiyMasteriesNode);
        }

        public Bitmap GetImage()
        {
            Bitmap bitmap = new Bitmap(offensiveMasteries.Image.Width * 3, offensiveMasteries.Image.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(offensiveMasteries.Image, new PointF(0.0f, 0.0f));
                g.DrawImage(defensiveMasteries.Image, new PointF(offensiveMasteries.Image.Width, 0.0f));
                g.DrawImage(utilityMasteries.Image, new PointF(offensiveMasteries.Image.Width * 2, 0.0f));    
            }
            
            return bitmap;
        }
    }

    public class Mastery
    {
        public string Rank;
        public string Name;
        public Bitmap Image;
        public Point OffensiveImageOffset;
    }

    public class OffensiveMasteries
    {
        public Bitmap Image;
        private Graphics Graphic;
        public Mastery[] Masteries;
        private static RectangleF TotalLocation = new RectangleF(85, 480, 200, 200);

        private static List<PointF> MasteryLocations = new List<PointF>()
        {
            new PointF(18.0f, 16.0f),
            new PointF(80.0f, 16.0f),
            new PointF(142.0f, 16.0f),
            new PointF(204.0f, 16.0f),
            new PointF(18.0f, 94.0f),
            new PointF(80.0f, 94.0f),
            new PointF(142.0f, 94.0f),
            new PointF(204.0f, 94.0f),
            new PointF(18.0f, 172.0f),
            new PointF(80.0f, 172.0f),
            new PointF(142.0f, 172.0f),
            new PointF(204.0f, 172.0f),
            new PointF(18.0f, 250.0f),
            new PointF(80.0f, 250.0f),
            new PointF(142.0f, 250.0f),
            new PointF(204.0f, 250.0f),
            new PointF(18.0f, 328.0f),
            new PointF(80.0f, 328.0f),
            new PointF(204.0f, 328.0f),
            new PointF(80.0f, 406.0f)
        };

        public OffensiveMasteries(HtmlNode offensiveNode)
        {
            Image = Properties.Resources.mastery_offense_bg;

            Graphic = Graphics.FromImage(Image);
            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

            InitializeMasteries(offensiveNode);
        }

        private void InitializeMasteries(HtmlNode offensiveNode)
        {
            var masteryNodes = offensiveNode.SelectNodes(".//div[contains(@class,'talent-icon-container')]");

            Masteries = new Mastery[masteryNodes.Count];

            int count = 0;

            for (int i = 0; i < Masteries.Length; i++)
            {
                var rank = masteryNodes[i].SelectSingleNode(".//span").InnerHtml;
                count += int.Parse(rank[0].ToString());   

                if (!rank.StartsWith("0"))
                {
                    Graphic.DrawImage(Properties.Resources.offensive_masteries.Clone(new RectangleF((i * 49.0f), 0.0f, 49.0f, 49.0f), Properties.Resources.offensive_masteries.PixelFormat), MasteryLocations[i]);
                    Graphic.DrawImage(TalentTree.MasteryBorder, new PointF(MasteryLocations[i].X - 2.0f, MasteryLocations[i].Y - 2.0f));
                    Graphic.DrawString(rank, new Font("arial", 8), Brushes.White, new RectangleF(MasteryLocations[i].X + 17.5f, MasteryLocations[i].Y + 41.0f, 50.0f, 50.0f));
                }
            }

            Graphic.DrawString(string.Format("{0} Offense", count), new Font(")Arial", 14), Brushes.White, TotalLocation);

            //Image.Save(@"C:\Users\Adam\Pictures\ScrapedMasteries\test_image.jpg", ImageFormat.Jpeg);
        }
    }

    public class DefensiveMasteries
    {
        public Bitmap Image;
        private Graphics Graphic;
        public Mastery[] Masteries;
        private static RectangleF TotalLocation = new RectangleF(85, 480, 200, 200);

        private static List<PointF> MasteryLocations = new List<PointF>()
        {
            new PointF(18.0f, 16.0f),
            new PointF(80.0f, 16.0f),
            new PointF(142.0f, 16.0f),
            new PointF(204.0f, 16.0f),
            new PointF(18.0f, 94.0f),
            new PointF(80.0f, 94.0f),
            new PointF(204.0f, 94.0f),
            new PointF(18.0f, 172.0f),
            new PointF(80.0f, 172.0f),
            new PointF(142.0f, 172.0f),
            new PointF(204.0f, 172.0f),
            new PointF(18.0f, 250.0f),
            new PointF(80.0f, 250.0f),
            new PointF(142.0f, 250.0f),
            new PointF(204.0f, 250.0f),
            new PointF(18.0f, 328.0f),
            new PointF(80.0f, 328.0f),
            new PointF(142.0f, 328.0f),
            new PointF(80.0f, 406.0f)
        };

        public DefensiveMasteries(HtmlNode defensiveNode)
        {
            Image = Properties.Resources.mastery_defense_bg;

            Graphic = Graphics.FromImage(Image);
            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

            InitializeMasteries(defensiveNode);
        }

        private void InitializeMasteries(HtmlNode defensiveNode)
        {
            var masteryNodes = defensiveNode.SelectNodes(".//div[contains(@class,'talent-icon-container')]");

            Masteries = new Mastery[masteryNodes.Count];

            int count = 0;

            for (int i = 0; i < Masteries.Length; i++)
            {
                var rank = masteryNodes[i].SelectSingleNode(".//span").InnerHtml;
                count += int.Parse(rank[0].ToString());

                if (!rank.StartsWith("0"))
                {
                    Graphic.DrawImage(Properties.Resources.defensive_masteries.Clone(new RectangleF((i * 49.0f), 0.0f, 49.0f, 49.0f), Properties.Resources.defensive_masteries.PixelFormat), MasteryLocations[i]);
                    Graphic.DrawImage(TalentTree.MasteryBorder, new PointF(MasteryLocations[i].X - 2.0f, MasteryLocations[i].Y - 2.0f));
                    Graphic.DrawString(rank, new Font("arial", 8), Brushes.White, new RectangleF(MasteryLocations[i].X + 17.5f, MasteryLocations[i].Y + 41.0f, 50.0f, 50.0f));
                }
            }

            Graphic.DrawString(string.Format("{0} Defense", count), new Font(")Arial", 14), Brushes.White, TotalLocation);
        }
    }

    public class UtilityMasteries
    {
        public Bitmap Image;
        private Graphics Graphic;
        public Mastery[] Masteries;
        private static RectangleF TotalLocation = new RectangleF(85, 480, 200, 200);

        private static List<PointF> MasteryLocations = new List<PointF>()
        {
            new PointF(18.0f, 16.0f),
            new PointF(80.0f, 16.0f),
            new PointF(142.0f, 16.0f),
            new PointF(204.0f, 16.0f),
            new PointF(80.0f, 94.0f),
            new PointF(142.0f, 94.0f),
            new PointF(204.0f, 94.0f),
            new PointF(18.0f, 172.0f),
            new PointF(80.0f, 172.0f),
            new PointF(142.0f, 172.0f),
            new PointF(204.0f, 172.0f),
            new PointF(18.0f, 250.0f),
            new PointF(80.0f, 250.0f),
            new PointF(142.0f, 250.0f),
            new PointF(204.0f, 250.0f),
            new PointF(80.0f, 328.0f),
            new PointF(142.0f, 328.0f),
            new PointF(80.0f, 406.0f)
        };

        public UtilityMasteries(HtmlNode utilityNode)
        {
            Image = Properties.Resources.mastery_utility_bg;

            Graphic = Graphics.FromImage(Image);
            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

            InitializeMasteries(utilityNode);
        }

        private void InitializeMasteries(HtmlNode utilityNode)
        {
            var masteryNodes = utilityNode.SelectNodes(".//div[contains(@class,'talent-icon-container')]");

            Masteries = new Mastery[masteryNodes.Count];

            int count = 0;

            for (int i = 0; i < Masteries.Length; i++)
            {
                var rank = masteryNodes[i].SelectSingleNode(".//span").InnerHtml;
                count += int.Parse(rank[0].ToString());

                if (!rank.StartsWith("0"))
                {
                    Graphic.DrawImage(Properties.Resources.utility_masteries.Clone(new RectangleF((i * 49.0f), 0.0f, 49.0f, 49.0f), Properties.Resources.utility_masteries.PixelFormat), MasteryLocations[i]);
                    Graphic.DrawImage(TalentTree.MasteryBorder, new PointF(MasteryLocations[i].X - 2.0f, MasteryLocations[i].Y - 2.0f));
                    Graphic.DrawString(rank, new Font("arial", 8), Brushes.White, new RectangleF(MasteryLocations[i].X + 17.5f, MasteryLocations[i].Y + 41.0f, 50.0f, 50.0f));
                }
            }

            Graphic.DrawString(string.Format("{0} Defense", count), new Font(")Arial", 14), Brushes.White, TotalLocation);
        }
    }
}

