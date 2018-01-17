using GestMoney.Clases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GestMoney.ViewModel
{
    class vmInicion : ObservableCollection<Factura>, INotifyPropertyChanged
    {
        #region Atributos
        private int selectedIndex;

        private int id;
        private string tipo;
        private string concepto;
        private decimal importe;
        private DateTime fecha_importe;
        private DateTime gc_fecha_creacion;
        private DateTime gc_fecha_modificacion;
        private ICommand addClientCommand;
        private ICommand clearCommand;
        #endregion

        #region Propiedades
        public int SelectedIndexOfCollection
        {
            get
            {
                return selectedIndex;
            }//Fin de get.
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndexOfCollection");

                //Activa el evento OnPropertyChanged de los atributos para refrescar las propiedades segun el indice seleccionado.
                OnPropertyChanged("Id");
                OnPropertyChanged("Name");
                OnPropertyChanged("LastName");
            }//Fin de set.
        }//Fin de propiedad Name.

        public int Id
        {
            get
            {
                if (this.SelectedIndexOfCollection > -1)
                {
                    return this.Items[this.SelectedIndexOfCollection].Id;
                }
                else
                {
                    return id;
                }
            }
            set { }
        }

        public string Tipo
        {
            get
            {
                if (this.SelectedIndexOfCollection > -1)
                {
                    return this.Items[this.SelectedIndexOfCollection].Tipo;
                }
                else
                {
                    return tipo;
                }
            }//Fin de get.
            set
            {
                if (this.SelectedIndexOfCollection > -1)
                {
                    this.Items[this.SelectedIndexOfCollection].Tipo = value;
                }
                else
                {
                    tipo = value;
                }
                OnPropertyChanged("Tipo");
            }
        }

        public string Concepto
        {
            get
            {
                if (this.SelectedIndexOfCollection > -1)
                {
                    return this.Items[this.SelectedIndexOfCollection].Concepto;
                }
                else
                {
                    return concepto;
                }
            }
            set
            {
                if (this.SelectedIndexOfCollection > -1)
                {
                    this.Items[this.SelectedIndexOfCollection].Concepto = value;
                }
                else
                {
                    concepto = value;
                }
                OnPropertyChanged("LastName");
            }
        }

        public ICommand AddClientCommand
        {
            get
            {
                return addClientCommand;
            }//Fin de get.
            set
            {
                addClientCommand = value;
            }//Fin de set.
        }//Fin de propiedad LastName.

        public ICommand ClearCommand
        {
            get
            {
                return clearCommand;
            }//Fin de get.
            set
            {
                clearCommand = value;
            }//Fin de set.
        }//Fin de propiedad LastName.
        #endregion

        #region Constructores
        public vmInicion()
        {
            //AddClientCommand = new CommandBase(param => this.AddClient());
            //ClearCommand = new CommandBase(new Action<Object>(ClearClient));
        }
        #endregion

        #region Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Metodos/Funcciones
        private void AddClient()
        {
            
        }

        private void ClearClient(object obj)
        {
            
        }
        #endregion

    }
}
