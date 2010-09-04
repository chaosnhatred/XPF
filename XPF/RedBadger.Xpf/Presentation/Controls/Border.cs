namespace RedBadger.Xpf.Presentation.Controls
{
    using System;
    using System.Collections.Generic;

    using RedBadger.Xpf.Internal;
    using RedBadger.Xpf.Presentation.Media;

    public class Border : UIElement
    {
        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
            "Background", typeof(Brush), typeof(Border), new PropertyMetadata(null));

        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register(
            "BorderBrush", 
            typeof(Brush), 
            typeof(Border), 
            new PropertyMetadata(null, (o, args) => ((IElement)o).InvalidateArrange()));

        public static readonly DependencyProperty BorderThicknessProperty =
            DependencyProperty.Register(
                "BorderThickness", 
                typeof(Thickness), 
                typeof(Border), 
                new PropertyMetadata(
                    new Thickness(), UIElementPropertyChangedCallbacks.InvalidateMeasureIfThicknessChanged));

        public static readonly DependencyProperty ChildProperty = DependencyProperty.Register(
            "Child", typeof(IElement), typeof(Border), new PropertyMetadata(null, ChildPropertyChangedCallback));

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            "Padding", 
            typeof(Thickness), 
            typeof(Border), 
            new PropertyMetadata(new Thickness(), UIElementPropertyChangedCallbacks.InvalidateMeasureIfThicknessChanged));

        private readonly IList<Rect> borders = new List<Rect>();

        private bool isBordersCollectionDirty;

        public Brush Background
        {
            get
            {
                return (Brush)this.GetValue(BackgroundProperty);
            }

            set
            {
                this.SetValue(BackgroundProperty, value);
            }
        }

        public Brush BorderBrush
        {
            get
            {
                return (Brush)this.GetValue(BorderBrushProperty);
            }

            set
            {
                this.SetValue(BorderBrushProperty, value);
            }
        }

        public Thickness BorderThickness
        {
            get
            {
                return (Thickness)this.GetValue(BorderThicknessProperty);
            }

            set
            {
                this.SetValue(BorderThicknessProperty, value);
            }
        }

        public IElement Child
        {
            get
            {
                return (UIElement)this.GetValue(ChildProperty);
            }

            set
            {
                this.SetValue(ChildProperty, value);
            }
        }

        public Thickness Padding
        {
            get
            {
                return (Thickness)this.GetValue(PaddingProperty);
            }

            set
            {
                this.SetValue(PaddingProperty, value);
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            IElement child = this.Child;

            if (child != null)
            {
                var finalRect = new Rect(new Point(), finalSize);

                finalRect = finalRect.Deflate(this.BorderThickness);
                finalRect = finalRect.Deflate(this.Padding);
                child.Arrange(finalRect);
            }

            return finalSize;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            this.isBordersCollectionDirty = true;

            Size borderSize = this.BorderThickness.Collapse();
            Size paddingSize = this.Padding.Collapse();

            var borderThicknessAndPaddingSize = new Size(
                borderSize.Width + paddingSize.Width, borderSize.Height + paddingSize.Height);

            IElement child = this.Child;
            if (child != null)
            {
                var childConstraint = new Size(
                    Math.Max(0, availableSize.Width - borderThicknessAndPaddingSize.Width), 
                    Math.Max(0, availableSize.Height - borderThicknessAndPaddingSize.Height));
                child.Measure(childConstraint);

                var size = new Size(
                    child.DesiredSize.Width + borderThicknessAndPaddingSize.Width, 
                    child.DesiredSize.Height + borderThicknessAndPaddingSize.Height);

                return size;
            }

            return borderThicknessAndPaddingSize;
        }

        protected override void OnRender(IDrawingContext drawingContext)
        {
            if (this.BorderThickness != new Thickness() && this.BorderBrush != null)
            {
                if (this.isBordersCollectionDirty)
                {
                    this.GenerateBorders();
                }

                foreach (Rect border in this.borders)
                {
                    drawingContext.DrawRectangle(border, this.BorderBrush);
                }
            }

            if (this.Background != null)
            {
                drawingContext.DrawRectangle(
                    new Rect(0, 0, this.ActualWidth, this.ActualHeight).Deflate(this.BorderThickness), this.Background);
            }
        }

        private static void ChildPropertyChangedCallback(
            DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var oldChild = args.OldValue as IElement;
            var newChild = args.NewValue as IElement;
            var border = (IElement)dependencyObject;

            border.InvalidateMeasure();

            if (oldChild != null)
            {
                oldChild.VisualParent = null;
            }

            if (newChild != null)
            {
                newChild.VisualParent = border;
            }
        }

        private void GenerateBorders()
        {
            this.borders.Clear();

            if (this.BorderThickness.Left > 0)
            {
                this.borders.Add(new Rect(0, 0, this.BorderThickness.Left, this.ActualHeight));
            }

            if (this.BorderThickness.Top > 0)
            {
                this.borders.Add(
                    new Rect(
                        this.BorderThickness.Left, 
                        0, 
                        this.ActualWidth - this.BorderThickness.Left, 
                        this.BorderThickness.Top));
            }

            if (this.BorderThickness.Right > 0)
            {
                this.borders.Add(
                    new Rect(
                        this.ActualWidth - this.BorderThickness.Right, 
                        this.BorderThickness.Top, 
                        this.BorderThickness.Right, 
                        this.ActualHeight - this.BorderThickness.Top));
            }

            if (this.BorderThickness.Bottom > 0)
            {
                this.borders.Add(
                    new Rect(
                        this.BorderThickness.Left, 
                        this.ActualHeight - this.BorderThickness.Bottom, 
                        this.ActualWidth - (this.BorderThickness.Left + this.BorderThickness.Right), 
                        this.BorderThickness.Bottom));
            }

            this.isBordersCollectionDirty = false;
        }
    }
}