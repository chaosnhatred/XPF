﻿namespace RedBadger.Xpf.Presentation.Controls
{
    using System.Windows;

    using RedBadger.Xpf.Internal;
    using RedBadger.Xpf.Presentation.Controls.Primitives;

    public class Button : ButtonBase
    {
        public static readonly XpfDependencyProperty PaddingProperty = XpfDependencyProperty.Register(
            "Padding", 
            typeof(Thickness), 
            typeof(Button), 
            new PropertyMetadata(new Thickness(), UIElementPropertyChangedCallbacks.InvalidateMeasureIfThicknessChanged));

        public Thickness Padding
        {
            get
            {
                return (Thickness)this.GetValue(PaddingProperty.Value);
            }

            set
            {
                this.SetValue(PaddingProperty.Value, value);
            }
        }

        public override void OnApplyTemplate()
        {
            if (this.Content != null)
            {
                this.Content.Margin = this.Padding;
            }
        }
    }
}