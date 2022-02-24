﻿#region License
/*
MIT License

Copyright(c) 2022 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using Eto.Drawing;
using Eto.Forms;
using StarMap2D.Common.SvgColorization;

namespace StarMap2D.Eto.Controls.Utilities;

/// <summary>
/// Some helper methods for Eto.Forms.
/// </summary>
public class EtoHelpers
{
    /// <summary>
    /// Creates a new <see cref="TableLayout"/> containing the specified control and a label with specified text.
    /// </summary>
    /// <param name="text">The text for the label.</param>
    /// <param name="control">The control.</param>
    /// <returns>A new instance to the <see cref="TableLayout"/> control.</returns>
    public static TableLayout LabelWrap(string text, Control control)
    {
        return new TableLayout(new TableRow(new TableCell(PaddingBottomWrap(new Label { Text = text }), true)),
                new TableRow(new TableCell(control, true)))
        { Padding = new Padding(5) };
    }

    /// <summary>
    /// Creates a new <see cref="Panel"/> control and contains the specified control within
    /// the panel using the specified padding value as the bottom padding.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="padding">The padding to use.</param>
    /// <returns>A new instance to the <see cref="Panel"/> control.</returns>
    public static Panel PaddingBottomWrap(Control control, int padding = 5)
    {
        return new Panel { Content = control, Padding = new Padding(0, 0, 0, padding) };
    }

    /// <summary>
    /// Creates a new <see cref="Panel"/> control and contains the specified control within
    /// the panel using the specified padding.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="padding">The padding to use.</param>
    /// <returns>A new instance to the <see cref="Panel"/> control.</returns>
    public static Panel PaddingWrap(Control control, int padding = 5)
    {
        return new Panel { Content = control, Padding = new Padding(padding) };
    }

    /// <summary>
    /// Create a <see cref="TableLayout"/> with one row containing the specified control with a label and a button.
    /// </summary>
    /// <param name="labelText">The label text.</param>
    /// <param name="buttonText">The button text.</param>
    /// <param name="control">The control to use inside the <see cref="TableLayout"/>.</param>
    /// <param name="clickHandler">The click handler for the <see cref="Button"/>.</param>
    /// <param name="space">A space between the specified control and the button.</param>
    /// <returns>A new instance to a <see cref="TableLayout"/> control.</returns>
    public static TableRow LabelWrapperWithButton(string labelText, string buttonText, Control control,
        EventHandler<EventArgs> clickHandler, int space = 5)
    {
        return new TableRow(
            EtoHelpers.LabelWrap(labelText,
                new TableLayout
                {
                    Rows =
                    {
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(control, true),
                                new TableCell(new Panel { Width = space}),
                                new TableCell(new Button(clickHandler) { Text = buttonText})
                            }
                        }
                    }
                })
        );
    }

    /// <summary>
    /// Create a <see cref="TableLayout"/> with one row containing the specified control with a label and a button with scaled SVG image.
    /// </summary>
    /// <param name="labelText">The label text.</param>
    /// <param name="buttonText">The button text.</param>
    /// <param name="control">The control to use inside the <see cref="TableLayout"/>.</param>
    /// <param name="clickHandler">The click handler for the <see cref="Button"/>.</param>
    /// <param name="svgImageBytes">The SVG image data in a byte array.</param>
    /// <param name="buttonColor">The <see cref="Color"/> for the SVG image.</param>
    /// <param name="imagePadding">The amount of pixels the image should be smaller than the button height.</param>
    /// <param name="space">A space between the specified control and the button.</param>
    /// <returns>A new instance to a <see cref="TableLayout"/> control.</returns>
    public static TableRow LabelWrapperWithButton(string labelText, string? buttonText, Control control,
        EventHandler<EventArgs> clickHandler, byte[] svgImageBytes, Color buttonColor, int imagePadding = 6, int space = 5)
    {
        return new TableRow(
            EtoHelpers.LabelWrap(labelText,
                new TableLayout
                {
                    Rows =
                    {
                        new TableRow
                        {
                            Cells =
                            {
                                new TableCell(control, true),
                                new TableCell(new Panel { Width = space}),
                                new TableCell(EtoHelpers.CreateImageButton(SvgColorize.FromBytes(svgImageBytes), buttonColor, imagePadding, clickHandler)),
                            }
                        }
                    }
                })
        );
    }

    /// <summary>
    /// Creates a new instance of a <see cref="Button"/> control with auto-scaling SVG image.
    /// </summary>
    /// <param name="svgColorize">An instance of the <see cref="SvgColorize"/> class containing the SVG data..</param>
    /// <param name="svgColor">The <see cref="Color"/> for the SVG image color.</param>
    /// <param name="imagePadding">The amount of pixels the image should be smaller than the button height.</param>
    /// <param name="clickHandler">The click handler for the <see cref="Button"/>.</param>
    /// <returns>A new instance to a <see cref="Button"/> control.</returns>
    public static Button CreateImageButton(SvgColorize svgColorize, Color svgColor, int imagePadding, EventHandler<EventArgs> clickHandler)
    {
        var button = new Button(clickHandler);
        button.ImagePosition = ButtonImagePosition.Below;

        bool allowImageDraw = false;

        button.Shown += (sender, args) =>
        {
            allowImageDraw = true;
        };

        var sizeWh = Math.Min(button.Width, button.Height) - 6;

        var color = new SvgColor(svgColor.Rb, svgColor.Gb, svgColor.Bb);
        var svgData = svgColorize
            .ColorizeElementsFill(SvgElement.All, color)
            .ColorizeElementsStroke(SvgElement.All, color);
        button.Image = SvgToImage.ImageFromSvg(svgData.ToBytes(), new Size(16, 16));

        button.SizeChanged += delegate (object? sender, EventArgs args)
        {
            var newSize = Math.Min(button.Width, button.Height) - 10;
            if (!allowImageDraw || sizeWh == newSize || newSize < 1)
            {
                return;
            }

            sizeWh = newSize;

            color = new SvgColor(svgColor.Rb, svgColor.Gb, svgColor.Bb);
            svgData = svgColorize
                .ColorizeElementsFill(SvgElement.All, color)
                .ColorizeElementsStroke(SvgElement.All, color);
            button.Image = SvgToImage.ImageFromSvg(svgData.ToBytes(), new Size(sizeWh, sizeWh));
        };

        return button;
    }
}