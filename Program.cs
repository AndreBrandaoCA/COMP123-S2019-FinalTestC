using COMP123_S2019_FinalTestC.Objects;
using COMP123_S2019_FinalTestC.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
/*
 * STUDENT NAME: ANDRE BRANDAO TEODORO
 * STUDENT ID: 300944427
 * DESCRIPTION: This is the application
 */
namespace COMP123_S2019_FinalTestC
{
    public static class Program
    {
        // temporary
        public static CharacterGenerationForm characterForm;
        public static CharacterPortfolio characterPortfolio;
        public static AboutForm aboutForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            characterForm = new CharacterGenerationForm();
            characterPortfolio = new CharacterPortfolio();
            aboutForm = new AboutForm();
            Application.Run(characterForm);
        }

    }
}
