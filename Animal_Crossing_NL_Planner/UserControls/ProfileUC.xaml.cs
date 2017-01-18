using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Animal_Xing_Planner
{
    public enum CAction
    {
        None,
        New,
        Edit,
    };

    /// <summary>
    /// Interaction logic for ProfileUC.xaml
    /// </summary>
    public partial class ProfileUC : UserControl
    {
        public ViewModel VillagerViewModel { get; set; }
        public Villager SelectedVillager { get; set; }

        public CAction Action;
        public CustomWindow ParentWindow;

        public ProfileUC()
        {
            InitializeComponent();

            VillagerViewModel = new ViewModel(this);
            DataContext = this;
        }

        public void Initialize(CustomWindow parent, CAction action)
        {
            ParentWindow = parent;
            Action = action;

            ResetControls();

            if (Action == CAction.Edit)
            {
                if (Globals.SettingsWindow.profileListView.SelectedItem != null)
                {
                    Profile tempProfile = Globals.SettingsWindow.profileListView.SelectedItem as Profile;

                    for (int i = 0; i < tempProfile.Villagers.Count; i++)
                        villagerListView.Items.Add(tempProfile.Villagers[i]);
                    villagerListView.UpdateLayout();

                    nameTextBox.Text = tempProfile.Mayor;
                    townTextBox.Text = tempProfile.Town;
                    LoadFruit(tempProfile.Fruit);

                    if (!string.IsNullOrEmpty(tempProfile.FC))
                        fcTextBox.Text = tempProfile.FC;
                    if (!string.IsNullOrEmpty(tempProfile.DC))
                        dcTextBox.Text = tempProfile.DC;
                    messageTextBox.Text = tempProfile.TagLine;

                    if (tempProfile.ProfileImagePath != null)
                        profileImg.Source = Globals.ImgConvert.ConvertFromString(tempProfile.ProfileImagePath) as ImageSource;
                    if (!string.IsNullOrEmpty(tempProfile.Fruit))
                        Globals.Main.fruitImage.Source = Globals.GetBitmapImage(tempProfile.Fruit, "fruit/") as ImageSource;
                }
            }

            nameTextBox.Focus();
        }

        private void ResetControls()
        {
            nameTextBox.Clear();
            townTextBox.Clear();
            messageTextBox.Clear();
            profileImg.Source = null;
            fcTextBox.Text = "0000-0000-0000";
            dcTextBox.Text = "0000-0000-0000";
            villagerListView.Items.Clear();
            fruitComboBox.SelectedItem = null;
            VillagerViewModel.Reset();
        }

        private void LoadFruit(string fruit)
        {
            if (string.IsNullOrEmpty(fruit))
                fruit = "cherry";

            if (fruit.Equals("apple", StringComparison.CurrentCultureIgnoreCase))
                fruitComboBox.SelectedItem = appleCBI;
            else if (fruit.Equals("peach", StringComparison.CurrentCultureIgnoreCase))
                fruitComboBox.SelectedItem = peachCBI;
            else if (fruit.Equals("pear", StringComparison.CurrentCultureIgnoreCase))
                fruitComboBox.SelectedItem = pearCBI;
            else if (fruit.Equals("orange", StringComparison.CurrentCultureIgnoreCase))
                fruitComboBox.SelectedItem = orangeCBI;
            else
                fruitComboBox.SelectedItem = cherryCBI;
        }

        private string GetSelectedFruit()
        {
            string fruit = string.Empty;

            if (fruitComboBox.SelectedItem.Equals(appleCBI))
                fruit = "apple";
            else if (fruitComboBox.SelectedItem.Equals(pearCBI))
                fruit = "pear";
            else if (fruitComboBox.SelectedItem.Equals(cherryCBI))
                fruit = "cherry";
            else if (fruitComboBox.SelectedItem.Equals(orangeCBI))
                fruit = "orange";
            else
                fruit = "peach";

            return fruit;
        }

        private void DeleteVillager()
        {
            // Make sure there is a selection
            if (villagerListView.SelectedItem == null)
            {
                Globals.MsgBox.Show(ParentWindow, "You haven't selected a villager from the list.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
                return;
            }

            if (villagerListView.Items.Count == 0)
                return;

            // Check if this villager has any notices linked to it
            Profile tempProfile = Globals.SettingsWindow.profileListView.SelectedItem as Profile;
            Villager villager = villagerListView.SelectedItem as Villager;
            var matches = tempProfile.Notices.FirstOrDefault(item => item.Name == villager.Name);

            if (matches != null)
            {
                if (Globals.MsgBox.Show(ParentWindow, "Are you sure you want to remove this villager?\nThe notices linked to it will disappear", "Confirmation", MessageBoxButton.YesNo, MessageBoxIconType.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Remove the villager from the list and any notices linked to it
                        for (int i = 0; i < tempProfile.Notices.Count; i++)
                            if (tempProfile.Notices[i].Name.Equals(villager.Name))
                                Globals.RemoveNotice(tempProfile.Notices[i]);
                        Globals.SaveProfiles();
                        villagerListView.Items.Remove(villagerListView.SelectedItem);
                        return;
                    }
                    catch (Exception ex) { Globals.Logger.Error("Unable to remove villager and/or notices linked to it : " + ex.Message); return; }
                }
            }

            if (Globals.MsgBox.Show(ParentWindow, "Are you sure you want to remove this villager?", "Confirmation", MessageBoxButton.YesNo, MessageBoxIconType.Question) == MessageBoxResult.Yes)
            {
                try { villagerListView.Items.Remove(villagerListView.SelectedItem); }
                catch (Exception ex) { Globals.Logger.Error("Unable to remove villager and/or notices linked to it : " + ex.Message); }
            }
        }

        #region Control Events
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (Action == CAction.New)
            {
                try
                {
                    if (fruitComboBox.SelectedItem == null)
                    {
                        Globals.MsgBox.Show(ParentWindow, "You haven't selected a fruit!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Info);
                        return;
                    }

                    if (Globals.Main.Profiles != null)
                    {
                        if (Globals.Main.Profiles.Count != 0)
                        {
                            if (Globals.Main.Profiles.Find(x => x.FC == fcTextBox.Text) != null)
                            {
                                Globals.MsgBox.Show(ParentWindow, "There is already a profile with that FC!", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                                return;
                            }
                        }
                    }

                    List<Villager> villagers = new List<Villager>();
                    for (int i = 0; i < villagerListView.Items.Count; i++)
                        villagers.Add(villagerListView.Items[i] as Villager);

                    Profile tempProfile = new Profile(nameTextBox.Text, townTextBox.Text, GetSelectedFruit(), fcTextBox.Text, dcTextBox.Text, messageTextBox.Text, villagers);

                    if (profileImg.Source != null)
                        tempProfile.ProfileImagePath = Globals.ImgConvert.ConvertToString(profileImg.Source);

                    Globals.Main.Profiles.Add(tempProfile);
                    Globals.SetProfile(tempProfile);
                    tempProfile = null;
                    Globals.SaveProfiles();
                }
                catch (Exception ex)
                {
                    Globals.MsgBox.Show(ParentWindow, "Error creating new profile, check the log for more details", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                    Globals.Logger.Error("Error creating new profile " + ex.Message);
                }
            }

            else
            {
                try
                {
                    if (Globals.SettingsWindow.profileListView.SelectedItem != null)
                    {
                        int index = Globals.Main.Profiles.FindIndex(x => x == Globals.SettingsWindow.profileListView.SelectedItem as Profile);
                        Profile town = Globals.Main.Profiles[index];

                        if (Globals.Main.Profiles.Find(x => x.FC == fcTextBox.Text) != null)
                        {
                            if (!town.Mayor.Equals(nameTextBox.Text))
                            {
                                Globals.MsgBox.Show(ParentWindow, "There is already a profile with that FC!", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                                return;
                            }
                        }

                        town.Villagers.Clear();
                        for (int i = 0; i < villagerListView.Items.Count; i++)
                        {
                            Villager villager = villagerListView.Items[i] as Villager;
                            town.Villagers.Add(villager);
                        }

                        town.Mayor = nameTextBox.Text;
                        town.Town = townTextBox.Text;
                        town.Fruit = GetSelectedFruit();
                        town.FC = fcTextBox.Text;
                        town.TagLine = messageTextBox.Text;

                        if (profileImg.Source != null)
                            town.ProfileImagePath = Globals.ImgConvert.ConvertToString(profileImg.Source);

                        Globals.SetProfile(town);
                        Globals.Main.Profiles[index] = town;
                        Globals.SaveProfiles();
                    }
                }
                catch (Exception ex) { Globals.Logger.Error("Error saving profile " + ex.Message); }
            }
            ParentWindow.HideWindow();
            ParentWindow.Owner.Activate();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (villagerListView.Items.Count > 10)
            {
                Globals.MsgBox.Show(ParentWindow, "You can only have a maximum of 10 villagers in your town.", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                return;
            }

            if (string.IsNullOrEmpty(typeComboBox.Text))
                Globals.MsgBox.Show(ParentWindow, "You haven't entered a villager personality.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
            else if (string.IsNullOrEmpty(speciesComboBox.Text))
                Globals.MsgBox.Show(ParentWindow, "You haven't entered a villager species.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
            else if (string.IsNullOrEmpty(nameComboBox.Text))
                Globals.MsgBox.Show(ParentWindow, "You haven't entered a villager name.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);

            else
            {
                if (villagerListView.Items.Count != 0)
                {
                    for (int i = 0; i < villagerListView.Items.Count; i++)
                    {
                        Villager villager = villagerListView.Items[i] as Villager;
                        if (villager.Name.Equals(nameComboBox.Text))
                        {
                            Globals.MsgBox.Show(ParentWindow, "You can't enter the same villager twice!", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                            return;
                        }
                    }

                    for (int j = 0; j < VillagerViewModel.Villagers.Count; j++)
                        if (VillagerViewModel.SelectedName.Equals(VillagerViewModel.Villagers[j].Name))
                            villagerListView.Items.Add(new Villager(VillagerViewModel.SelectedName, VillagerViewModel.SelectedPersonality, VillagerViewModel.SelectedSpecies, VillagerViewModel.Villagers[j].Birthday));
                }

                else
                {
                    for (int j = 0; j < VillagerViewModel.Villagers.Count; j++)
                        if (VillagerViewModel.SelectedName.Equals(VillagerViewModel.Villagers[j].Name))
                            villagerListView.Items.Add(new Villager(VillagerViewModel.SelectedName, VillagerViewModel.SelectedPersonality, VillagerViewModel.SelectedSpecies, VillagerViewModel.Villagers[j].Birthday));
                }
            }
            VillagerViewModel.Reset();
        }

        private void deleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DeleteVillager();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            ParentWindow.HideWindow();
            ParentWindow.Owner.Activate();
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression be = nameTextBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void townTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression ba = townTextBox.GetBindingExpression(TextBox.TextProperty);
            ba.UpdateSource();
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression be = nameTextBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void townTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression ba = townTextBox.GetBindingExpression(TextBox.TextProperty);
            ba.UpdateSource();
        }

        private void fcTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bi = fcTextBox.GetBindingExpression(TextBox.TextProperty);
            bi.UpdateSource();
        }

        private void fcTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression bi = fcTextBox.GetBindingExpression(TextBox.TextProperty);
            bi.UpdateSource();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteVillager();
        }

        private void dcTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bu = dcTextBox.GetBindingExpression(TextBox.TextProperty);
            bu.UpdateSource();
        }

        private void profile_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (villagerListView.SelectedItem != null)
                villagerListView.SelectedItem = null;
        }

        private void profile_Loaded(object sender, RoutedEventArgs e)
        {
            BindingExpression be = nameTextBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();

            BindingExpression ba = townTextBox.GetBindingExpression(TextBox.TextProperty);
            ba.UpdateSource();

            BindingExpression bi = fcTextBox.GetBindingExpression(TextBox.TextProperty);
            bi.UpdateSource();

            BindingExpression bu = dcTextBox.GetBindingExpression(TextBox.TextProperty);
            bu.UpdateSource();
        }
        #endregion
    }
}
