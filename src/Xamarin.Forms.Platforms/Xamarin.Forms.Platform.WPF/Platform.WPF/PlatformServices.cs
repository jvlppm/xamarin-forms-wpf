using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Xamarin.Forms.Platform.WPF
{
    class PlatformServices : IPlatformServices
    {
        HttpClient HttpClient = new HttpClient();

        Thread _uiThread;

        public bool IsInvokeRequired
        {
            get
            {
                lock (this)
                {
                    if (_uiThread == null)
                        Application.Current.Dispatcher.Invoke(delegate { _uiThread = Thread.CurrentThread; });
                }

                return Thread.CurrentThread != _uiThread;
            }
        }

        public void BeginInvokeOnMainThread(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action);
        }

        public ITimer CreateTimer(Action<object> callback, object state, int dueTime, int period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback)
        {
            return new ThreadTimer(callback);
        }

        public ITimer CreateTimer(Action<object> callback, object state, long dueTime, long period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback, object state, uint dueTime, uint period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public async Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (uri.IsFile)
                return await Task.Factory.StartNew(() => File.OpenRead(uri.AbsolutePath), cancellationToken);

            var response = await HttpClient.GetAsync(uri, cancellationToken);
            return await response.Content.ReadAsStreamAsync();
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            var scope = IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain;
            var isolatedStorage = System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(scope, null, null);
            return new IsolatedStorageFile(isolatedStorage);
        }

        public void OpenUriAction(Uri uri)
        {
            System.Diagnostics.Process.Start(uri.ToString());
        }

        public void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            Timer timer = null;
            TimerCallback timerCallback = delegate
            {
                if (!callback())
                    timer.Dispose();
            };

            timer = new Timer(timerCallback, null, interval, interval);
        }
    }
}
