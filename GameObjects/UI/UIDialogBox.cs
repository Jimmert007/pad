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

namespace HarvestValley.GameObjects
{
    class UIDialogueBox : UI
    {
        public UIDialogueBox(string _assetName, int _x, int _y, float _scale) : base(_assetName)
        {
            position.X = _x;
            position.Y = _y;
            scale = _scale;
        }
    }
}
