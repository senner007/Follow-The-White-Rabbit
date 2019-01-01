using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Collections;

namespace ParseCSV
{
    public class CreateDictionary
    {
        public static IDictionary<int, List<string>> CreateDict(List<WordObject> list)
        {
        
            IDictionary<int, List<string>> dict = new Dictionary<int,  List<string>>();

            foreach ( var ws in list) {
                
                if (dict.ContainsKey(ws.AsciiSum)) { 
                    dict[ws.AsciiSum].Add(ws.Word);
                 } else {
                     dict.Add(ws.AsciiSum, new List<string>{ws.Word});
                 }
            }

            return dict;

        }   
    }
}
    