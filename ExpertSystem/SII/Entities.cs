using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpertSystem.SII
{
    //Общее

    public class Questionnaire
    {

        public int age;
        public int holidaysLength;
        public int price;
        public string insurance;
        public string country;
        public string region;
        public string destination;
        public string health;
        public string climate;
        public string location;
        public string level;
        public string type;
        public string meals;
        public string[] hotelServices;
        public string[] roomServices;
        public string[] childService;


    }

    public class Country
    {
        public string Name;
        public List<Region> Regions =  new List<Region>();
        private Country(string name)
        {
            this.Name = name;
            
        }

        public static Country GetCountry(string name)
        {
            try
            {
                return II.CurentII.Countries[name];
            }
            catch 
            {
                Country country = new Country(name);
                II.CurentII.Countries[name] = country;
                II.CurentII.Regions[name] = new Dictionary<string, Region>();
                return country;
            }
        }
    }

    public class Region
    {
        public string Name;
        public Country Country;

        private Region(Country country, string region)
        {
            this.Country = country;
            this.Name = region;
            
        }

        private Region(string country, string region)
        {

            this.Country = Country.GetCountry(country);
            this.Name = region;
           
        }

        public static Region GetRegion(Country country, string region) 
        {
            return GetRegion(country.Name,region);
        }

        public static Region GetRegion(string countryName, string regionName)
        {
            try
            {
                return II.CurentII.Regions[countryName][regionName];
            }
            catch
            {
                Country country = Country.GetCountry(countryName);
                Region region = new Region(countryName, regionName);
                II.CurentII.Regions[countryName][regionName] = region;
                country.Regions.Add(region);
                return region;
            }
        }

    }

    public class Hotel
    {
        public string name;
        public Region region;
        public string destination;
        public string location;
        public string level;
        public string type;
        public string meals;
        public string[] hotelServices;
        public string[] roomServices;
        public string[] childServices;
        public string price;

    }

    //Генерация

    public class HotelQueue
    {
        public Hotel Hotel;
        public string[][] Nodes;

    }

    public class HotelsTree
    {
        public HotelNode Root;
    }

    public class HotelNode
    {
        public List<HotelQueue> Queues = new List<HotelQueue>();
        public string NodeName = null;
        public string[] NodeNameList = null;
        public bool Type;
        public List<HotelNode> Children = new List<HotelNode>();
        public HotelNode(string nodeName)
        {
            this.NodeName = nodeName;
            Type = true;
        }
        public HotelNode(string[] nodeName)
        {
            this.NodeNameList = nodeName;
            Type = false;
        }

    }

    public class QuestTree
    {
        public QuestNode Root;
    }

    public class QuestNode
    {
        public string Name;
        public QuestNode(string nodeName)
        {
            this.Name = nodeName;

        }
        public List<QuestLink> Links = new List<QuestLink>();
    }

    public class QuestLink
    {
        public string Link;
        public string Arg;
        public string Function;
        public bool Used = false;
        public QuestNode Node;
        public string KU;
    }
    
    //Продукции
    
    public class Production
    {
        public string State;
        public string Function;
        public string Arg1;
        public string Arg2;
        public string StateResult;
        public string KU;
    }

    public class HotelFindResult 
    {        
        public Hotel Hotel;
        public double KU;
        public Production[] Productions;
    }

}
