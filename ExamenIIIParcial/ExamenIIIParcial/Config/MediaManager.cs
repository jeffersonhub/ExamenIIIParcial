using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace ExamenIIIParcial.Config
{
    public class MediaManager
    {

    

        public string ErrorMessage { get; private set; }
        public string Path { get; private set; }
        public MediaFile MediaFile { get; private set; }
        public bool IsSuccess { get; private set; }
        public ImageSource Image { get; private set; }
        public byte [] ByteImage{ get; private set; }
        private MemoryStream  MemStream {  get;  set; }


        public MediaManager()
        {
            MemStream = new MemoryStream();
        }
        public async Task<bool> TakePicture()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                ErrorMessage = "La Camara No esta disponible en este dispositivo.";
                return IsSuccess = false;
            }

            MediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Medium,
               
                
            });

            if (MediaFile == null)
            {
                ErrorMessage = "La archivo de photo esta vacio.";
                return IsSuccess = false;
            }
               
            Path = MediaFile.Path;

            Image = ImageSource.FromStream(() => MediaFile.GetStream());
            MediaFile.GetStream().CopyTo(MemStream);
            ByteImage = MemStream.ToArray();
            return IsSuccess = true;
        }

        public async Task<bool> PickPicture()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                ErrorMessage = "No se puede acceder a  la galeria.";
                return IsSuccess=false;
            }
            else
            {
                PickMediaOptions mediaOption = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                MediaFile = await CrossMedia.Current.PickPhotoAsync();
                if (MediaFile == null)
                {
                    ErrorMessage = "La imagen seleccionada es nula.";
                    return IsSuccess = false;
                }
                

                Path = MediaFile.Path;
                Image = ImageSource.FromStream(() => MediaFile.GetStream());
                 MediaFile.GetStream().CopyTo(MemStream);
                ByteImage = MemStream.ToArray();
                return IsSuccess = true; 
            }
        }

        public static ImageSource ConvertByteArrayToImage(byte[] image)
        {
            return ImageSource.FromStream(()=> { return (Stream)new MemoryStream(image); });
        }
        
     
    }

    

}
