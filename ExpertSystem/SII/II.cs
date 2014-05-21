using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExpertSystem.SII
{
    class II
    {

        private static II curentII;

        //Пути до файлов
        //private const string kuFilename = @"C:\Documents and Settings\Администратор\Мои документы\Visual Studio 2010\Projects\ExpertSystem\ExpertSystem\ExpertSystem\bin\Debug\files\ku_tables.xlsx";
        //private const string climatesFilename = @"C:\Documents and Settings\Администратор\Мои документы\Visual Studio 2010\Projects\ExpertSystem\ExpertSystem\ExpertSystem\bin\Debug\files\climate.xlsx";
        //private const string priceFilename = @"C:\Documents and Settings\Администратор\Мои документы\Visual Studio 2010\Projects\ExpertSystem\ExpertSystem\ExpertSystem\bin\Debug\files\price.xlsx";
        private string kuFilename = Environment.CurrentDirectory + @"\files\ku_tables.xlsx";
        private string climatesFilename = Environment.CurrentDirectory + @"\files\climate.xlsx";
        private string priceFilename = Environment.CurrentDirectory + @"\files\price.xlsx";
        private string blacklistFilename = Environment.CurrentDirectory + @"\files\countries_blacklist.csv";

        //КУ
        private Dictionary<string, Dictionary<string, double>> climateHealthKU;
        private Dictionary<string, Dictionary<string, double>> climateAgeKU;
        private Dictionary<string, Dictionary<string, double>> destinationHealthKU;
        private Dictionary<string, Dictionary<string, double>> destinationAgeKU;
        private Dictionary<string, Dictionary<string, double>> destinationInsuranceKU;
        private Dictionary<string, Dictionary<string, double>> mealHealthKU;
        private Dictionary<string, Dictionary<string, double>> mealAgeKU;
        private Dictionary<string, Dictionary<string, double>> insuranceHealthKU;

        //Климат
        private Dictionary<Region, string> climate  = new Dictionary<Region, string>();
        //Страны
        private Dictionary<string, Country> countries = new Dictionary<string, Country>();
        //Регионы
        private Dictionary<string, Dictionary<string, Region>> regions = new Dictionary<string, Dictionary<string, Region>>();
        //Цены
        private Dictionary<string, int> price = new Dictionary<string, int>();
        //черный список
        private List<Country> countriesBlacklist;

        //Продукции
        private Generater generater = new Generater();
        private Finder finder = new Finder();
        private string bz;
        private Production[] productions;
        private Hotel[] hotels;
        
        // Цена услуг
        public int ServiceCost = 0;

        private string[][,] getExcelTables(string xslFilename)
        {
            string[][,] tables = null;

            Excel.Application excel = new Excel.Application();
            Excel.Workbook workbook = excel.Workbooks.Open(xslFilename);

            int sheetsCount = workbook.Sheets.Count;
            tables = new string[sheetsCount][,];

            for (int sheetNum = 1; sheetNum <= sheetsCount; sheetNum++)
            {
                Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets.get_Item(sheetNum);
                Excel.Range range = worksheet.UsedRange;

                int rowsCount = range.Rows.Count;
                int columnsCount = range.Columns.Count;
                tables[sheetNum-1] = new string[rowsCount, columnsCount];

                for (int rowNum = 1; rowNum <= rowsCount; rowNum++)
                {
                    for (int columnNum = 1; columnNum <= columnsCount; columnNum++)
                    {
                        tables[sheetNum - 1][rowNum - 1, columnNum - 1] = Convert.ToString(worksheet.Cells[rowNum, columnNum].Value);
                    }
                }

            }

            workbook.Close(true);
            excel.Quit();

            return tables;

        }

        private Dictionary<string, Dictionary<string, double>> getKUFromTable(string[,] table)
        {

            Dictionary<string, Dictionary<string, double>> kuTable = new Dictionary<string, Dictionary<string, double>>();

            for (int rowNum = 1; rowNum < table.GetLength(0); rowNum++)
            {

                string rowName = table[rowNum, 0];
                
                kuTable[rowName] = new Dictionary<string, double>();

                for (int columnNum = 1; columnNum < table.GetLength(1); columnNum++)
                {
                    string columnName = table[0, columnNum];

                    double ku = Double.Parse(table[rowNum, columnNum].Replace(".", ","));
                    kuTable[rowName][columnName] = ku;
                }
            }

            return kuTable;

        }

        private void initKU(string kuFilename)
        {
            //Загружаем и парсим файл экселя
            string[][,] tables = getExcelTables(kuFilename);
            //Инициализируем таблицы КУ
            climateHealthKU = getKUFromTable(tables[0]);
            climateAgeKU = getKUFromTable(tables[1]);
            destinationHealthKU = getKUFromTable(tables[2]);
            destinationAgeKU = getKUFromTable(tables[3]);
            destinationInsuranceKU = getKUFromTable(tables[4]);
            mealHealthKU = getKUFromTable(tables[5]);
            mealAgeKU = getKUFromTable(tables[6]);
            insuranceHealthKU = getKUFromTable(tables[7]);

        }


        /// <summary>
        /// Загружает список стран, в которые невозможен въезд без страховки
        /// </summary>
        /// <returns></returns>
        private List<Country> LoadCountriesBlacklist()
        {
            List<Country> blacklist = new List<Country>();

            string line;
            System.IO.StreamReader file = null;
            try
            {
                file = new System.IO.StreamReader(blacklistFilename,Encoding.GetEncoding("windows-1251"),true);
                file.ReadLine(); // пропускаем заголовок
                while ((line = file.ReadLine()) != null)
                {
                    blacklist.Add(Country.GetCountry(line));
                }
            }
            catch (IOException) { }
            finally
            {
                if (file!=null)
                    file.Close();
            }

            return blacklist;
        }

        private Dictionary<Region, string> getClimateFromTable(string[,] table)
        {

            Dictionary<Region, string> climate = new Dictionary<Region, string>();

            for (int rowNum = 0; rowNum < table.GetLength(0); rowNum++)
            {
                Region region = Region.GetRegion(table[rowNum, 0], table[rowNum, 1]);
                climate[region] = table[rowNum, 2];
            }

            return climate;

        }

        private void initClimates(string climatesFilename)
        {
            //Загружаем и парсим файл экселя
            string[][,] tables = getExcelTables(climatesFilename);
            climate = getClimateFromTable(tables[0]);

        }

        private Dictionary<string, int> getPriceFromTable(string[,] table)
        {
            Dictionary<string, int> price = new Dictionary<string, int>();

            for (int rowNum = 0; rowNum < table.GetLength(0); rowNum++)
            {
                price[table[rowNum, 0]] = Int32.Parse(table[rowNum, 1]);
            }

            return price;
        }

        private void initPrice(string priceFilename)
        {
            //Загружаем и парсим файл экселя
            string[][,] tables = getExcelTables(priceFilename);
            price = getPriceFromTable(tables[0]);
        }

        internal void Init()
        {
            curentII = this;
            //Загрузка и парсинг файла КУ
            initKU(kuFilename);
            //Загрузка и парсинг файла с климатами
            initClimates(climatesFilename);
            //Загрузка и парсинг файла с ценами
            initPrice(priceFilename);

            countriesBlacklist = LoadCountriesBlacklist();
        }

        public static II CurentII
        {
            get
            {
                return curentII;
            }
        }
        
        public Dictionary<Region, string> Climate
        {
            get
            {
                return climate;
            }
        }

        public Dictionary<string, Country> Countries
        {
            get
            {
                return countries;
            }
        }

        public Dictionary<string, Dictionary<string, Region>> Regions
        {
            get
            {
                return regions;
            }
        }

        public List<Country> CountriesBlacklist
        {
            get
            {
                return countriesBlacklist;
            }
        }


        internal void GenerateBZ(string hotelsFilename)
        {
            bz = generater.Generate(hotelsFilename);
            productions = generater.GetProductions(bz);
            hotels = generater.Hotels;
        }

        public Hotel[] Hotels
        {
            get
            {
                return hotels;
            }
        }

        public Dictionary<string, Dictionary<string, double>> ClimateHealthKU
        {
            get
            {
                return climateHealthKU;
            }
        }

        public Dictionary<string, Dictionary<string, double>> ClimateAgeKU
        {
            get
            {
                return climateAgeKU;
            }
        }

        public Dictionary<string, Dictionary<string, double>> DestinationHealthKU
        {
            get
            {
                return destinationHealthKU;
            }
        }

        public Dictionary<string, Dictionary<string, double>> DestinationAgeKU
        {
            get
            {
                return destinationAgeKU;
            }
        }

        public Dictionary<string, Dictionary<string, double>> DestinationInsuranceKU
        {
            get
            {
                return destinationInsuranceKU;
            }
        }

        public Dictionary<string, Dictionary<string, double>> MealHealthKU
        {
            get
            {
                return mealHealthKU;
            }
        }

        public Dictionary<string, Dictionary<string, double>> MealAgeKU
        {
            get
            {
                return mealAgeKU;
            }
        }

        public Dictionary<string, Dictionary<string, double>> InsuranceHealthKU
        {
            get
            {
                return insuranceHealthKU;
            }
        }

        internal HotelFindResult[] Find(Questionnaire questionnaire)
        {
            ServiceCost = 0;
            for (int i = 0; i < questionnaire.childService.Length; i++)
            {
                ServiceCost += this.price[questionnaire.childService[i]];
            }
            
            for (int i = 0; i < questionnaire.roomServices.Length; i++)
            {
                ServiceCost += this.price[questionnaire.roomServices[i]];
            }
            
            for (int i = 0; i < questionnaire.hotelServices.Length; i++)
            {
                ServiceCost += this.price[questionnaire.hotelServices[i]];
            }

            return finder.Find(questionnaire, productions);
        }

        public string BZ
        {
            get
            {
                return bz;
            }

            set {
                bz = value;
                productions = generater.GetProductions(bz);
            }
        }
    }
}
