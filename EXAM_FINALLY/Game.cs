using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections;
namespace ConsoleApp1
{
    class Game
    {
        User user;
        public Game(User user)
        {
            this.user = user;
        }
        public void game_start(string theam_name)
        {
            DateTime timestart = DateTime.Now;
            int score = default;
            List<Question> questions = Question.Get_Guests(theam_name);

            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine(i);
                if (questions[i].asking())
                    score++;
            }
            DateTime timeend = DateTime.Now;
            Statistics @new = new Statistics(this.user.login,theam_name, score, (Convert.ToInt64(timeend.Second) - Convert.ToInt64(timestart.Second)) /100);
            @new.Add_Stat();
        }
    }
}
