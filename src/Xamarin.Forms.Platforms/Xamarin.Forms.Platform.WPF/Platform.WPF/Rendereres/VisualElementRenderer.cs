using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(VisualElement), typeof(Xamarin.Forms.Platform.WPF.Rendereres.VisualElementRenderer<VisualElement, object>))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class VisualElementRenderer<TModel, T> : UserControl, IWPFRenderer
        where TModel : VisualElement
    {
        StackDictionary<BindableProperty, Func<BindableProperty, bool>> Handlers = new StackDictionary<BindableProperty, Func<BindableProperty, bool>>();
        IDictionary<string, BindableProperty> HandledProperties = new Dictionary<string, BindableProperty>();

        Element IWPFRenderer.Model
        {
            set { Model = (TModel)value; }
            get { return Model; }
        }

        TModel _model;
        public TModel Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                    UnloadModel(_model);
                _model = value;
                LoadModel(value);
            }
        }
        public System.Windows.FrameworkElement Element { get { return this; } }
        public new T Content
        {
            get { return (T)base.Content; }
            set { base.Content = value; }
        }

        protected virtual void LoadModel(TModel model)
        {
            _model.PropertyChanged += OnPropertyChanged;
            foreach (var handler in Handlers)
                handler.Value(handler.Key);
        }

        protected virtual void UnloadModel(TModel model)
        {
            _model.PropertyChanged -= OnPropertyChanged;
        }

        protected void HandleProperty(BindableProperty property, Func<BindableProperty, bool> handler)
        {
            if (!property.DeclaringType.IsAssignableFrom(typeof(TModel)))
                throw new ArgumentException("Specified property is not contained in " + typeof(T).Name);

            HandledProperties[property.PropertyName] = property;
            Handlers.Add(property, handler);
        }

        protected virtual void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

            BindableProperty property;
            if (HandledProperties.TryGetValue(e.PropertyName, out property))
                Handlers[property].FirstOrDefault(handle => handle(property));
        }
    }
}
