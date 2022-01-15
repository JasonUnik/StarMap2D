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

using VPKSoft.StarCatalogs.Interfaces;

namespace VPKSoft.StarCatalogs.Providers
{
    /// <summary>
    /// Star data entry for the Tycho catalog.
    /// Implements the <see cref="IStarData" />
    /// </summary>
    /// <seealso cref="IStarData" />
    public class TychoStarData: IStarData
    {
        /// <summary>
        /// Gets or sets the TYC number.
        /// </summary>
        /// <value>The TYC number.</value>
        // ReSharper disable once InconsistentNaming
        public string TYC { get; set; } = string.Empty;

        /// <inheritdoc cref="IStarData.RightAscension"/>
        public double RightAscension { get; set; }

        /// <inheritdoc cref="IStarData.Declination"/>
        public double Declination { get; set; }

        /// <inheritdoc cref="IStarData.Magnitude"/>
        public double Magnitude { get; set; }

        /// <inheritdoc cref="IStarData.RawData"/>
        public string? RawData { get; set; }
    }
}
