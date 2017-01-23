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
    public partial class ProfileUC
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
                if (Globals.SettingsWindow.ProfileListView.SelectedItem != null)
                {
                    Profile tempProfile = Globals.SettingsWindow.ProfileListView.SelectedItem as Profile;

                    for (int i = 0; i < tempProfile.Villagers.Count; i++)
                        VillagerListView.Items.Add(tempProfile.Villagers[i]);
                    VillagerListView.UpdateLayout();

                    NameTextBox.Text = tempProfile.Mayor;
                    TownTextBox.Text = tempProfile.Town;
                    LoadFruit(tempProfile.Fruit);

                    if (!string.IsNullOrEmpty(tempProfile.Fc))
                        FcTextBox.Text = tempProfile.Fc;
                    if (!string.IsNullOrEmpty(tempProfile.Dc))
                        DcTextBox.Text = tempProfile.Dc;
                    MessageTextBox.Text = tempProfile.TagLine;

                    try
                    {
                        if (tempProfile.ProfileImagePath != null)
                            ProfileImage.Source = Globals.ImgConvert.ConvertFromString(tempProfile.ProfileImagePath) as ImageSource;

                        if (!string.IsNullOrEmpty(tempProfile.Fruit))
                            Globals.Main.FruitImage.Source = Globals.GetBitmapImage(tempProfile.Fruit, "fruit/");
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            NameTextBox.Focus();
        }

        private void ResetControls()
        {
            NameTextBox.Clear();
            TownTextBox.Clear();
            MessageTextBox.Clear();
            ProfileImage.Source = null;
            FcTextBox.Text = "0000-0000-0000";
            DcTextBox.Text = "0000-0000-0000";
            VillagerListView.Items.Clear();
            FruitComboBox.SelectedItem = null;
            VillagerViewModel.Reset();
        }

        private void LoadFruit(string fruit)
        {
            if (string.IsNullOrEmpty(fruit))
                fruit = "cherry";

            if (fruit.Equals("apple", StringComparison.CurrentCultureIgnoreCase))
                FruitComboBox.SelectedItem = AppleCbItem;
            else if (fruit.Equals("peach", StringComparison.CurrentCultureIgnoreCase))
                FruitComboBox.SelectedItem = PeachCbItem;
            else if (fruit.Equals("pear", StringComparison.CurrentCultureIgnoreCase))
                FruitComboBox.SelectedItem = PearCbItem;
            else if (fruit.Equals("orange", StringComparison.CurrentCultureIgnoreCase))
                FruitComboBox.SelectedItem = OrangeCbItem;
            else
                FruitComboBox.SelectedItem = CherryCbItem;
        }

        private string GetSelectedFruit()
        {
            string fruit;

            if (FruitComboBox.SelectedItem.Equals(AppleCbItem))
                fruit = "apple";
            else if (FruitComboBox.SelectedItem.Equals(PearCbItem))
                fruit = "pear";
            else if (FruitComboBox.SelectedItem.Equals(CherryCbItem))
                fruit = "cherry";
            else if (FruitComboBox.SelectedItem.Equals(OrangeCbItem))
                fruit = "orange";
            else
                fruit = "peach";

            return fruit;
        }

        private void DeleteVillager()
        {
            // Make sure there is a selection
            if (VillagerListView.SelectedItem == null)
            {
                Globals.MsgBox.Show(ParentWindow, "You haven't selected a villager from the list.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
                return;
            }

            if (VillagerListView.Items.Count == 0)
                return;

            // Check if this villager has any notices linked to it
            Profile tempProfile = Globals.SettingsWindow.ProfileListView.SelectedItem as Profile;
            Villager villager = VillagerListView.SelectedItem as Villager;
            var matches = tempProfile?.Notices.FirstOrDefault(item => villager != null && item.Name == villager.Name);

            if (matches != null)
            {
                if (Globals.MsgBox.Show(ParentWindow, "Are you sure you want to remove this villager?\nThe notices linked to him/her will disappear", "Confirmation", MessageBoxButton.YesNo, MessageBoxIconType.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Remove the villager from the list and any notices linked to it
                        for (int i = 0; i < tempProfile.Notices.Count; i++)
                            if (villager != null && tempProfile.Notices[i].Name.Equals(villager.Name))
                                Globals.RemoveNotice(tempProfile.Notices[i]);
                        Globals.SaveProfiles();
                        VillagerListView.Items.Remove(VillagerListView.SelectedItem);
                        return;
                    }
                    catch (Exception ex) { Globals.Logger.Error("Unable to remove villager and/or notices linked to it : " + ex.Message); return; }
                }
            }

            if (Globals.MsgBox.Show(ParentWindow, "Are you sure you want to remove this villager?", "Confirmation", MessageBoxButton.YesNo, MessageBoxIconType.Question) == MessageBoxResult.Yes)
            {
                try { VillagerListView.Items.Remove(VillagerListView.SelectedItem); }
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
                    if (FruitComboBox.SelectedItem == null)
                    {
                        Globals.MsgBox.Show(ParentWindow, "You haven't selected a fruit!", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Info);
                        return;
                    }

                    List<Villager> villagers = new List<Villager>();
                    for (int i = 0; i < VillagerListView.Items.Count; i++)
                        villagers.Add(VillagerListView.Items[i] as Villager);

                    Profile tempProfile = new Profile(NameTextBox.Text, TownTextBox.Text, GetSelectedFruit(), FcTextBox.Text, DcTextBox.Text, MessageTextBox.Text, villagers);

                    if (ProfileImage.Source != null)
                        tempProfile.ProfileImagePath = Globals.ImgConvert.ConvertToString(ProfileImage.Source);

                    Globals.Main.Profiles.Add(tempProfile);
                    Globals.SetProfile(tempProfile);
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
                    if (Globals.SettingsWindow.ProfileListView.SelectedItem != null)
                    {
                        int index = Globals.Main.Profiles.FindIndex(x => x == Globals.SettingsWindow.ProfileListView.SelectedItem as Profile);
                        Profile town = Globals.Main.Profiles[index];

                        if (Globals.Main.Profiles.Find(x => x.Fc == FcTextBox.Text) != null)
                        {
                            if (!town.Mayor.Equals(NameTextBox.Text))
                            {
                                Globals.MsgBox.Show(ParentWindow, "There is already a profile with that FC!", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                                return;
                            }
                        }

                        town.Villagers.Clear();
                        for (int i = 0; i < VillagerListView.Items.Count; i++)
                        {
                            Villager villager = VillagerListView.Items[i] as Villager;
                            town.Villagers.Add(villager);
                        }

                        town.Mayor = NameTextBox.Text;
                        town.Town = TownTextBox.Text;
                        town.Fruit = GetSelectedFruit();
                        town.Fc = FcTextBox.Text;
                        town.TagLine = MessageTextBox.Text;

                        if (ProfileImage.Source != null)
                            town.ProfileImagePath = Globals.ImgConvert.ConvertToString(ProfileImage.Source);

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
            if (VillagerListView.Items.Count > 10)
            {
                Globals.MsgBox.Show(ParentWindow, "You can only have a maximum of 10 villagers in your town.", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                return;
            }

            if (string.IsNullOrEmpty(TypeComboBox.Text))
                Globals.MsgBox.Show(ParentWindow, "You haven't entered a villager personality.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
            else if (string.IsNullOrEmpty(SpeciesComboBox.Text))
                Globals.MsgBox.Show(ParentWindow, "You haven't entered a villager species.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);
            else if (string.IsNullOrEmpty(NameComboBox.Text))
                Globals.MsgBox.Show(ParentWindow, "You haven't entered a villager name.", "Missing information", MessageBoxButton.OK, MessageBoxIconType.Warning);

            else
            {
                if (VillagerListView.Items.Count != 0)
                {
                    for (int i = 0; i < VillagerListView.Items.Count; i++)
                    {
                        Villager villager = VillagerListView.Items[i] as Villager;
                        if (villager != null && villager.Name.Equals(NameComboBox.Text))
                        {
                            Globals.MsgBox.Show(ParentWindow, "You can't enter the same villager twice!", "Error", MessageBoxButton.OK, MessageBoxIconType.Error);
                            return;
                        }
                    }

                    for (int j = 0; j < VillagerViewModel.Villagers.Count; j++)
                        if (VillagerViewModel.SelectedName.Equals(VillagerViewModel.Villagers[j].Name))
                            VillagerListView.Items.Add(new Villager(VillagerViewModel.SelectedName, VillagerViewModel.SelectedPersonality, VillagerViewModel.SelectedSpecies, VillagerViewModel.Villagers[j].Birthday));
                }

                else
                {
                    for (int j = 0; j < VillagerViewModel.Villagers.Count; j++)
                        if (VillagerViewModel.SelectedName.Equals(VillagerViewModel.Villagers[j].Name))
                            VillagerListView.Items.Add(new Villager(VillagerViewModel.SelectedName, VillagerViewModel.SelectedPersonality, VillagerViewModel.SelectedSpecies, VillagerViewModel.Villagers[j].Birthday));
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
            BindingExpression be = NameTextBox.GetBindingExpression(TextBox.TextProperty);
            be?.UpdateSource();
        }

        private void townTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression ba = TownTextBox.GetBindingExpression(TextBox.TextProperty);
            ba?.UpdateSource();
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression be = NameTextBox.GetBindingExpression(TextBox.TextProperty);
            be?.UpdateSource();
        }

        private void townTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression ba = TownTextBox.GetBindingExpression(TextBox.TextProperty);
            ba?.UpdateSource();
        }

        private void fcTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bi = FcTextBox.GetBindingExpression(TextBox.TextProperty);
            bi?.UpdateSource();
        }

        private void fcTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BindingExpression bi = FcTextBox.GetBindingExpression(TextBox.TextProperty);
            bi?.UpdateSource();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteVillager();
        }

        private void dcTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BindingExpression bu = DcTextBox.GetBindingExpression(TextBox.TextProperty);
            bu?.UpdateSource();
        }

        private void profile_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (VillagerListView.SelectedItem != null)
                VillagerListView.SelectedItem = null;
        }

        private void profile_Loaded(object sender, RoutedEventArgs e)
        {
            BindingExpression be = NameTextBox.GetBindingExpression(TextBox.TextProperty);
            be?.UpdateSource();

            BindingExpression ba = TownTextBox.GetBindingExpression(TextBox.TextProperty);
            ba?.UpdateSource();

            BindingExpression bi = FcTextBox.GetBindingExpression(TextBox.TextProperty);
            bi?.UpdateSource();

            BindingExpression bu = DcTextBox.GetBindingExpression(TextBox.TextProperty);
            bu?.UpdateSource();
        }
        #endregion
    }
}
