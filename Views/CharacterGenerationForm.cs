using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using COMP123_S2019_FinalTestC.Objects;
using System.Linq;
using System.Diagnostics;
/*
* STUDENT NAME: ANDRE BRANDAO TEODORO
* STUDENT ID: 300944427
* DESCRIPTION: This is the main form for the application
*/

namespace COMP123_S2019_FinalTestC.Views
{
    
    public partial class CharacterGenerationForm : COMP123_S2019_FinalTestC.Views.MasterForm
    {
        Random rand = new Random();
        public CharacterGenerationForm()
        {
            InitializeComponent();
        }
        #region BackButton
        /// <summary>
        /// This is the event handler for the BackButton Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_Click(object sender, EventArgs e)
        {
            NextButton.Enabled = true;
            if (MainTabControl.SelectedIndex != 0)
            {
               
                MainTabControl.SelectedIndex--;
            }
            else
            {
                BackButton.Enabled = false;
            }
        }
        #endregion
        #region NextButton  
        /// <summary>
        /// This is the event handler for the NextButton Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, EventArgs e)
        {
            BackButton.Enabled = true;
            if (MainTabControl.SelectedIndex < MainTabControl.TabPages.Count -1)
            {
                
                MainTabControl.SelectedIndex++;
            }
            else
            {
                NextButton.Enabled = false;
            }
        }
        #endregion
        #region Save To File
        /// <summary>
        /// This is the event handler for Save File button on Tool Strip and Menu Strip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            // configure the file dialog
            CharacterSaveFileDialog.FileName = "Character.txt";
            CharacterSaveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            CharacterSaveFileDialog.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";

            // open the file dialog
            var result = CharacterSaveFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                // open the stream for writing
                using (StreamWriter outputStream = new StreamWriter(
                    File.Open(CharacterSaveFileDialog.FileName, FileMode.Create)))
                {
                    // write content - string type - to the file
                    outputStream.WriteLine(Program.characterPortfolio.Identity.FirstName);
                    outputStream.WriteLine(Program.characterPortfolio.Identity.LastName);
                    outputStream.WriteLine(Program.characterPortfolio.Strength);
                    outputStream.WriteLine(Program.characterPortfolio.Dexterity);
                    outputStream.WriteLine(Program.characterPortfolio.Endurance);
                    outputStream.WriteLine(Program.characterPortfolio.Intellect);
                    outputStream.WriteLine(Program.characterPortfolio.Education);
                    outputStream.WriteLine(Program.characterPortfolio.SocialStanding);

                    // cleanup
                    outputStream.Close();
                    outputStream.Dispose();

                    // give feedback to the user that the file has been saved
                    // this is a "modal" form
                    MessageBox.Show("File Saved...", "Saving File...",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion
        #region Open From File
        /// <summary>
        /// This is the event handler for Open file button on Tool Strip and Menu Strip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            // configure the file dialog
            CharacterOpenFileDialog.FileName = "Character.txt";
            CharacterOpenFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            CharacterOpenFileDialog.Filter = "Text Files (*.txt)|*.txt| All Files (*.*)|*.*";

            // open the file dialog
            var result = CharacterOpenFileDialog.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                try
                {
                    // Open the  streawm for reading
                    using (StreamReader inputStream = new StreamReader(
                        File.Open(CharacterOpenFileDialog.FileName, FileMode.Open)))
                    {
                        // read from the file
                        Program.characterPortfolio.Identity.FirstName = inputStream.ReadLine();
                        Program.characterPortfolio.Identity.LastName = inputStream.ReadLine();
                        Program.characterPortfolio.Strength = inputStream.ReadLine();
                        Program.characterPortfolio.Dexterity = inputStream.ReadLine();
                        Program.characterPortfolio.Endurance = inputStream.ReadLine();
                        Program.characterPortfolio.Intellect = inputStream.ReadLine();
                        Program.characterPortfolio.Education = inputStream.ReadLine();
                        Program.characterPortfolio.SocialStanding = inputStream.ReadLine();


                        // cleanup
                        inputStream.Close();
                        inputStream.Dispose();
                    }

                    NextButton_Click(sender, e);
                }
                catch (IOException exception)
                {

                    Debug.WriteLine("ERROR: " + exception.Message);

                    MessageBox.Show("ERROR: " + exception.Message, "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FormatException exception)
                {
                    Debug.WriteLine("ERROR: " + exception.Message);

                    MessageBox.Show("ERROR: " + exception.Message + "\n\nPlease select the appropriate file type", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        #region GenerateName
        /// <summary>
        /// This is the event handler for the Generate Name Button
        /// It assigns a First and Last name based on the input from two files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        private void GenerateNameButton_Click(object sender, EventArgs e)
        {

            string firstNameFile = "C:\\Users\\Andre\\Desktop\\COMP123-S2019-FinalTestC\\Data\\firstNames.txt";

            var readAllLinesfirstNameFile = File.ReadAllLines(firstNameFile);
            List<string> firstNameList = readAllLinesfirstNameFile.ToList();
            int firstNameFileLength = readAllLinesfirstNameFile.Length;
            Program.characterPortfolio.Identity.FirstName = firstNameList[rand.Next(firstNameFileLength)];

            string lastNameFile = "C:\\Users\\Andre\\Desktop\\COMP123-S2019-FinalTestC\\Data\\lastNames.txt";
            var readAllLineslastNameFile = File.ReadAllLines(lastNameFile);
            List<string> lastNameList = readAllLineslastNameFile.ToList();
            int lastNameListeFileLength = readAllLineslastNameFile.Length;
            Program.characterPortfolio.Identity.LastName = lastNameList[rand.Next(lastNameListeFileLength)];

            FirstNameDataLabel.Text = Program.characterPortfolio.Identity.FirstName;
            LastNameDataLabel.Text = Program.characterPortfolio.Identity.LastName;
        }


        #endregion
        #region AbilitiesDistribution
        /// <summary>
        /// This is the event handler for the Abilities distribution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        private void GenerateAbilitiesButton_Click(object sender, EventArgs e)
        {
            int skillpoints = 14;
            Program.characterPortfolio.Strength  = "0";;
            Program.characterPortfolio.Dexterity = "0";;
            Program.characterPortfolio.Endurance = "0";;
            Program.characterPortfolio.Intellect = "0";;
            Program.characterPortfolio.Education = "0"; ;
            Program.characterPortfolio.SocialStanding = "0"; ;
            for (int i = 0; i< skillpoints; i++) {
                int point = rand.Next(1, 7);
                switch(point){
                    case 1:
                        Program.characterPortfolio.Strength = (Int32.Parse(Program.characterPortfolio.Strength) + 1).ToString();
                        break;
                    case 2:
                        Program.characterPortfolio.Dexterity = (Int32.Parse(Program.characterPortfolio.Dexterity) + 1).ToString();
                        break;
                    case 3:
                        Program.characterPortfolio.Endurance = (Int32.Parse(Program.characterPortfolio.Endurance) + 1).ToString();
                        break;
                    case 4:
                        Program.characterPortfolio.Intellect = (Int32.Parse(Program.characterPortfolio.Intellect) + 1).ToString();
                        break;
                    case 5:
                        Program.characterPortfolio.Education = (Int32.Parse(Program.characterPortfolio.Strength) + 1).ToString();
                        break;
                    case 6:
                        Program.characterPortfolio.SocialStanding = (Int32.Parse(Program.characterPortfolio.SocialStanding) + 1).ToString();
                        break;
                }
            }
            StrengthDataLabel.Text = Program.characterPortfolio.Strength;
            DexterityDataLabel.Text = Program.characterPortfolio.Dexterity;
            EnduranceDataLabel.Text = Program.characterPortfolio.Endurance;
            IntellectDataLabel.Text = Program.characterPortfolio.Intellect;
            EducationDataLabel.Text = Program.characterPortfolio.Education;
            SocialStandingDataLabel.Text = Program.characterPortfolio.SocialStanding;
        }


        #endregion
        #region Exit
        /// <summary>
        /// These are the event handler for exiting the appliation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CharacterGenerationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region AboutForm
        /// <summary>
        /// This is the event handler for the Help Tool Strip button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpToolStripButton_Click(object sender, EventArgs e)
        {
            Program.aboutForm.ShowDialog();
        }
        #endregion
    }
}
