using System;
using System.Collections.Generic;

namespace Models
{
    public class UserComment
    {
        public string Comment { get; set; }

        public UserComment() { }

        public UserComment(string comment)
        {
            Comment = comment;
        }
    }

    public class AttractionDescription
    {
        public string Description { get; set; }

        public AttractionDescription() { }

        public AttractionDescription(string description)
        {
            Description = description;
        }
    }

    public interface ISeed<T>
    {
        //In order to separate from real and seeded instances
        public bool Seeded { get; set; }

        //Seeded The instance
        public T Seed(csSeedGenerator seedGenerator);
    }

    public class csSeedGenerator : Random
    {

        string[] _firstnames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco, Frodo, Gandalf, Sam, Peregrin, Saruman".Split(", ");
        string[] _lastnames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White".Split(", ");


        string[] _attractionTitles = "Enchanted Forest, Historic Castle, Adventure Island, Nature Retreat, Cultural Experience, Amusement Park, Wildlife Safari, Scenic Hike, Mystical Cavern, Ocean Paradise, Art Gallery Tour, Skydiving Adventure, Mountain Expedition, Zipline Thrills, Underwater Exploration, Space Odyssey, Desert Safari, Ancient Ruins, Lakeside ,Music Festival".Split(", ");


        string[][] _city =
            {
                "Stockholm, Göteborg, Malmö, Uppsala, Linköping, Örebro".Split(", "),
                "Oslo, Bergen, Trondheim, Stavanger, Dramen".Split(", "),
                "Köpenhamn, Århus, Odense, Aahlborg, Esbjerg".Split(", "),
                "Helsingfors, Espoo, Tampere, Vaanta, Oulu".Split(", "),
             };

        string[][] _address =
        {
                "Svedjevägen, Ringvägen, Vasagatan, Odenplan, Birger Jarlsgatan, Äppelviksvägen, Kvarnbacksvägen".Split(", "),
                "Bygdoy alle, Frognerveien, Pilestredet, Vidars gate, Sågveien, Toftes gate, Gardeveiend".Split(", "),
                "Rolighedsvej, Fensmarkgade, Svanevej, Gröndalsvej, Githersgade, Classensgade, Moltekesvej".Split(", "),
                "Arkandiankatu, Liisankatu, Ruoholahdenkatu, Pohjoistranta, Eerikinkatu, Vauhtitie, Itainen Vaideki".Split(", ")
        };

        string[] _country = "Sweden, Norway, Denmark, Finland".Split(", ");



        AttractionDescription[] _descriptions = {
            new AttractionDescription("Explore the breathtaking natural beauty of our serene landscapes."),
            new AttractionDescription("Experience the thrill of adventure in the heart of the wilderness."),
            new AttractionDescription("Journey through history and discover the secrets of ancient civilizations."),
            new AttractionDescription("Immerse yourself in the vibrant culture and traditions of our city."),
            new AttractionDescription("Indulge in culinary delights from around the world at our diverse eateries."),
            new AttractionDescription("Witness stunning performances that will leave you in awe."),
            new AttractionDescription("Get your adrenaline pumping with exciting rides and attractions."),
            new AttractionDescription("Relax and unwind in our tranquil gardens and scenic parks."),
            new AttractionDescription("Learn about wildlife conservation and encounter fascinating creatures."),
            new AttractionDescription("Sail the high seas and embark on a nautical adventure."),
            new AttractionDescription("Marvel at architectural wonders and iconic landmarks."),
            new AttractionDescription("Embark on a gastronomic journey through our vibrant food scene."),
            new AttractionDescription("Discover the mysteries of the universe at our state-of-the-art planetarium."),
            new AttractionDescription("Take a step back in time and relive historical events."),
            new AttractionDescription("Join thrilling sporting events and cheer for your favorite teams."),
            new AttractionDescription("Get up close and personal with rare and exotic animals."),
            new AttractionDescription("Experience the magic of our enchanting fairy tale world."),
            new AttractionDescription("Satisfy your shopping cravings at our bustling markets and boutiques."),
            new AttractionDescription("Celebrate the spirit of creativity at our art and music festivals."),
        };


        UserComment[] _comments = {
            new UserComment("Discover the romantic charm of our enchanting gardens."),
            new UserComment("Savor the flavors of our international culinary delights."),
            new UserComment("Unearth the secrets of ancient civilizations in our historical exhibits."),
            new UserComment("Experience the magic of love in our fairy tale world."),
            new UserComment("Find love in the heart of the city's vibrant culture."),
            new UserComment("Love can be as thrilling as an adventure in the wilderness."),
            new UserComment("Witness the fiery passion of love in our breathtaking performances."),
            new UserComment("Love is a journey with many historical landmarks."),
            new UserComment("Experience love's sweet embrace at our cozy cafes."),
            new UserComment("The four most important words in any romantic getaway—'I'll book the room.'"),
            new UserComment("Love is sweeter than coffee, especially in our cafes."),
            new UserComment("Feel that tingly sensation when you explore attractions together."),
            new UserComment("Choose the lazy river ride for a relaxing day at our water park."),
            new UserComment("Take a break and do nothing at our tranquil gardens."),
            new UserComment("We'll explain everything you need to know during your visit."),
            new UserComment("Our management ensures a hassle-free experience for you."),
            new UserComment("Our teamwork philosophy: 'One for all and all for one.'"),
            new UserComment("Imagine a world without work—it's possible here."),
            new UserComment("Don't lose your belongings, but if you do, we have a lost and found."),
            new UserComment("Creativity is encouraged, mistakes are embraced."),
            new UserComment("If you hit 'escape,' you'll still enjoy your time at our park."),
            new UserComment("Work hard, play hard—enjoy our thrilling rides."),
            new UserComment("Good work leads to more fun and adventure."),
            new UserComment("Let our executives handle everything while you relax."),
            new UserComment("Start tomorrow's adventures today—no procrastination here."),
            new UserComment("We're experts in creative approaches to not getting things done."),
            new UserComment("Procrastination is like a vacation you pay for later."),
            new UserComment("Experience the efficiency of panic mode on our thrilling rides."),
            new UserComment("Stop putting off fun—start your adventure tomorrow."),
            new UserComment("Procrastination always leads to exciting experiences."),
            new UserComment("Wasting time can be a valuable investment in your happiness."),
            new UserComment("Keep up with the attractions of yesterday, today."),
            new UserComment("The last-minute decision to visit us is the best one."),
            new UserComment("Work is fascinating, especially when you visit us."),
            new UserComment("Slow down and enjoy our solution to procrastination."),
            new UserComment("Don't procrastinate—visit us today!"),
        };

        public string AttractionTitle => _attractionTitles[this.Next(0, _attractionTitles.Length)];

        public string FirstName => _firstnames[this.Next(0, _firstnames.Length)];
        public string LastName => _lastnames[this.Next(0, _lastnames.Length)];
        public string FullName => $"{FirstName} {LastName}";

        
        //General random truefalse
        public bool Bool => (this.Next(0, 10) < 5) ? true : false;

        public string Country => _country[this.Next(0, _country.Length)];

        public string City(string Country = null)
        {

            var cIdx = this.Next(0, _city.Length);
            if (Country != null)
            {
                //Give a City in that specific country
                cIdx = Array.FindIndex(_country, c => c.ToLower() == Country.Trim().ToLower());

                if (cIdx == -1) throw new Exception("Country not found");
            }

            return _city[cIdx][this.Next(0, _city[cIdx].Length)];
        }

        public string StreetAddress(string Country = null)
        {

            var cIdx = this.Next(0, _city.Length);
            if (Country != null)
            {
                //Give a City in that specific country
                cIdx = Array.FindIndex(_country, c => c.ToLower() == Country.Trim().ToLower());

                if (cIdx == -1) throw new Exception("Country not found");
            }

            return $"{_address[cIdx][this.Next(0, _address[cIdx].Length)]} {this.Next(1, 51)}";
        }

        #region Seed from own datastructures
        public TEnum FromEnum<TEnum>() where TEnum : struct
        {
            if (typeof(TEnum).IsEnum)
            {

                var _names = typeof(TEnum).GetEnumNames();
                var _name = _names[this.Next(0, _names.Length)];

                return Enum.Parse<TEnum>(_name);
            }
            throw new ArgumentException("Not an enum type");
        }
        public TItem FromList<TItem>(List<TItem> items)
        {
            return items[this.Next(0, items.Count)];
        }
        #endregion

        #region generate seeded Lists
        public List<TItem> ToList<TItem>(int NrOfItems)
            where TItem : ISeed<TItem>, new()
        {
            //Create a list of seeded items
            var _list = new List<TItem>();
            for (int c = 0; c < NrOfItems; c++)
            {
                _list.Add(new TItem().Seed(this));
            }
            return _list;
        }

        //Create a list of unique randomly seeded items
        public List<TItem> ToListUnique<TItem>(int tryNrOfItems, List<TItem> appendToUnique = null)
             where TItem : ISeed<TItem>, IEquatable<TItem>, new()
        {
            //Create a list of uniquely seeded items
            HashSet<TItem> _set = (appendToUnique == null) ? new HashSet<TItem>() : new HashSet<TItem>(appendToUnique);

            while (_set.Count < tryNrOfItems)
            {
                var _item = new TItem().Seed(this);

                int _preCount = _set.Count();
                int tries = 0;
                do
                {
                    _set.Add(_item);
                    if (++tries >= 5)
                    {
                        //it takes more than 5 tries to generate a random item.
                        //Assume this is it. return the list
                        return _set.ToList();
                    }
                } while (!(_set.Count > _preCount));
            }

            return _set.ToList();
        }


        //Pick a number of unique items from a list of TItem (which does not have to be unique)
        public List<TItem> FromListUnique<TItem>(int tryNrOfItems, List<TItem> list = null)
        where TItem : ISeed<TItem>, IEquatable<TItem>, new()
        {
            //Create a list of uniquely seeded items
            HashSet<TItem> _set = new HashSet<TItem>();

            while (_set.Count < tryNrOfItems)
            {
                var _item = list[this.Next(0, list.Count)];

                int _preCount = _set.Count();
                int tries = 0;
                do
                {
                    _set.Add(_item);
                    if (++tries >= 5)
                    {
                        //it takes more than 5 tries to generate a random item.
                        //Assume this is it. return the list
                        return _set.ToList();
                    }
                } while (!(_set.Count > _preCount));
            }

            return _set.ToList();
        }

        #endregion

        #region Comments
        public List<UserComment> AllComments => _comments.ToList<UserComment>();
        public UserComment Comment => _comments[this.Next(0, _comments.Length)];
        #endregion

        #region Descriptions
        public List<AttractionDescription> AllDescriptions => _descriptions.ToList<AttractionDescription>();
        public AttractionDescription Description => _descriptions[this.Next(0, _descriptions.Length)];
        #endregion

        #region Unused seeding
        //public string RegNr
        //{
        //    get
        //    {
        //        {
        //            char c1 = (char)this.Next('A', 'Z' + 1);
        //            char c2 = (char)this.Next('A', 'Z' + 1);
        //            char c3 = (char)this.Next('A', 'Z' + 1);

        //            int i1 = this.Next(0, 10);
        //            int i2 = this.Next(0, 10);
        //            int i3 = this.Next(0, 10);

        //            return $"{c1}{c2}{c3} {i1}{i2}{i3}";
        //        }
        //    }
        //}

        //public DateTime getDateTime(int? fromYear = null, int? toYear = null)
        //{
        //    bool dateOK = false;
        //    DateTime _date = default;
        //    while (!dateOK)
        //    {
        //        fromYear ??= DateTime.Today.Year;
        //        toYear ??= DateTime.Today.Year + 1;

        //        try
        //        {
        //            int year = this.Next(Math.Min(fromYear.Value, toYear.Value),
        //                Math.Max(fromYear.Value, toYear.Value));
        //            int month = this.Next(1, 13);
        //            int day = this.Next(1, 32);

        //            _date = new DateTime(year, month, day);
        //            dateOK = true;
        //        }
        //        catch
        //        {
        //            dateOK = false;
        //        }
        //    }

        //    return DateTime.SpecifyKind(_date, DateTimeKind.Utc);
        //}

        //public string Email(string fname = null, string lname = null)
        //{
        //    fname ??= FirstName;
        //    lname ??= LastName;

        //    return $"{fname}.{lname}@{_domains[this.Next(0, _domains.Length)]}";
        //}
        //public int ZipCode => this.Next(10101, 100000);
        //string[] _domains = "icloud.com, me.com, mac.com, hotmail.com, gmail.com".Split(", ");
        //public string Phone => $"{this.Next(700, 800)} {this.Next(100, 1000)} {this.Next(100, 1000)}";

        #endregion

    }
}

