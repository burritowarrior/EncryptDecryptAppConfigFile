using System;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Diagnostics;
namespace EncryptDecryptAppConfigFile
{
    // http://www.codeproject.com/Tips/598863/EncryptionplusDecryptionplusConnectionplusStringpl
    public partial class EncryptDecrypt : Form
    {
        public EncryptDecrypt()
        {
            InitializeComponent();
        }
        string fileName = string.Empty;


        public static void EncryptConnectionString(bool encrypt, string fileName)
        {
            try
            {
                // Open the configuration file and retrieve the connectionStrings section.
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(fileName);
                // var configSection = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
                var configSection = configuration.GetSection("appSettings") as AppSettingsSection;

                if (configSection != null && 
                    ((!(configSection.ElementInformation.IsLocked)) && (!(configSection.SectionInformation.IsLocked))))
                {
                    if (encrypt && !configSection.SectionInformation.IsProtected)
                    {
                        //this line will encrypt the file
                        configSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                    }

                    if (!encrypt && configSection.SectionInformation.IsProtected)//encrypt is true so encrypt
                    {
                        //this line will decrypt the file. 
                        configSection.SectionInformation.UnprotectSection();
                    }
                    //re-save the configuration file section
                    configSection.SectionInformation.ForceSave = true;
                    // Save the current configuration

                    configuration.Save();
                    Process.Start("notepad.exe", configuration.FilePath);
                    //configFile.FilePath 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void EncryptFile(string fileName) {
            try {
                // Open the configuration file and retrieve the connectionStrings section.
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(fileName);

                // var configSection = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
                var configSection = configuration.GetSection("appSettings") as AppSettingsSection;

                if (configSection != null &&
                    ((!(configSection.ElementInformation.IsLocked)) && (!(configSection.SectionInformation.IsLocked)))) {

                    if (!configSection.SectionInformation.IsProtected) {
                        //this line will encrypt the file
                        configSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                    }

                    //re-save the configuration file section
                    configSection.SectionInformation.ForceSave = true;

                    // Save the current configuration
                    // configuration.Save();
                    configuration.Save(ConfigurationSaveMode.Full);
                    
                    Process.Start("notepad.exe", configuration.FilePath);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void DecryptFile(string fileName) {
            try {
                // Open the configuration file and retrieve the connectionStrings section.
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(fileName);
                // var configSection = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
                var configSection = configuration.GetSection("appSettings") as AppSettingsSection;

                if (configSection != null &&
                    ((!(configSection.ElementInformation.IsLocked)) && (!(configSection.SectionInformation.IsLocked)))) {

                    if (configSection.SectionInformation.IsProtected)//encrypt is true so encrypt
                    {
                        //this line will decrypt the file. 
                        configSection.SectionInformation.UnprotectSection();
                    }

                    //re-save the configuration file section
                    // configSection.SectionInformation.ForceSave = true;

                    // Save the current configuration
                    // configuration.Save();

                    Process.Start("notepad.exe", configuration.FilePath);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EncryptFile()
        {
            if (File.Exists(fileName))
            {
                // EncryptConnectionString(true, fileName);
                EncryptFile(fileName);
            }
            else
            {
                MessageBox.Show(@"File does not exist");
            }
        }

        private void DecryptFile()
        {
            if (File.Exists(fileName))
            {
                // EncryptConnectionString(false, fileName);
                DecryptFile(fileName);
            }
            else
            {
                MessageBox.Show(@"File does not exist");
            }
        }

        private void cmdOpen_Click_1(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            openFileDialog1.Filter = @".Net Executables|*.exe";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            fileName = openFileDialog1.FileName;
            txtEncryption.Text = fileName;
        }

        private void cmdEncrypt_Click_1(object sender, EventArgs e)
        {
            EncryptFile();
        }

        private void cmdDecrypt_Click_1(object sender, EventArgs e)
        {
            DecryptFile();
        }
    }
}
