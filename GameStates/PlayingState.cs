using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BaseProject
{
    class PlayingState : GameState
    {
        public int ScreenWidth;
        public int ScreenHeight;
        Hotbar hotbar;
        float HotbarCount = 9;
        float HalfHotbar;

        public PlayingState()
        {

            ScreenWidth = GameEnvironment.screen.X;
            ScreenHeight = GameEnvironment.screen.Y;

            //gameObjectList.Add(new GameObject("spr_background"));  
            //gameObjectList.Add(new Cell("test", new Vector2(10, 10), new Vector2(0, 0)));


            hotbar = new Hotbar("test");
                            //gameObjectList.Add(hotbar);


            for (int i = 0; i < HotbarCount; i++)
            {
                
                GameObject hItem = new GameObject("test");

                hotbar.hotbarItemList.Add(hItem);
                gameObjectList.Add(hItem);
               
                HalfHotbar = HotbarCount / 2 * hotbar.hotbarItemList[i].texture.Width;
                
                hotbar.hotbarItemList[i].position.X = ScreenWidth / 2 - HalfHotbar;
                hotbar.hotbarItemList[i].position.X += 80*i;
                hotbar.hotbarItemList[i].position.Y = ScreenHeight - hotbar.hotbarItemList[i].texture.Height;
               /*                                                                                                     Debug.Print("X " + i + " = " + hotbar.hotbarItemList[i].position.X.ToString());
                                                                                                                      Debug.Print("Y " + i + " = " + hotbar.hotbarItemList[i].position.Y.ToString());
               */
            }

        }

       

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
