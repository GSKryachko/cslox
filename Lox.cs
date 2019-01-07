using System;
using System.Collections.Generic;
using System.IO;

namespace cslox
{
    class Lox
    {
        private static bool hadError = false;

        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: cslox [script]");
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        private static void RunFile(string path)
        {
            var contents = File.ReadAllText(path);
            Run(contents);
            if (hadError)
                Environment.Exit(65);

        }

        private static void RunPrompt()
        {
            while (true)
            {
                Console.Write("> ");
                Run(Console.ReadLine());
                hadError = false;      
            }
        }

        private static void Run(String source)
        {
            var scanner = new Scanner(source);
            var tokens = scanner.ScanTokens();

            // For now, just print the tokens.        
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }

        public static void RegisterError(int line, string message)
        {
            Report(line, "", message);
        }

        private static void Report(int line, string where, string message)
        {
            Console.Error.WriteLine($"[line {line}] Error {where}: {message}");
            hadError = true;
        }
    }
}