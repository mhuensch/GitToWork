using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Run00.GitWorkItems
{
	internal static class ExtensionsForString
	{
		public static ImageSource ToFontAwesomeIcon(this string text)
		{
			return ToFontAwesomeIcon(text, Brushes.DarkGray, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
		}

		public static ImageSource ToFontAwesomeIcon(this string text, Brush brush)
		{
			return ToFontAwesomeIcon(text, brush, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);
		}

		public static ImageSource ToFontAwesomeIcon(this string text, Brush foreBrush, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch)
		{
			var fontFamily = new FontFamily("/GitWorkItems;component/Resources/#FontAwesome");
			if (fontFamily != null && !String.IsNullOrEmpty(text))
			{
				//premier essai, on charge la police directement
				Typeface typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);

				GlyphTypeface glyphTypeface;
				if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
				{
					//si ça ne fonctionne pas (et pour le mode design dans certains cas) on ajoute l'uri pack://application
					typeface = new Typeface(new FontFamily(new Uri("pack://application:,,,"), fontFamily.Source), fontStyle, fontWeight, fontStretch);
					if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
						throw new InvalidOperationException("No glyphtypeface found");
				}

				//détermination des indices/tailles des caractères dans la police
				ushort[] glyphIndexes = new ushort[text.Length];
				double[] advanceWidths = new double[text.Length];

				for (int n = 0; n < text.Length; n++)
				{
					ushort glyphIndex;
					try
					{
						glyphIndex = glyphTypeface.CharacterToGlyphMap[text[n]];

					}
					catch (Exception)
					{
						glyphIndex = 42;
					}
					glyphIndexes[n] = glyphIndex;

					double width = glyphTypeface.AdvanceWidths[glyphIndex] * 1.0;
					advanceWidths[n] = width;
				}

				try
				{

					//création de l'objet DrawingImage (compatible avec Imagesource) à partir d'un glyphrun
					GlyphRun gr = new GlyphRun(glyphTypeface, 0, false, 1.0, glyphIndexes,
																		 new Point(0, 0), advanceWidths, null, null, null, null, null, null);

					GlyphRunDrawing glyphRunDrawing = new GlyphRunDrawing(foreBrush, gr);
					return new DrawingImage(glyphRunDrawing);
				}
				catch (Exception ex)
				{
					// ReSharper disable LocalizableElement
					Console.WriteLine("Error in generating Glyphrun : " + ex.Message);
					// ReSharper restore LocalizableElement
				}
			}
			return null;
		}

	}
}
