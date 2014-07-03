[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.Image), typeof(Xamarin.Forms.Platform.WPF.Rendereres.ImageRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.WPF;
    using Xamarin.Forms.Platform.WPF.Rendereres;

    public class ImageRenderer : ViewRenderer<Image, System.Windows.Controls.Image>
    {
        CancellationTokenSource _imageLoadCancellationSource;

        public ImageRenderer()
        {
            Content = new System.Windows.Controls.Image();
            HandleProperty(Image.SourceProperty, Handle_SourceProperty);
            HandleProperty(Image.AspectProperty, Handle_AspectProperty);
            HandleProperty(Image.IsOpaqueProperty, Handle_IsOpaqueProperty);
        }

        protected virtual bool Handle_SourceProperty(BindableProperty property)
        {
            if (_imageLoadCancellationSource != null)
                _imageLoadCancellationSource.Cancel();
            _imageLoadCancellationSource = new CancellationTokenSource();

            LoadImageAsync(_imageLoadCancellationSource.Token);
            return true;
        }

        protected virtual bool Handle_IsOpaqueProperty(BindableProperty property)
        {
            // TODO: Disable image opacity when Model.IsOpaque
            return true;
        }

        protected virtual bool Handle_AspectProperty(BindableProperty property)
        {
            switch(Model.Aspect)
            {
                case Aspect.AspectFill:
                    Content.StretchDirection = System.Windows.Controls.StretchDirection.DownOnly;
                    Content.Stretch = System.Windows.Media.Stretch.UniformToFill;
                    break;

                default:
                case Aspect.AspectFit:
                    Content.StretchDirection = System.Windows.Controls.StretchDirection.DownOnly;
                    Content.Stretch = System.Windows.Media.Stretch.Uniform;
                    break;

                case Aspect.Fill:
                    Content.StretchDirection = System.Windows.Controls.StretchDirection.Both;
                    Content.Stretch = System.Windows.Media.Stretch.Fill;
                    break;
            }

            return true;
        }

        static async Task<Stream> LoadStreamAsync(ImageSource source, CancellationToken cancellation)
        {
            var fileSource = source as FileImageSource;
            if (fileSource != null)
                return await Device.PlatformServices.GetStreamAsync(new System.Uri(fileSource.File), cancellation);

            var uriSource = source as UriImageSource;
            if (uriSource != null)
                return await Device.PlatformServices.GetStreamAsync(uriSource.Uri, cancellation);

            var streamSource = source as StreamImageSource;
            if (streamSource != null)
                return await streamSource.GetStreamAsync(cancellation);

            throw new NotImplementedException("Not supported image source");
        }

        async void LoadImageAsync(CancellationToken cancellation)
        {
            var loadImageCompletion = new TaskCompletionSource<bool>();
            EventHandler<System.Windows.ExceptionRoutedEventArgs> imageFailed = (s, e) => { loadImageCompletion.TrySetException(e.ErrorException); };
            EventHandler<System.Windows.Media.ExceptionEventArgs> bmiLoadError = (s, e) => { loadImageCompletion.TrySetException(e.ErrorException); };
            EventHandler loadCompleted = delegate { loadImageCompletion.TrySetResult(true); };

            System.Windows.Media.Imaging.BitmapImage bmi = null;
            Model.IsLoading = true;
            Content.Source = null;

            try
            {
                var stream = await LoadStreamAsync(Model.Source, cancellation);

                bmi = new System.Windows.Media.Imaging.BitmapImage();
                Content.ImageFailed += imageFailed;
                bmi.DownloadFailed += bmiLoadError;
                bmi.DecodeFailed += bmiLoadError;
                bmi.DownloadCompleted += loadCompleted;

                bmi.BeginInit();
                bmi.StreamSource = stream;
                bmi.EndInit();
                if (bmi.IsDownloading)
                    await loadImageCompletion.Task;
                if (bmi.PixelWidth > 0 && bmi.PixelHeight > 0)
                    Content.Source = bmi;
            }
            catch (OperationCanceledException) { return; }
            catch { }
            finally
            {
                Content.ImageFailed -= imageFailed;
                if (bmi != null)
                {
                    bmi.DownloadFailed -= bmiLoadError;
                    bmi.DecodeFailed -= bmiLoadError;
                    bmi.DownloadCompleted -= loadCompleted;
                }
            }
            Model.IsLoading = false;
        }
    }
}
