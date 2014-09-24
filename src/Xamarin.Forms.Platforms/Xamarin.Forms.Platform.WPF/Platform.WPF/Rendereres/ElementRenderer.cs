[assembly: Xamarin.Forms.Platform.WPF.ExportRenderer(typeof(Xamarin.Forms.Element), typeof(Xamarin.Forms.Platform.WPF.Rendereres.ElementRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.WPF;

    public class ElementRenderer : ElementRenderer<Element, System.Windows.FrameworkElement> { }

    public class ElementRenderer<TModel, TView> : UserControl, IWPFRenderer
        where TModel : Element
    {
        #region Static
        // Using a DependencyProperty as the backing store for Model.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(TModel), typeof(ElementRenderer<TModel, TView>), new PropertyMetadata(null, Model_ChangedCallback));

        static void Model_ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rend = d as ElementRenderer<TModel, TView>;

            var oldModel = e.OldValue as TModel;
            if (oldModel != null)
                rend.UnloadModel(oldModel);

            var newModel = e.NewValue as TModel;
            if (newModel != null)
                rend.LoadModel(newModel);
        }
        #endregion

        #region Attributes
        MultiValueDictionary<BindableProperty, Func<BindableProperty, bool>> Handlers;
        MultiValueDictionary<string, BindableProperty> HandledProperties;
        #endregion

        #region Properties
        Element IWPFRenderer.Model
        {
            set { Model = (TModel)value; }
            get { return Model; }
        }

        public TModel Model
        {
            get { return (TModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        public System.Windows.FrameworkElement Element { get { return this; } }
        public new TView Content
        {
            get { return (TView)base.Content; }
            set { base.Content = value; }
        }
        #endregion

        #region Constructors
        public ElementRenderer()
        {
            Handlers = new MultiValueDictionary<BindableProperty, Func<BindableProperty, bool>>();
            HandledProperties = new MultiValueDictionary<string, BindableProperty>();
        }
        #endregion

        #region Methods
        protected virtual void LoadModel(TModel model)
        {
            model.PropertyChanged += OnPropertyChanged;
            foreach (var handlers in this.Handlers)
            {
                foreach (var handler in handlers.Value)
                {
                    handler(handlers.Key);
                }
            }
        }

        protected virtual void UnloadModel(TModel model)
        {
            model.PropertyChanged -= OnPropertyChanged;
        }

        protected void HandleProperty(BindableProperty property, Func<BindableProperty, bool> handler)
        {
            HandledProperties.Add(property.PropertyName, property);
            Handlers.Add(property, handler);
        }

        protected virtual void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Xamarin.Forms: Unfortunately we don't know which
            // property did change, if they have the same name.
            IReadOnlyCollection<BindableProperty> properties;
            if (!HandledProperties.TryGetValue(e.PropertyName, out properties))
            {
                return;
            }

            foreach (var property in properties)
            {
                Handlers[property].FirstOrDefault(handle => handle(property));
            }
        }
        #endregion
    }
}
