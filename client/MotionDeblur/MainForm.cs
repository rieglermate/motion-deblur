using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionDeblur
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Container class for providing easy access to user settings.
        /// </summary>
        public class Settings
        {
            public string ServerAddress { get; set; }
            public string APIPath { get; set; }
        }

        #region Fields

        /// <summary>
        /// Stored user settings.
        /// </summary>
        private Settings settings;
        /// <summary>
        /// Handles connection with the API.
        /// </summary>
        private HttpClient client;
        /// <summary>
        /// Currently active image, this will be sent when making a request.
        /// </summary>
        private string currentImageBase64;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Form event handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            settings = JsonSerializer.Deserialize<Settings>(System.IO.File.ReadAllText("Settings.json"));
            client = new HttpClient
            {
                BaseAddress = new Uri(settings.ServerAddress),
                Timeout = TimeSpan.FromSeconds(15)
            };
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                    //Length of byte array can be considered equal to file size
                    if (imageBytes.Length > 50 * (1024 * 1024))
                    {
                        MessageBox.Show("A kiválasztott kép túl nagy (>50MB)", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    currentImageBase64 = Convert.ToBase64String(imageBytes);
                    labelOpenFile.Text = System.IO.Path.GetFileName(openFileDialog.FileName);
                    toolStripStatusLabel.Text = "Kép betöltve";
                    buttonStart.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Nem sikerült betölteni a képet!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSaveResize_Click(object sender, EventArgs e)
        {
            //Uses the same image file format list, and also defaults to PNG
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Kép mentése...",
                Filter = openFileDialog.Filter,
                FilterIndex = openFileDialog.FilterIndex
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Save method takes care of file formats
                    pictureBoxResize.Image.Save(saveFileDialog.FileName);
                    toolStripStatusLabel.Text = String.Format("Kép elmentve ({0})",
                        System.IO.Path.GetFileName(saveFileDialog.FileName));
                }
                catch
                {
                    MessageBox.Show("Nem sikerült elmenteni a képet!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonSaveDeblur_Click(object sender, EventArgs e)
        {
            //Uses the same image file format list, and also defaults to PNG
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Title = "Kép mentése...",
                Filter = openFileDialog.Filter,
                FilterIndex = openFileDialog.FilterIndex
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //Save method takes care of file formats
                    pictureBoxDeblur.Image.Save(saveFileDialog.FileName);
                    toolStripStatusLabel.Text = String.Format("Kép elmentve ({0})",
                        System.IO.Path.GetFileName(saveFileDialog.FileName));
                }
                catch
                {
                    MessageBox.Show("Nem sikerült elmenteni a képet!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void buttonStart_Click(object sender, EventArgs e)
        {
            //User should wait until the completion of the most recent request
            buttonOpenFile.Enabled = false;
            buttonStart.Enabled = false;

            toolStripStatusLabel.Text = "Kérés küldése a szerver felé";

            var json = new Dictionary<string, string>
            {
                ["imageData"] = currentImageBase64
            };

            byte[] jsonString = JsonSerializer.SerializeToUtf8Bytes(json);

            try
            {
                using var response = await client.PostAsync(settings.APIPath, new ByteArrayContent(jsonString));
                response.EnsureSuccessStatusCode();

                //At this point we successfully got the answer
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody);

                toolStripStatusLabel.Text = "Válasz feldolgozása";

                pictureBoxResize.Image = Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String(responseJson["imageResize"])));
                pictureBoxDeblur.Image = Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String(responseJson["imageDeblur"])));

                //If the request was successful, the user should be able to save the results
                toolStripStatusLabel.Text = "Kép feldolgozása sikeres";
                buttonSaveDeblur.Enabled = true;
                buttonSaveResize.Enabled = true;
            }
            catch (HttpRequestException)
            {
                toolStripStatusLabel.Text = "Kommunikáció a szerverrel sikertelen volt";
            }
            catch (TaskCanceledException)
            {
                toolStripStatusLabel.Text = "Időtúllépés miatt meghiúsult a kommunikáció a szerverrel";
            }
            finally
            {
                //Anything happened, make sure we enable selecting and starting again
                buttonOpenFile.Enabled = true;
                buttonStart.Enabled = true;
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            //Simplest built-in method of providing help to the user
            Help.ShowHelp(this, "Help.html");
            toolStripStatusLabel.Text = "Segítség fájl megnyitva az alapértelmezett böngészőben";
        }

        #endregion
    }
}
