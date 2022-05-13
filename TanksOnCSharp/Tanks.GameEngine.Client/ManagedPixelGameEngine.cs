using System;
using System.Collections.Generic;
using System.Text;
using olc.managed;


namespace Example
{
    public class ManagedPixelGameEngine : PixelGameEngineManaged
    {

        private SpriteManaged test2;

        public override bool OnUserCreate()
        {
            // Called once at the start, so create things here
         

            return true;
        }

        public override bool OnUserUpdate(float fElapsedTime)
        {

            var rand = new Random();

            for (int x = 0; x < ScreenWidth(); x++)
                for (int y = 0; y < ScreenHeight(); y++)
                    Draw(x, y, new PixelManaged(0, 0, 0));


            //     DrawString(3, 10, "HELLO!", PixelColor.BLACK);


            //     DrawSprite(0, 0, test2);

            if (GetKey(KeyManaged.A).bHeld)
            {
                DrawString(3, 20, "A Held", PixelColor.YELLOW);
            }

            if (GetKey(KeyManaged.UP).bHeld)
            {
                DrawString(3, 20, "Name", PixelColor.WHITE);
            }

            if (GetKey(KeyManaged.K2).bHeld)
            {
                DrawString(25, 20, "HELLO", PixelColor.YELLOW);
                DrawString(15, 35, "user1", PixelColor.YELLOW);
                DrawString(25, 50, "fhjhfjhg", PixelColor.YELLOW);
            }


            //olc.managed.olcClearBuffer(PixelColor.BLACK, true);
            //if (GetKey(KeyManaged.Q).bPressed)
            //{
            //    DrawString(3, 30, "Q Pressed", PixelColor.BLACK);
            //}

            //if (GetKey(KeyManaged.S).bPressed)
            //{
            //    SpriteUtilitiesManaged.SwitchAxis(test2);
            //}




            return true;
        }

        public override bool OnUserDestroy()
        {
            olcPGEXSoundManaged.DestroyAudio();
            return true;
        }
    }
}
