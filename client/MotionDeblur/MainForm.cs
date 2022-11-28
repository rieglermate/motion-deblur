using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotionDeblur
{
    public partial class MainForm : Form
    {
        public class Settings
        {
            public string ServerAddress { get; set; }
            public string APIPath { get; set; }
        }

        #region Fields

        private Settings settings;
        private HttpClient client;
        private string currentImageBase64;

        #endregion

        #region Constructors

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
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<Dictionary<string, string>>(responseBody);

                toolStripStatusLabel.Text = "Válasz feldolgozása";

                pictureBoxResize.Image = Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String(responseJson["imageResize"])));
                pictureBoxDeblur.Image = Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String(responseJson["imageDeblur"])));

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
                buttonOpenFile.Enabled = true;
                buttonStart.Enabled = true;
            }
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help.html");
            toolStripStatusLabel.Text = "Segítség fájl megnyitva az alapértelmezett böngészőben";
        }

        #endregion
    }
}
