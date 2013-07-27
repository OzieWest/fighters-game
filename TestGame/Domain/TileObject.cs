using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Domain
{
	public class TileObject : BaseTileObject
	{
		public TileObject(List<TileObject> tiles, SpriteBatch spriteBatch, ContentManager content, String fileName)
			: base(spriteBatch, content, fileName)
		{
			tiles.Add(this);
		}

		public void SetColors(Color defaulf, Color selected)
		{
			_colorDefault = defaulf;
			_colorSelected = selected;
		}

		public void ToggleColor()
		{
			if (_colorCurrent == _colorDefault)
			{
				_colorCurrent = _colorSelected;
			}
			else
			{
				_colorCurrent = _colorDefault;
			}
		}

		public void ColorDefault()
		{
			_colorCurrent = _colorDefault;
		}

		public void ColorSelected()
		{
			_colorCurrent = _colorSelected;
		}

	}
}
