using ConwaysLife.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysLife.ViewModels
{
    internal class Board
    {
        public ObservableCollection<ObservableCollection<Cell>> Cells { get; set; }
        public int Columns { get; set; } = 100;
        public int Rows { get; set; } = 50; 
        public Board()
        {
            InitCells();
        }
        private void InitCells()
        {
          
            Cells = new ObservableCollection<ObservableCollection<Cell>>();
            for (int y = 0; y < Rows; y++)
            {
                var cellsColumn = new ObservableCollection<Cell>(); 
                Cells.Add(cellsColumn);
                for (int x = 0; x < Columns; x++)
                {
                    
                    cellsColumn.Add(new Cell());
                }
            }
            for (int y = 0; y < Rows; y++)
            {
               
                for (int x = 0; x < Columns; x++)
                {
                    SetCellNeighbors(y, x);
                }
            }
        }
        
        private void SetCellNeighbors(int yp, int xp)
        {
            Cells[yp][xp].Neighbors = new List<Cell>();
            for (int y = -1; y <= 1; y++)
            {
                int yc = yp + y;
                if(yc==-1)
                {
                    yc = Rows-1;
                }
                else if(yc==Rows)
                {
                    yc = 0;
                }
                for (int x = -1; x <= 1; x++)
                {
                    int xc = xp + x;
                    if(xc==-1)
                    { 
                        xc = Columns-1; 
                    }
                    else if(xc==Columns)
                    {
                        xc=0;
                    }

                    if(x==0&&y==0)
                    {
                        continue;
                    }
                    else
                    {
                        var neighbor = Cells[yc][xc];
                        Cells[yp][xp].Neighbors.Add(neighbor);
                    }
                }
            }
        }
        private bool GetCellNextLife(bool isAlive, int aliveNeighborCells)
        {
            int n = aliveNeighborCells;
            switch (n)
            {
                case 2:
                    return isAlive;
                case 3:
                    return true;
                default:
                    return false;
            }
          
        }

        public void SetCellsLifeRandom()
        {
            var rand = new Random();
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Cells[y][x].IsAlive = rand.Next(0, 2) == 1;
                }
            }
            
        }
        public void SetCellsLifeDead()
        {
            var rand = new Random();
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    Cells[y][x].IsAlive = false;
                }
            }

        }
        public void SetCellsLifeNext()
        {
            bool[,] nextLife = new bool[Rows, Columns]; 
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    bool isAlive = Cells[y][x].IsAlive;
                    int aliveNeighbors = Cells[y][x].Neighbors.Where(c=>c.IsAlive).Count();
                    nextLife[y, x] = GetCellNextLife(isAlive, aliveNeighbors);
                }
            }
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {

                    Cells[y][x].IsAlive = nextLife[y, x];
                }
            }
        }
    }
}
