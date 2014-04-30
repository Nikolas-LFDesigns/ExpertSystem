using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExpertSystem.SII;
using System.IO;

namespace ExpertSystem
{
    public partial class Main : Form
    {

        private II ii = new II();
        private HotelFindResult[] result;
        private List<CheckBox> roomService;
        private List<CheckBox> hotelService;
        private List<CheckBox> childrenService;

        public Main()
        {
            InitializeComponent();
            ii.Init();
            this.countryComboBox.Items.Add("все");
            this.countryComboBox.Items.AddRange(ii.Countries.Select(x => x.Value.Name).ToArray());
            
            this.multiPaneControl.SelectedPage = this.multiPanePage1;
            this.healthComboBox.SelectedIndex = 0;
            this.countryComboBox.SelectedIndex = 0;
            this.locationComboBox.SelectedIndex = 0;
            this.appointmentComboBox.SelectedIndex = 0;
            this.serviceLevelComboBox.SelectedIndex = 0;
            this.buildingComboBox.SelectedIndex = 0;
            this.nutritionComboBox.SelectedIndex = 0;

            this.dateTimePicker.MaxDate = DateTime.Now;
            this.dateTimePicker.MinDate = DateTime.Now.AddYears(-99);

            this.splitContainer3.SplitterDistance = this.splitContainer3.Height - this.searchButton.Height - 16;
            this.splitContainer4.SplitterDistance = this.splitContainer4.Height - this.editButton.Height - 16;
            this.splitContainer1.SplitterDistance = this.groupBox5.Width + 26 + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            this.searchButton.Enabled = false;

            roomService = new List<CheckBox>();
            roomService.Add(this.roomService1CheckBox);
            roomService.Add(this.roomService2CheckBox);
            roomService.Add(this.roomService3CheckBox);
            roomService.Add(this.roomService4CheckBox);
            roomService.Add(this.roomService5CheckBox);
            roomService.Add(this.roomService6CheckBox);
            roomService.Add(this.roomService7CheckBox);
            roomService.Add(this.roomService8CheckBox);
            roomService.Add(this.roomService9CheckBox);
            roomService.Add(this.roomService10CheckBox);
            roomService.Add(this.roomService11CheckBox);
            roomService.Add(this.roomService12CheckBox);

            hotelService = new List<CheckBox>();
            hotelService.Add(this.hotelService1CheckBox);
            hotelService.Add(this.hotelService2CheckBox);
            hotelService.Add(this.hotelService3CheckBox);
            hotelService.Add(this.hotelService4CheckBox);
            hotelService.Add(this.hotelService5CheckBox);
            hotelService.Add(this.hotelService6CheckBox);
            hotelService.Add(this.hotelService7CheckBox);
            hotelService.Add(this.hotelService8CheckBox);
            hotelService.Add(this.hotelService9CheckBox);
            hotelService.Add(this.hotelService10CheckBox);
            hotelService.Add(this.hotelService11CheckBox);
            hotelService.Add(this.hotelService12CheckBox);
            hotelService.Add(this.hotelService13CheckBox);
            hotelService.Add(this.hotelService14CheckBox);
            hotelService.Add(this.hotelService15CheckBox);
            hotelService.Add(this.hotelService16CheckBox);
            hotelService.Add(this.hotelService17CheckBox);
            hotelService.Add(this.hotelService18CheckBox);
            hotelService.Add(this.hotelService19CheckBox);
            hotelService.Add(this.hotelService20CheckBox);
            hotelService.Add(this.hotelService21CheckBox);
            hotelService.Add(this.hotelService22CheckBox);
            hotelService.Add(this.hotelService23CheckBox);
            hotelService.Add(this.hotelService24CheckBox);
            hotelService.Add(this.hotelService25CheckBox);
            hotelService.Add(this.hotelService26CheckBox);
            hotelService.Add(this.hotelService27CheckBox);
            hotelService.Add(this.hotelService28CheckBox);
            hotelService.Add(this.hotelService29CheckBox);
            hotelService.Add(this.hotelService30CheckBox);
            hotelService.Add(this.hotelService31CheckBox);
            hotelService.Add(this.hotelService32CheckBox);
            hotelService.Add(this.hotelService33CheckBox);
            hotelService.Add(this.hotelService34CheckBox);
            hotelService.Add(this.hotelService35CheckBox);
            hotelService.Add(this.hotelService36CheckBox);
            hotelService.Add(this.hotelService37CheckBox);
            hotelService.Add(this.hotelService38CheckBox);
            hotelService.Add(this.hotelService39CheckBox);
            hotelService.Add(this.hotelService40CheckBox);
            hotelService.Add(this.hotelService41CheckBox);
            hotelService.Add(this.hotelService42CheckBox);
            hotelService.Add(this.hotelService43CheckBox);
            hotelService.Add(this.hotelService44CheckBox);
            hotelService.Add(this.hotelService45CheckBox);

            childrenService = new List<CheckBox>();
            childrenService.Add(this.childrenService1CheckBox);
            childrenService.Add(this.childrenService2CheckBox);
            childrenService.Add(this.childrenService3CheckBox);
            childrenService.Add(this.childrenService4CheckBox);
            childrenService.Add(this.childrenService5CheckBox);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.dateTimePicker.Value = this.dateTimePicker.MaxDate;
            this.moneyTextBox.Text = "0";
            this.numberDaysTextBox.Text = "0";
            this.insuranceCheckBox.Checked = false;
            this.healthComboBox.SelectedIndex = 0;
            this.countryComboBox.SelectedIndex = 0;
            this.regionComboBox.SelectedIndex = 0;
            this.climateComboBox.SelectedIndex = 0;
            this.locationComboBox.SelectedIndex = 0;
            this.appointmentComboBox.SelectedIndex = 0;
            this.serviceLevelComboBox.SelectedIndex = 0;
            this.buildingComboBox.SelectedIndex = 0;
            this.nutritionComboBox.SelectedIndex = 0;
            for (int i = 0; i < roomService.Count; i++)
                roomService[i].Checked = false;
            for (int i = 0; i < hotelService.Count; i++)
                hotelService[i].Checked = false;
            for (int i = 0; i < childrenService.Count; i++)
                childrenService[i].Checked = false;
            this.multiPaneControl.SelectedPage = this.multiPanePage1;
        }

        private void hotelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hotelsOpenFileDialog.ShowDialog();
        }

        private void hotelsOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            ii.GenerateBZ(this.hotelsOpenFileDialog.FileName);
            this.searchButton.Enabled = true;
        }

        private void knowledgeBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.knowledgeOpenFileDialog.ShowDialog();
        }

        private void knowledgeOpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader streamReader = new StreamReader(knowledgeOpenFileDialog.FileName);
            ii.BZ = streamReader.ReadToEnd();
            streamReader.Close();
            this.searchButton.Enabled = true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        private void saveFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName);
            streamWriter.Write(ii.BZ);
            streamWriter.Close();
        }

        private void viewHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "Help.chm", HelpNavigator.TableOfContents);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private Questionnaire getQuestionnaire()
        {
            Questionnaire questionnaire = new Questionnaire();
            questionnaire.age = DateTime.Now.Year - this.dateTimePicker.Value.Year;
            questionnaire.childService = childrenService.FindAll(p => p.Checked).Select(x => x.Text.ToLower()).ToArray();
            questionnaire.climate = this.climateComboBox.Text.ToLower();
            questionnaire.country = this.countryComboBox.Text;
            questionnaire.destination = this.appointmentComboBox.Text.ToLower();
            questionnaire.health = this.healthComboBox.Text.ToLower();
            questionnaire.holidaysLength = Int32.Parse(this.numberDaysTextBox.Text);
            questionnaire.hotelServices = hotelService.FindAll(p => p.Checked).Select(x => x.Text.ToLower()).ToArray();
            questionnaire.level = this.serviceLevelComboBox.Text.ToLower();
            questionnaire.location = this.locationComboBox.Text.ToLower();
            questionnaire.meals = this.nutritionComboBox.Text.ToLower();
            questionnaire.price = Int32.Parse(this.moneyTextBox.Text);
            questionnaire.region = this.regionComboBox.Text;
            questionnaire.roomServices = roomService.FindAll(p => p.Checked).Select(x => x.Text.ToLower()).ToArray();
            questionnaire.type = this.buildingComboBox.Text.ToLower();
            questionnaire.insurance = this.insuranceCheckBox.Checked ? "Есть" : "Нет";
            return questionnaire;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            result = ii.Find(getQuestionnaire());

            dataGridView.RowCount = result.Length;
            for (int i = 0; i < result.Length; i++)
            {
                dataGridView.Rows[i].Cells[0].Value = result[i].Hotel.name;
                dataGridView.Rows[i].Cells[1].Value = result[i].KU;
            }

            this.ageLabel.Text = (DateTime.Now.Year - this.dateTimePicker.Value.Year).ToString();
            this.moneyLabel.Text = int.Parse(this.moneyTextBox.Text).ToString();
            this.numberDaysLabel.Text = int.Parse(this.numberDaysTextBox.Text).ToString();
            this.healthLabel.Text = this.healthComboBox.Text.ToLower();
            this.insuranceLabel.Text = this.insuranceCheckBox.Checked ? "Есть" : "Нет";
            this.countryLabel.Text = this.countryComboBox.Text;
            this.regionLabel.Text = this.regionComboBox.Text;
            this.climateLabel.Text = this.climateComboBox.Text.ToLower();
            this.locationLabel.Text = this.locationComboBox.Text.ToLower();
            this.appointmentLabel.Text = this.appointmentComboBox.Text.ToLower();
            this.serviceLevelLabel.Text = this.serviceLevelComboBox.Text.ToLower();
            this.buildingLabel.Text = this.buildingComboBox.Text.ToLower();
            this.nutritionLabel.Text = this.nutritionComboBox.Text;
            this.roomServiceTextBox.Text = string.Join(", ",
                roomService.FindAll(p => p.Checked).Select(x => x.Text.ToLower()).ToArray());
            this.hotelServiceTextBox.Text = string.Join(", ",
                hotelService.FindAll(p => p.Checked).Select(x => x.Text.ToLower()).ToArray());
            this.childrenServiceTextBox.Text = string.Join(", ",
                childrenService.FindAll(p => p.Checked).Select(x => x.Text.ToLower()).ToArray());

            this.multiPaneControl.SelectedPage = this.multiPanePage2;

            if(result.Length > 0)
                this.dataGridView_CellEnter(this.dataGridView, new DataGridViewCellEventArgs(0, 0));
            MessageBox.Show("Найдено " + result.Length.ToString() + " отель (-ля, -лей)", "Информация о результате",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            this.multiPaneControl.SelectedPage = this.multiPanePage1;
            this.dateTimePicker.Focus();
        }

        private void moneyTextBox_Leave(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Equals("") || int.Parse((sender as TextBox).Text) == 0) (sender as TextBox).Text = "1";
        }

        private void numberDaysTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void climateComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int l1 = int.Parse(ii.ClimateAgeKU[this.climateComboBox.Text.ToLower()].Keys.
                    Last(p => int.Parse(p) <= (DateTime.Now.Year - this.dateTimePicker.Value.Year)));
                int l2 = int.Parse(ii.ClimateAgeKU[this.climateComboBox.Text.ToLower()].Keys.
                    First(p => int.Parse(p) > (DateTime.Now.Year - this.dateTimePicker.Value.Year)));
                if(ii.ClimateAgeKU[this.climateComboBox.Text.ToLower()][l1 + ""] +
                    ii.ClimateAgeKU[this.climateComboBox.Text.ToLower()][l2 + ""] != 2)
                {
                    MessageBox.Show("Для вашего возраста не рекомендуется выбранный климат",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ii.ClimateHealthKU[this.climateComboBox.Text.ToLower()][this.healthComboBox.Text.ToLower()] != 1)
                {
                    MessageBox.Show("Для вашего здоровья не рекомендуется выбранный климат",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch(Exception)
            {
                MessageBox.Show("Термин в словаре не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void appointmentComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int l1 = int.Parse(ii.DestinationAgeKU[this.appointmentComboBox.Text.ToLower()].Keys.
                    Last(p => int.Parse(p) <= (DateTime.Now.Year - this.dateTimePicker.Value.Year)));
                int l2 = int.Parse(ii.DestinationAgeKU[this.appointmentComboBox.Text.ToLower()].Keys.
                    First(p => int.Parse(p) > (DateTime.Now.Year - this.dateTimePicker.Value.Year)));
                if (ii.DestinationAgeKU[this.appointmentComboBox.Text.ToLower()][l1 + ""] +
                    ii.DestinationAgeKU[this.appointmentComboBox.Text.ToLower()][l2 + ""] != 2)
                {
                    MessageBox.Show("Для вашего возраста не рекомендуется выбранное назначение",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ii.DestinationHealthKU[this.appointmentComboBox.Text.ToLower()][this.healthComboBox.Text.ToLower()] != 1)
                {
                    MessageBox.Show("Для вашего здоровья не рекомендуется выбранное назначение",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Термин в словаре не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nutritionComboBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int l1 = int.Parse(ii.MealAgeKU[this.nutritionComboBox.Text.ToLower()].Keys.
                    Last(p => int.Parse(p) <= (DateTime.Now.Year - this.dateTimePicker.Value.Year)));
                int l2 = int.Parse(ii.MealAgeKU[this.nutritionComboBox.Text.ToLower()].Keys.
                    First(p => int.Parse(p) > (DateTime.Now.Year - this.dateTimePicker.Value.Year)));
                if (ii.MealAgeKU[this.nutritionComboBox.Text.ToLower()][l1 + ""] +
                    ii.MealAgeKU[this.nutritionComboBox.Text.ToLower()][l2 + ""] != 2)
                {
                    MessageBox.Show("Для вашего возраста не рекомендуется выбранное питание",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (ii.MealHealthKU[this.nutritionComboBox.Text.ToLower()][this.healthComboBox.Text.ToLower()] != 1)
                {
                    MessageBox.Show("Для вашего здоровья не рекомендуется выбранное питание",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Термин в словаре не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void countryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.regionComboBox.Items.Clear();
            this.regionComboBox.Items.Add("все");
            Dictionary<string, SII.Region> values = new Dictionary<string, SII.Region>();
            if (ii.Regions.TryGetValue((sender as ComboBox).Text, out values))
            {
                this.regionComboBox.Items.AddRange(values.Select(x=>x.Value.Name).ToArray());
            }
            this.regionComboBox.SelectedIndex = 0;
        }

        private void regionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.climateComboBox.Items.Clear();
            if (this.regionComboBox.Text.ToLower().Equals("все"))
            {
                this.climateComboBox.Items.Add("очень холодно");
                this.climateComboBox.Items.Add("холодно");
                this.climateComboBox.Items.Add("прохладно");
                this.climateComboBox.Items.Add("тепло");
                this.climateComboBox.Items.Add("жарко");
                this.climateComboBox.Items.Add("очень жарко");
            }
            else
            {
                string value;
                if(ii.Climate.TryGetValue(SII.Region.GetRegion(this.countryComboBox.Text,
                    this.regionComboBox.Text), out value))
                this.climateComboBox.Items.Add(value);
            }
            this.climateComboBox.SelectedIndex = 0;
        }

        private void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            int index = e.RowIndex;
            string indexStr = (index + 1).ToString();
            object header = (sender as DataGridView).Rows[index].HeaderCell.Value;
            if (header == null || !header.Equals(indexStr))
                (sender as DataGridView).Rows[index].HeaderCell.Value = indexStr;
        }

        private void splitContainer3_Panel1_Click(object sender, EventArgs e)
        {
            this.splitContainer3.Panel1.Focus();
        }

        private void splitContainer4_Panel1_Click(object sender, EventArgs e)
        {
            this.splitContainer4.Panel1.Focus();
        }

        private void dataGridView_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            string res = "";
            if (e.RowIndex >= 0 && e.RowIndex < (sender as DataGridView).RowCount)
                for (int i = 0; i < result[e.RowIndex].Productions.Length; i++)
                {
                    Production pr = result[e.RowIndex].Productions[i];
                    res += "ЕСЛИ " + pr.State + " И " + pr.Arg1 + " " + pr.Function + " " + pr.Arg2 + " ТО " + pr.StateResult + " (КУ=" + pr.KU + ")\n";
                }
            this.detailRichTextBox.Text = res;
        }
    }
}
