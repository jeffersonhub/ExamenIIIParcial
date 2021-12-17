using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ExamenIIIParcial.Config;
using ExamenIIIParcial.Models;
using Xamarin.Forms;

namespace ExamenIIIParcial.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {

        public Command SaveCommand { get; }
        public Command SelectMediaCommand { get; }

        string descripcion;
        
        byte[] fotoByteArray;
        ImageSource fotoRecibo;
        double monto;
        private DateTime fechaSelected;
        public DateTime FechaSelected
        {
            get { return fechaSelected.Date; }
            set
            {
                SetProperty(ref fechaSelected, value);   //Call INPC Interface when property changes, so the view will know it has to update
            }
        }


        public string Descripcion { get => descripcion; set { SetProperty(ref descripcion, value); } }
      

        public ImageSource FotoRecibo { get => fotoRecibo; set => SetProperty( ref fotoRecibo, value); }
        public double Monto { get => monto; set => SetProperty(ref monto,value); }

        Page Page;
        Pagos Pago;
        public EmployeesViewModel(Page page, Pagos pago)
        {
            
             Page = page;

             Pago = pago;
          
            LoadData();
            SelectMediaCommand = new Command(OnSelectedMedia);
            SaveCommand = new Command(async()=> {
                if (Validate())
                {
                    CargarDatos();
                    UserDialogs.Instance.ShowLoading("Guardando");
                    int respuesta = await DataBase.CurrentDB.Save(Pago);
                    if (respuesta == Constants.SUCCESS)
                    {
                        Constants.WasChange = true; //Variable bandera para determinar si se realizo un cambio
                        await Page.DisplayAlert("Información", "Guardado con éxito.", "Aceptar");
                        await Page.Navigation.PopAsync();
                    }
                    else
                        await Page.DisplayAlert("Información", "Error al guardar.", "Aceptar");
                    UserDialogs.Instance.HideLoading();
                }
                else
                    await Page.DisplayAlert("Advertensia", "Debe llenar todos los campos.", "Aceptar");
            });
        }

         void LoadData()
        {

            if (Pago.IdPago>0)
            {
                Title = "Actualizar pago";
                Descripcion = Pago.Descripcion;
                FechaSelected = Pago.Fecha;
                Monto = Pago.Monto;
                fotoByteArray = Pago.FotoRecibo;
                FotoRecibo = MediaManager.ConvertByteArrayToImage(Pago.FotoRecibo);
            }
          else
            {
                FechaSelected = DateTime.Now;
                Title = "Agregar pago";
                FotoRecibo = ImageSource.FromFile("perfil_default.png");
            }
        }
       
        bool Validate()
        {
            if (Monto >0 && !string.IsNullOrEmpty(Descripcion)
                && fotoByteArray!=null  )
                return true;
            else
                return false;
        }
        void CargarDatos()
        {
            Pago.Descripcion = Descripcion;
            Pago.Fecha = FechaSelected;
            Pago.FotoRecibo = fotoByteArray;
            Pago.Monto = Monto;
        }



        public async void OnSelectedMedia()
        {

            string action = await Page.DisplayActionSheet("Cargar desde:", "Cancelar", null, Constants.CAMARA, Constants.GALERIA);
            Console.WriteLine("Seleccionó " + action);

            if (action == Constants.CAMARA)
            {
                OnOpenGalery();

            }
            else if (action == Constants.GALERIA)
            {
                OnTakePhoto();
            }
        }
        private async void OnOpenGalery()
        {
            var media = new MediaManager();

            UserDialogs.Instance.ShowLoading("Cargando");
            bool isSuccess = await media.TakePicture();
            LoadPhoto(isSuccess, media);
            UserDialogs.Instance.HideLoading();
        }
     
        private async void OnTakePhoto()

        {
            var media = new MediaManager();
          
            UserDialogs.Instance.ShowLoading("Cargando");
            bool isSuccess = await media.PickPicture();
            LoadPhoto(isSuccess, media);
            UserDialogs.Instance.HideLoading();
        }

      
        private void LoadPhoto(bool isSucces, MediaManager media)
        {
            if (isSucces)
            {
                fotoByteArray = media.ByteImage;
                FotoRecibo = media.Image;
            }
        }
    }
    
}
