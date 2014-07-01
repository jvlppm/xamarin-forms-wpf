using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

[assembly: ExportRenderer(typeof(VisualElement), typeof(Xamarin.Forms.Platform.WPF.Rendereres.VisualElementRenderer))]
namespace Xamarin.Forms.Platform.WPF.Rendereres
{
    public class VisualElementRenderer : VisualElementRenderer<VisualElement, System.Windows.FrameworkElement> { }

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

        public TModel Model
        {
            get { return (TModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Model.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(TModel), typeof(VisualElementRenderer<TModel, T>), new PropertyMetadata(null, Model_ChangedCallback));

        static void Model_ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rend = d as VisualElementRenderer<TModel, T>;

            var oldModel = e.OldValue as TModel;
            if(oldModel != null)
                rend.UnloadModel(oldModel);

            var newModel = e.NewValue as TModel;
            if (newModel != null)
                rend.LoadModel(newModel);
        }

        public System.Windows.FrameworkElement Element { get { return this; } }
        public new T Content
        {
            get { return (T)base.Content; }
            set { base.Content = value; }
        }

        public VisualElementRenderer()
        {
            HandleProperty(VisualElement.BackgroundColorProperty, Handle_BackgroundColorProperty);
            HandleProperty(VisualElement.IsEnabledProperty, Handle_IsEnabledProperty);
            HandleProperty(VisualElement.IsVisibleProperty, Handle_IsVisibleProperty);
            HandleProperty(VisualElement.InputTransparentProperty, Handle_InputTransparentProperty);
            HandleProperty(VisualElement.OpacityProperty, Handle_OpacityProperty);
        }

        protected virtual void LoadModel(TModel model)
        {
            model.PropertyChanged += OnPropertyChanged;
            foreach (var handler in Handlers)
                handler.Value(handler.Key);
        }

        protected virtual void UnloadModel(TModel model)
        {
            model.PropertyChanged -= OnPropertyChanged;
        }

        #region Property Handlers
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

        protected virtual bool Handle_BackgroundColorProperty(BindableProperty property)
        {
            Background = Model.BackgroundColor.ToWPFBrush();
            return true;
        }

        protected virtual bool Handle_IsEnabledProperty(BindableProperty property)
        {
            IsEnabled = Model.IsEnabled;
            return true;
        }

        protected virtual bool Handle_IsVisibleProperty(BindableProperty property)
        {
            Visibility = Model.IsVisible ? System.Windows.Visibility.Visible
                                         : System.Windows.Visibility.Hidden;
            return true;
        }

        protected virtual bool Handle_InputTransparentProperty(BindableProperty property)
        {
            IsHitTestVisible = !Model.InputTransparent;
            return true;
        }

        protected virtual bool Handle_OpacityProperty(BindableProperty property)
        {
            Opacity = Model.Opacity;
            return true;
        }
        #endregion
    }
}
