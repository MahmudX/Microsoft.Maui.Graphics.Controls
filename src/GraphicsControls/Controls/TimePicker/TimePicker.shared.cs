﻿using System;
using System.Collections.Generic;
using System.Graphics;
using GraphicsControls.Effects;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class TimePicker : GraphicsVisualView
    {
        TimePickerDialogRoutingEffect _timePickerEffect;

        public static class Layers
        {
            public const string Background = "TimePicker.Layers.Background";
            public const string Border = "TimePicker.Layers.Border";
            public const string Placeholder = "TimePicker.Layers.Placeholder";
            public const string Date = "TimePicker.Layers.Date";
        }

        public static readonly BindableProperty FormatProperty =
            BindableProperty.Create(nameof(Format), typeof(string), typeof(TimePicker), "t");

        public static readonly BindableProperty TimeProperty =
            BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(TimePicker), new TimeSpan(0), BindingMode.TwoWay, (bindable, value) =>
            {
                var time = (TimeSpan)value;
                return time.TotalHours < 24 && time.TotalMilliseconds >= 0;
            });

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(XColor), typeof(TimePicker), XColor.Default);

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public XColor TextColor
        {
            get { return (XColor)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public List<string> TimePickerLayers = new List<string>
        {
            Layers.Background,
            Layers.Border,
            Layers.Placeholder,
            Layers.Date
        };

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    HeightRequest = 56;
                    break;
                case VisualType.Cupertino:
                    HeightRequest = 36;
                    break;
                case VisualType.Fluent:
                    HeightRequest = 32;
                    break;
            }

            _timePickerEffect = new TimePickerDialogRoutingEffect();

            Effects.Add(_timePickerEffect);
        }

        public override void Unload()
        {
            Effects.Remove(_timePickerEffect);
        }

        public override List<string> GraphicsLayers =>
            TimePickerLayers;

        public override void DrawLayer(string layer, ICanvas canvas, RectangleF dirtyRect)
        {
            switch (layer)
            {
                case Layers.Background:
                    DrawTimePickerBackground(canvas, dirtyRect);
                    break;
                case Layers.Border:
                    DrawTimePickerBorder(canvas, dirtyRect);
                    break;
                case Layers.Placeholder:
                    DrawTimePickerPlaceholder(canvas, dirtyRect);
                    break;
                case Layers.Date:
                    DrawTimePickerDate(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawTimePickerBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialTimePickerBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoTimePickerBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentTimePickerBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawTimePickerBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialTimePickerBorder(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoTimePickerBorder(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentTimePickerBorder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawTimePickerPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                    DrawMaterialTimePickerPlaceholder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawTimePickerDate(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialTimePickerDate(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoTimePickerDate(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentTimePickerDate(canvas, dirtyRect);
                    break;
            }
        }
    }
}