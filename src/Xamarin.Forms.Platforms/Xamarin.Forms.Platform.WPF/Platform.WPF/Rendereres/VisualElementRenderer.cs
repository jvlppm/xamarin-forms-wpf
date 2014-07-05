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

    public class VisualElementRenderer<TModel, TView> : UserControl, IWPFRenderer
        where TModel : VisualElement
    {
        #region Static
        // Using a DependencyProperty as the backing store for Model.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register("Model", typeof(TModel), typeof(VisualElementRenderer<TModel, TView>), new PropertyMetadata(null, Model_ChangedCallback));

        static void Model_ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rend = d as VisualElementRenderer<TModel, TView>;

            var oldModel = e.OldValue as TModel;
            if (oldModel != null)
                rend.UnloadModel(oldModel);

            var newModel = e.NewValue as TModel;
            if (newModel != null)
                rend.LoadModel(newModel);
        }
        #endregion

        #region Attributes
        StackDictionary<BindableProperty, Func<BindableProperty, bool>> Handlers;
        MultiDictionary<string, BindableProperty> HandledProperties;
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
        public VisualElementRenderer()
        {
            Handlers = new StackDictionary<BindableProperty, Func<BindableProperty, bool>>();
            HandledProperties = new MultiDictionary<string, BindableProperty>();

            HandleProperty(VisualElement.BackgroundColorProperty, Handle_BackgroundColorProperty);
            HandleProperty(VisualElement.IsEnabledProperty, Handle_IsEnabledProperty);
            HandleProperty(VisualElement.IsVisibleProperty, Handle_IsVisibleProperty);
            HandleProperty(VisualElement.InputTransparentProperty, Handle_InputTransparentProperty);
            HandleProperty(VisualElement.OpacityProperty, Handle_OpacityProperty);
        }
        #endregion

        #region Methods
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

        protected void HandleProperty(BindableProperty property, Func<BindableProperty, bool> handler)
        {
            HandledProperties.Add(property.PropertyName, property);
            Handlers.Add(property, handler);
        }

        protected virtual void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Xamarin.Forms: Unfortunately we don't know which
            // property did change, if they have the same name.
            foreach (var property in HandledProperties[e.PropertyName])
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
