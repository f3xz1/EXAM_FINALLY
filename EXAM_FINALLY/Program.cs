using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
namespace ConsoleApp1
{
    class player
    {
        static void Main(string[] args)
        {
            Directory.CreateDirectory("game");
            Directory.CreateDirectory("game\\users");
            Directory.CreateDirectory("game\\questions");
            User user = null;
            while (user == null || user.login == string.Empty && user.password == string.Empty)
            {
                user = Menu.log_menu(Menu.choice_unlog());
            }
            while (true)
            {
                Menu.player_menu(user);
            }
        }
    }
}