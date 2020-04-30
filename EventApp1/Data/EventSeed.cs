using EventApp1.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp1.Data
{
    public static class EventSeed
    {
        //seeding the data into the tables in the database
        public static void OnSeed(EventContext context)
        {
            //migrating the c# code into the tables
            context.Database.Migrate();

            
            //adding data into the tables only if the data is not available
            if (!context.EventTypes.Any())
            {
                //add range is like adding data into the tables
               
                context.EventTypes.AddRange(GetPreConfiguredEventType());
                context.SaveChanges();
            }

            if (!context.EventLocations.Any())
            {
                context.EventLocations.AddRange(GetPreConfiguredEventLocations());
                context.SaveChanges();
            }

            if (!context.Event_Details.Any())
            {
                context.Event_Details.AddRange(GetPreConfiguredEventDetails());
                context.SaveChanges();
            }
        }



        //Creating method for getpreconfiguredcatalogbrand to add brands in our 
        //table changing to ienumerable because of collection
        private static IEnumerable<EventType> GetPreConfiguredEventType()
        {
            //create a new list and add the brands we want
            return new List<EventType>
          {
              new EventType{Type = "Concerts"},
              new EventType{Type = "Culture and Education"},
              new EventType{Type = "Social"},
              new EventType{Type = "Sports"},
              new EventType{Type = "Tech Conferences"}
          };
        }


        private static IEnumerable<EventLocations> GetPreConfiguredEventLocations()
        {
            //create a new list and add the brands we want
            return new List<EventLocations>
          {
              new EventLocations{Location = "Bellevue"},
              new EventLocations{Location = "Redmond"},
              new EventLocations{Location = "Renton"},
              new EventLocations{Location = "Seattle"},
              new EventLocations{Location = "Sammamish"},
              new EventLocations{Location = "Lynwood"},
              new EventLocations{Location = "Kirkland"},
              new EventLocations{Location = "Woodinville"},
              new EventLocations{Location = "Anacortes"},
              new EventLocations{Location = "Everett"}
          };
        }


        private static IEnumerable<EventDetails> GetPreConfiguredEventDetails()
        {
            return new List<EventDetails>
            {
                new EventDetails{
                    EventTypeId= 1,
                    EventLocationId=2,
                    Name = " Justin Beiber Songs",
                    Description ="Canadian-born singer - song writer Justein Beiber is back! following a few years off from the industry",
                    Venue = "Redmond High School,17272 Northeast 104th Street,Redmond, WA 98052",
                    Price = 18,
                    Age = "Above 17",
                    Occupancy= 200,
                    DateTime = "May 3rd,7:00 PM",
                    Day = "Saturday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/1"},

                new EventDetails{
                    EventTypeId= 1,
                    EventLocationId=7,
                    Name = " Moors and McCumber",
                    Description ="Join us for a night with Moors and McCumber, as they delve into love and life through haunting lyris,soaring harmonies and dazzling instrumental proficiency",
                    Venue = "Stage 7 Pianos,12037 124th Avenue Northeast,Kirkland, WA 98034",
                    Price = 50,
                    Age = "20 plus",
                    Occupancy= 150,
                    DateTime = "May 4th,7:30 PM ",
                    Day = "Sunday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/2"},

                 new EventDetails{
                    EventTypeId= 1,
                    EventLocationId=4,
                    Name = "Playback Music",
                    Description ="Bring your friends&family for an evening you won't soon forget with some of cripple creek's finest talents as they strut their stuff on stage at the buttle theater",
                    Venue = "Seattle Asian Medicine and Martial Arts,12025 Lake City Way NE, STE B,Seattle, WA 98125 ",
                    Price = 25,
                    Age = "Above 15",
                    Occupancy= 250,
                    DateTime = "May 9th, 6:30 PM",
                    Day = "Friday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/3"},

                  new EventDetails{
                    EventTypeId= 1,
                    EventLocationId=3,
                    Name = "Chain Smoker Concert",
                    Description ="Open-Air performance venue, with a variety of food trucks, hosting an annual summer soncert series",
                    Venue = "High Dive Renton, Renton, WA",
                    Price = 65,
                    Age = "Above 18",
                    Occupancy= 400,
                    DateTime = "May 10th, 5:00 PM",
                    Day = "Saturday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/4"},

                   new EventDetails{
                    EventTypeId= 2,
                    EventLocationId=1,
                    Name = "Carrier Skills",
                    Description ="Our Journey towards sustainity an inclusive culture. Come and join us",
                    Venue = "Bellevue College, Bellevue",
                    Price = 30,
                    Age = "Above 21",
                    Occupancy= 200 ,
                    DateTime = "May 11th, 6:15 PM",
                    Day = "Sunday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/5"},

                    new EventDetails{
                    EventTypeId= 2,
                     EventLocationId=5,
                    Name = "How To Build A Culture People Want to Work In",
                    Description ="Your company's culture is 8x's more influential than strategy in determining performance. We'll teach you to build a one page culture plan",
                    Venue = "801 228th Ave SE,. Sammamish, WA 98075",
                    Price =65 ,
                    Age = "19 plus",
                    Occupancy= 200 ,
                    DateTime = "May 16th, 4:30 PM",
                    Day = "Friday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/6"},

                     new EventDetails{
                    EventTypeId= 2,
                     EventLocationId=4,
                    Name = "Culture Night",
                    Description ="Come join us for a friendly night of cultural immersion! Dinner and entertainment are provided",
                    Venue = "U WASHINGTON College Students,TBD,Seattle, WA",
                    Price = 35,
                    Age = "Above 18",
                    Occupancy= 300,
                    DateTime = "May 17th, 6:30 PM",
                    Day = "Saturday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/7"},

                      new EventDetails{
                    EventTypeId= 2,
                    EventLocationId=4,
                    Name = " An ingenious way to Life",
                    Description =" Fostering Disabilty culture in higher education",
                    Venue = "The 2100 Building,2100 24th Avenue South,Seattle, WA 98144",
                    Price = 30,
                    Age = "Above 20",
                    Occupancy= 150,
                    DateTime = "May 18th, 7:00 PM",
                    Day = "Sunday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/8"},

                       new EventDetails{
                    EventTypeId= 3,
                    EventLocationId=4,
                    Name = "The Society for Financial Awarness",
                    Description ="Social Security are big part of most amerian.s retirment plan. This course is designed to make sure you know how social security works and how benefits are determined",
                    Venue = "Hyatt Regency Seattle | 3rd Floor - Columbia Ballroom,808 Howell Street,Seattle, WA 98101",
                    Price = 40,
                    Age = "Above 24",
                    Occupancy= 320,
                    DateTime = "May 27th,6:30 PM",
                    Day = "Wednesday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/9"},

                        new EventDetails{
                    EventTypeId= 3,
                    EventLocationId=6,
                    Name = " Beefsteak Night",
                    Description ="Concept of beefsteak originated in NY as a blue collar dinner meant to have people in large groups eat with reckless abandon",
                    Venue = "Funhouse,109 Eastlake Ave E,LynnWood",
                    Price = 28,
                    Age = "Above 23",
                    Occupancy= 300,
                    DateTime = "May 30th,7:00 PM",
                    Day = "Friday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/Eventpic/10"},

                         new EventDetails{
                    EventTypeId= 3,
                    EventLocationId=7,
                    Name = "Free Emotion Regulation + Social Skills class",
                    Description ="All- Star kiddos,Child Development Center,WA is holding a free trial emotion regulation +social skills class.",
                    Venue = "Stage 7 Pianos,12037 124th Avenue Northeast,Kirkland, WA 98034" ,
                    Price = 30,
                    Age = "Between 6-11 year old",
                    Occupancy= 150,
                    DateTime = "May 31st, 4:30 PM",
                    Day = "Saturday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/11"},

                        new EventDetails{
                    EventTypeId= 3,
                    EventLocationId=9,
                    Name = "GoGreen Conference",
                    Description ="The Go-Green Conference is a sustainably learning experience for business and government decision-makers ",
                    Venue = "100 Commercial Ave, Anacortes, WA 98221",
                    Price = 65,
                    Age = "Above 22",
                    Occupancy= 320,
                    DateTime = "June 4th, 6:30 PM",
                    Day = "Thursday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/12"},

                          new EventDetails{
                    EventTypeId= 4,
                    EventLocationId=8,
                    Name = "SPIN Seattle- Community Ping Pong Lessons",
                    Description ="SPIN is introducing a community building group lessons once per month on our center court",
                    Venue = "Woodinville,WA",
                    Price = 45,
                    Age = "Between 11-16",
                    Occupancy= 80,
                    DateTime = "June 7th, 4:30 PM",
                    Day = "Saturday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/13"},

                          new EventDetails{
                    EventTypeId= 4,
                    EventLocationId=10,
                    Name = " Running 101: Training Fundamentals with Michael Ea",
                    Description ="An introduction to running fundamentals for any distance with three- time Olymic Trials Qualifier Michael Eaton",
                    Venue = "Verlocal,Redmond, WA 98052, United States, 16421 Cleveland St Ste B,Everett, WA 98052", 
                    Price = 55,
                    Age = "Alove 25",
                    Occupancy= 150 ,
                    DateTime = "June 8th, 3:30 PM",
                    Day = "Sunday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/14"},

                          new EventDetails{
                    EventTypeId= 4,
                    EventLocationId=1,
                    Name = "Dance Sport TryOut Class for Kids",
                    Description ="Special Class for the children introducing them to the world of DanceSport",
                    Venue = "Bellevue College,3000 Landerholm Circle Southeast,Bellevue, WA 98007",
                    Price = 20,
                    Age = "Between 3-5",
                    Occupancy= 110,
                    DateTime = "June 14th, 5:30 PM",
                    Day = "Friday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/15"},

                              new EventDetails{
                    EventTypeId= 4,
                    EventLocationId=1,
                    Name = "Under The Lights Flag Football ",
                    Description ="Under the Lights is the exclusive youth flag football partner of Under Armour, Inc. The league is for both boys and girls of all skill level",
                    Venue = "Bellevue,YOUR CHOICE,Bellevue, WA 98004",
                    Price = 99,
                    Age = "Above 10",
                    Occupancy= 150,
                    DateTime = "June 10th, 5:00 PM",
                    Day = "Saturday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/16"},

                               new EventDetails{
                    EventTypeId= 5,
                    EventLocationId=2,
                    Name = "Socially Inept: Tech Roast Show",
                    Description ="Tech is awesome. Tech people are terrible. But you’re afraid to tell them how awful they are, because you think they have deadly robot crows",
                    Venue = "2757 152nd Ave NE,2757 152nd Avenue Northeast,Redmond, WA 98052",
                    Price = 75,
                    Age = "Above 20",
                    Occupancy= 150,
                    DateTime = "June 12th,5PM",
                    Day = "Tuesday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/17"},

                                new EventDetails{
                    EventTypeId= 5,
                    EventLocationId=3,
                    Name = "Kal Academy Information Session",
                    Description ="At Kal Academy, our next cohort of classes is starting soon. Come hear us talk about the program and get your questions answered",
                    Venue = "Hale's Ales Palladium,4301 Leary Way NW,Renton, WA 98107",
                    Price = 55,
                    Age = "Above 21",
                    Occupancy= 60,
                    DateTime = "June 15th , 11:30 AM",
                    Day = "Tuesday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/18"},

                                 new EventDetails{
                    EventTypeId= 5,
                    EventLocationId=2,
                    Name = "ProdCon Seattle",
                    Description ="Get the inside scoop from top Product People before The Product Management Conference kick-off, followed by the exclusive VIP dinner",
                    Venue ="Seattle,Virtual Workshop,Seattle, WA",
                    Price = 45,
                    Age = "Above 22",
                    Occupancy= 200,
                    DateTime = "June 17th, 12:00 PM",
                    Day = "Wednesday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/19"},

                                  new EventDetails{
                    EventTypeId= 5,
                    EventLocationId=10,
                    Name = "Develop a Successful Healthcare Tech Startup Business Today!",
                    Description ="Develop a Successful Healthcare Tech Startup Business Today! Medical - Digital Health -Hackathon - Virtual - Webinar",
                    Venue ="TBC,Everett, WA 98121", 
                    Price = 89,
                    Age = "Above 19",
                    Occupancy= 220,
                    DateTime = "June 18, 11:00 AM",
                    Day = "Thursday",
                    PictureUrl= "http://externalcatalogbaseurltobereplaced/api/pic/20"},
            };




          }



     }
 }

