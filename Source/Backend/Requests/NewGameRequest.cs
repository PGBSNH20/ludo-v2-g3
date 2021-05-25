using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Requests
{
    public class NewGameRequest
    {
        public string SessionName { get; set; }
        public string PlayerOne { get; set; }
        public string PlayerTwo { get; set; }
        public string PlayerThree { get; set; }
        public string PlayerFour { get; set; }
    }
}