using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpertSystem.SII
{
    class Finder
    {

        public string SystemMessage { get; private set; }

        private List<HotelFindResult> hotelFindResults;

        public Finder()
        {
            SystemMessage = "";
        }

        private bool contain(string p, string[] p_2)
        {
            string[] res = p.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            bool flag = true;

            for (int i = 0; i < p_2.Length; i++)
            {
                bool local_flag = false;
                for (int j = 0; j < res.Length; j++)
                {
                    if (res[j].Equals(p_2[i]))
                    {
                        local_flag = true;
                        break;
                    }
                }
                if (!local_flag)
                {
                    flag = false;
                    break;
                }
            }

            return flag;

        }


        private void f(string State, Questionnaire questionnaire, Production[] productions, double ku, List<Production> listProduction)
        {
            
            if (ku != 0)
            {
                for (int prodNum = 0; prodNum < productions.Length; prodNum++)
                {
                    if (State.Equals(productions[prodNum].State))
                    {
                        switch (productions[prodNum].Arg1)
                        {
                            case "Страна":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.country))
                                {
                                    continue;
                                }
                                break;
                            case "Регион":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.region))
                                {
                                    continue;
                                }
                                break;
                            case "Климат":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.climate))
                                {
                                    continue;
                                }

                                break;
                            case "Здоровье":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.health))
                                {
                                    continue;
                                }
                                else
                                {
                                    ku *= Double.Parse(productions[prodNum].KU);
                                }
                                break;
                            case "Возраст":
                                /*
                                int ost = questionnaire.age % 10 < 5 ? 0 : 5;
                                int l1 = (questionnaire.age / 10) * 10 + ost;
                                int l2 = (questionnaire.age / 10) * 10 + ost + 5;
                                double d = ((questionnaire.age % 10)- ost ) / 5.0;
                                double ku1 = 0, ku2 = 0;
                                switch (productions[prodNum].KU)
                                {
                                    case "Назначение/Возраст":
                                        ku1 = II.CurentII.DestinationAgeKU[productions[prodNum].Arg2][l1 + ""];
                                        ku2 = II.CurentII.DestinationAgeKU[productions[prodNum].Arg2][l2 + ""];
                                        break;
                                    case "Питание/Возраст":
                                        ku1 = II.CurentII.MealAgeKU[productions[prodNum].Arg2][l1 + ""];
                                        ku2 = II.CurentII.MealAgeKU[productions[prodNum].Arg2][l2 + ""];
                                        break;
                                    case "Климат/Возраст":
                                        ku1 = II.CurentII.ClimateAgeKU[productions[prodNum].Arg2][l1 + ""];
                                        ku2 = II.CurentII.ClimateAgeKU[productions[prodNum].Arg2][l2 + ""];
                                        break;
                                }

                                ku *= ku1 + (ku2-ku1) * d;*/
                                double ku1 = 0.0, ku2=0.0;
                                int l1=0, l2=0;
                                switch (productions[prodNum].KU)
                                {
                                    case "Назначение/Возраст":
                                        l1 = int.Parse(II.CurentII.DestinationAgeKU[productions[prodNum].Arg2].Keys.Last(p => int.Parse(p) <= questionnaire.age));
                                        l2 = int.Parse(II.CurentII.DestinationAgeKU[productions[prodNum].Arg2].Keys.First(p => int.Parse(p) > questionnaire.age));
                                        ku1 = II.CurentII.DestinationAgeKU[productions[prodNum].Arg2][l1 + ""];
                                        ku2 = II.CurentII.DestinationAgeKU[productions[prodNum].Arg2][l2 + ""];
                                        break;
                                    case "Питание/Возраст":
                                        l1 = int.Parse(II.CurentII.MealAgeKU[productions[prodNum].Arg2].Keys.Last(p => int.Parse(p) <= questionnaire.age));
                                        l2 = int.Parse(II.CurentII.MealAgeKU[productions[prodNum].Arg2].Keys.First(p => int.Parse(p) > questionnaire.age));
                                        ku1 = II.CurentII.MealAgeKU[productions[prodNum].Arg2][l1 + ""];
                                        ku2 = II.CurentII.MealAgeKU[productions[prodNum].Arg2][l2 + ""];
                                        break;
                                    case "Климат/Возраст":
                                        l1 = int.Parse(II.CurentII.ClimateAgeKU[productions[prodNum].Arg2].Keys.Last(p => int.Parse(p) <= questionnaire.age));
                                        l2 = int.Parse(II.CurentII.ClimateAgeKU[productions[prodNum].Arg2].Keys.First(p => int.Parse(p) > questionnaire.age));
                                        ku1 = II.CurentII.ClimateAgeKU[productions[prodNum].Arg2][l1 + ""];
                                        ku2 = II.CurentII.ClimateAgeKU[productions[prodNum].Arg2][l2 + ""];
                                        break;
                                }
                                ku *= (ku2 - ku1) * (questionnaire.age - l1) / (l2 - l1) + ku1;
                                break;
                            case "Назначение":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.destination))
                                {
                                    continue;
                                }
                                break;
                            case "Страховка":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.insurance))
                                {
                                    continue;
                                }
                                else
                                {
                                    ku *= Double.Parse(productions[prodNum].KU);
                                }
                                break;
                            case "Расположение":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.location))
                                {
                                    continue;
                                }
                                break;
                            case "Уровень":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.level))
                                {
                                    continue;
                                }
                                break;
                            case "Тип":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.type))
                                {
                                    continue;
                                }
                                break;
                            case "Питание":
                                if (!productions[prodNum].Arg2.Equals(questionnaire.meals))
                                {
                                    continue;
                                }
                                break;
                            case "Услуги в номере":
                                if (!contain(productions[prodNum].Arg2, questionnaire.roomServices))
                                {
                                    continue;
                                }
                                break;
                            case "Услуги в отеле":
                                if (!contain(productions[prodNum].Arg2, questionnaire.hotelServices))
                                {
                                    continue;
                                }
                                break;
                            case "Услуги для детей":
                                if (!contain(productions[prodNum].Arg2, questionnaire.childService))
                                {
                                    continue;
                                }
                                break;
                            case "Сумма":
                                int price_sum = (int)((Int32.Parse(productions[prodNum].Arg2) + II.CurentII.ServiceCost));
                                int client_sum = (int)(questionnaire.price * 1.2/questionnaire.holidaysLength);
                                double ku_pr;
                                if (price_sum <= client_sum)
                                {
                                    ku_pr = 1.0;
                                }
                                else if (price_sum >= (int)(client_sum * 1.25))
                                {
                                    ku_pr = 0.0;
                                }
                                else
                                {
                                    ku_pr = (client_sum - price_sum) / (questionnaire.price * 0.3/questionnaire.holidaysLength) + 1;
                                }
                                ku *= ku_pr;
                                break;
                            case "Отель":
                                string hotelname = productions[prodNum].StateResult;
                                ku *= PostProcessResult(hotelname, questionnaire);
                                if (ku == 0)
                                    continue;
                                Console.WriteLine(hotelname + " " + ku);
                                HotelFindResult hotelFindResult = new HotelFindResult();
                                Hotel hotel = new Hotel();
                                hotel.name = hotelname;
                                hotelFindResult.Hotel = hotel;
                                hotelFindResult.KU = ku;
                                hotelFindResult.Productions = listProduction.ToArray();
                                hotelFindResults.Add(hotelFindResult);

                                continue;
                        }
                        List<Production> listProductionNew = new List<Production>();
                        for (int i = 0; i < listProduction.Count; i++)
                        {
                            listProductionNew.Add(listProduction[i]);
                        }
                        listProductionNew.Add(productions[prodNum]);
                        f(productions[prodNum].StateResult, questionnaire, productions, ku, listProductionNew);

                    }
                }
            }
        }

        /// <summary>
        /// Так как страховка не входит в понятие отель, данная функция
        /// выставляет коэффициент уверенности по страховке
        /// для входящих в отель параметров
        /// </summary>
        /// <param name="hotelname">имя отеля</param>
        /// <param name="userDefines">анкета</param>
        /// <param name="arg1">параметр проверки</param>
        /// <param name="arg2">значение параметра проверки</param>
        /// <returns>КУ, выставленное по параметру</returns>
        private double AddInsuranceKU(string hotelname,Questionnaire userDefines, string arg1,string arg2)
        {
            switch (arg1){
                case "Здоровье":
                    double ku = II.CurentII.InsuranceHealthKU[userDefines.insurance][arg2];
                    if (userDefines.insurance == Values.InsuranceNo && ku<1)
                        SystemMessage += "Указанное здоровье предполагает страховку. "+
                        "Страховка не включена, уверенность в отеле " + hotelname + " снижена, КУ:" + ku + "\n";
                    return ku;
            }
            return 1;
        }

        /// <summary>
        /// Просматривает отель на предмет соответствия введенным данным.
        /// </summary>
        /// <param name="hotelname">имя отеля</param>
        /// <param name="userDefines">анкета</param>
        /// <returns>добавочный КУ отеля после проверки</returns>
        private double PostProcessResult(string hotelname, Questionnaire userDefines)
        {
            double value = 1;
            bool hasInsurance = userDefines.insurance == Values.InsuranceYes; 
            // проверка на страну, где обязательно наличие страховки
            if (!hasInsurance)
            {
                value = 0;
                Hotel current = findHotel(hotelname);
                if (current != null)
                {
                    string cname = current.region.Country.Name;
                    IEnumerable<Country> inBlacklistQuery =
                        from c in II.CurentII.CountriesBlacklist
                        where c.Name == cname
                        select c;
                    List<Country> inBlacklistQueryList = inBlacklistQuery.ToList();
                    if (inBlacklistQuery == null)
                        value = 1;
                    else if (inBlacklistQuery.Count() > 0)
                    {
                        SystemMessage += "Страна, в которой находится отель \"" + hotelname +
                            "\" - " + cname + ", предполагает обязательную страховку, " +
                            "поэтому он был исключен из найденных\n";
                    } else
                        value = 1;
                }
            }
            // добавление КУ по здоровью
            value *= AddInsuranceKU(hotelname, userDefines, "Здоровье", userDefines.health);
            return value;
        }


        private Hotel findHotel(string hotel)
        {
            
            for (int i = 0; i < II.CurentII.Hotels.Length; i++)
            {
                if (II.CurentII.Hotels[i].name.Equals(hotel)) {
                    return II.CurentII.Hotels[i];
                }
            }
                return null;
        }

        internal HotelFindResult[] Find(Questionnaire questionnaire, Production[] productions)
        {
            SystemMessage = "";
            hotelFindResults = new List<HotelFindResult>();
            List<Production> listProduction = new List<Production>();

            f("START", questionnaire, productions, 1.0, listProduction);

            return hotelFindResults.ToArray();
        }
    }
}
