// ***********************************************************************
// Assembly         : HP Resize Console
// Author           : Mario
// Created          : 06-10-2016
//
// Last Modified By : Mario
// Last Modified On : 06-23-2016
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using HP_Resize;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP_Resize_Console
{
    /// <summary>
    /// Class Program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        private static async Task MainAsync(string[] args)
        {
            if (args.Count() == 2)
            {
                var file = args[0];
                int percentage = Convert.ToInt32(args[1]);
                // Dir or file?
                if (Directory.Exists(file))
                {
                    // only in current directory, not traversing child directories
                    var files = Directory.EnumerateFiles(file, "*.py", SearchOption.TopDirectoryOnly).ToList();
                    await Task.Factory.StartNew(() =>
                    {
                        MultiResizer multiResizer = new MultiResizer(files, percentage);
                        multiResizer.OnResize += MultiResizer_OnResize;
                        multiResizer.Resize();
                    });
                }
                else
                {
                    var resize = new Resizer(file, percentage);
                    resize.Resize();
                }
            }
        }

        /// <summary>
        /// Multis the resizer_ on resize.
        /// </summary>
        /// <param name="resized">The resized.</param>
        /// <param name="total">The total.</param>
        private static void MultiResizer_OnResize(int resized, int total)
        {
            Console.WriteLine("Parsing file {0} out of {1}", resized, total);
        }
    }
}
