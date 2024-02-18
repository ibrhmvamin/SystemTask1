using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SystemTask1.Commands;

namespace SystemTask1.ViewModel
{
    public class MainWindowViewModel:INotifyPropertyChanged
        {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _textBoxText;

        public string TextBoxText
        {
            get { return _textBoxText; }
            set
            {
                if (_textBoxText != value)
                {
                    _textBoxText = value;
                    OnPropertyChanged(nameof(TextBoxText));
                }
            }
        }
        private ObservableCollection<Process> _blacklistProcesses;
        public ObservableCollection<Process> BlacklistProcesses
        {
            get { return _blacklistProcesses; }
            set
            {
                if (_blacklistProcesses != value)
                {
                    _blacklistProcesses = value;
                    OnPropertyChanged(nameof(BlacklistProcesses));
                }
            }
        }

        public TextBox txtbox { get; set; }
            public Process SelectedProcess { get; set; }
            public Process[] Processes { get; }
            public ICommand CreateProcessCommand { get; set; }
            public ICommand EndProcessCommand { get; set; }
        public ICommand ToBlackListCommand { get; set; }

        public MainWindowViewModel()
            {
                Processes = Process.GetProcesses();
                CreateProcessCommand = new RelayCommand(NewProcess);
                EndProcessCommand = new RelayCommand(EndedProcess);
            ToBlackListCommand = new RelayCommand(ToBlackList);
            BlacklistProcesses = new ObservableCollection<Process>();

        }
        public void UpdateSelectedProcess(Process process)
        {
            SelectedProcess = process;
            OnPropertyChanged(nameof(SelectedProcess));
        }
        public void ToBlackList(object? param)
        {
            try
            {
                if (SelectedProcess != null)
                {
                    BlacklistProcesses.Add(SelectedProcess);
                    MessageBox.Show("Process added successfully.");
                }
                else
                {
                    MessageBox.Show("No process selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void NewProcess(object? param)
            {
                try
                {
                if (!BlacklistProcesses.Contains(SelectedProcess))
                {
                    string processName = TextBoxText;
                    Process.Start(processName);
                }

            }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            public void EndedProcess(object? param)
            {

                try
                {
                    if (SelectedProcess != null)
                    {
                        SelectedProcess.Kill();
                        MessageBox.Show("Process ended successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No process selected.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
