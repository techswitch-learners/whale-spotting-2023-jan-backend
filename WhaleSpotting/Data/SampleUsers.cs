using System.Collections.Generic;
using System.Linq;
using WhaleSpotting.Models.Database;
using WhaleSpotting.Repositories;

namespace WhaleSpotting.Data
{
    public static class SampleUsers
    {
        public const int NumberOfUsers = 100;

        private static readonly IList<IList<string>> Data = new List<IList<string>>
        {
            new List<string> {  "admin","Password123","https://picsum.photos/id/1/200/300","He found the chocolate covered roaches quite tasty."},
            new List<string> {  "sgariff1","Password123","https://picsum.photos/id/2/200/300","Some bathing suits just shouldn’t be worn by some people."},
            new List<string> {  "cburgiss2","Password123","https://picsum.photos/id/3/200/300","So long and thanks for the fish."},
            new List<string> {  "bpercival3","Password123","https://picsum.photos/id/4/200/300","She was disgusted he couldn’t tell the difference between lemonade and limeade."},
            new List<string> {  "bnarraway4","Password123","https://picsum.photos/id/5/200/300","We have a lot of rain in June."},
            new List<string> {  "csakins5","Password123","https://picsum.photos/id/6/200/300","The fox in the tophat whispered into the ear of the rabbit."},
            new List<string> {  "zbarkworth6","Password123","https://picsum.photos/id/7/200/300","He colored deep space a soft yellow."},
            new List<string> {  "hverick7","Password123","https://picsum.photos/id/8/200/300","After exploring the abandoned building he started to believe in ghosts."},
            new List<string> {  "member","Password123","https://picsum.photos/id/9/200/300","Pink horses galloped across the sea."},
            new List<string> {  "asmyth9","Password123","https://picsum.photos/id/10/200/300","The waitress was not amused when he ordered green eggs and ham."},
            new List<string> {  "roscallana","Password123","https://picsum.photos/id/11/200/300","The skeleton had skeletons of his own in the closet."},
            new List<string> {  "kbevingtonb","Password123","https://picsum.photos/id/12/200/300","He excelled at firing people nicely."},
            new List<string> {  "scowc","Password123","https://picsum.photos/id/13/200/300","He realized there had been several deaths on this road but his concern rose when he saw the exact number."},
            new List<string> {  "gnorthaged","Password123","https://picsum.photos/id/14/200/300","She could hear him in the shower singing with a joy she hoped he'd retain after she delivered the news."},
            new List<string> {  "jbalsome","Password123","https://picsum.photos/id/15/200/300","I caught my squirrel rustling through my gym bag."},
            new List<string> {  "lgalgeyf","Password123","https://picsum.photos/id/16/200/300","He was sitting in a trash can with high street class."},
            new List<string> {  "olaurantg","Password123","https://picsum.photos/id/17/200/300","It caught him off guard that space smelled of seared steak."},
            new List<string> {  "dmableyh","Password123","https://picsum.photos/id/18/200/300","As he looked out the window he saw a clown walk by."},
            new List<string> {  "sguillondi","Password123","https://picsum.photos/id/19/200/300","He had concluded that pigs must be able to fly in Hog Heaven."},
            new List<string> {  "mdjuricicj","Password123","https://picsum.photos/id/20/200/300","Smoky the Bear secretly started the fires."},
            new List<string> {  "radamoviczk","Password123","https://picsum.photos/id/21/200/300","He swore he just saw his sushi move."},
            new List<string> {  "zgoodacrel","Password123","https://picsum.photos/id/22/200/300","Everybody should read Chaucer to improve their everyday vocabulary."},
            new List<string> {  "fblowm","Password123","https://picsum.photos/id/23/200/300","The Guinea fowl flies through the air with all the grace of a turtle."},
            new List<string> {  "zpritchettn","Password123","https://picsum.photos/id/24/200/300","He dreamed of eating green apples with worms."},
            new List<string> {  "kfilshino","Password123","https://picsum.photos/id/25/200/300","The random sentence generator generated a random sentence about a random sentence."},
            new List<string> {  "agoneaup","Password123","https://picsum.photos/id/26/200/300","Tuesdays are free if you bring a gnome costume."},
            new List<string> {  "nmackrillq","Password123","https://picsum.photos/id/27/200/300","It was at that moment that he learned there are certain parts of the body that you should never Nair."},
            new List<string> {  "ewadlyr","Password123","https://picsum.photos/id/28/200/300","Separation anxiety is what happens when you can't find your phone."},
            new List<string> {  "afeedhams","Password123","https://picsum.photos/id/29/200/300","The fish listened intently to what the frogs had to say."},
            new List<string> {  "fchestneyt","Password123","https://picsum.photos/id/30/200/300","Buried deep in the snow he hoped his batteries were fresh in his avalanche beacon."},
            new List<string> {  "cguillouxu","Password123","https://picsum.photos/id/31/200/300","Jeanne wished she has chosen the red button."},
            new List<string> {  "jhucksteppv","Password123","https://picsum.photos/id/32/200/300","Lightning Paradise was the local hangout joint where the group usually ended up spending the night."},
            new List<string> {  "bsanctow","Password123","https://picsum.photos/id/33/200/300","Dan took the deep dive down the rabbit hole."},
            new List<string> {  "sjeevesx","Password123","https://picsum.photos/id/34/200/300","My cheese is on the third shelf."},
            new List<string> {  "ljereatty","Password123","https://picsum.photos/id/35/200/300","She was the type of girl who wanted to live in a pink house."},
            new List<string> {  "kternouthz","Password123","https://picsum.photos/id/36/200/300","They say that dogs are man's best friend but this cat was setting out to sabotage that theory."},
            new List<string> {  "vmcmenamin10","Password123","https://picsum.photos/id/37/200/300","He was willing to find the depths of the rabbit hole in order to be with her."},
            new List<string> {  "sgreenhalf11","Password123","https://picsum.photos/id/38/200/300","Of course she loves her pink bunny slippers."},
            new List<string> {  "sfellgate12","Password123","https://picsum.photos/id/39/200/300","This is the last random sentence I will be writing and I am going to stop mid-sent"},
            new List<string> {  "bdickens13","Password123","https://picsum.photos/id/40/200/300","She had a habit of taking showers in lemonade."},
            new List<string> {  "bmckaile14","Password123","https://picsum.photos/id/41/200/300","Lucifer was surprised at the amount of life at Death Valley."},
            new List<string> {  "vaishford15","Password123","https://picsum.photos/id/42/200/300","Each person who knows you has a different perception of who you are."},
            new List<string> {  "kgauford16","Password123","https://picsum.photos/id/43/200/300","The ants enjoyed the barbecue more than the family."},
            new List<string> {  "cseelbach17","Password123","https://picsum.photos/id/44/200/300","Hit me with your pet shark!"},
            new List<string> {  "ewinsper18","Password123","https://picsum.photos/id/45/200/300","Please tell me you don't work in a morgue."},
            new List<string> {  "bwelds19","Password123","https://picsum.photos/id/46/200/300","I want to buy a onesie… but know it won’t suit me."},
            new List<string> {  "bkerin1a","Password123","https://picsum.photos/id/47/200/300","My dentist tells me that chewing bricks is very bad for your teeth."},
            new List<string> {  "mtompkins1b","Password123","https://picsum.photos/id/48/200/300","The busker hoped that passers-by would throw money but they threw tomatoes instead so he exchanged his hat for a juicer."},
            new List<string> {  "aclever1c","Password123","https://picsum.photos/id/49/200/300","When transplanting seedlings candied teapots will make the task easier."},
            new List<string> {  "ndenny1d","Password123","https://picsum.photos/id/50/200/300","There is a fly in the car with us."},
            new List<string> {  "tscorah1e","Password123","https://picsum.photos/id/51/200/300","He wondered why at 18 he was old enough to go to war"},
            new List<string> {  "lmcgow1f","Password123","https://picsum.photos/id/52/200/300","When he had to picnic on the beach he purposely put sand in other people’s food."},
            new List<string> {  "ajannasch1g","Password123","https://picsum.photos/id/53/200/300","I am out of paper for the printer."},
            new List<string> {  "mdommett1h","Password123","https://picsum.photos/id/54/200/300","We had a three-course meal."},
            new List<string> {  "enorcop1i","Password123","https://picsum.photos/id/55/200/300","Writing a list of random sentences is harder than I initially thought it would be."},
            new List<string> {  "ebaline1j","Password123","https://picsum.photos/id/56/200/300","The efficiency we have at removing trash has made creating trash more acceptable."},
            new List<string> {  "rdorcey1k","Password123","https://picsum.photos/id/57/200/300","He took one look at what was under the table and noped the hell out of there."},
            new List<string> {  "psurplice1l","Password123","https://picsum.photos/id/58/200/300","The pizza smells delicious."},
            new List<string> {  "tdyott1m","Password123","https://picsum.photos/id/59/200/300","You bite up because of your lower jaw."},
            new List<string> {  "tconnachan1n","Password123","https://picsum.photos/id/60/200/300","They throw cabbage that turns your brain into emotional baggage."},
            new List<string> {  "jmcclelland1o","Password123","https://picsum.photos/id/61/200/300","Whenever he saw a red flag warning at the beach he grabbed his surfboard."},
            new List<string> {  "nmaund1p","Password123","https://picsum.photos/id/62/200/300","She wondered what his eyes were saying beneath his mirrored sunglasses."},
            new List<string> {  "manselmi1q","Password123","https://picsum.photos/id/63/200/300","The sun had set and so had his dreams."},
            new List<string> {  "gantoniazzi1r","Password123","https://picsum.photos/id/64/200/300","As you consider all the possible ways to improve yourself and the world you notice John Travolta seems fairly unhappy."},
            new List<string> {  "mengelbrecht1s","Password123","https://picsum.photos/id/65/200/300","If you like tuna and tomato sauce- try combining the two. It’s really not as bad as it sounds."},
            new List<string> {  "mtommasetti1t","Password123","https://picsum.photos/id/66/200/300","Twin 4-month-olds slept in the shade of the palm tree while the mother tanned in the sun."},
            new List<string> {  "efredy1u","Password123","https://picsum.photos/id/67/200/300","Now I need to ponder my existence and ask myself if I'm truly real"},
            new List<string> {  "pmccowen1v","Password123","https://picsum.photos/id/68/200/300","You're unsure whether or not to trust him but very thankful that you wore a turtle neck."},
            new List<string> {  "jdossettor1w","Password123","https://picsum.photos/id/69/200/300","Her hair was windswept as she rode in the black convertible."},
            new List<string> {  "dogdahl1x","Password123","https://picsum.photos/id/70/200/300","He's in a boy band which doesn't make much sense for a snake."},
            new List<string> {  "msearle1y","Password123","https://picsum.photos/id/71/200/300","Yeah I think it's a good environment for learning English."},
            new List<string> {  "bmaclise1z","Password123","https://picsum.photos/id/72/200/300","I checked to make sure that he was still alive."},
            new List<string> {  "mhillitt20","Password123","https://picsum.photos/id/73/200/300","Flying fish few by the space station."},
            new List<string> {  "btumielli21","Password123","https://picsum.photos/id/74/200/300","While on the first date he accidentally hit his head on the beam."},
            new List<string> {  "rdupey22","Password123","https://picsum.photos/id/75/200/300","Mrs Miller wants the entire house repainted."},
            new List<string> {  "iheineke23","Password123","https://picsum.photos/id/76/200/300","Stop waiting for exceptional things to just happen."},
            new List<string> {  "iangric24","Password123","https://picsum.photos/id/77/200/300","The snow-covered path was no help in finding his way out of the backcountry."},
            new List<string> {  "esteljes25","Password123","https://picsum.photos/id/78/200/300","I want more detailed information."},
            new List<string> {  "lashard26","Password123","https://picsum.photos/id/79/200/300","Two seats were vacant."},
            new List<string> {  "ddevons27","Password123","https://picsum.photos/id/80/200/300","Be careful with that butter knife."},
            new List<string> {  "wundrell28","Password123","https://picsum.photos/id/81/200/300","Fluffy pink unicorns are a popular status symbol among macho men."},
            new List<string> {  "ilangworthy29","Password123","https://picsum.photos/id/82/200/300","He invested some skill points in Charisma and Strength."},
            new List<string> {  "mminards2a","Password123","https://picsum.photos/id/83/200/300","You have every right to be angry but that doesn't give you the right to be mean."},
            new List<string> {  "kbennion2b","Password123","https://picsum.photos/id/84/200/300","The gruff old man sat in the back of the bait shop grumbling to himself as he scooped out a handful of worms."},
            new List<string> {  "onorridge2c","Password123","https://picsum.photos/id/85/200/300","His confidence would have been admirable if it wasn't for his stupidity."},
            new List<string> {  "rtraske2d","Password123","https://picsum.photos/id/86/200/300","He figured a few sticks of dynamite were easier than a fishing pole to catch fish."},
            new List<string> {  "gmccard2e","Password123","https://picsum.photos/id/87/200/300","This is a Japanese doll."},
            new List<string> {  "zcapstaff2f","Password123","https://picsum.photos/id/88/200/300","The urgent care center was flooded with patients after the news of a new deadly virus was made public."},
            new List<string> {  "isleford2g","Password123","https://picsum.photos/id/89/200/300","She had the gift of being able to paint songs."},
            new List<string> {  "nnary2h","Password123","https://picsum.photos/id/90/200/300","He drank life before spitting it out."},
            new List<string> {  "jlukianov2i","Password123","https://picsum.photos/id/91/200/300","Going from child to childish to childlike is only a matter of time."},
            new List<string> {  "adurkin2j","Password123","https://picsum.photos/id/92/200/300","Erin accidentally created a new universe."},
            new List<string> {  "ccoronas2k","Password123","https://picsum.photos/id/93/200/300","The tour bus was packed with teenage girls heading toward their next adventure."},
            new List<string> {  "jkeener2l","Password123","https://picsum.photos/id/94/200/300","She is never happy until she finds something to be unhappy about; then she is overjoyed."},
            new List<string> {  "gwynett2m","Password123","https://picsum.photos/id/95/200/300","The crowd yells and screams for more memes."},
            new List<string> {  "scordelle2n","Password123","https://picsum.photos/id/96/200/300","The tart lemonade quenched her thirst but not her longing."},
            new List<string> {  "sdeport2o","Password123","https://picsum.photos/id/97/200/300","He barked orders at his daughters but they just stared back with amusement."},
            new List<string> {  "zperchard2p","Password123","https://picsum.photos/id/98/200/300","His seven-layer cake only had six layers."},
            new List<string> {  "jiceton2q","Password123","https://picsum.photos/id/99/200/300","I ate a sock because people on the Internet told me to."},
            new List<string> {  "mbeadell2r","Password123","https://picsum.photos/id/100/200/300","Before he moved to the inner city he had always believed that security complexes were psychological."},
            };

        public static IEnumerable<User> GetUsers()
        {
            return Enumerable.Range(0, NumberOfUsers).Select(CreateRandomUser);
        }

        private static User CreateRandomUser(int index)
        {
            return new User
            {
                Username = Data[index][0].ToLower(),
                Password = Data[index][1],
                ProfileImageUrl = Data[index][2],
                UserBio = Data[index][3],
                UserType = index < 5 ? UserType.Admin : UserType.Member,
            };
        }
    }
}
