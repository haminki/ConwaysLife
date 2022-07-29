using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConwaysLife.Models
{
    internal class Cell : INotifyPropertyChanged
    {
        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }

            set 
            {
                if(isAlive!=value)
                {
                    isAlive = value;
                    OnPropertyChanged();
                }
            }

        }
        public List<Cell> Neighbors { get; set; }

        public Cell()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
