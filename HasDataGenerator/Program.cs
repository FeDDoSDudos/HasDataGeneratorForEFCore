using System;

namespace HasDataGenerator
{
    public class Program
    {
        static void Main(string[] args)
        {
            string dataConnString = @"C:\Users\frolo\source\repos\HasDataGenerator\HasDataGenerator\data.txt";
            string dataRsultConnString = @"C:\Users\frolo\source\repos\HasDataGenerator\HasDataGenerator\result.txt";

            string[] dataInLines = File.ReadAllLines(dataConnString);
            
            Console.WriteLine("Ready");
            string classString = Console.ReadLine();

            string[] propName = dataInLines[0].Split('\t');

            List<string> resList = new List<string>();

            for (int i = 1; i < dataInLines.Length; i++)
            {
                string[] dataLine = dataInLines[i].Split('\t');
                string res = "new " + classString + "{";
                for (int j = 0; j < dataLine.Length; j++)
                {
                    {
                        if (long.TryParse(dataLine[j], out long numericValue1))
                        {
                            if (dataLine[j] == "0" && (propName[j] == "IsPrimary" || propName[j] == "IsMultiSystem" 
                                || propName[j] == "IsFractional" || propName[j] == "IsOccasion" || propName[j] == "IsMiddleNotSumm"
                                || propName[j] == "IsIncrescent" || propName[j] == "IsLessValueIsBetter" || propName[j] == "IsOnceAMonth"
                                || propName[j] == "UsedForCharts" || propName[j] == "UsedForRating" || propName[j] == "UsedForOperationalMonitoring"))
                                res += propName[j] + " = false";
                            else if (dataLine[j] == "1" && (propName[j] == "IsPrimary" || propName[j] == "IsMultiSystem"
                                || propName[j] == "IsFractional" || propName[j] == "IsOccasion" || propName[j] == "IsMiddleNotSumm"
                                || propName[j] == "IsIncrescent" || propName[j] == "IsLessValueIsBetter" || propName[j] == "IsOnceAMonth"
                                || propName[j] == "UsedForCharts" || propName[j] == "UsedForRating" || propName[j] == "UsedForOperationalMonitoring"))
                                res += propName[j] + " = true";
                            else if (propName[j] == "CodeNum")
                                res += propName[j] + " = (IndicatorNameEn)" + dataLine[j];
                            else
                                res += propName[j] + " = " + dataLine[j];   
                        } 
                        else if (DateTime.TryParse(dataLine[j], out DateTime numericValue2) && propName[j] == "AlgorithmStartTime")
                        {
                            res += propName[j] + " = DateTime.Parse(\"" + dataLine[j] + "\").ToUniversalTime()";
                        }
                        else
                            res += propName[j] + " = \"" + dataLine[j] + "\"";
                    }
                    if (j + 1 != dataLine.Length)
                        res += ", ";
                }
                res += "},";
                resList.Add(res);
            }

            File.WriteAllLines(dataRsultConnString, resList);

            foreach (string res in resList)
            {
                Console.WriteLine(res);
            }
        }
    }
}