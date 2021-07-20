using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace RegexFileReplacer
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var regex = new Regex(@"(?<=\buse\s+)(?:(?'db1'pattern1)|(?'db2'pattern2))\b", RegexOptions.IgnoreCase);
                foreach (var file in args)
                {
                    if (!File.Exists(file))
                    {
                        Console.Error.WriteLine($"file {file} does not exist.");
                    }

                    var text = File.ReadAllText(file);
                    var result = regex.Replace(text, match => match.Groups["db1"].Success ? "NORMAL" : "TIMESERIES");
                    var outputFilename = Path.ChangeExtension(file, ".out");
                    File.WriteAllText(outputFilename, result);
                }
            }
            catch (Exception ex)
            {
                var fullname = System.Reflection.Assembly.GetEntryAssembly().Location;
                var progname = Path.GetFileNameWithoutExtension(fullname);
                Console.Error.WriteLine($"{progname} Error: {ex.Message}");
            }

        }
    }
}
