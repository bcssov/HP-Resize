// ***********************************************************************
// Assembly         : HP Resize
// Author           : Mario
// Created          : 06-23-2016
//
// Last Modified By : Mario
// Last Modified On : 06-23-2016
// ***********************************************************************
// <copyright file="MultiResizer.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;

namespace HP_Resize
{
    /// <summary>
    /// Class MultiResizer.
    /// </summary>
    public class MultiResizer
    {
        #region Delegates

        /// <summary>
        /// Delegate ResizedDelegate
        /// </summary>
        /// <param name="resized">The resized.</param>
        /// <param name="total">The total.</param>
        public delegate void ResizedDelegate(int resized, int total);

        #endregion Delegates

        #region Events

        /// <summary>
        /// Occurs when [on resize].
        /// </summary>
        public event ResizedDelegate OnResize;

        #endregion Events

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiResizer" /> class.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="percentage">The percentage.</param>
        public MultiResizer(List<string> files, int percentage)
        {
            this.Files = files;
            this.Percentage = percentage;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public List<string> Files { get; private set; }

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
            var counter = 0;
            var total = this.Files.Count;
            foreach (var item in this.Files)
            {
                var resizer = new Resizer(item, this.Percentage);
                resizer.Resize();
                counter++;
                if (this.OnResize != null)
                {
                    this.OnResize(counter, total);
                }
            }
        }

        #endregion Methods
    }
}