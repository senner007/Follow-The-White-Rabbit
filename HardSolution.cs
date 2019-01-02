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

    public class Hard
    {

        public static void Solution(List<WordObject> wordsList)
        {

            var asciisumanagram = AsciiSumAnagram();

            // var words = ReadCSVFile<WordObject>("c:\\gits7\\wordlist");

            // var wordsList = words.ToList();

            List<WordObject> newListObject = new List<WordObject>();

            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();

            // Begin timing.
            stopwatch.Start();

            foreach (var w in wordsList) {

                foreach(var ww in wordsList) {
                    
                    if((w.Word + " " + ww.Word).Length > 15) continue;

                    if(w.HowManyS + ww.HowManyS > 2) continue;
                    if(w.HowManyO + ww.HowManyO > 2) continue;
                    if(w.HowManyU + ww.HowManyU > 2) continue;
                    if(w.HowManyT + ww.HowManyT > 4) continue;

                    if(w.Word.Contains('a') && ww.Word.Contains('a')) continue;
                    if(w.Word.Contains('n') && ww.Word.Contains('n')) continue;
                    if(w.Word.Contains('i') && ww.Word.Contains('i')) continue;
                    if(w.Word.Contains('l') && ww.Word.Contains('l')) continue;
                    if(w.Word.Contains('p') && ww.Word.Contains('p')) continue;
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
                // Stop timing.
            stopwatch.Stop();

            var dict = CreateDictionary.CreateDict(wordsList);

            // Write result.
            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            System.Console.WriteLine( newListObject.Count  );

            var newListObject2 = new List<WordObject>();

            stopwatch.Start();

            foreach (var w in newListObject) {

                foreach(var ww in wordsList) {

                    if (!dict.ContainsKey(asciisumanagram - (w.AsciiSum + ww.AsciiSum))) continue;

                    if((w.Word + " " + ww.Word).Length < 9) continue;
                    if((w.Word + " " + ww.Word).Length > 18) continue;

                    if(w.HowManyS + ww.HowManyS > 2) continue;
                    if(w.HowManyO + ww.HowManyO > 2) continue;
                    if(w.HowManyU + ww.HowManyU > 2) continue;
                    if(w.HowManyT + ww.HowManyT > 4) continue;

                    if(w.Word.Contains('a') && ww.Word.Contains('a')) continue;
                    if(w.Word.Contains('n') && ww.Word.Contains('n')) continue;
                    if(w.Word.Contains('i') && ww.Word.Contains('i')) continue;
                    if(w.Word.Contains('l') && ww.Word.Contains('l')) continue;
                    if(w.Word.Contains('p') && ww.Word.Contains('p')) continue;
                    if(w.Word.Contains('r') && ww.Word.Contains('r')) continue;
                    if(w.Word.Contains('w') && ww.Word.Contains('w')) continue;
                    if(w.Word.Contains('y') && ww.Word.Contains('y')) continue;

                    

                    newListObject2.Add(new WordObject { 
                            Word = w.Word  + " " + ww.Word, 
                            HowManyS = w.HowManyS + ww.HowManyS,
                            HowManyO =  w.HowManyO + ww.HowManyO,
                            HowManyU =  w.HowManyU + ww.HowManyU,
                            HowManyT = w.HowManyT + ww.HowManyT,
                            AsciiSum = w.AsciiSum + ww.AsciiSum
                        });
        
                    
                }
                
            }
             stopwatch.Stop();

            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);  


            System.Console.WriteLine(   newListObject2.Count);

            stopwatch.Start();

            var newListObject3 = new List<WordObject>();



            foreach (var w in newListObject2) {

          

                //  if (!dict.ContainsKey(asciisumanagram - w.AsciiSum)) continue;

                foreach(var ww in dict[asciisumanagram - w.AsciiSum]) {


                    // if(w.Word.Length + ww.Length != 21) continue;


                    // if(w.ContainsS + ww.ContainsS != 2) continue;
                    // if(w.ContainsO + ww.ContainsO != 2) continue;
                    // if(w.ContainsU + ww.ContainsU != 2) continue;
                    // if(w.ContainsT + ww.ContainsT != 4) continue;
   

                    if(w.Word.Contains('a') && ww.Contains('a')) continue;
                    if(w.Word.Contains('n') && ww.Contains('n')) continue;
                    if(w.Word.Contains('i') && ww.Contains('i')) continue;
                    if(w.Word.Contains('l') && ww.Contains('l')) continue;
                    if(w.Word.Contains('p') && ww.Contains('p')) continue;
                    if(w.Word.Contains('r') && ww.Contains('r')) continue;
                    if(w.Word.Contains('w') && ww.Contains('w')) continue;
                    if(w.Word.Contains('y') && ww.Contains('y')) continue;

                  
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string hash = Hashing.GetMd5Hash(md5Hash, w.Word + " " + ww);

                         if ("665e5bcb0c20062fe8abaaf4628bb154" == hash) {
                            System.Console.WriteLine("found hard string: " + w.Word + " " + ww + " with hash string: '665e5bcb0c20062fe8abaaf4628bb154'");
                            // breakFromLoop = true;
                            stopwatch.Stop();

                            Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
                            System.Environment.Exit(0);
                         }
                    }
                    
                }
                
            }
            


        }

         static int AsciiSumAnagram () {
            var sum = 0;
            var anagram = "poultryoutwitsants";

            foreach ( var c in anagram) {
                sum += (int)c;
            }

            return sum;

        }

        // private static IEnumerable<WordObject> ReadCSVFile<T>(string path) where T : IWordObject, new()
        // {    
        //     string anagram = "poultry outwits ants";
        //     // int counter = 0;
        //     var prev = "";

        //     return File.ReadAllLines(path)
        //         .Where(line => line.Length > 1)
        //         .Where(line => !line.Contains("'"))
        //         .Select(line => {

        //             var word = line.ToLower().Trim();

        //             if (word == prev) return new WordObject { Include=false };
        //             // System.Console.WriteLine(  counter++ );
        //             // System.Console.WriteLine(  line );
        //             string anagramCopy = anagram;

        //             var s = 0;
        //             var o = 0;
        //             var u = 0;
        //             var t = 0;

        //             var asciisum = 0;

        //             foreach (var c in word) {

        //                 asciisum += (int)c;
                        
        //                 var index = anagramCopy.IndexOf(c);   
        //                 if (c == 's') s++;
        //                 if (c == 'o') o++;
        //                 if (c == 'u') u++;
        //                 if (c == 't') t++;

        //                 if (index != -1) {
        //                     StringBuilder sb = new StringBuilder(anagramCopy);
        //                     sb.Remove(index, 1);
        //                     anagramCopy = sb.ToString();
        //                 } else {
                            
        //                     return new WordObject { Include=false };
        //                 }
        //             }
        //             prev = word;
                
        //             return new WordObject { 
        //                 Word = word, 
        //                 Remaining = anagramCopy, 
        //                 Include = true, 
        //                 HowManyS = s, 
        //                 HowManyO = o, 
        //                 HowManyU = u, 
        //                 HowManyT = t,
        //                 AsciiSum = asciisum
        //                 };
        //         })
        //         .Where(w => w.Include == true);

        // }
    }
}
    