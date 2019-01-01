using System;
using System.Collections.Generic;

namespace ParseCSV
{
    public interface IWordObject
    {
       string Word { get; set; }
       string Remaining { get; set; }
       bool Include { get; set; }

       int HowManyS { get; set; }
       int HowManyT { get; set; }
       int HowManyO { get; set; }
       int HowManyU { get; set; }

       int AsciiSum { get; set; }



    }
    public class WordObject : IWordObject
    {
        public string Word { get; set; }
        public string Remaining { get; set; }
        public bool Include { get; set; }
        public int HowManyS { get; set; }
        public int HowManyT { get; set; }
        public int HowManyO { get; set; }
        public int HowManyU { get; set; }

        public int AsciiSum { get; set; }


    }

}