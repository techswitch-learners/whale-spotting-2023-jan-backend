﻿using System.Collections.Generic;
using System.Linq;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Models.Request;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Data
{
    public static class SampleSightings
    {
        public const int NumberOfSightings = 15;

        private static readonly IList<IList<string>> Data = new List<IList<string>>
        {
            new List<string> { "46.850749","-31.945634","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpP_riFxOaMgaWTGRkHMnhkWfHLzCq263lcg&usqp=CAU","7","Tuesdays are free if you bring a gnome costume.","8","97"},
            new List<string> { "-14.1925714591675","-15.5979766958147","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS000xmAAc7ApcmxNVlYWeGKTH_7TpT-pdfBw&usqp=CAU","10","The random sentence generator generated a random sentence about a random sentence.","12","42"},
            new List<string> { "-7.29727","71.030575","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQRWs91blvKQ8ep1X55zTOIvJLMSbOaF0_anw&usqp=CAU","5","It caught him off guard that space smelled of seared steak.","2","89"},
            new List<string> { "-6.6140016180921","96.0402183092394","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTBmYymvKtpHaBX3hhFkF0wWHNsW5mwDAKe5A&usqp=CAU","2","He was willing to find the depths of the rabbit hole in order to be with her.","11","80"},
            new List<string> { "-30.1431867783806","45.7914859493589","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTarKIvd0Xr3BrWdtP5ZSdEAY7n0TG6XAWUb_q_RJ2gF49K2ZF3XxKetiUhi20HTMfpXv4&usqp=CAU","8","As you consider all the possible ways to improve yourself and the world you notice John Travolta seems fairly unhappy.","15","4"},
            new List<string> { "8.25654135155628","141.929471605934","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTcvCPzBVjs9sOKHTZO5Owksu7UWuv9PWTw9HlNalcZscf1eCUMw6oprIUVuPUMVpkRCt0&usqp=CAU","9","Dan took the deep dive down the rabbit hole.","1","33"},
            new List<string> { "-29.8760764108341","-83.5678579600153","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQdiZDzfANHEdL9Dfxv8SCp-QD8_aH1Djg2eg&usqp=CAU","10","He invested some skill points in Charisma and Strength.","15","40"},
            new List<string> { "12.2646479091386","-111.842679072519","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ96CMAEXoStmI1SA28uZSz1c0vmvTeq-fGiw&usqp=CAU","5","When transplanting seedlings candied teapots will make the task easier.","8","40"},
            new List<string> { "51.4841445343099","0.197197209248326","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDgVvRgSeiY2MdSPPf6b2G3AZ5xhMQwl1qZw&usqp=CAU","8","The urgent care center was flooded with patients after the news of a new deadly virus was made public.","2","42"},
            new List<string> { "57.3212092164886","-4.43578975774163","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQGHHoYyKkg_VjOGMVuLVklz3ox2AOf77xEw&usqp=CAU","8","I am out of paper for the printer.","6","97"},
            new List<string> { "72.1640023745701","-65.3730928590327","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTwHVjNgZJ56nX3WY-CzCDJ8pRwdi9tHiaBvw&usqp=CAU","9","You bite up because of your lower jaw.","11","26"},
            new List<string> { "20.2272476393577","-171.594819630174","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRX2QVDcPIV-M1GDneAXunaz7fLoeccDD9TUw&usqp=CAU","8","Separation anxiety is what happens when you can't find your phone.","10","60"},
            new List<string> { "49.7855858377441","149.030181938103","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT5tAmADk6EQ2kkFqsmEDSuvFmwuEUXshxxYA&usqp=CAU","9","Tuesdays are free if you bring a gnome costume.","2","68"},
            new List<string> { "-41.5693529417908","24.2254939822084","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR4b2uzBl3-Qabp0mwZDBo-pFBSptoBMhDaLw&usqp=CAU","5","Her hair was windswept as she rode in the black convertible.","13","20"},
            new List<string> { "-73.7493320592588","-32.9034133437029","https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTYiEWIBUk5Im3sxkDk97H0GndxUUbGlQMIxg&usqp=CAU","4","The tour bus was packed with teenage girls heading toward their next adventure.","7","10"},
        };

        public static IEnumerable<SampleWhaleSightingRequest> GetSightings()
        {
            return Enumerable.Range(0, NumberOfSightings).Select(CreateRandomSighting);
        }

        private static SampleWhaleSightingRequest CreateRandomSighting(int index)
        {
            Random gen = new Random();
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            DateTime newDateOfSghting = start.AddDays(gen.Next(range));

            return new SampleWhaleSightingRequest
            {
                DateOfSighting = newDateOfSghting,
                LocationLatitude = float.Parse(Data[index][0]),
                LocationLongitude = float.Parse(Data[index][1]),
                PhotoImageURL = Data[index][2],
                NumberOfWhales = int.Parse(Data[index][3]),
                ApprovalStatus = ApprovalStatus.Pending,
                Description = Data[index][4],
                WhaleSpeciesId = int.Parse(Data[index][5]),
                UserId = int.Parse(Data[index][6]),
            };
        }
    }
}
