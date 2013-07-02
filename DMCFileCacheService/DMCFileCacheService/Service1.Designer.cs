using System.Timers;

namespace DMCFileCacheService
{
    partial class DMCFileCacheService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(eventLog1)).BeginInit();
            // 
            // DMCFileCacheService
            // 
            this.ServiceName = "DMCFileCacheService";
            ((System.ComponentModel.ISupportInitialize)(eventLog1)).EndInit();

        }

        #endregion

        private static System.Diagnostics.EventLog eventLog1;
        private readonly Timer _listWatcher;
    }
}
