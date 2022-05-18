using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace ConsoleApp1
{
    class Statistics:IComparable
    {
        public string user_login { get; set; }
        public string theam_name { get; set; }
        public int score { get; set; }
        public double time { get; set; }
        public Statistics(string login,string theam_name, int score, double time)
        {
            this.user_login = login;
            this.theam_name = theam_name;
            this.score = score;
            this.time = time;
        }
        public void Add_Stat()
        {
            JsonSerializer Serializer = new JsonSerializer();

            List<Statistics> Stat_List = new List<Statistics>();

            if (new FileInfo(IFiled.user_path + "\\" + this.user_login + "\\RecordList.json").Length != 0)
            {
                using (StreamReader reader = new StreamReader(IFiled.user_path + "\\" + user_login + "\\RecordList.json", false))
                {
                    Stat_List = JsonConvert.DeserializeObject<List<Statistics>>(reader.ReadLine());
                }
            }
            Stat_List.Add(this);
            using (StreamWriter writer = new StreamWriter(IFiled.user_path + "\\" + this.user_login + "\\RecordList.json", false))
            {
                Serializer.Serialize(writer, Stat_List);
            }
        }
        public static List<Statistics> Users_Record_list(string user_login)
        {
            if (new FileInfo(IFiled.user_path + "\\" + user_login + "\\RecordList.json").Length == 0)
            {
                return new List<Statistics>();
            }
            using (StreamReader reader = new StreamReader(IFiled.user_path + "\\" + user_login + "\\RecordList.json",false))
            {
                return JsonConvert.DeserializeObject<List<Statistics>>(reader.ReadLine());
            }
        }
        public static List<Statistics> Top20_Record_list(string theam_name)
        {
            List<string> list_of_users = new List<string>();
            List<Statistics> list_of_stats = new List<Statistics>();
            list_of_users = Directory.GetDirectories(IFiled.user_path).Select((i) => i = i.Split("\\")[2]).ToList();
            foreach (var item in list_of_users)
            {
                list_of_stats.AddRange(Statistics.Users_Record_list(item));
            }
            list_of_stats = (List<Statistics>)(from i in list_of_stats
                                               where i.theam_name == theam_name
                                               orderby i.score
                                               orderby i.time
                                               select i).ToList();
            //list_of_stats.Where((i) => i.theam_name == theam_name).ToList();
            list_of_stats.Sort();
            if (list_of_stats.Count >= 20)
                return list_of_stats.GetRange(0, 20);
            return list_of_stats;
        }
        public override string ToString()
        {
            return new string($"\nИгрок: {this.user_login}\nТема: {this.theam_name}\nСчет: {this.score}\nВремя: {this.time} sec");
        }

        public int CompareTo(object obj)
        {
            if (this == (obj as Statistics))
            {
                return 0;
            }
            else if(this > (obj as Statistics))
            {
                return 1;
            }
            return -1;
        }

        public static bool operator >(Statistics a, Statistics b)
        {
            return a.time > b.time && a.score > b.score;
        }
        public static bool operator <(Statistics a, Statistics b)
        {
            return a.time < b.time && a.score < b.score;
        }
        public static bool operator ==(Statistics a,Statistics b)
        {
            if(a.user_login == b.user_login && a.score == b.score && a.time == b.time)
            {
                return true;
            }
                return false;
        }
        public static bool operator !=(Statistics a,Statistics b)
        {
            if(a.user_login == b.user_login && a.score == b.score && a.time == b.time)
            {
                return false;
            }
            return true;
        }
    }
}
