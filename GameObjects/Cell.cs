﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    class Cell : GameObject
    {
        public Cell(string _assetName, Vector2 _position, Vector2 _size) : base(_assetName){
            position = _position;
            size = _size;
        }
    }
}
