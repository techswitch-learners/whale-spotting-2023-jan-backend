using System.Collections.Generic;
using System.Linq;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Data
{
    public static class SampleSpecies
    {
        public const int NumberOfSpecies = 16;

        private static readonly IList<IList<string>> Data = new List<IList<string>>
        {
            new List<string> { "https://i.guim.co.uk/img/static/sys-images/Guardian/Pix/pictures/2014/3/25/1395760641926/06c5fad9-0135-4db1-a887-10c8992b374a-460x276.jpeg?quality=85&auto=format&fit=max&s=1f727ca993deaf3626c556f6b7421671","Andrews’ beaked whale","0","0","0","White","South","Krill"},
            new List<string> { "https://iwc.int/public/images/j4I_X/cMattCurnock2010-8296-MR.jpg","Antarctic minke whale","0","1","2","Grey","South","Krill"},
            new List<string> { "https://www.whaletrail.co.za/images/arnouxs-beaked-whale-south-africa-590x390.jpg","Arnoux’s beaked whale","0","0","1","White","South","Squid"},
            new List<string> { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRXgOvmQJCxRnsMeB2d000FhJhQ_dq5KhX04Q&usqp=CAU","Baird’s beaked whale","1","0","2","Black","North","Squid"},
            new List<string> { "https://uk.whales.org/wp-content/uploads/sites/6/2019/06/blainvilles-beaked-whale-sg-colin-macleod4.jpg","Blainville’s beaked whale","1","0","1","Grey","North and South","Fish"},
            new List<string> { "https://files.worldwildlife.org/wwfcmsprod/images/Blue_Whale/hero_small/gex0x01aq_shutterstock_764499823.jpg","Blue whale","0","1","3","Blue","North and South","Krill"},
            new List<string> { "https://www.science.org/do/10.1126/article.71709/full/sn-whalesh_2.jpg","Bowhead whale","1","1","2","Black","North","Squid"},
            new List<string> { "https://uk.whales.org/wp-content/uploads/sites/6/2018/07/brydes-whale-jirayu-tour-ekkul-sg.jpg","Bryde's whale","0","1","2","Black","North and South","Squid"},
            new List<string> { "https://upload.wikimedia.org/wikipedia/commons/f/f4/Dwarf_minke_whale_%2830694501214%29.jpg","Common minke whale","0","1","1","Blue","North and South","Krill"},
            new List<string> { "https://uk.whales.org/wp-content/uploads/sites/6/2020/08/cuviers-beaked-whale-bmmro-1.jpg","Cuvier’s beaked whale","1","0","1","Blue","North and South","Squid"},
            new List<string> { "https://www.ourendangeredworld.com/wp-content/uploads/2022/07/Dwarf-Sperm-Whale.jpg.webp","Dwarf sperm whale","1","0","0","Black","North and South","Fish"},
            new List<string> { "https://uk.whales.org/wp-content/uploads/sites/6/2018/08/brydes-whale-jirayu-tour-ekkul-sg-4-1024x682.jpg","Eden's whale","0","1","2","Blue","North and South","Fish"},
            new List<string> { "https://cdn.roaring.earth/wp-content/uploads/2016/10/1_JR5dC4zcXeiljmDVTmHWqw-scaled.jpeg","False killer whale","0","0","1","White","North and South","Squid"},
            new List<string> { "https://upload.wikimedia.org/wikipedia/commons/c/ce/Finhval_%281%29.jpg","Fin whale","0","1","3","Black","North and South","Fish"},
            new List<string> { "https://iwdg.ie/cms_files/wp-content/uploads/2019/04/Gervaiss-beaked-whale-observed-off-Tenerife-Canary-Islands-Note-the-head-morphology.png","Gervais’ beaked whale","0","0","1","Blue","North","Fish"},
            new List<string> { "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpBbvLGCNADqfPTZ9usm8sFfZdRJkhIA-zhA&usqp=CAU","Ginkgo-toothed beaked whale","1","0","1","Black","North and South","Krill"},
        };

        public static IEnumerable<WhaleSpecies> GetSpecies()
        {
            return Enumerable.Range(0, NumberOfSpecies).Select(CreateRandomSpecies);
        }

        private static WhaleSpecies CreateRandomSpecies(int index)
        {
            var newTailType = new TailType();
            var newTeethType = new TeethType();
            var newWhaleSize = new WhaleSize();
            if (Data[index][2] == "0") 
            {
                newTailType = TailType.Bifurcated;
            }
            else
            {
                newTailType = TailType.NonBifurcated;
            }
            if (Data[index][3] == "0") 
            {
                newTeethType = TeethType.Baleen;
            }
            else
            {
                newTeethType = TeethType.Toothed;
            }
            if (Data[index][4] == "0") 
            {
                newWhaleSize = WhaleSize.Small0To5m;
            }
            else if (Data[index][4] == "1")
            {
                newWhaleSize = WhaleSize.Medium5To10m;
            }
            else if (Data[index][4] == "2")
            {
                newWhaleSize = WhaleSize.Large10To20m;
            }
            else if (Data[index][4] == "2")
            {
                newWhaleSize = WhaleSize.VeryLargeOver20m;
            }
            return new WhaleSpecies
            {
                ImageUrl = Data[index][0],
                Name = Data[index][1],
                TailType = newTailType,
                TeethType = newTeethType,
                Size= newWhaleSize,
                Colour = Data[index][5],
                Location = Data[index][6],
                Diet = Data[index][7],
            };
        }
    }
}
