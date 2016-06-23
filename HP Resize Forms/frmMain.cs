// ***********************************************************************
// Assembly         : HP Resize
// Author           : Mario
// Created          : 06-10-2016
//
// Last Modified By : Mario
// Last Modified On : 06-23-2016
// ***********************************************************************
// <copyright file="frmMain.cs" company="">
//     Copyright ©  2016
// </copyright>
// <summary></summary>
// ***********************************************************************
using HP_Resize;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HP_Resize_Forms
{
    /// <summary>
    /// Class frmMain.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmMain : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain" /> class.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog1.FileName;
                var resize = new Resizer(file, (int)numericUpDown1.Value);
                resize.Resize();
            }
        }

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                progressBar1.Visible = true;
                var path = folderBrowserDialog1.SelectedPath;
                // only in current directory, not traversing child directories
                var files = Directory.EnumerateFiles(path, "*.py", SearchOption.TopDirectoryOnly).ToList();
                progressBar1.Maximum = files.Count;
                progressBar1.Value = 0;
                Task.Factory.StartNew(() =>
                {
                    MultiResizer multiResizer = new MultiResizer(files, (int)numericUpDown1.Value);
                    multiResizer.OnResize += MultiResizer_OnResize;
                    multiResizer.Resize();
                });
            }
        }

        /// <summary>
        /// Multis the resizer_ on resize.
        /// </summary>
        /// <param name="resized">The resized.</param>
        /// <param name="total">The total.</param>
        private void MultiResizer_OnResize(int resized, int total)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.progressBar1.Value = resized;
                }));
            }
            else
            {
                this.progressBar1.Value = resized;
            }
        }
    }
}
