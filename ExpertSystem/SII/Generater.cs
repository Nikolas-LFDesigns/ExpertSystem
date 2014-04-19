using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExpertSystem.SII
{
    class Generater
    {

        private Hotel[] hotels;

        private Region[] regions;
        private Country[] countrys;
        private string[] destinations;
        private string[] locations;
        private string[] levels;
        private string[] types;
        private string[] mealses;
        private List<List<string>> hotelServicesSets = new List<List<string>>();
        private List<List<string>> roomServicesSets = new List<List<string>>();
        private List<List<string>> childServicesSets = new List<List<string>>();
        private string[] prices;

        private Hotel[] loadHotels(string hotelsFile)
        {
            List<Hotel> hotels = new List<Hotel>();
            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Workbooks.Open(hotelsFile);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(1);
            Excel.Range range = worksheet.UsedRange;


            for (int i = 2; i <= range.Rows.Count; i++)
            {
                if (Convert.ToString(worksheet.Cells[i, 1].Value)!=null &&
                    !Convert.ToString(worksheet.Cells[i, 1].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 2].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 2].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 3].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 3].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 4].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 4].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 5].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 5].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 6].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 6].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 7].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 7].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 8].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 8].Value).Equals("") &&
                    Convert.ToString(worksheet.Cells[i, 12].Value) != null &&
                    !Convert.ToString(worksheet.Cells[i, 12].Value).Equals(""))
                {
                    Hotel hotel = new Hotel();
                    hotel.name = Convert.ToString(worksheet.Cells[i, 1].Value);
                    Country country = Country.GetCountry(Convert.ToString(worksheet.Cells[i, 2].Value));
                    Region region = Region.GetRegion(country, Convert.ToString(worksheet.Cells[i, 3].Value));
                    hotel.region = region;
                    hotel.destination = Convert.ToString(worksheet.Cells[i, 4].Value);
                    hotel.location = Convert.ToString(worksheet.Cells[i, 5].Value);
                    hotel.level = Convert.ToString(worksheet.Cells[i, 6].Value);
                    hotel.type = Convert.ToString(worksheet.Cells[i, 7].Value);
                    hotel.meals = Convert.ToString(worksheet.Cells[i, 8].Value);

                    string roomServices = Convert.ToString(worksheet.Cells[i, 9].Value);

                    List<string> l = roomServices.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).OfType<string>().ToList();
                    l.Sort((x, y) => String.CompareOrdinal(x, y));
                    hotel.roomServices = l.ToArray();

                    string hotelServices = Convert.ToString(worksheet.Cells[i, 10].Value);

                    l = hotelServices.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).OfType<string>().ToList();
                    l.Sort((x, y) => String.CompareOrdinal(x, y));
                    hotel.hotelServices = l.ToArray();


                    string childServices = Convert.ToString(worksheet.Cells[i, 11].Value);

                    l = childServices.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).OfType<string>().ToList();
                    l.Sort((x, y) => String.CompareOrdinal(x, y));
                    hotel.childServices = l.ToArray();

                    hotel.price = Convert.ToString(worksheet.Cells[i, 12].Value);

                    hotels.Add(hotel);
                }
            }
            workbook.Close(true);
            excel.Quit();

            return hotels.ToArray();

        }

        private void initLists(Hotel[] hotels)
        {
            List<Region> regionsList = new List<Region>();
            List<Country> countrysList = new List<Country>();
            List<string> destinationsList = new List<string>();
            List<string> locationsList = new List<string>();
            List<string> levelsList = new List<string>();
            List<string> typesList = new List<string>();
            List<string> mealsesList = new List<string>();
            List<string> pricesList = new List<string>();

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < regionsList.Count; j++)
                {
                    if (regionsList[j].Name.Equals(hotels[i].region.Name))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    regionsList.Add(hotels[i].region);
                }
            }

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < countrysList.Count; j++)
                {
                    if (countrysList[j].Name.Equals(hotels[i].region.Country.Name))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    countrysList.Add(hotels[i].region.Country);
                }
            }

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < destinationsList.Count; j++)
                {
                    if (destinationsList[j].Equals(hotels[i].destination))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    destinationsList.Add(hotels[i].destination);
                }
            }

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < locationsList.Count; j++)
                {
                    if (locationsList[j].Equals(hotels[i].location))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    locationsList.Add(hotels[i].location);
                }
            }

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < levelsList.Count; j++)
                {
                    if (levelsList[j].Equals(hotels[i].level))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    levelsList.Add(hotels[i].level);
                }
            }

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < typesList.Count; j++)
                {
                    if (typesList[j].Equals(hotels[i].type))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    typesList.Add(hotels[i].type);
                }
            }

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < mealsesList.Count; j++)
                {
                    if (mealsesList[j].Equals(hotels[i].meals))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    mealsesList.Add(hotels[i].meals);
                }
            }

            for (int i = 0; i < hotels.Length; i++)
            {
                List<string> l = new List<string>();
                for (int k = 0; k < hotels[i].hotelServices.Length; k++)
                {
                    l.Add(hotels[i].hotelServices[k]);
                }

                hotelServicesSets.Add(l);


            }

            for (int i = 0; i < hotels.Length; i++)
            {
                List<string> l = new List<string>();
                for (int k = 0; k < hotels[i].roomServices.Length; k++)
                {
                    l.Add(hotels[i].roomServices[k]);
                }

                roomServicesSets.Add(l);

            }

            for (int i = 0; i < hotels.Length; i++)
            {
                List<string> l = new List<string>();
                for (int k = 0; k < hotels[i].childServices.Length; k++)
                {
                    l.Add(hotels[i].childServices[k]);
                }

                childServicesSets.Add(l);

            }

            for (int i = 0; i < hotels.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < pricesList.Count; j++)
                {
                    if (pricesList[j].Equals(hotels[i].price))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    pricesList.Add(hotels[i].price);
                }
            }

            countrys = countrysList.ToArray();
            regions = regionsList.ToArray();
            destinations = destinationsList.ToArray();
            locations = locationsList.ToArray();
            levels = levelsList.ToArray();
            types = typesList.ToArray();
            mealses = mealsesList.ToArray();
            prices = pricesList.ToArray();

        }

        private HotelQueue[] getHotelQueues(Hotel[] hotels)
        {

            List<HotelQueue> hotelQueues = new List<HotelQueue>();

            for (int i = 0; i < hotels.Length; i++)
            {
                HotelQueue queue = new HotelQueue();

                queue.Hotel = hotels[i];

                queue.Nodes = new string[11][];
                queue.Nodes[0] = new string[] { hotels[i].region.Country.Name };
                queue.Nodes[1] = new string[] { hotels[i].region.Name };
                queue.Nodes[2] = new string[] { hotels[i].destination };
                queue.Nodes[3] = new string[] { hotels[i].location };
                queue.Nodes[4] = new string[] { hotels[i].level };
                queue.Nodes[5] = new string[] { hotels[i].type };
                queue.Nodes[6] = new string[] { hotels[i].meals };
                queue.Nodes[7] = hotels[i].hotelServices;
                queue.Nodes[8] = hotels[i].roomServices;
                queue.Nodes[9] = hotels[i].childServices;
                queue.Nodes[10] = new string[] { hotels[i].price };

                hotelQueues.Add(queue);

            }

            return hotelQueues.ToArray();
        }

        private HotelsTree getHotelsTree(HotelQueue[] queues)
        {
            HotelsTree tree = new HotelsTree();

            tree.Root = new HotelNode("Root_Node");

            List<List<HotelQueue>> queueListList = new List<List<HotelQueue>>();
            List<HotelNode> NodeList = new List<HotelNode>();
            for (int i = 0; i < countrys.Length; i++)
            {
                List<HotelQueue> queueList = new List<HotelQueue>();

                for (int j = 0; j < queues.Length; j++)
                {
                    if (queues[j].Nodes[0][0].Equals(countrys[i].Name))
                    {
                        queueList.Add(queues[j]);
                    }
                }

                if (queueList.Count != 0)
                {
                    HotelNode Node = new HotelNode(countrys[i].Name);
                    tree.Root.Children.Add(Node);
                    NodeList.Add(Node);
                    queueListList.Add(queueList);
                }

            }

            List<List<HotelQueue>> queueListList_1 = new List<List<HotelQueue>>();
            List<HotelNode> NodeList_1 = new List<HotelNode>();

            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = 0; j < regions.Length; j++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();

                    for (int k = 0; k < queueListList[i].Count; k++)
                    {
                        if (queueListList[i][k].Nodes[1][0].Equals(regions[j].Name))
                        {
                            queueList.Add(queueListList[i][k]);
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(regions[j].Name);
                        NodeList[i].Children.Add(Node);
                        NodeList_1.Add(Node);
                        queueListList_1.Add(queueList);
                    }
                }
            }

            queueListList = new List<List<HotelQueue>>();
            NodeList = new List<HotelNode>();

            for (int i = 0; i < NodeList_1.Count; i++)
            {
                for (int j = 0; j < destinations.Length; j++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();

                    for (int k = 0; k < queueListList_1[i].Count; k++)
                    {
                        if (queueListList_1[i][k].Nodes[2][0].Equals(destinations[j]))
                        {
                            queueList.Add(queueListList_1[i][k]);
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(destinations[j]);
                        NodeList_1[i].Children.Add(Node);
                        NodeList.Add(Node);
                        queueListList.Add(queueList);
                    }
                }
            }

            queueListList_1 = new List<List<HotelQueue>>();
            NodeList_1 = new List<HotelNode>();

            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = 0; j < locations.Length; j++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();

                    for (int k = 0; k < queueListList[i].Count; k++)
                    {
                        if (queueListList[i][k].Nodes[3][0].Equals(locations[j]))
                        {
                            queueList.Add(queueListList[i][k]);
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(locations[j]);
                        NodeList[i].Children.Add(Node);
                        NodeList_1.Add(Node);
                        queueListList_1.Add(queueList);
                    }
                }
            }

            queueListList = new List<List<HotelQueue>>();
            NodeList = new List<HotelNode>();

            for (int i = 0; i < NodeList_1.Count; i++)
            {
                for (int j = 0; j < levels.Length; j++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();

                    for (int k = 0; k < queueListList_1[i].Count; k++)
                    {
                        if (queueListList_1[i][k].Nodes[4][0].Equals(levels[j]))
                        {
                            queueList.Add(queueListList_1[i][k]);
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(levels[j]);
                        NodeList_1[i].Children.Add(Node);
                        NodeList.Add(Node);
                        queueListList.Add(queueList);
                    }
                }
            }

            queueListList_1 = new List<List<HotelQueue>>();
            NodeList_1 = new List<HotelNode>();

            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = 0; j < types.Length; j++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();

                    for (int k = 0; k < queueListList[i].Count; k++)
                    {
                        if (queueListList[i][k].Nodes[5][0].Equals(types[j]))
                        {
                            queueList.Add(queueListList[i][k]);
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(types[j]);
                        NodeList[i].Children.Add(Node);
                        NodeList_1.Add(Node);
                        queueListList_1.Add(queueList);
                    }
                }
            }

            queueListList = new List<List<HotelQueue>>();
            NodeList = new List<HotelNode>();

            for (int i = 0; i < NodeList_1.Count; i++)
            {
                for (int j = 0; j < mealses.Length; j++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();

                    for (int k = 0; k < queueListList_1[i].Count; k++)
                    {
                        if (queueListList_1[i][k].Nodes[6][0].Equals(mealses[j]))
                        {
                            queueList.Add(queueListList_1[i][k]);
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(mealses[j]);
                        NodeList_1[i].Children.Add(Node);
                        NodeList.Add(Node);
                        queueListList.Add(queueList);
                    }
                }
            }

            queueListList_1 = new List<List<HotelQueue>>();
            NodeList_1 = new List<HotelNode>();

            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int k = 0; k < queueListList[i].Count; k++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();
                    int j;
                    for (j = 0; j < hotelServicesSets.Count; j++)
                    {
                        bool flag = queueListList[i][k].Nodes[7].Length == hotelServicesSets[j].Count;
                        for (int l = 0; flag && l < queueListList[i][k].Nodes[7].Length && l < hotelServicesSets[j].Count; l++)
                        {
                            if (!queueListList[i][k].Nodes[7][l].Equals(hotelServicesSets[j][l]))
                            {
                                flag = false;

                            }
                        }
                        if (flag)
                        {
                            queueList.Add(queueListList[i][k]);
                            break;
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(hotelServicesSets[j].ToArray());
                        NodeList[i].Children.Add(Node);
                        NodeList_1.Add(Node);
                        queueListList_1.Add(queueList);
                       
                    }
                }
            }

            queueListList = new List<List<HotelQueue>>();
            NodeList = new List<HotelNode>();

            for (int i = 0; i < NodeList_1.Count; i++)
            {
                for (int k = 0; k < queueListList_1[i].Count; k++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();
                    int j;
                    for (j = 0; j < roomServicesSets.Count; j++)
                    {
                    
                        bool flag = queueListList_1[i][k].Nodes[8].Length == roomServicesSets[j].Count;
                        for (int l = 0; flag && l < queueListList_1[i][k].Nodes[8].Length && l < roomServicesSets[j].Count; l++)
                        {
                            if (!queueListList_1[i][k].Nodes[8][l].Equals(roomServicesSets[j][l]))
                            {
                                flag = false;

                            }
                        }
                        if (flag)
                        {
                            queueList.Add(queueListList_1[i][k]);
                            break;
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(roomServicesSets[j].ToArray());
                        NodeList_1[i].Children.Add(Node);
                        NodeList.Add(Node);
                        queueListList.Add(queueList);
                        
                    }
                }
            }

            queueListList_1 = new List<List<HotelQueue>>();
            NodeList_1 = new List<HotelNode>();

            for (int i = 0; i < NodeList.Count; i++)
            {
                
                    for (int k = 0; k < queueListList[i].Count; k++)
                    {
                    List<HotelQueue> queueList = new List<HotelQueue>();
                    int j;
                    for (j = 0; j < childServicesSets.Count; j++)
                    {
                        bool flag = queueListList[i][k].Nodes[9].Length == childServicesSets[j].Count;
                        for (int l = 0; flag && l < queueListList[i][k].Nodes[9].Length && l < childServicesSets[j].Count; l++)
                        {
                            if (!queueListList[i][k].Nodes[9][l].Equals(childServicesSets[j][l]))
                            {
                                flag = false;

                            }
                        }
                        if (flag)
                        {
                            queueList.Add(queueListList[i][k]);
                            break;
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(childServicesSets[j].ToArray());
                        NodeList[i].Children.Add(Node);
                        NodeList_1.Add(Node);
                        queueListList_1.Add(queueList);
                        
                    }
                }
            }

            queueListList = new List<List<HotelQueue>>();
            NodeList = new List<HotelNode>();

            for (int i = 0; i < NodeList_1.Count; i++)
            {
                for (int j = 0; j < prices.Length; j++)
                {
                    List<HotelQueue> queueList = new List<HotelQueue>();

                    for (int k = 0; k < queueListList_1[i].Count; k++)
                    {
                        if (queueListList_1[i][k].Nodes[10][0].Equals(prices[j]))
                        {
                            queueList.Add(queueListList_1[i][k]);
                        }
                    }

                    if (queueList.Count != 0)
                    {
                        HotelNode Node = new HotelNode(prices[j]);
                        NodeList_1[i].Children.Add(Node);
                        NodeList.Add(Node);
                        queueListList.Add(queueList);
                    }
                }
            }


            for (int i = 0; i < NodeList.Count; i++)
            {
                for (int j = 0; j < queueListList[i].Count; j++)
                {
                    HotelNode Node = new HotelNode(queueListList[i][j].Hotel.name);
                    NodeList[i].Children.Add(Node);
                }
            }

            return tree;
        }

        private QuestTree getQuestTree(HotelsTree hotelTree)
        {
            QuestTree tree = new QuestTree();

            tree.Root = new QuestNode("START");

            QuestNode node;
            QuestLink link;

            for (int i = 0; i < hotelTree.Root.Children.Count; i++)
            {
                node = new QuestNode("C" + i);

                link = new QuestLink();

                link.KU = "1.0";
                link.Arg = "Страна";
                link.Function = "=";
                link.Link = hotelTree.Root.Children[i].NodeName;
                link.Node = node;

                tree.Root.Links.Add(link);

            }


            QuestNode node_all_c = new QuestNode("ALL");
            link = new QuestLink();

            link.KU = "1.0";
            link.Arg = "Страна";
            link.Function = "=";
            link.Link = "все";
            link.Node = node_all_c;

            tree.Root.Links.Add(link);

            for (int i = 0; i < hotelTree.Root.Children.Count; i++)
            {
                QuestNode cn = tree.Root.Links[i].Node;
                QuestNode node_all = new QuestNode("ALL_" + i);

                for (int j = 0; j < hotelTree.Root.Children[i].Children.Count; j++)
                {

                    node = new QuestNode("R" + i + "_" + j);

                    link = new QuestLink();

                    link.KU = "1.0";
                    link.Arg = "Регион";
                    link.Function = "=";
                    link.Link = hotelTree.Root.Children[i].Children[j].NodeName;
                    link.Node = node;

                    cn.Links.Add(link);

                    Region region = Region.GetRegion(hotelTree.Root.Children[i].NodeName, hotelTree.Root.Children[i].Children[j].NodeName);
                    link = new QuestLink();
                    link.KU = "1.0";
                    link.Arg = "Климат";
                    link.Function = "=";
                    link.Link = II.CurentII.Climate[region];
                    link.Node = node;
                    node_all_c.Links.Add(link);

                    link = new QuestLink();
                    link.KU = "1.0";
                    link.Arg = "Климат";
                    link.Function = "=";
                    link.Link = II.CurentII.Climate[region];
                    link.Node = node;
                    node_all.Links.Add(link);

                }


                link = new QuestLink();

                link.KU = "1.0";
                link.Arg = "Регион";
                link.Link = "все";
                link.Function = "=";
                link.Node = node_all;

                cn.Links.Add(link);


            }


            for (int i = 0; i < hotelTree.Root.Children.Count; i++)
            {

                for (int j = 0; j < hotelTree.Root.Children[i].Children.Count; j++)
                {

                    Region region = Region.GetRegion(hotelTree.Root.Children[i].NodeName, hotelTree.Root.Children[i].Children[j].NodeName);
                    string climate = II.CurentII.Climate[region];
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node;
                    node = new QuestNode("CZ" + i + "_" + j);

                    link = new QuestLink();

                    link.KU = II.CurentII.ClimateHealthKU[climate]["плохое"] + "";
                    link.Arg = "Здоровье";
                    link.Function = "=";
                    link.Link = "плохое";
                    link.Node = node;
                    cn.Links.Add(link);

                    link = new QuestLink();

                    link.KU = II.CurentII.ClimateHealthKU[climate]["ниже среднего"] + "";
                    link.Arg = "Здоровье";
                    link.Function = "=";
                    link.Link = "ниже среднего";
                    link.Node = node;
                    cn.Links.Add(link);

                    link = new QuestLink();

                    link.KU = II.CurentII.ClimateHealthKU[climate]["среднее"] + "";
                    link.Arg = "Здоровье";
                    link.Function = "=";
                    link.Link = "среднее";
                    link.Node = node;
                    cn.Links.Add(link);

                    link = new QuestLink();

                    link.KU = II.CurentII.ClimateHealthKU[climate]["выше среднего"] + "";
                    link.Arg = "Здоровье";
                    link.Function = "=";
                    link.Link = "выше среднего";
                    link.Node = node;
                    cn.Links.Add(link);

                    link = new QuestLink();

                    link.KU = II.CurentII.ClimateHealthKU[climate]["хорошее"] + "";
                    link.Arg = "Здоровье";
                    link.Function = "=";
                    link.Link = "хорошее";
                    link.Node = node;
                    cn.Links.Add(link);



                }

            }

            for (int i = 0; i < hotelTree.Root.Children.Count; i++)
            {

                for (int j = 0; j < hotelTree.Root.Children[i].Children.Count; j++)
                {
                    Region region = Region.GetRegion(hotelTree.Root.Children[i].NodeName, hotelTree.Root.Children[i].Children[j].NodeName);
                    string climate = II.CurentII.Climate[region];

                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node;
                    node = new QuestNode("CV" + i + "_" + j);

                    link = new QuestLink();

                    link.KU = "Климат/Возраст";
                    link.Arg = "Возраст";
                    link.Function = "подходит к";
                    link.Link = climate;
                    link.Node = node;

                    cn.Links.Add(link);

                }

            }

            List<QuestNode> questNodeList = new List<QuestNode>();

            for (int i = 0; i < hotelTree.Root.Children.Count; i++)
            {

                for (int j = 0; j < hotelTree.Root.Children[i].Children.Count; j++)
                {
                    for (int k = 0; k < hotelTree.Root.Children[i].Children[j].Children.Count; k++)
                    {
                        QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node;
                        node = new QuestNode("DIS" + i + "_" + j + "_" + k);

                        link = new QuestLink();

                        link.KU = "1.0";
                        link.Arg = "Назначение";
                        link.Function = "=";
                        link.Link = hotelTree.Root.Children[i].Children[j].Children[k].NodeName;
                        link.Node = node;

                        cn.Links.Add(link);

                        questNodeList.Add(node);
                    }

                }

            }

            List<QuestNode> questNodeList_2 = new List<QuestNode>();

            for (int z = 0; z < questNodeList.Count; z++)
            {
                string[] sbstr = questNodeList[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);

                string dis = hotelTree.Root.Children[i].Children[j].Children[k].NodeName;

                QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node;
                node = new QuestNode("DIZ" + i + "_" + j + "_" + k);

                link = new QuestLink();

                link.KU = II.CurentII.DestinationHealthKU[dis]["плохое"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "плохое";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.DestinationHealthKU[dis]["ниже среднего"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "ниже среднего";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.DestinationHealthKU[dis]["среднее"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "среднее";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.DestinationHealthKU[dis]["выше среднего"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "выше среднего";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.DestinationHealthKU[dis]["хорошее"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "хорошее";
                link.Node = node;
                cn.Links.Add(link);

                questNodeList_2.Add(node);

            }

            questNodeList = new List<QuestNode>();

            for (int z = 0; z < questNodeList_2.Count; z++)
            {
                string[] sbstr = questNodeList_2[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);



                QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node;
                node = new QuestNode("DIV" + i + "_" + j + "_" + k);

                link = new QuestLink();

                link.KU = "Назначение/Возраст";
                link.Arg = "Возраст";
                link.Function = "подходит";
                link.Link = hotelTree.Root.Children[i].Children[j].Children[k].NodeName;
                link.Node = node;

                cn.Links.Add(link);

                questNodeList.Add(node);

            }

            List<QuestNode> questNodeList_1 = new List<QuestNode>();

            for (int z = 0; z < questNodeList.Count; z++)
            {
                string[] sbstr = questNodeList[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);


                for (int l = 0; l < hotelTree.Root.Children[i].Children[j].Children[k].Children.Count; l++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node;
                    node = new QuestNode("LOC" + i + "_" + j + "_" + k + "_" + l);

                    link = new QuestLink();

                    link.KU = "1.0";
                    link.Arg = "Расположение";
                    link.Function = "=";
                    link.Link = hotelTree.Root.Children[i].Children[j].Children[k].Children[l].NodeName;
                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList_1.Add(node);
                }
            }



            questNodeList = new List<QuestNode>();

            for (int z = 0; z < questNodeList_1.Count; z++)
            {
                string[] sbstr = questNodeList_1[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);

                for (int m = 0; m < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children.Count; m++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node;
                    node = new QuestNode("LVL" + i + "_" + j + "_" + k + "_" + l + "_" + m);

                    link = new QuestLink();

                    link.KU = "1.0";
                    link.Arg = "Уровень";
                    link.Function = "=";
                    link.Link = hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].NodeName;
                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList.Add(node);
                }
            }

            questNodeList_1 = new List<QuestNode>();

            for (int z = 0; z < questNodeList.Count; z++)
            {
                string[] sbstr = questNodeList[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);


                for (int n = 0; n < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children.Count; n++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node;
                    node = new QuestNode("TPY" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n);

                    link = new QuestLink();

                    link.KU = "1.0";
                    link.Arg = "Тип";
                    link.Function = "=";
                    link.Link = hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].NodeName;
                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList_1.Add(node);
                }
            }


            questNodeList = new List<QuestNode>();

            for (int z = 0; z < questNodeList_1.Count; z++)
            {
                string[] sbstr = questNodeList_1[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);


                for (int o = 0; o < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children.Count; o++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node;
                    node = new QuestNode("MLS" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n + "_" + o);

                    link = new QuestLink();

                    link.KU = "1.0";
                    link.Arg = "Питание";
                    link.Function = "=";
                    link.Link = hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].NodeName;
                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList.Add(node);
                }
            }

            questNodeList_1 = new List<QuestNode>();

            for (int z = 0; z < questNodeList.Count; z++)
            {
                string[] sbstr = questNodeList[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);
                int o = Int32.Parse(sbstr[6]);


                QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node.Links[o].Node;
                node = new QuestNode("MLZ" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n + "_" + o);

                string meals = hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].NodeName;

                link = new QuestLink();

                link.KU = II.CurentII.MealHealthKU[meals]["плохое"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "плохое";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.MealHealthKU[meals]["ниже среднего"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "ниже среднего";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.MealHealthKU[meals]["среднее"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "среднее";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.MealHealthKU[meals]["выше среднего"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "выше среднего";
                link.Node = node;
                cn.Links.Add(link);

                link = new QuestLink();

                link.KU = II.CurentII.MealHealthKU[meals]["хорошее"] + "";
                link.Arg = "Здоровье";
                link.Function = "=";
                link.Link = "хорошее";
                link.Node = node;
                cn.Links.Add(link);

                questNodeList_1.Add(node);

            }

            questNodeList = new List<QuestNode>();

            for (int z = 0; z < questNodeList_1.Count; z++)
            {
                string[] sbstr = questNodeList_1[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);
                int o = Int32.Parse(sbstr[6]);


                QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node.Links[o].Node.Links[0].Node;
                node = new QuestNode("MLV" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n + "_" + o);

                link = new QuestLink();

                link.KU = "Питание/Возраст";
                link.Arg = "Возраст";
                link.Function = "подходит к";
                link.Link = hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].NodeName;
                link.Node = node;

                cn.Links.Add(link);

                questNodeList.Add(node);

            }

            questNodeList_1 = new List<QuestNode>();

            for (int z = 0; z < questNodeList.Count; z++)
            {
                string[] sbstr = questNodeList[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);
                int o = Int32.Parse(sbstr[6]);


                for (int p = 0; p < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children.Count; p++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node.Links[o].Node.Links[0].Node.Links[0].Node;
                    node = new QuestNode("UHT" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n + "_" + o + "_" + p);

                    link = new QuestLink();
                    link.KU = "1.0";
                    link.Arg = "Услуги в отеле";
                    link.Function = "содержат";
                    link.Link = String.Join(", ", hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].NodeNameList);
                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList_1.Add(node);
                }
            }

            questNodeList = new List<QuestNode>();

            for (int z = 0; z < questNodeList_1.Count; z++)
            {
                string[] sbstr = questNodeList_1[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);
                int o = Int32.Parse(sbstr[6]);
                int p = Int32.Parse(sbstr[7]);


                for (int q = 0; q < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children.Count; q++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node.Links[o].Node.Links[0].Node.Links[0].Node.Links[p].Node;
                    node = new QuestNode("URM" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n + "_" + o + "_" + p + "_" + q);

                    link = new QuestLink();
                    link.KU = "1.0";
                    link.Arg = "Услуги в номере";
                    link.Function = "содержат";
                    link.Link = String.Join(", ", hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children[q].NodeNameList);

                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList.Add(node);
                }
            }

            questNodeList_1 = new List<QuestNode>();

            for (int z = 0; z < questNodeList.Count; z++)
            {
                string[] sbstr = questNodeList[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);
                int o = Int32.Parse(sbstr[6]);
                int p = Int32.Parse(sbstr[7]);
                int q = Int32.Parse(sbstr[8]);


                for (int r = 0; r < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children[q].Children.Count; r++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node.Links[o].Node.Links[0].Node.Links[0].Node.Links[p].Node.Links[q].Node;
                    node = new QuestNode("UCH" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n + "_" + o + "_" + p + "_" + q + "_" + r);

                    link = new QuestLink();
                    link.KU = "1.0";
                    link.Arg = "Услуги для детей";
                    link.Function = "содержат";
                    link.Link = String.Join(", ", hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children[q].Children[r].NodeNameList);

                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList_1.Add(node);
                }
            }

            questNodeList = new List<QuestNode>();

            for (int z = 0; z < questNodeList_1.Count; z++)
            {
                string[] sbstr = questNodeList_1[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);
                int o = Int32.Parse(sbstr[6]);
                int p = Int32.Parse(sbstr[7]);
                int q = Int32.Parse(sbstr[8]);
                int r = Int32.Parse(sbstr[9]);


                for (int s = 0; s < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children[q].Children[r].Children.Count; s++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node.Links[o].Node.Links[0].Node.Links[0].Node.Links[p].Node.Links[q].Node.Links[r].Node;
                    node = new QuestNode("SRM" + i + "_" + j + "_" + k + "_" + l + "_" + m + "_" + n + "_" + o + "_" + p + "_" + q + "_" + r + "_" + s);

                    link = new QuestLink();
                    link.KU = "Сумма";
                    link.Arg = "Сумма";
                    link.Function = "без услуг приблизительно равна";
                    link.Link = hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children[q].Children[r].Children[s].NodeName;

                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList.Add(node);
                }
            }

            questNodeList_1 = new List<QuestNode>();

            for (int z = 0; z < questNodeList.Count; z++)
            {
                string[] sbstr = questNodeList[z].Name.Substring(3).Split('_');
                int i = Int32.Parse(sbstr[0]);
                int j = Int32.Parse(sbstr[1]);
                int k = Int32.Parse(sbstr[2]);
                int l = Int32.Parse(sbstr[3]);
                int m = Int32.Parse(sbstr[4]);
                int n = Int32.Parse(sbstr[5]);
                int o = Int32.Parse(sbstr[6]);
                int p = Int32.Parse(sbstr[7]);
                int q = Int32.Parse(sbstr[8]);
                int r = Int32.Parse(sbstr[9]);
                int s = Int32.Parse(sbstr[10]);


                for (int t = 0; t < hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children[q].Children[r].Children[s].Children.Count; t++)
                {
                    QuestNode cn = tree.Root.Links[i].Node.Links[j].Node.Links[0].Node.Links[0].Node.Links[k].Node.Links[0].Node.Links[0].Node.Links[l].Node.Links[m].Node.Links[n].Node.Links[o].Node.Links[0].Node.Links[0].Node.Links[p].Node.Links[q].Node.Links[r].Node.Links[s].Node;
                    node = new QuestNode(hotelTree.Root.Children[i].Children[j].Children[k].Children[l].Children[m].Children[n].Children[o].Children[p].Children[q].Children[r].Children[s].Children[t].NodeName);

                    link = new QuestLink();
                    link.KU = "1.0";
                    link.Arg = "Отель";
                    link.Function = "является";
                    link.Link = "";

                    link.Node = node;

                    cn.Links.Add(link);

                    questNodeList_1.Add(node);
                }
            }

            return tree;
        }

        private string getBZ(QuestTree questTree)
        {
            return bz(questTree.Root);
        }

        private string bz(QuestNode l)
        {
            string s = "";
            if (l != null && l.Links != null)
            {
                for (int i = 0; i < l.Links.Count; i++)
                {
                    if (!l.Links[i].Used)
                    {

                        s += "\n ЕСЛИ '" + l.Name + "' И '" + l.Links[i].Arg + "'" + l.Links[i].Function + "'" + l.Links[i].Link + "' ТО '" + l.Links[i].Node.Name + "' (КУ = " + l.Links[i].KU + ")";

                        l.Links[i].Used = true;
                        s += bz(l.Links[i].Node);
                    }
                }
            }
            return s;
        }

        internal string Generate(string hotelsFilename)
        {
            //Загружаем отели
            hotels = loadHotels(hotelsFilename);
            //Создаем листы
            initLists(hotels);
            //Строим цепочки отелей
            HotelQueue[] queues = getHotelQueues(hotels);
            //Строим дерево отелей
            HotelsTree hotelTree = getHotelsTree(queues);
            //Строим дерево анкеты
            QuestTree questTree = getQuestTree(hotelTree);
            // Создаем базу знаний
            string bz = getBZ(questTree);
            //System.IO.File.WriteAllText(Environment.CurrentDirectory + "\\bz.txt", bz);
            return bz;
        }

        public Hotel[] Hotels
        {
            get
            {
                return hotels;
            }
        }

        private static string getStringTo(char c, string str)
        {

            string s = "";

            for (int i = 0; i < str.Length && str[i] != c; i++)
            {
                s += str[i];
            }

            return s;

        }

        internal Production[] GetProductions(string bz)
        {
            string[] pr = bz.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            List<Production> productions = new List<Production>();

            for (int i = 0; i < pr.Length; i++)
            {
                Production production = new Production();
                string str = pr[i].Trim();
                str = str.Substring(6);

                production.State = getStringTo('\'', str);
                str = str.Substring(production.State.Length + 5);

                production.Arg1 = getStringTo('\'', str);
                str = str.Substring(production.Arg1.Length + 1);

                production.Function = getStringTo('\'', str);
                str = str.Substring(production.Function.Length + 1);

                production.Arg2 = getStringTo('\'', str);
                str = str.Substring(production.Arg2.Length + 6);

                production.StateResult = getStringTo('\'', str);
                str = str.Substring(production.StateResult.Length + 8);

                production.KU = getStringTo(')', str);

                productions.Add(production);

            }

            return productions.ToArray();

        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          