using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Data;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace BaseProject
{
    class UIDialogueBox : UI
    {
        public UIDialogueBox(string _assetName, int _x, int _y, int _w, int _h) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            size.X = _w;
            size.Y = _h;
            texture = GameEnvironment.ContentManager.Load<Texture2D>(_assetName);
        }
    }
}
