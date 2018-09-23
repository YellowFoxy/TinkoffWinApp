using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace TinkoffWinApp.Controls
{
    public class LoopItemsPanel : Panel
    {
        private Slider sliderHorizontal;
        private double offsetSeparator;        
        private double itemWidth = 1d;
        private bool templateApplied;

        private int ItemsCount => Children != null ? Children.Count : 0;

        public LoopItemsPanel()
        {
            ManipulationMode = (ManipulationModes.TranslateX | ManipulationModes.TranslateInertia);

            ManipulationDelta += OnManipulationDelta;
            sliderHorizontal = new Slider
            {
                Orientation = Orientation.Horizontal,
                SmallChange = 0.0000000001,
                Minimum = double.MinValue,
                Maximum = double.MaxValue,
                StepFrequency = 0.0000000001
            };
            sliderHorizontal.ValueChanged += OnHorizontalOffsetChanged;
        }

        private void OnHorizontalOffsetChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            UpdatePositions(e.NewValue - e.OldValue);
        }

        private void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (e == null)
                return;

            var translation = e.Delta.Translation;
            UpdatePositions(translation.X / 2);
        }

        private void UpdatePositions(double offsetDelta)
        {
            var maxLogicalWidth = ItemsCount * itemWidth;

            offsetSeparator = (offsetSeparator + offsetDelta) % maxLogicalWidth;

            var itemNumberSeparator = (int)(Math.Abs(offsetSeparator) / itemWidth);

            int itemIndexChanging;
            double offsetAfter;
            double offsetBefore;

            if (offsetSeparator > 0)
            {
                itemIndexChanging = ItemsCount - itemNumberSeparator - 1;
                offsetAfter = offsetSeparator;

                if (offsetSeparator % maxLogicalWidth == 0)
                    itemIndexChanging++;

                offsetBefore = offsetAfter - maxLogicalWidth;
            }
            else
            {
                itemIndexChanging = itemNumberSeparator;
                offsetBefore = offsetSeparator;
                offsetAfter = maxLogicalWidth + offsetBefore;
            }

            UpdatePosition(itemIndexChanging, ItemsCount, offsetBefore);

            UpdatePosition(0, itemIndexChanging, offsetAfter);
        }

        private void UpdatePosition(int startIndex, int endIndex, double offset)
        {
            for (var i = startIndex; i < endIndex; i++)
            {
                UIElement loopListItem = Children[i];

                TranslateTransform compositeTransform = (TranslateTransform)loopListItem.RenderTransform;

                if (compositeTransform == null)
                    continue;
                compositeTransform.X = offset;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Clip = new RectangleGeometry { Rect = new Rect(0, 0, finalSize.Width, finalSize.Height) };

            var positionLeft = 0d;

            foreach (UIElement item in Children)
            {
                if (item == null)
                    continue;

                var desiredSize = item.DesiredSize;

                if (double.IsNaN(desiredSize.Width) || double.IsNaN(desiredSize.Height))
                    continue;

                var rect = new Rect(positionLeft, 0, desiredSize.Width, finalSize.Height);
                item.Arrange(rect);

                TranslateTransform compositeTransform = new TranslateTransform();
                item.RenderTransform = compositeTransform;

                positionLeft += desiredSize.Width;
            }

            templateApplied = true;

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var s = base.MeasureOverride(availableSize);

            Clip = new RectangleGeometry { Rect = new Rect(0, 0, s.Width, s.Height) };

            foreach (UIElement container in Children)
            {
                container.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

                if (itemWidth != container.DesiredSize.Width)
                    itemWidth = container.DesiredSize.Width;
            }

            return s;
        }
    }
}
