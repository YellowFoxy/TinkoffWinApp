using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace TinkoffWinApp.Controls.Carousel
{
    public class LoopingCarouselItem : SelectorItem
    {
        private const string PointerOverState = "PointerOver";
        private const string PointerOverSelectedState = "PointerOverSelected";
        private const string PressedState = "Pressed";
        private const string PressedSelectedState = "PressedSelected";
        private const string SelectedState = "Selected";
        private const string NormalState = "Normal";
        
        public LoopingCarouselItem()
        {
            DefaultStyleKey = typeof(LoopingCarouselItem);

            RegisterPropertyChangedCallback(SelectorItem.IsSelectedProperty, OnIsSelectedChanged);
        }
        
        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            base.OnPointerEntered(e);

            VisualStateManager.GoToState(this, IsSelected ? PointerOverSelectedState : PointerOverState, true);
        }
        
        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            base.OnPointerExited(e);

            VisualStateManager.GoToState(this, IsSelected ? SelectedState : NormalState, true);
        }
        
        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            base.OnPointerPressed(e);

            VisualStateManager.GoToState(this, IsSelected ? PressedSelectedState : PressedState, true);
        }

        internal event EventHandler Selected;

        private void OnIsSelectedChanged(DependencyObject sender, DependencyProperty dp)
        {
            var item = (LoopingCarouselItem)sender;

            if (item.IsSelected)
            {
                Selected?.Invoke(this, EventArgs.Empty);
                VisualStateManager.GoToState(item, SelectedState, true);
            }
            else
            {
                VisualStateManager.GoToState(item, NormalState, true);
            }
        }
    }
}
