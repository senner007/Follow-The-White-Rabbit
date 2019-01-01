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
    class Program
    {
        static void Main(string[] args)
        {
            
            // System.Console.WriteLine(  AsciiSumAnagram() );

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var words = ReadCSVFile<WordObject>("c:\\gits7\\wordlist");
            var wordsList = words.ToList();


            Console.WriteLine("Time after generating list excluding impossible: {0}", stopwatch.Elapsed);

            // Create first List
            var firstList = CreateFirstList(wordsList);

            Console.WriteLine("Time after generating list with 2 words: {0}", stopwatch.Elapsed);

            System.Console.WriteLine( "Length of list with 2 words: {0}", firstList.Count  );
     

            // Create final List

            CreateLastList(firstList, wordsList);

            stopwatch.Stop();

            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);  

        }

        static int AsciiSumAnagram () 
        {
            var sum = 0;
            var anagram = "poultryoutwitsants";

            foreach ( var c in anagram) {
                sum += (int)c;
            }

            return sum;
        }

        static void CreateLastList (List<WordObject> firstList, List<WordObject> initialList )
        {
            var counter = 0;

            int asciisumanagram = AsciiSumAnagram();
            
            // create Dictionary ( asciisum , words )
            var dict = CreateDictionary.CreateDict(initialList);

            foreach (var w in firstList) {

                if (!dict.ContainsKey(asciisumanagram - w.AsciiSum)) continue;

                foreach(var ww in dict[asciisumanagram - w.AsciiSum]) {

                    //  if(w.Word.Length + ww.Length != 19) continue;
                  
                    // if (w.AsciiSum + ww.AsciiSum != asciisumanagram) continue;

                    // if(w.HowManyS + ww.HowManyS != 2) continue;
                    // if(w.HowManyO + ww.HowManyO != 2) continue;
                    // if(w.HowManyU + ww.HowManyU != 2) continue;
                    // if(w.HowManyT + ww.HowManyT != 4) continue;
   

                    if(w.Word.Contains('a') && ww.Contains('a')) continue;
                    if(w.Word.Contains('i') && ww.Contains('i')) continue;
                    if(w.Word.Contains('l') && ww.Contains('l')) continue;
                    if(w.Word.Contains('p') && ww.Contains('p')) continue;
                    if(w.Word.Contains('n') && ww.Contains('n')) continue;
                    if(w.Word.Contains('r') && ww.Contains('r')) continue;
                    if(w.Word.Contains('w') && ww.Contains('w')) continue;
                    if(w.Word.Contains('y') && ww.Contains('y')) continue;

                    // if (w.AsciiSum + ww.AsciiSum != asciisumanagram) continue;
                     counter++;  
                    

                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = Hashing.GetMd5Hash(md5Hash, w.Word + " " + ww);


                        if ("e4820b45d2277f3844eac66c903e84be" == hash) {
                            System.Console.WriteLine("found easy string: " + w.Word + " " + ww + " with hash string: 'e4820b45d2277f3844eac66c903e84be'");
                        }

                        if ("23170acc097c24edb98fc5488ab033fe" == hash) {
                            System.Console.WriteLine("found medium string: " + w.Word + " " + ww + " with hash string: '23170acc097c24edb98fc5488ab033fe'");
                        }

                    }

                    
                }
                
            }

            System.Console.WriteLine(  "final candidates hashed: " + counter );
        }

        static List<WordObject> CreateFirstList (List<WordObject> list)
        {
            List<WordObject> newListObject = new List<WordObject>();

            foreach (var w in list) {

                foreach(var ww in list) {

                    if(w.Word.Length + ww.Word.Length < 7) continue;
                    if(w.Word.Length + ww.Word.Length > 16) continue;

                    if(w.HowManyS + ww.HowManyS > 2) continue;
                    if(w.HowManyO + ww.HowManyO > 2) continue;
                    if(w.HowManyU + ww.HowManyU > 2) continue;
                    if(w.HowManyT + ww.HowManyT > 4) continue;

                    if(w.Word.Contains('a') && ww.Word.Contains('a')) continue;
                    if(w.Word.Contains('i') && ww.Word.Contains('i')) continue;
                    if(w.Word.Contains('l') && ww.Word.Contains('l')) continue;
                    if(w.Word.Contains('p') && ww.Word.Contains('p')) continue;
                    if(w.Word.Contains('n') && ww.Word.Contains('n')) continue;
                    if(w.Word.Contains('r') && ww.Word.Contains('r')) continue;
                    if(w.Word.Contains('w') && ww.Word.Contains('w')) continue;
                    if(w.Word.Contains('y') && ww.Word.Contains('y')) continue;

                    newListObject.Add(new WordObject { 
                        Word = w.Word  + " " + ww.Word, 
                        HowManyS = w.HowManyS + ww.HowManyS,
                        HowManyO =  w.HowManyO + ww.HowManyO,
                        HowManyU =  w.HowManyU + ww.HowManyU,
                        HowManyT = w.HowManyT + ww.HowManyT,
                        AsciiSum = w.AsciiSum + ww.AsciiSum
                        });
             
                }
                
            }

            return newListObject;
        }
        private static IEnumerable<WordObject> ReadCSVFile<T>(string path) where T : IWordObject, new()
        {    
            string anagram = "poultry outwits ants";
            var prev = "";

            return File.ReadAllLines(path)
                .Where(line => line.Length > 1)
                .Where(line => !line.Contains("'"))
                .Select(line => {

                    var word = line.ToLower().Trim();

                    // exclude if identical to previous
                    if (word == prev) return new WordObject { Include=false };

                    string anagramCopy = anagram;

                    var s = 0;
                    var o = 0;
                    var u = 0;
                    var t = 0;

                    var asciisum = 0;

                    foreach (var c in word) {

                       asciisum += (int)c;
                        
                        var index = anagramCopy.IndexOf(c);   
                        if (c == 's') s++;
                        if (c == 'o') o++;
                        if (c == 'u') u++;
                        if (c == 't') t++;

                        if (index != -1) {
                            StringBuilder sb = new StringBuilder(anagramCopy);
                            sb.Remove(index, 1);
                            anagramCopy = sb.ToString();
                        } else {
                            return new WordObject { Include=false };
                        }
                    }
                    prev = word;
                
                    return new WordObject { 
                        Word=word, 
                        Remaining=anagramCopy, 
                        Include=true, 
                        HowManyS = s, 
                        HowManyO = o, 
                        HowManyU = u, 
                        HowManyT = t,
                        AsciiSum = asciisum
                        };
                })
                .Where(w => w.Include == true);
  
        }
    }
}
    