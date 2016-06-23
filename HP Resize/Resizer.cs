// ***********************************************************************
// Assembly         : HP Resize
// Author           : Mario
// Created          : 06-10-2016
//
// Last Modified By : Mario
// Last Modified On : 06-23-2016
// ***********************************************************************
// <copyright file="Class1.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace HP_Resize
{
    /// <summary>
    /// Class Resizer.
    /// </summary>
    public class Resizer
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Resizer" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="percentage">The percentage.</param>
        public Resizer(string filePath, int percentage)
        {
            this.FilePath = filePath;
            this.Percentage = percentage;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; private set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public int Percentage { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Resizes this instance.
        /// </summary>
        public void Resize()
        {
            var multiplier = Convert.ToDouble(this.Percentage) / 100d;
            var culture = new CultureInfo("en-US");
            // could have used regex, but am too lazy
            List<string> newContents = new List<string>();
            var contents = File.ReadAllLines(this.FilePath).ToList();
            foreach (var content in contents)
            {
                if (content.Contains(".SetPosition("))
                {
                    var values = content.Split("(".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    var positions = values[1].Replace(")", string.Empty).Replace(" ", string.Empty).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (positions.Count() == 3)
                    {
                        var x = Convert.ToDouble(positions[0], culture) * multiplier;
                        var y = Convert.ToDouble(positions[1], culture) * multiplier;
                        var z = Convert.ToDouble(positions[2], culture) * multiplier;
                        newContents.Add(string.Format("{0}({1}, {2}, {3})", values[0], x.ToString("N6", culture), y.ToString("N6", culture), z.ToString("N6", culture)));
                    }
                    else
                    {
                        newContents.Add(content);
                    }
                }
                else
                {
                    newContents.Add(content);
                }
            }
            File.WriteAllLines(this.FilePath, newContents);
        }

        #endregion Methods
    }
}