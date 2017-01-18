using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;

namespace Animal_Xing_Planner
{
    /// <summary>
    /// Viewmodel for comboboxes in ProfileUC
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        private ProfileUC owner;

        public ViewModel(ProfileUC owner)
        {
            this.owner = owner;

            var stopWatch = new Stopwatch();
            stopWatch.Start();
          
            Villagers = XmlHandler.LoadVillagers();

            stopWatch.Stop();
            var executionTime = stopWatch.Elapsed;

            Names = new CustomObservableCollection<string>();
            Personalities = new CustomObservableCollection<string>();
            Species = new CustomObservableCollection<string>();

            for (int i = 0; i < Villagers.Count; i++)
            {
                Names.Add(Villagers[i].Name);

                if (!Personalities.Contains(Villagers[i].Personality))
                    Personalities.Add(Villagers[i].Personality);
            }

            Sort(Personalities);
        }

        public IList<Villager> Villagers
        {
            get;
            private set;
        }

        public CustomObservableCollection<string> Names
        {
            get;
            private set;
        }

        public CustomObservableCollection<string> Species
        {
            get;
            private set;
        }

        public CustomObservableCollection<string> Personalities
        {
            get;
            private set;
        }

        private string selectedName;
        public string SelectedName
        {
            get
            {
                return selectedName;
            }
            set
            {
                selectedName = value;
                OnPropertyChanged("Names");
            }
        }

        private string selectedSpecies;
        public string SelectedSpecies
        {
            get
            {
                return selectedSpecies;
            }
            set
            {
                selectedSpecies = value;

                // Loop through all villagers
                for (int i = 0; i < Villagers.Count; i++)
                {
                    // Make sure the villagers species and personality are the same as the selected values
                    if (Villagers[i].Species.Equals(SelectedSpecies) && Villagers[i].Personality.Equals(SelectedPersonality))
                            if (!Names.Contains(Villagers[i].Name))
                                Names.Add(Villagers[i].Name);
                }

                Sort(Names);
                owner.nameComboBox.IsEnabled = true;
                OnPropertyChanged("Species");
            }
        }

        private string selectedPersonality;
        public string SelectedPersonality
        {
            get
            {
                return selectedPersonality;
            }
            set
            {
                selectedPersonality = value;

                Names.Clear();
                Species.Clear();

                // Loop through all villagers
                for (int i = 0; i < Villagers.Count; i++)
                {
                    // Make sure the villagers personality is the same as the selected value
                    if (Villagers[i].Personality.Equals(SelectedPersonality))
                        if (!Species.Contains(Villagers[i].Species))
                            Species.Add(Villagers[i].Species);
                }

                Sort(Species);

                owner.speciesComboBox.IsEnabled = true;
                owner.nameComboBox.IsEnabled = false;
                owner.nameComboBox.Text = string.Empty;
                SelectedName = string.Empty;

                OnPropertyChanged("Personality");
            }
        }

        /// <summary>
        /// Reset to initial values
        /// </summary>
        public void Reset()
        {
            if (owner == null)
                return;

            owner.typeComboBox.Text = string.Empty;
            SelectedPersonality = string.Empty;

            owner.speciesComboBox.IsEnabled = false;
            owner.speciesComboBox.Text = string.Empty;
            SelectedSpecies = string.Empty;

            owner.nameComboBox.IsEnabled = false;
            owner.nameComboBox.Text = string.Empty;
            SelectedName = string.Empty;
        }

        /// <summary>
        /// Sort a CustomObservableCollection
        /// </summary>
        /// <param name="list">List to sort</param>
        private void Sort(CustomObservableCollection<string> list)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(list);
            dataView.SortDescriptions.Clear();
            dataView.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
            dataView.Refresh();
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }

    /// <summary>
    /// ObservableCollection for comboboxes in ProfileUC
    /// </summary>
    public class CustomObservableCollection<T> : ObservableCollection<T>
    {
        public CustomObservableCollection()
          : base()
        {
        }

        public CustomObservableCollection(IEnumerable<T> collection)
          : base(collection)
        {
        }

        public CustomObservableCollection(List<T> list)
          : base(list)
        {
        }

        public void Repopulate(IEnumerable<T> collection)
        {
            Items.Clear();
            foreach (var item in collection)
                Items.Add(item);

            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
