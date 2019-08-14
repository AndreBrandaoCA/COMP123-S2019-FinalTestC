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
        public static Random rand = new Random();
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
            if(MainTabControl.SelectedIndex == 3)
            {
                LoadCharacterSheet();
            }
        }
        #endregion
        #region AbilitiesPoints
        /// <summary>
        /// This is the event handler for the Abilities distribution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        private void GenerateAbilitiesButton_Click(object sender, EventArgs e)
        {
            const int SKILLCAP = 15;
            // assign skill points and update form with values
            StrengthDataLabel.Text      = Program.characterPortfolio.Strength = rand.Next(1, SKILLCAP).ToString();
            DexterityDataLabel.Text     = Program.characterPortfolio.Dexterity = rand.Next(1, SKILLCAP).ToString();
            EnduranceDataLabel.Text     = Program.characterPortfolio.Endurance = rand.Next(1, SKILLCAP).ToString();
            IntellectDataLabel.Text     = Program.characterPortfolio.Intellect = rand.Next(1, SKILLCAP).ToString();
            EducationDataLabel.Text     = Program.characterPortfolio.Education = rand.Next(1, SKILLCAP).ToString(); ;
            SocialStandingDataLabel.Text = Program.characterPortfolio.SocialStanding = rand.Next(1, SKILLCAP).ToString();
        }
        #endregion
        #region GenerateSkills
        private void GenerateSkillsButton_Click(object sender, EventArgs e)
        {
            Program.characterPortfolio.Skills.Clear();
            // read first name file and assign random to property
            string SkillsFile = "..\\..\\Data\\skills.txt";
            var readAllLinesSkillsFile = File.ReadAllLines(SkillsFile);
            List<string> SkillsList = readAllLinesSkillsFile.ToList();
            int SkillsFileLength = readAllLinesSkillsFile.Length;
            for(int i=0; i <= 3; i++)
            {
                Skill skill = new Skill();
                skill.Name = SkillsList[rand.Next(SkillsFileLength)];
                Program.characterPortfolio.Skills.Add(skill);
            }
            FirstCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[0].Name;
            SecondCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[1].Name;
            ThirdCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[2].Name;
            FourthCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[3].Name;
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
        /// <summary>
        /// This is the event Handler for Character Generation Form closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterGenerationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region GenerateNames
        /// <summary>
        /// This is the method that generate the First and Last Name for the Character Portfolio
        /// </summary>
        private void GenerateNames()
        {
            // read first name file and assign random to property
            string firstNameFile = "..\\..\\Data\\firstNames.txt";
            var readAllLinesfirstNameFile = File.ReadAllLines(firstNameFile);
            List<string> firstNameList = readAllLinesfirstNameFile.ToList();
            int firstNameFileLength = readAllLinesfirstNameFile.Length;
            Program.characterPortfolio.Identity.FirstName = firstNameList[rand.Next(firstNameFileLength)];
            // read last name file and assign random to property
            string lastNameFile = "..\\..\\Data\\lastNames.txt";
            var readAllLineslastNameFile = File.ReadAllLines(lastNameFile);
            List<string> lastNameList = readAllLineslastNameFile.ToList();
            int lastNameListeFileLength = readAllLineslastNameFile.Length;
            Program.characterPortfolio.Identity.LastName = lastNameList[rand.Next(lastNameListeFileLength)];
            // updating form with values
            FirstNameDataLabel.Text = Program.characterPortfolio.Identity.FirstName;
            LastNameDataLabel.Text = Program.characterPortfolio.Identity.LastName;
        }
        #endregion
        #region Form Load
        /// <summary>
        /// This is the event handler for the Generation Form Load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CharacterGenerationForm_Load(object sender, EventArgs e)
        {
            GenerateNames();
        }
        #endregion
        #region SaveFile
        /// <summary>
        /// This is the method for saving the Character Porfolio to a file
        /// </summary>
        private void SaveFile()
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
                    outputStream.WriteLine(Program.characterPortfolio.Skills[0].Name);
                    outputStream.WriteLine(Program.characterPortfolio.Skills[1].Name);
                    outputStream.WriteLine(Program.characterPortfolio.Skills[2].Name);
                    outputStream.WriteLine(Program.characterPortfolio.Skills[3].Name);

                    // cleanup
                    outputStream.Close();
                    outputStream.Dispose();

                    // give feedback to the user that the file has been saved
                    MessageBox.Show("File Saved...", "Saving File...",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        #endregion
        #region OpenFile
        /// <summary>
        /// This is the method to open a Character Portfolio from a file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile(object sender, EventArgs e)
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
                        Program.characterPortfolio.Skills[0].Name = inputStream.ReadLine();
                        Program.characterPortfolio.Skills[1].Name = inputStream.ReadLine();
                        Program.characterPortfolio.Skills[2].Name = inputStream.ReadLine();
                        Program.characterPortfolio.Skills[3].Name = inputStream.ReadLine();

                        // cleanup
                        inputStream.Close();
                        inputStream.Dispose();
                    }

                    NextButton_Click(sender, e);
                }
                catch (IOException exception)
                {
                    // error handler
                    Debug.WriteLine("ERROR: " + exception.Message);

                    MessageBox.Show("ERROR: " + exception.Message, "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (FormatException exception)
                {
                    // error handler
                    Debug.WriteLine("ERROR: " + exception.Message);

                    MessageBox.Show("ERROR: " + exception.Message + "\n\nPlease select the appropriate file type", "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        #region OpenToolStripMenuItem_Click
        /// <summary>
        /// This is the event handler for the OpenOpenToolStripMenuItem_Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile(sender, e);
        }
        #endregion
        #region SaveToolStripButton_Click
        /// <summary>
        /// This is the event handler for SaveToolStripButton_Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        #endregion

        #region SaveToolStripMenuItem_Click
        /// <summary>
        /// This is the event handler for SaveToolStripMenuItem_Click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        #endregion
        #region OpenToolStripButton_Click
        /// <summary>
        /// This is the event handler for Open file button on Tool Strip and Menu Strip
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFile(sender,e);
        }
        #endregion
        #region GenerateNameButtonClick
        /// <summary>
        /// This is the event handler for the Generate Name Button
        /// It assigns a First and Last name based on the input from two files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>   
        private void GenerateNameButton_Click(object sender, EventArgs e)
        {
            GenerateNames();

        }
        #endregion
        #region HelpToolStripButton
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
        #region LoadCharacterSheet
        /// <summary>
        /// This is the method to load the Character Portfolio to the Character Sheet
        /// </summary>
        private void LoadCharacterSheet()
        {
            CharSheetFirstNameDataLabel.Text = Program.characterPortfolio.Identity.FirstName;
            CharSheetLastNameDataLabel.Text = Program.characterPortfolio.Identity.LastName;
            CharSheetStrengthDataLabel.Text = Program.characterPortfolio.Strength;
            CharSheetDexDataLabel.Text = Program.characterPortfolio.Dexterity;
            CharSheetEnduranceDataLabel.Text = Program.characterPortfolio.Endurance;
            CharSheetIntellectDataLabel.Text = Program.characterPortfolio.Intellect;
            CharSheetEducationDataLabel.Text = Program.characterPortfolio.Education;
            CharSheetSocStandingDataLabel.Text = Program.characterPortfolio.SocialStanding;
            if(Program.characterPortfolio.Skills.Count != 0) {
                CharSheetFirstCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[0].Name;
                CharSheetSecondCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[1].Name;
                CharSheetThirdCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[2].Name;
                CharSheetFourthCharacterSkillsDataLabel.Text = Program.characterPortfolio.Skills[3].Name;
            }
            
        }

        #endregion
        #region MainTabControl_TabIndexChanged
        /// <summary>
        /// This is the event handler to load Character Sheet if MainTabControl_TabIndexChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainTabControl_TabIndexChanged(object sender, EventArgs e)
        {
            if (MainTabControl.SelectedIndex == 3)
            {
                LoadCharacterSheet();
            }
        }
        #endregion

    }
}
